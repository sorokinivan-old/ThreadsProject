using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ThreadApi.Controllers
{
    [Route("[controller]/[action].{format}"), FormatFilter]
    [ApiController]
    public class HomeController : ControllerBase
    {
       [HttpPost]
        public ReturnView Post([FromBody] string value)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            sw.Stop();
            var time = sw.ElapsedMilliseconds.ToString();
            return new ReturnView { State = "OK" ,ThreadId = value, Time = time };
        }
    }
}
