using EventWebhook.Models;
using EventWebhook.Parser;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventWebhook.Controllers
{
    [Route("/")]
    public class EventWebhookController : Controller
    {
        /// <summary>
        /// GET : Index page
        /// </summary>
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// POST : Event webhook handler
        /// </summary>
        /// <returns></returns>
        [Route("/events")]
        [HttpPost]
        public async Task<IActionResult> Events()
        {
            IEnumerable<Event> events = await EventParser.ParseAsync(Request.Body);
             
            return Ok();
        }   
    }
}
