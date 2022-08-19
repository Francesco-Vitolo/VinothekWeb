using VinothekManagerWeb.Models;

namespace VinothekManagerWeb.ViewModels
{
    public class ProductIndexViewModel
    {
        public bool IsDescending { get; set; } = false;
        public string OrderBy { get; set; } = "Name";
        public IEnumerable<ProductModel> Products { get; set; }
        public ProductIndexViewModel(IEnumerable<ProductModel> products)
        {
            Products = products;
        }

        public ProductIndexViewModel()
        {
        }
    }
}
