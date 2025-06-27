using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GTMotive.microservice.Api.Records;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GTMotive.microservice.Api.Controllers
{
    /// <summary>
    /// Provides authentication-related endpoints for managing user login functionality. ONLY FOR ADD SOME AUTHENTICATION TASKS, TYPICALLY IT IS A IDENTITY SERVER OR OAUTH
    /// </summary>
    /// <remarks>THIS ONE IS ONLY FOR ADD SOME AUTH TO THE TASK</remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly Dictionary<string, string> users;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            users = _configuration.GetSection("Users").GetChildren().ToDictionary(x => x.Key, x => x.Value ?? string.Empty);
        }

        /// <summary>
        /// To get the token --> username : "my_user" and password: "Aa@1234"
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (!users.ContainsKey(request.Username) || users[request.Username] != request.Password)
                return Unauthorized("User or Password wrong");

            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, request.Username)
            }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpireMinutes"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                Token = tokenHandler.WriteToken(token)
            });
        }
    }
}
