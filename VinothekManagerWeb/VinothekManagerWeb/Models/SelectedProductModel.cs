namespace VinothekManagerWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VinothekManagerWeb.Core;

    public class SelectedProductModel
    {
        public ProductModel Product { get; set; }
        public int ProductId { get; set; }
        public bool IsSelected { get; set; }
        public SelectedProductModel(ProductModel product, bool isSelected)
        {
            Product = product;
            ProductId = product.ProductId;
            IsSelected = isSelected;
        }

        public SelectedProductModel()
        {
        }
    }
}
