using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Database.DataAccessLayer;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Database.Controllers
{
    public class TokenController : Controller
    {
        private readonly ILogger<TokenController> _logger;

        private readonly IConfiguration _configuration;

        private readonly EmployeeDAL _employeeDal;

        public TokenController(IConfiguration configuration,EmployeeDAL employeeDAL,ILogger<TokenController> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _employeeDal = employeeDAL;
        }


        [HttpPost]
        [Route("Token")]
        public IActionResult Token(Login Credentials)
        {
            var result = GenerateToken(Credentials);
            return Json(result);
        }


        private object GenerateToken(Login Credentials)
        {
           var user = _employeeDal.GetEmployee(Credentials.Email, Credentials.Password);
           try
            {
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Email,user.Email!),
                        new Claim("UserId",user.id.ToString()),
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(360),
                    signingCredentials: signIn);
                var Result = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpiryInMinutes = 360,
                    UserId=user.id,
                };

                return Result;
            }
              catch(Exception exception)
            {
                _logger.LogError("TokenService: GenerateToken(Login Credentials) : (Error:{Message}",exception.Message);
                throw;
            }   
        }
    }
}