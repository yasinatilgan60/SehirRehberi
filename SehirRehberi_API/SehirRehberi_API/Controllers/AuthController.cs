using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SehirRehberi_API.Data;
using SehirRehberi_API.Dtos;
using SehirRehberi_API.Models;

namespace SehirRehberi_API.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private IAuthRepository _authRepository;
        // token konfigürasyondan getirilir.
        private IConfiguration _configuration;
        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
        {
            // Kullanıcı sistemde var mı?
            if (await _authRepository.UserExists(userForRegisterDto.UserName)) 
            {
                ModelState.AddModelError("UserName", "Username is already exists.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Bir kullanıcı oluşturmak için;
            var userToCreate = new User()
            {
                UserName = userForRegisterDto.UserName
            };
            var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);
            return StatusCode(201);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            var user = await _authRepository.Login(userForLoginDto.UserName, userForLoginDto.Password);
            if (user==null)
            {
                return Unauthorized();
            }
            // Kullanıcı var ise bir token gönderilir ve bununla ilgili yerlere giriş yetkisi verilir.
            // handle etmek - ilgili işle uğraşmak.
            var tokenHandler = new JwtSecurityTokenHandler(); // Microsoft tarafından yazılmış sınıf.
            // appsetting.json'a ulaşmak için;
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            // Token neleri tutacak?
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                // Subject tokendaki genel verileri tutar.
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName) 
                }),
                // Expires ile tokenın ne kadar süre geçerli olacağı ayarlanır ?
                Expires = DateTime.Now.AddDays(1),
                // key ile hangi algoritmanın kullanılacağının ayarı yapılacaktır ?
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token); // tokenın string değeri alınır.
            return Ok(tokenString);
        }
    }
}