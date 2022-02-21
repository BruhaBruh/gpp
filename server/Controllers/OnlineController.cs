using GPPlanetGQL.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPPlanetGQL.Controllers
{
    public class OnlineController : Controller
    {
        private readonly LimeService _limeService;

        public OnlineController([Service] LimeService limeService)
        {
            _limeService = limeService;
        }

        // GET: OnlineController
        public ActionResult Index()
        {
            return Ok(_limeService.GetOnline().Result);
        }
    }
}
