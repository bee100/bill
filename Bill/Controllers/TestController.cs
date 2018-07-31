using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bill.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bill.Controllers
{
    public class TestController : Controller
    {
        [HttpGet("/api/values")] 
        public List<TestDto> Get()
        {
          return getTestDtos();
        }

        private List<TestDto> getTestDtos()
        {
            TestDto[] names = new TestDto[3];

            names[0] = new TestDto();
            names[1] = new TestDto();
            names[2] = new TestDto();

            names[0].Name = "frank";
            names[1].Name = "robin";
            names[2].Name = "ruurd";

            return names.ToList();
        }
    }
}
