using GPPlanetGQL.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPPlanetGQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineController : Controller
    {
        private readonly LimeService _limeService;

        public OnlineController([Service] LimeService limeService)
        {
            _limeService = limeService;
        }

        [HttpGet()]
        public ActionResult Index()
        {
            return Ok(_limeService.GetOnline().Result);
        }
    }
}
