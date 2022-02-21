using GPPlanetGQL.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPPlanetGQL.Controllers
{
    public class OnlineController : Controller
    {
        private LimeService _limeService;

        public OnlineController(IConfiguration config, [Service] LimeService _limeService)
        {
            _limeService = _limeService;
        }

        // GET: OnlineController
        public ActionResult Index()
        {
            return Ok(_limeService.GetOnline().Result);
        }
    }
}
