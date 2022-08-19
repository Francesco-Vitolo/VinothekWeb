namespace VinothekManagerWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VinothekManagerWeb.Core;

    public class ProductModel
    {

        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage ="Bitte Namen eingeben<br>")]
        public string Name { get; set; }

        public string Art { get; set; } = ListOptions.Art[0];
        public string Qualitätssiegel { get; set; } = ListOptions.Qualität[0];
        public string? Rebsorten { get; set; } = null;
        public string Geschmack { get; set; } = ListOptions.Geschmack[0];
        public double? Alkoholgehalt { get; set; } = null;

        public int? Jahrgang { get; set; } = null;

        [MaxLength(300)]
        public string? Beschreibung { get; set; } = null;
        public double? Preis { get; set; } = null;
        public bool Aktiv { get; set; } = true;

        public virtual int? ImageId { get; set; } = null;
        public virtual ImageModel? Image { get; set; } = null;

        public virtual int? ProducerId { get; set; } = null;
        public virtual ProducerModel? Producer { get; set; }

        public ICollection<EventProductModel>? EventProducts { get; set; }

    }
}
