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
        public FileResult GeneratePDF(string productname = "", double productquantity = 0)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFSHarp";

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);


            XImage logo = XImage.FromFile(@"C:\Users\basti\source\repos\webdummy\WebDummy\Content\images\Dispolink-logo-01.jpg");
            gfx.DrawImage(logo, 0, 0, 175, 124);

            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
            gfx.DrawString("Facture", font, XBrushes.Black, new XRect(50, 100, page.Width, page.Height), XStringFormats.TopLeft);

            XPen pen = new XPen(XColors.Black, 1);

            gfx.DrawRectangle(pen, XBrushes.White, 50, 150, 500, 500);

            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            byte[] bytes = stream.ToArray();

            return File(bytes, "application/pdf");
        }
    }
}