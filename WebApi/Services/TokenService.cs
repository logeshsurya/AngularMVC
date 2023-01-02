using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Database.DataAccessLayer;
using Database.Models;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        private readonly EmployeeDAL _employeeDal;

        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration configuration,EmployeeDAL employeeDAL,ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _employeeDal = employeeDAL;
            _logger = logger;
        }

        public object GenerateToken(Login Credentials)
        {
            var user = _employeeDal.GetEmployee(Credentials.Email, Credentials.Password);
            if(user == null) throw new ValidationException("null");
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