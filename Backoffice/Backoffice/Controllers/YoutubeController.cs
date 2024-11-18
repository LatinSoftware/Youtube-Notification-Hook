using Backoffice.Hubs;
using Backoffice.Models;
using Backoffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backoffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YoutubeController(
        IHubContext<YoutubeNotificationHub, IYoutubeNotificationHub> context, 
        PubSubHubSubscriber subscriber,
        IConfiguration configuration
        ) : ControllerBase
    {
        // GET: api/<YoutubeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<YoutubeController>/5
        [HttpPost("notify")]
        public async Task<ActionResult> Notify([FromBody]NotificationRequest notification)
        {
            await context.Clients.All.ReceiveMessage(notification.Channel, notification.Link);
            return Ok(notification);
        }

        [HttpPost("subscribe")]
        [Consumes("application/xml")]
        public async Task<ActionResult> Subscribe()
        {
            string callbackUrl = $"{configuration["Host"]}/api/youtubefeed/callback";
            string topicUrl = $"https://www.youtube.com/xml/feeds/videos.xml?channel_id={configuration["Channel"]}";

            bool success = await subscriber.SubscribeAsync(callbackUrl, topicUrl);

            if (!success) return Conflict("Ha ocurrido un error intentando subscribirse");
            return Ok("subscrito correctamente");

        }



        [HttpGet("callback")]
        public IActionResult HandleVerification([FromQuery] string hubMode, [FromQuery] string hubChallenge, [FromQuery] string hubTopic)
        {
            if (hubMode == "subscribe")
            {
                // Verificar que la solicitud proviene del hub esperado (opcional).
                return Ok(hubChallenge); // Responder con el valor del hub.challenge.
            }

            return BadRequest();
        }

        [HttpPost("callback")]
        [Consumes("application/xml")]
        public async Task<ActionResult> ReceiveNotification([FromBody] Feed feed)
        {
            if (feed == null)
            {
                return BadRequest("Invalid XML format.");
            }

            string responseMessage = $"Feed Title: {feed.Title}\n" +
                                     $"Updated: {feed.Updated}\n" +
                                     $"Video Title: {feed.Entry?.Title}\n" +
                                     $"Video ID: {feed.Entry?.VideoId}\n" +
                                     $"Channel ID: {feed.Entry?.ChannelId}";

            await Task.CompletedTask;

            return Ok(new { Message = "Feed processed successfully.", Data = responseMessage });
        }
    }
}
