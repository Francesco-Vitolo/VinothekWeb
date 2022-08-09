using Microsoft.AspNetCore.Mvc;
using VinothekManagerWeb.Data;
using VinothekManagerWeb.Models;

namespace VinothekManagerWeb.Controllers
{
    public class ProducerController : Controller
    {
        private readonly VinothekDbContext _ctx;

        public ProducerController(VinothekDbContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Index()
        {
            IEnumerable<ProducerModel> prodList = _ctx.Producer.ToList().OrderBy(x => x.Name);
            return View(prodList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProducerModel prod)
        {
            if (ModelState.IsValid)
            {
                _ctx.Producer.Add(prod);
                _ctx.SaveChanges();
                TempData["notification"] = $"{prod.Name} wurde erstellt.";
                return RedirectToAction("Index");
            }
            return View(prod);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var producer = _ctx.Producer.Find(id);

            if (producer == null)
            {
                return NotFound();
            }
            var Products = _ctx.Product.Where(x => x.ProducerId == producer.ProducerId);
            if (Products.Count() > 0)
                ViewBag.Products = Products;
            return View(producer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProducerModel producer)
        {
            if (ModelState.IsValid)
            {
                _ctx.Update(producer);
                _ctx.SaveChanges();
                TempData["notification"] = $"{producer.Name} wurde bearbeitet.";

                return RedirectToAction("Index");
            }
            return View(producer);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = _ctx.Producer.Find(id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? ProducerId)
        {
            var producer = _ctx.Producer.Find(ProducerId);
            if (producer == null)
            {
                return NotFound();
            }
            else if (_ctx.Product.FirstOrDefault(x => x.ProducerId == ProducerId) is not null)
            {
                TempData["notification"] = $"{producer.Name} konnte nicht gelöscht werden";
                return RedirectToAction("Index");
            }
            _ctx.Producer.Remove(producer);
            _ctx.SaveChanges();
            TempData["notification"] = $"{producer.Name} wurde gelöscht.";
            return RedirectToAction("Index");
        }
    }
}
