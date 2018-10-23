using System.Collections.Generic;
using System.Linq;
using Bill.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bill.Controllers
{
  public class TestController : Controller
  {
    public TestController()
    {
    }

    [HttpGet("/api/values")]
    [Authorize]
    public List<PersonDto> Get()
    {
      return GetTestDtos();
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
