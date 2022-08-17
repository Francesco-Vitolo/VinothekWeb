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
        private string PathDownload { get; }
        private string PathUpload { get; }

        public ProductController(VinothekDbContext ctx, IWebHostEnvironment environment)
        {
            _ctx = ctx;
            _environment = environment;
            PathDownload = Path.Combine(_environment.WebRootPath, "Downloads");
            PathUpload = Path.Combine(_environment.WebRootPath, "Uploads");
        }
        public IActionResult Index()
        {
            IEnumerable<ProductModel> prodList = _ctx.Product.ToList().OrderBy(x => x.Name);
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
        public IActionResult Create(ProductModel prod, IFormFile file)
        {
            ViewBag.Qualitätssiegel = ListOptions.Qualität;
            ViewBag.Art = ListOptions.Art;
            ViewBag.Geschmack = ListOptions.Geschmack;
            ViewBag.Producer = _ctx.Producer.AsQueryable();
            if (UploadImageHandler(prod, file))
            {
                _ctx.Product.Add(prod);
                _ctx.SaveChanges();
                TempData["notification"] = $"{prod.Name} wurde erstellt.";
                return RedirectToAction("Index");
            }
            return View(prod);
        }
        private bool UploadImageHandler(ProductModel prod, IFormFile file)
        {
            ImageModel Img = null;

            //DeleteOldImg für EditAction
            if (prod.ImageId is not null)
            {
                Img = _ctx.Image.Find(prod.ImageId);
                FileInfo fileInfo = new FileInfo(Path.Combine(PathUpload, Img.FilePath));
                fileInfo.Delete();
            }
            //

            if (file is not null)
            {
                if (!file.ContentType.Contains("image"))
                {
                    TempData["notification"] = "Bitte nur geeignete Dateiformate verwenden";
                    return false;
                }
                string fileName = Path.GetFileName(file.FileName);
                using (FileStream stream = new FileStream(Path.Combine(PathUpload, fileName), FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                Img = new ImageModel(fileName);
                _ctx.Image.Add(Img);
                _ctx.SaveChanges();
                prod.ImageId = Img.ImageId;
            }                      
            return true;
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
                product.Image = _ctx.Image.Find(product.ImageId);
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductModel product, IFormFile file)
        {
            ViewBag.Qualitätssiegel = ListOptions.Qualität;
            ViewBag.Art = ListOptions.Art;
            ViewBag.Geschmack = ListOptions.Geschmack;
            ViewBag.Producer = _ctx.Producer.AsQueryable();

            if (UploadImageHandler(product, file))
            {
                _ctx.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _ctx.SaveChanges();
                TempData["notification"] = $"{product.Name} wurde erstellt.";
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
            if (product.ImageId is not null)
            {
                var Img = _ctx.Image.Find(product.ImageId);
                FileInfo file = new FileInfo(Path.Combine(PathUpload, Img.FilePath));
                file.Delete();
                _ctx.Image.Remove(Img);
            }
            _ctx.Product.Remove(product);
            _ctx.SaveChanges();
            TempData["notification"] = $"{product.Name} wurde gelöscht.";
            return RedirectToAction("Index");
        }
        
        public IActionResult DownloadImage(int? id)
        {
            var prod = _ctx.Product.FirstOrDefault(x => x.ProductId == id);
            if (prod.ImageId != null)
            {
                ImageModel? Img = _ctx.Image.Find(prod.ImageId);
                string fullName = Path.Combine(PathUpload, Img.FilePath);

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
            byte[] bytes = PdfHandler(id);
            if (bytes is null)
                return RedirectToAction("Index");
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, "temp.pdf");
        }

        private byte[] PdfHandler(int? id)
        {
            var prod = _ctx.Product.FirstOrDefault(x => x.ProductId == id);
            prod.Producer = _ctx.Producer.Find(prod.ProducerId);
            if (prod is not null)
            {
                string path = Path.Combine(PathDownload, "temp.pdf");
                PDF pdf = new PDF(path, PathUpload);
                prod.Image = _ctx.Image.Find(prod.ImageId);
                byte[] bytes = pdf.Create(prod);
                return bytes;
            }
            return null;
        }
        public IActionResult ShowPDF(int? id)
        {          
            byte[] bytes = PdfHandler(id);
            if (bytes is null)
                return RedirectToAction("Index"); 
            return File(bytes, "application/pdf");
        }
    }
}
