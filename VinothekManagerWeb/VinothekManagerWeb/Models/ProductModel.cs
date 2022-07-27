namespace VinothekManagerWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VinothekManagerWeb.Models;

    public class ProductModel
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string? Art { get; set; } = ListOptions.Art[0];
        public string? Qualitätssiegel { get; set; } = "";
        public string? Rebsorten { get; set; } = "";
        public string? Geschmack { get; set; } = ListOptions.Geschmack[0];
        public double? Alkoholgehalt { get; set; } = 0;
        public int Jahrgang { get; set; } = DateTime.Now.Year-1;
        [MaxLength(300)]
        public string? Beschreibung { get; set; } = "";
        public double? Preis { get; set; } = 0;
        public bool Aktiv { get; set; } = true;
    }
}
