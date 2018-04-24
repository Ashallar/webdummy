using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDummy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TestSmoothScrollUp()
        {
            return View();
        }

        public ActionResult GeneratePDFInvoice()
        {
            return View();
        }

        [HttpPost]
        public FileResult GeneratePDF(string productname, double productquantity)
        {
            System.Diagnostics.Debug.WriteLine($"Name: {productname} Quantity: {productquantity}");

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFSHarp";

            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);


            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            gfx.DrawString("Hello, World!", font, XBrushes.Black,
            new XRect(0, 0, page.Width, page.Height),
            XStringFormats.Center);
            const string filename = "HelloWorld.pdf";

            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            byte[] bytes = stream.ToArray();

            return File(bytes, "application/pdf");
        }
    }
}