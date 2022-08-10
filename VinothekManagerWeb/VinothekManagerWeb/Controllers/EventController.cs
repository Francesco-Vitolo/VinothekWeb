using Microsoft.AspNetCore.Mvc;
using VinothekManagerWeb.Data;

namespace VinothekManagerWeb.Controllers
{
    public class EventController : Controller
    {
        private readonly VinothekDbContext _ctx;
        public IActionResult Index()
        {
            var v = _ctx.Event.AsQueryable().OrderBy(x => x.Name);
            return View();
        }
    }
}
