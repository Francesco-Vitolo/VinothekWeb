using System.ComponentModel.DataAnnotations;

namespace VinothekManagerWeb.Models
{
    public class EventModel
    {
        [Key]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Bitte Namen eingeben<br>")]
        public string Name { get; set; }
        public ICollection<EventProductModel>? EventProducts { get; set; }

    }
}
