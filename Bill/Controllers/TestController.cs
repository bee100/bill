using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bill.Controllers
{
    [Route("api/[Controller]")]
    public class TestController : Controller
    {
        [HttpGet("/api/values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "frank", "sjoerd", "robin", "remy", "ruurd", "maikel" };
        }
    }
}