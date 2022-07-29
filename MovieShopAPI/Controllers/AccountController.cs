using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AccountController(IAccountService accountService, IConfiguration configuration, IUserRepository userRepository)
        {
            _accountService = accountService;
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var user = await _accountService.RegisterUser(model);
            return Ok(user);
        }

        [HttpGet]
        [Route("check-email")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var user = await _accountService.ValidateUser(model.Email, model.Password);

            if (user != null)
            {
                // create token
                var jwtToken = CreateJwtToken(user);
                return Ok(new { token = jwtToken });
            }

            // iOS, Android, Angular, React, Java
            // JWT token (Json Web Token)

            // Client will send email and password to API (POST)
            // API will validate the info and if valid then API will create the JWT and send to client
            // It's client's responsibility to store the token somewhere (e.g. Angular, React -> local or session storage in browser, iOS/Android -> store in device)
            // When client needs some secure info or needs to perform any operation that requires users to be authenticated, then client needs to send the token to the API in the Http Header
            // Once the API receives the token from client it will validate the JWT and if valid it will send the data back to the client
            // If JWT invalid or expired, then API will send 401 Unauthorized



            return Unauthorized(new { errorMessage = "Please check email and password" });
        }

        private string CreateJwtToken(UserModel user)
        {
            // create the claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim("Country", "USA"),
                new Claim("Language", "English")
            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            // specify a secret key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]));

            // specify the algorithm
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // specify the expiration of the token
            var tokenExpiration = DateTime.UtcNow.AddHours(2);

            // create an object with all the above info to create the token
            var tokenDetails = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = tokenExpiration,
                SigningCredentials = credentials,
                Issuer = "MovieShop, Inc",
                Audience = "MovieShop Clients"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedJwt = tokenHandler.CreateToken(tokenDetails);
            return tokenHandler.WriteToken(encodedJwt);
        }
    }
}
