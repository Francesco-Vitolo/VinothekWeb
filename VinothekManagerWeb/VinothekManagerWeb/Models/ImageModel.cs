using System.ComponentModel.DataAnnotations;

namespace VinothekManagerWeb.Models
{
    public class ImageModel
    {
        [Key]
        public int ImageId { get; set; }
        public string FilePath { get; set; }
        public virtual int ProductId { get; set; }
        public virtual ProductModel Product { get; set; }

    }
}
