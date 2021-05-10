using System.Threading.Tasks;
using EventWebhook.Parser;
using Microsoft.AspNetCore.Mvc;

namespace EventWebhook.Controllers
{
    [Route("/events")]
    [ApiController]
    public class EventWebhookController : ControllerBase
    {
        /// <summary>
        /// POST : Event webhook handler
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Events()
        {
            var events = await EventParser.ParseAsync(Request.Body);
             
            return Ok(events);
        }
    }
}
