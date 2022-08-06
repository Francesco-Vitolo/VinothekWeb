using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using VinothekManagerWeb.Models;


namespace VinothekManagerWeb.Core
{
    public class PDF
    {
        private PdfDocument Document { get; }
        private XFont Font { get; }
        private XFont Ueberschrift { get; }
        private PdfPage Page { get; set; }
        private XTextFormatter Tf { get; set; }
        private XGraphics Gfx { get; set; }
        private ProductModel Prod { get; set; }
        private int PosY { get; set; } = 120;
        private Random Rand { get; set; }
        public string filename { get; private set; }

        public PDF()
        {
            Document = new PdfDocument();
            Ueberschrift = new XFont("Garamond", 44, (XFontStyle)4);
            Font = new XFont("Garamond", 20);
            Rand = new Random();
        }
        public byte[] Create(ProductModel prod, string filename)
        {
            Prod = prod;
            Create();
            Document.Save(filename);
            return File.ReadAllBytes(filename);
        }       

        private void Create()
        {
            Page = Document.AddPage();
            Gfx = XGraphics.FromPdfPage(Page);
            Tf = new XTextFormatter(Gfx); //Absatz automatisch
            PosY = 120;            
            Gfx.DrawString($"{Prod.Name}", Ueberschrift, XBrushes.Black,
                new XRect(0, 40, Page.Width, Page.Height), XStringFormats.TopCenter);
            Drawing($"Bezeichnung: {Prod.Qualitätssiegel}");           
            Drawing($"Jahrgang: {Prod.Jahrgang}");
            Drawing($"Rebsorte(n): {Prod.Rebsorten}");
            Drawing($"Geschmack: {Prod.Geschmack}");
            Drawing($"Alkoholgehalt: {Prod.Alkoholgehalt} % vol.");
            Gfx.DrawString($"Beschreibung:", Font, XBrushes.Black,
                new XRect(40, PosY + 100, Page.Width, Page.Height), XStringFormats.TopLeft);
            Tf.DrawString($"{Prod.Beschreibung}", Font, XBrushes.Black,
                new XRect(40, PosY + 160, Page.Width - 100, Page.Height), XStringFormats.TopLeft);
        }

        private void Drawing(string s)
        {
            PosY = PosY + 30;
            Gfx.DrawString(s, Font, XBrushes.Black,
                new XRect(40, PosY, Page.Width, Page.Height), XStringFormats.TopLeft);
        }
    }
}
