using Bill.Entities;
using Bill.Model.Authentication.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bill.Controllers
{
    public class AccountController: Controller
    {
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager)
        {
          _userManager = userManager;
        }

        [HttpGet("/api/account/getAccount")]
        [Authorize]
        public async Task<PersonDto> Get()
        {
          User user = await _userManager.FindByNameAsync(User.Identity.Name);
          return new PersonDto();
        }


    [HttpPost("/api/account")]
    public async Task Create(string userName)
    {
      User testUser = new User
      {
        UserName = userName,
        Email = userName + "@test.nl"
      };
      var identity = await _userManager.CreateAsync(testUser, "test");
    }

    [HttpPost("/api/account/login")]
    public async Task<IActionResult> Login([FromBody]LoginCredentialsDto credentials)
    {
      var userToVerify = await _userManager.FindByNameAsync(credentials.Username);
      var user = await _userManager.CheckPasswordAsync(userToVerify, credentials.Password);

      if (user == false)
      {
        return StatusCode(401, "Gebruikersnaam of wachtwoord incorrect");
      }

      string accessToken = GenerateJwtToken(userToVerify);

      return Ok(new { token = accessToken });
    }

    private string GenerateJwtToken(User user)
    {
      var claims = new List<Claim>
          {
              new Claim("name", user.UserName),
              new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString())
          };

      var key = AppSettings._signingKey;
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var expires = DateTime.Now.AddDays(Convert.ToDouble(2));

      var token = new JwtSecurityToken(
          AppSettings.Issuer,
          AppSettings.Issuer,
          claims,
          expires: expires,
          signingCredentials: creds
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
