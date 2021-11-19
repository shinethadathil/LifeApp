using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LifeApp.API.Data;
using LifeApp.API.DTO;
using LifeApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LifeApp.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthRepository repo, IConfiguration configuration)
        {
            this._configuration = configuration;
            this._repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDTO) //  Register([FromBody] UserForRegisterDTO userForRegisterDTO) -- if no [APiController]
        {
            // if (!ModelState.IsValid)
            //     return BadRequest(ModelState);            
            userForRegisterDTO.Username = userForRegisterDTO.Username.ToLower();
            if (await _repo.UserExists(userForRegisterDTO.Username))
                return BadRequest("Username already exists");

            var userToCreate = new User
            {
                UserName = userForRegisterDTO.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDTO.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDTO)
        {
            var userFromRepo = await _repo.Login(userForLoginDTO.Username.ToLower(), userForLoginDTO.Password.ToLower());
            if (userFromRepo == null)
                return Unauthorized();

            //JWT token creation
            var claims = new[]
            {
                new Claim(ClaimTypes.Country, "India"),
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Appsettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds    
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });

        }


    }
}