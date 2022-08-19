using Microsoft.AspNetCore.Mvc;
using VinothekManagerWeb.Core;
using VinothekManagerWeb.Data;
using VinothekManagerWeb.Models;

namespace VinothekManagerWeb.Controllers
{
    public class EventController : Controller
    {
        private readonly VinothekDbContext _ctx;
        private readonly IWebHostEnvironment _environment;
        private string PathDownload { get; }
        private string PathUpload { get; }
        public EventController(VinothekDbContext ctx, IWebHostEnvironment environment)
        {
            _environment = environment;
            _ctx = ctx;
            PathDownload = Path.Combine(_environment.WebRootPath, "Downloads");
            PathUpload = Path.Combine(_environment.WebRootPath, "Uploads");
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
            List<SelectedProductViewModel> list = new List<SelectedProductViewModel>();
            foreach(var product in prods)
            {
                if(_ctx.EventProduct.Any(x => x.EventID == evnt.EventId && x.ProductId == product.ProductId))
                {
                    SelectedProductViewModel prod = new SelectedProductViewModel(product, true);
                    list.Add(prod);
                }
                else
                {
                    SelectedProductViewModel prod = new SelectedProductViewModel(product, false);
                    list.Add(prod);
                }
            }
            return View(list);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(List<SelectedProductViewModel> products, string nameEvent, int EventId)
        {
            var evnt = _ctx.Event.Find(EventId);
            evnt.Name = nameEvent;
            foreach (var product in products)
            {
                product.Product = _ctx.Product.Find(product.ProductId);
                EventProductModel eventProduct = new EventProductModel();
                eventProduct.Product = _ctx.Product.Find(product.ProductId);
                eventProduct.Event = evnt;
                if (product.IsSelected is true && !_ctx.EventProduct.Any(x => x.EventID == EventId && x.ProductId == product.ProductId))
                {
                    _ctx.EventProduct.Add(eventProduct);
                }
                else if(product.IsSelected is false && _ctx.EventProduct.Any(x => x.EventID == EventId && x.ProductId == product.ProductId))
                {
                    _ctx.EventProduct.Remove(eventProduct);
                }
            }                      
            _ctx.Event.Update(evnt);
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var evnt = _ctx.Event.Find(id);

            if (evnt == null)
            {
                return NotFound();
            }
            return View(evnt);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? eventId)
        {
            var evnt = _ctx.Event.Find(eventId);
            if (evnt == null)
            {
                return NotFound();
            }
            _ctx.Event.Remove(evnt);
            _ctx.SaveChanges();
            TempData["notification"] = $"{evnt.Name} wurde gelöscht.";
            return RedirectToAction("Index");
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
            var evnt = _ctx.Event.Find(id);
            if (evnt is not null)
            {
                evnt.EventProducts = _ctx.EventProduct.Where(x => x.EventID == id).ToList();
                
                foreach(var eventProduct in evnt.EventProducts)
                {
                    eventProduct.Product = _ctx.Product.FirstOrDefault(x => x.ProductId == eventProduct.ProductId);
                    eventProduct.Product.Producer = _ctx.Producer.FirstOrDefault(x => x.ProducerId == eventProduct.Product.ProducerId);
                    eventProduct.Product.Image = _ctx.Image.FirstOrDefault(x => x.ImageId == eventProduct.Product.ImageId);
                }
                string path = Path.Combine(PathDownload, "temp.pdf");
                PDF pdf = new PDF(path, PathUpload);
                byte[] bytes = pdf.CreateFromEvent(evnt);
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
