using Microsoft.AspNetCore.Mvc;
using VinothekManagerWeb.Data;
using VinothekManagerWeb.Models;

namespace VinothekManagerWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly VinothekDbContext _ctx;

        public ProductController(VinothekDbContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Index()
        {
            IEnumerable<ProductModel> prodList = _ctx.Product.ToList();
            return View(prodList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductModel prod)
        {
            if (ModelState.IsValid)
            {
                _ctx.Product.Add(prod);
                _ctx.SaveChanges();
                TempData["success"] = $"{prod.Name} wurde erstellt.";
                return RedirectToAction("Index");
            }
            return View(prod);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var product = _ctx.Product.Find(id);

            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                _ctx.Update(product);
                _ctx.SaveChanges();
                TempData["success"] = $"{product.Name} wurde bearbeitet.";
                return RedirectToAction("Index");
            }
            return View(product);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = _ctx.Product.Find(id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var product = _ctx.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _ctx.Product.Remove(product);
            _ctx.SaveChanges();
            TempData["success"] = $"{product.Name} wurde gelöscht.";
            return RedirectToAction("Index");
        }
    }
}
