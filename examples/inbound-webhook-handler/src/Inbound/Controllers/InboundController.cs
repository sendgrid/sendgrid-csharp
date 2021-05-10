using Inbound.Parsers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Inbound.Controllers
{
    [Route("/")]
    [ApiController]
    public class InboundController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Process POST from Inbound Parse and print received data.
        [HttpPost]
        [Route("inbound")]
        public IActionResult InboundParse()
        {
            InboundWebhookParser _inboundParser = new InboundWebhookParser(Request.Body);

            var inboundEmail = _inboundParser.Parse();

            return Ok();
        }

        private void Log(IDictionary<string, string> keyValues)
        {
            if(keyValues == null)
            {
                return;
            }
            Console.WriteLine(JsonConvert.SerializeObject(keyValues));
        }
    }
}