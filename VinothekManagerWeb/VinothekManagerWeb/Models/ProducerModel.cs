using System.ComponentModel.DataAnnotations;

namespace VinothekManagerWeb.Models
{
    public class ProducerModel
    {
        [Key]
        public int ProducerId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Region { get; set; } = null;

        public ICollection<ProductModel>? Products { get; set; }
    }
}
