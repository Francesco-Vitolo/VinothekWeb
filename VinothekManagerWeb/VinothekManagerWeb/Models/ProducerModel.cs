using System.ComponentModel.DataAnnotations;

namespace VinothekManagerWeb.Models
{
    public class ProducerModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Region { get; set; } = "";

        public ICollection<ProductModel>? Products { get; set; }        
    }
}
