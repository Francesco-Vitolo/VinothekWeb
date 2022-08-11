using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VinothekManagerWeb.Data;
using VinothekManagerWeb.Models;

namespace VinothekManagerWeb.Controllers
{
    public class EventController : Controller
    {
        private readonly VinothekDbContext _ctx;

        public EventController(VinothekDbContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            var eventList = _ctx.Event.AsQueryable().OrderBy(x => x.Name);
            return View(eventList);
        }

        public IActionResult Create()
        {
            var products = _ctx.Product.AsQueryable().OrderBy(x => x.Name);
            ViewBag.Products = products;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EventModel evnt, ICollection<int> selectedProdIds)
        {
            _ctx.Event.Add(evnt);
            _ctx.SaveChanges();
            if (selectedProdIds is not null)
            {
                foreach (int selectedProd in selectedProdIds)
                {
                    EventProductModel eventProduct = new EventProductModel();
                    eventProduct.Product = _ctx.Product.Find(selectedProd);
                    eventProduct.Event = evnt;
                    _ctx.EventProduct.Add(eventProduct);
                    _ctx.SaveChanges();
                }
            }
            TempData["notification"] = $"{evnt.Name} wurde erstellt.";
            return RedirectToAction("Index");
        }
    }
}
