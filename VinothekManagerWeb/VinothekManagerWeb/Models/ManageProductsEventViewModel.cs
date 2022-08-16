namespace VinothekManagerWeb.Models
{
    public class ManageProductsEventViewModel
    {
        public ManageProductsEventViewModel(List<SelectedProductModel> productList)
        {
            ProductList = productList;
        }

        public ManageProductsEventViewModel()
        {
            ProductList = new List<SelectedProductModel>();
        }

        public List<SelectedProductModel> ProductList { get; set; }
    }
}
