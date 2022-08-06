using Microsoft.AspNetCore.Mvc;
using VinothekManagerWeb.Data;
using VinothekManagerWeb.Models;

namespace VinothekManagerWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly VinothekDbContext _ctx;
        private IWebHostEnvironment Environment;

        public ProductController(VinothekDbContext ctx, IWebHostEnvironment environment)
        {
            _ctx = ctx;
            Environment = environment;
        }
        public IActionResult Index()
        {            
            IEnumerable<ProductModel> prodList = _ctx.Product.ToList();
            return View(prodList);
        }

        public IActionResult Create()
        {
            ViewBag.Qualitätssiegel = ListOptions.Qualität;
            ViewBag.Art = ListOptions.Art;
            ViewBag.Geschmack = ListOptions.Geschmack;
            ViewBag.Producer = _ctx.Producer.AsQueryable();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductModel prod)
        {
            prod.Producer = _ctx.Producer.FirstOrDefault(x => x.ProducerId == prod.Producer.ProducerId);
            _ctx.Product.Add(prod);
            _ctx.SaveChanges();
            TempData["success"] = $"{prod.Name} wurde erstellt.";
            return RedirectToAction("Index");
            //return View(prod);
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
        public IActionResult DeletePOST(int? ProductId)
        {
            var product = _ctx.Product.Find(ProductId);
            if (product == null)
            {
                return NotFound();
            }
            _ctx.Product.Remove(product);
            _ctx.SaveChanges();
            TempData["success"] = $"{product.Name} wurde gelöscht.";
            return RedirectToAction("Index");
        }

        public IActionResult UploadImage(int? id)
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
        public IActionResult UploadImage(IFormFile file, int ProductId)
        {
            string path = Path.Combine(Environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Path.GetFileName(file.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            ImageModel Img = new ImageModel();
            Img.ProductId = ProductId;
            Img.FilePath = fileName;
            _ctx.Image.Add(Img);
            _ctx.SaveChanges();
            return View();        
        }
    }
}
