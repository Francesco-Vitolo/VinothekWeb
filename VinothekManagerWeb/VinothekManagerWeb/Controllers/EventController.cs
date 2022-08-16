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
                    TempData["notification"] = $"{evnt.Name} wurde erstellt.";
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var evnt = _ctx.Event.Find(id);
            if (evnt == null)
            {
                return NotFound();
            }
            TempData["Id"] = id;
            TempData["Name"] = evnt.Name;
            var prods = _ctx.Product.ToList();
            var eventProducts = _ctx.EventProduct.Where(x => x.EventID == evnt.EventId).Select(x => x.ProductId);
            List<SelectedProductModel> list = new List<SelectedProductModel>();
            foreach(var product in prods)
            {
                if(_ctx.EventProduct.Any(x => x.EventID == evnt.EventId && x.ProductId == product.ProductId))
                {
                    SelectedProductModel s = new SelectedProductModel(product, true);
                    list.Add(s);
                }
                else
                {
                    SelectedProductModel s = new SelectedProductModel(product, false);
                    list.Add(s);
                }
            }
            return View(list);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(List<SelectedProductModel> products, string nameEvent, int EventId)
        {
            var evnt = _ctx.Event.Find(EventId);
            evnt.Name = nameEvent;
            foreach (var product in products)
            {
                product.Product = _ctx.Product.Find(product.ProductId);
                EventProductModel eventProduct = new EventProductModel();
                eventProduct.Product = _ctx.Product.Find(product.ProductId);
                eventProduct.Event = evnt;
                if (product.IsSelected is true && !(_ctx.EventProduct.Any(x => x.EventID == EventId && x.ProductId == product.ProductId)))
                {
                    _ctx.EventProduct.Add(eventProduct);
                }
                else if(_ctx.EventProduct.Any(x => x.EventID == EventId && x.ProductId == product.ProductId && !product.IsSelected))
                {
                    _ctx.EventProduct.Remove(eventProduct);
                }
            }                      
            _ctx.Event.Update(evnt);
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
