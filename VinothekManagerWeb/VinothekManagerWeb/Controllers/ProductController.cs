using Microsoft.AspNetCore.Mvc;
using VinothekManagerWeb.Core;
using VinothekManagerWeb.Data;
using VinothekManagerWeb.Models;

namespace VinothekManagerWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly VinothekDbContext _ctx;
        private readonly IWebHostEnvironment _environment;

        public ProductController(VinothekDbContext ctx, IWebHostEnvironment environment)
        {
            _ctx = ctx;
            _environment = environment;
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
            string path = Path.Combine(_environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Path.GetFileName(file.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            ImageModel Img = new ImageModel(fileName);
            _ctx.Image.Add(Img);
            _ctx.Product.Find(ProductId).Image = Img;
            _ctx.SaveChanges();
            return View();
        }

        public IActionResult DownloadImage(int? id)
        {
            var prod = _ctx.Product.FirstOrDefault(x => x.ProductId == id);
            if (prod.ImageId != null)
            {
                ImageModel? Img = _ctx.Image.Find(prod.ImageId);
                string path = Path.Combine(_environment.WebRootPath, "Uploads");
                string fullName = Path.Combine(path, Img.FilePath);

                byte[] fileBytes = GetFile(fullName);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, prod.Image.FilePath);
            }
            else
            {
                TempData["success"] = $"Kein Bild vorhanden";
                return RedirectToAction("Index");
            }
        }

        public IActionResult CreatePDF(int? id)
        {
            string path = Path.Combine(_environment.WebRootPath, "Downloads", "test.pdf");
            PDF pdf = new PDF();
            var prod = _ctx.Product.FirstOrDefault(x => x.ProductId == id);
            byte[] bytes = pdf.Create(prod, path);
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, path);
        }
            
        byte[] GetFile(string s)
        {
            FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new IOException(s);
            return data;
        }
    }
}
