using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ThreadApi.Controllers
{
    [Route("[controller]/[action].{format}"), FormatFilter]
    [ApiController]
    public class HomeController : ControllerBase
    {
       [HttpPost]
        public ResponseView Post([FromBody] RequestView request)
        {
            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            string threadId;
            string state;
            long processingTime;
            long responseTime;
            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            if(request != null)
            {
                threadId = request.ThreadId;
                state = "OK";
            }
            else
            {
                threadId = null;
                state = "ERROR";    
            }
            sw2.Stop();
            processingTime = sw2.ElapsedMilliseconds;
            sw1.Stop();
            responseTime = sw1.ElapsedMilliseconds;
            var response = new ResponseView { State = state, ThreadId = threadId, ResponseTime = responseTime, ProcessingTime = processingTime };
            sw1.Reset();
            sw2.Reset();
            return response; 
        }
    }
}
