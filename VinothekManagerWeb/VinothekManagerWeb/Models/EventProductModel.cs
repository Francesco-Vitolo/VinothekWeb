

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinothekManagerWeb.Models
{
    [Keyless]
    public class EventProductModel
    {
        public int EventID { get; set; }
        public EventModel Event { get; set; }
        public int? ProductId { get; set; }
        public ProductModel Product { get; set; }
    }
}
