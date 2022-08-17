using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using VinothekManagerWeb.Data;
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
        public string PathDownload { get; private set; }
        public string PathUpload { get; private set; }

        public PDF(string pathDownload, string pathUpload)
        {
            PathDownload = pathDownload;
            PathUpload = pathUpload;
            Document = new PdfDocument();
            Ueberschrift = new XFont("Garamond", 44, (XFontStyle)4);
            Font = new XFont("Garamond", 20);
        }
        public byte[] Create(ProductModel prod)
        {
            Prod = prod;
            Create();
            Document.Save(PathDownload);
            return File.ReadAllBytes(PathDownload);
        }

        public byte[] CreateFromEvent(EventModel evnt)
        {
            foreach(var prod in evnt.EventProducts.Select(x => x.Product))
            {
                Prod = prod;
                Create();
            }           
            Document.Save(PathDownload);
            return File.ReadAllBytes(PathDownload);
        }

        private void Create()
        {
            Page = Document.AddPage();
            Gfx = XGraphics.FromPdfPage(Page);
            Tf = new XTextFormatter(Gfx); //Absatz automatisch
            PosY = 120;
            if (Prod.ImageId is not null)
            {                
                XImage image = XImage.FromFile(Path.Combine(PathUpload,Prod.Image.FilePath));
                Gfx.DrawImage(image, 460, 100, 90, 300);
            }
            Gfx.DrawString($"{Prod.Name}", Ueberschrift, XBrushes.Black,
                new XRect(0, 40, Page.Width, Page.Height), XStringFormats.TopCenter);
            Drawing($"Bezeichnung: {Prod.Qualitätssiegel}");
            if (Prod.Producer is not null)
            {
                Drawing($"Erzeuger: {Prod.Producer.Name}");                          
            }

            if(Prod.Producer.Region is not null)
                Drawing($"Region: {Prod.Producer.Region}");           

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
