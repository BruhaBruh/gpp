using GPPlanetGQL.Models;
using GPPlanetGQL.Services;
using Microsoft.AspNetCore.Mvc;

namespace GPPlanetGQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LimeController : ControllerBase
    {
        private readonly gpplanetContext _ctx;
        private readonly LimeService _lime;

        public LimeController([Service] gpplanetContext ctx, [Service] LimeService lime)
        {
            _lime = lime;
            _ctx = ctx;
        }

        [HttpPost("new")]
        public async Task<IActionResult> New(
                [FromQuery(Name = "token")] string token,
                [FromQuery(Name = "discord_id")] long discordId
            )
        {
            if (!_lime.IsValidToken(token))
            {
                return StatusCode(500, new { message = "invalid token" });
            }
            _ctx.Users.Add(new User
            {
                DiscordId = discordId,
            });
            try
            {
                _ctx.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ошибка при добавлении" });
            }
            return Ok();
        }
    }
}
