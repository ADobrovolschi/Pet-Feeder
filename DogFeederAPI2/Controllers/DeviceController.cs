using DogFeederAPI2.Services;
using Microsoft.AspNetCore.Mvc;

namespace DogFeederAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeederController : ControllerBase
    {
        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new { message = "Dog Feeder API is up and running!" });
        }
       
        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnection([FromServices] IoTHubService ioTHubService)
        {
            var isConnected = await ioTHubService.TestConnectionAsync();
            if (isConnected)
                return Ok(new { message = "Connection successful!" });
            else
                return StatusCode(500, new { message = "Connection failed!" });
        }

        [HttpPost("send-test-message")]
        public async Task<IActionResult> SendTestMessage([FromServices] IoTHubService ioTHubService)
        {
            try
            {
                await ioTHubService.SendTestMessageAsync();
                return Ok(new { message = "Test message sent successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Failed to send test message: {ex.Message}" });
            }
        }

        [HttpPost("feedNow")]
        public async Task<IActionResult> FeedNow([FromServices] IoTHubService ioTHubService)
        {
            try
            {
                await ioTHubService.SendFeedNowCommandAsync();
                return Ok(new { message = "Feeding now!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Failed to feed now: {ex.Message}" });
            }
        }



    }
}
