using Database.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly TokenService _tokenService;

        public TokenController(ILogger<TokenController> logger, TokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult AuthToken(Login Credentials)
        {
            if(Credentials == null) return BadRequest("Login cannot be null");
            try
            {
                var result = _tokenService.GenerateToken(Credentials);
                return Ok(result);
            }
            catch(Exception exception)
            {
                _logger.LogError("TokenController :AuthToken(Login Credentials) : (Error: {Message})",exception.Message);
                return Problem(exception.Message);
            }
        }
    }
}