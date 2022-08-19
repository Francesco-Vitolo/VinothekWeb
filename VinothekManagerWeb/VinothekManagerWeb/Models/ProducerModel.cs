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
        public string? Adresse { get; set; } = null;
        public int? Telefon { get; set; } = null;
        public string? Email { get; set; } = null;

        public ICollection<ProductModel>? Products { get; set; }
    }
}
