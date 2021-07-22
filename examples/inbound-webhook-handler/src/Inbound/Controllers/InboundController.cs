using System.Threading.Tasks;
using Inbound.Parsers;
using Microsoft.AspNetCore.Mvc;

namespace Inbound.Controllers
{
    [Route("/inbound")]
    [ApiController]
    public class InboundController : ControllerBase
    {
        // Process POST from Inbound Parse and print received data.
        [HttpPost]
        public async Task<IActionResult> InboundParse()
        {
            var inboundEmail = await InboundWebhookParser.ParseAsync(Request.Body);

            return Ok(inboundEmail);
        }
    }
}