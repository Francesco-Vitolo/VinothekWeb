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
            TempData["notification"] = $"{prod.Name} wurde erstellt.";
            return RedirectToAction("Index");
            //return View(prod);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Qualitätssiegel = ListOptions.Qualität;
            ViewBag.Art = ListOptions.Art;
            ViewBag.Geschmack = ListOptions.Geschmack;
            ViewBag.Producer = _ctx.Producer.AsQueryable();
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var product = _ctx.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            if (product.ImageId is not null)
            {
                var Img = _ctx.Image.Find(product.ImageId);
                ViewBag.FileName = Img.FilePath;
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductModel product)
        {
            ViewBag.Qualitätssiegel = ListOptions.Qualität;
            ViewBag.Art = ListOptions.Art;
            ViewBag.Geschmack = ListOptions.Geschmack;
            ViewBag.Producer = _ctx.Producer.AsQueryable();
            _ctx.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _ctx.SaveChanges();
            TempData["notification"] = $"{product.Name} wurde bearbeitet.";
            return RedirectToAction("Index");
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
            if (product.ImageId is not null)
            {
                var Img = _ctx.Image.Find(product.ImageId);
                FileInfo file = new FileInfo(Path.Combine(_environment.WebRootPath, "Uploads", Img.FilePath));
                file.Delete();
                _ctx.Image.Remove(Img);
            }
            _ctx.Product.Remove(product);
            _ctx.SaveChanges();
            TempData["notification"] = $"{product.Name} wurde gelöscht.";
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
        [ValidateAntiForgeryToken]
        public IActionResult UploadImage(IFormFile file, int ProductId)
        {
            var prod = _ctx.Product.Find(ProductId);
            if (!file.ContentType.Contains("image"))
            {
                TempData["notification"] = "Bitte nur geeignete Dateiformate verwenden";
                return View(prod);
            }

            string path = Path.Combine(_environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //DeleteOldImg
            ImageModel Img = _ctx.Image.Find(prod.ImageId);
            FileInfo fileInfo = new FileInfo(Path.Combine(_environment.WebRootPath, "Uploads", Img.FilePath));
            fileInfo.Delete();
            //
            string fileName = Path.GetFileName(file.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Img = new ImageModel(fileName);
            _ctx.Image.Add(Img);
            _ctx.SaveChanges();
            prod.ImageId = Img.ImageId;
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult DownloadImage(int? id)
        {
            var prod = _ctx.Product.FirstOrDefault(x => x.ProductId == id);
            if (prod.ImageId != null)
            {
                ImageModel? Img = _ctx.Image.Find(prod.ImageId);
                string fullName = Path.Combine(_environment.WebRootPath, "Uploads", Img.FilePath);

                byte[] fileBytes = GetFile(fullName);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, prod.Image.FilePath);
            }
            else
            {
                TempData["notification"] = $"Kein Bild vorhanden"; //falsche TempData
                return RedirectToAction("Index");
            }
        }
        private byte[] GetFile(string s)
        {
            FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new IOException(s);
            return data;
        }

        public IActionResult DownloadPDF(int? id)
        {
            string path = Path.Combine(_environment.WebRootPath, "Downloads", "test.pdf");
            PDF pdf = new PDF();
            var prod = _ctx.Product.FirstOrDefault(x => x.ProductId == id);
            byte[] bytes = pdf.Create(prod, path);
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, "test.pdf");
        }

        public IActionResult ShowPDF(int? id)
        {
            var prod = _ctx.Product.FirstOrDefault(x => x.ProductId == id);
            if(prod is not null)
            {
                string path = Path.Combine(_environment.WebRootPath, "Downloads", "test.pdf");
                PDF pdf = new PDF();
                byte[] bytes = pdf.Create(prod, path);
                return File(bytes, "application/pdf");
            }
            return RedirectToAction("Index");
        }

    }
}
