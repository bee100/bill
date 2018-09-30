using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Bill.Entities;
using Bill.Model.Authentication.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Bill.Controllers
{
  public class TestController : Controller
  {

    private readonly UserManager<User> _userManager;

    public TestController(UserManager<User> userManager)
    {
      _userManager = userManager;
    }

    [HttpGet("/api/values")]
    [Authorize]
    public List<PersonDto> Get()
    {
      return GetTestDtos();
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

    [HttpPost("/api/login")]
    public async Task<IActionResult> Login([FromBody]LoginCredentialsDto credentials)
    {
      var userToVerify = await _userManager.FindByNameAsync(credentials.Username);
      var user = await _userManager.CheckPasswordAsync(userToVerify, credentials.Password);

      if (user == false)
      {
        return StatusCode(401, "Gebruikersnaam of wachtwoord incorrect");
      }

      string test = await GenerateJwtToken(userToVerify);

      return Ok(new {token= test});
    }

    private async Task<string> GenerateJwtToken(User user)
    {
      var claims = new List<Claim>
          {
              new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
              new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString())
          };

      var key = AppSettings._signingKey;
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var expires = DateTime.Now.AddDays(Convert.ToDouble(2));

      var token = new JwtSecurityToken(
          AppSettings.Issuer,
          AppSettings.Issuer,
          claims,
          expires:expires,
          signingCredentials:creds
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private List<PersonDto> GetTestDtos()
    {
      PersonDto[] names = new PersonDto[3];

      names[0] = new PersonDto();
      names[1] = new PersonDto();
      names[2] = new PersonDto();

      names[0].Name = "frank";
      names[1].Name = "robin";
      names[2].Name = "ruurd";

      return names.ToList();
    }
  }
}
