using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkillSwapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MMController : ControllerBase
    {
        private static readonly List<string> MMQueue = new();

        [HttpPost("join")]
        public IActionResult JoinQueue([FromBody] string plrId)
        {
            MMQueue.Add(plrId);
            return Ok(new {Message = " Player added to the queue"});
        }

        [HttpPost("findmatch")]
        public IActionResult FindMatch()
        {
            if (MMQueue.Count >= 2)
            {
                var plr1 = MMQueue[0];
                var plr2 = MMQueue[1];
                MMQueue.RemoveRange(0, 2);

                // Log the players in the match for debugging purposes
                Console.WriteLine($"Match found: {plr1} vs {plr2}");

                return Ok(new { MatchFound = true, Player1 = plr1, Player2 = plr2 });
            }

            // Log that no match was found
            Console.WriteLine("No match found.");
            return Ok(new { MatchFound = false });
        }

        
        [HttpGet("queue")]
        public IActionResult GetQueue()
        {
            return Ok(MMQueue);
        }
    }
}
