using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;

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
            string deliveryLine1 = "30 avenue du président Wilson";
            string deliveryLine2 = "94230 CACHAN";
            string invoiceId = "VT-105";


            PrivateFontCollection privateFontCollection = GetPrivateFontCollection();

            FontFamily family = privateFontCollection.Families.Where(x => x.Name == "Roboto").SingleOrDefault();
            FontFamily robotoMedium = GetRobotoMedium().Families[0];

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFSHarp";

            PdfPage page = document.AddPage();
            Debug.WriteLine($"{page.Width} - {page.Height}");
            XGraphics gfx = XGraphics.FromPdfPage(page);


            XImage logo = XImage.FromFile(@"C:\Users\basti\source\repos\webdummy\WebDummy\Content\images\Dispolink-logo-01.jpg");
            gfx.DrawImage(logo, 0, -15, 186, 132);

            XFont robotoRegularBig = new XFont(new Font(family, 25, FontStyle.Regular, GraphicsUnit.World), null);
            XFont robotoBoldBig = new XFont(new Font(family, 25, FontStyle.Bold, GraphicsUnit.World), null);
            XFont robotoRegularSmall = new XFont(new Font(family, 10, FontStyle.Regular, GraphicsUnit.World), null);
            XFont robotoBoldSmall = new XFont(new Font(family, 10, FontStyle.Bold, GraphicsUnit.World), null);

            gfx.DrawString("DISPOLINK", robotoBoldSmall, XBrushes.Black, new XRect(35, 75, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("1 rue de la communication", robotoRegularSmall, XBrushes.Black, new XRect(35, 87, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("59175 TEMPLEMARS", robotoRegularSmall, XBrushes.Black, new XRect(35, 99, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("dispolink.fr", robotoBoldSmall, XBrushes.Black, new XRect(35, 129, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("03.20.71.40.17", robotoBoldSmall, XBrushes.Black, new XRect(35, 141, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("contact@dispolink.fr", robotoBoldSmall, XBrushes.Black, new XRect(35, 153, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("TVA N°: FR 48828075432", robotoRegularSmall, XBrushes.Black, new XRect(35, 183, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("Code NAF: 6201Z", robotoRegularSmall, XBrushes.Black, new XRect(35, 195, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("RCS: 828075432 ROS LILLE METROPOLE", robotoRegularSmall, XBrushes.Black, new XRect(35, 207, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Livrer à:", robotoBoldSmall, XBrushes.Black, new XRect(205, 75, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(deliveryLine1, robotoRegularSmall, XBrushes.Black, new XRect(205, 87, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(deliveryLine2, robotoRegularSmall, XBrushes.Black, new XRect(205, 99, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString($"Facture", robotoBoldBig, XBrushes.Black, new XRect(370, 75, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(invoiceId, robotoRegularBig, XBrushes.Black, new XRect(450, 75, page.Width, page.Height), XStringFormats.TopLeft);


            //XFont fontBig = new XFont("Verdana", 20, XFontStyle.Bold);
            //gfx.DrawString("Facture", fontBig, XBrushes.Black, new XRect(50, 100, page.Width, page.Height), XStringFormats.TopLeft);

            //XPen pen = new XPen(XColors.Black, 0.1);

            //gfx.DrawRectangle(pen, XBrushes.White, 50, 150, 500, 500);

            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            byte[] bytes = stream.ToArray();

            return File(bytes, "application/pdf");
        }

        [HttpPost]
        public FileResult GeneratePDFHtml(string productname = "", double productquantity = 0)
        {
            string html = "<div>";
            html += "<p>Ceci est un test</p>";
            html += "</div>";

            CssData data = CssData.Parse(null, "");

            PdfDocument document = PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);

            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            byte[] bytes = stream.ToArray();

            return File(bytes, "application/pdf");
        }


        private PrivateFontCollection GetPrivateFontCollection()
        {
            PrivateFontCollection privateFontCollection = new PrivateFontCollection();

            privateFontCollection.AddFontFile(@"C:\Users\basti\source\repos\webdummy\WebDummy\fonts\Roboto-Black.ttf");
            privateFontCollection.AddFontFile(@"C:\Users\basti\source\repos\webdummy\WebDummy\fonts\Roboto-BlackItalic.ttf");
            privateFontCollection.AddFontFile(@"C:\Users\basti\source\repos\webdummy\WebDummy\fonts\Roboto-Bold.ttf");
            privateFontCollection.AddFontFile(@"C:\Users\basti\source\repos\webdummy\WebDummy\fonts\Roboto-BoldItalic.ttf");
            privateFontCollection.AddFontFile(@"C:\Users\basti\source\repos\webdummy\WebDummy\fonts\Roboto-Italic.ttf");
            privateFontCollection.AddFontFile(@"C:\Users\basti\source\repos\webdummy\WebDummy\fonts\Roboto-Light.ttf");
            privateFontCollection.AddFontFile(@"C:\Users\basti\source\repos\webdummy\WebDummy\fonts\Roboto-LightItalic.ttf");
            privateFontCollection.AddFontFile(@"C:\Users\basti\source\repos\webdummy\WebDummy\fonts\Roboto-Medium.ttf");
            privateFontCollection.AddFontFile(@"C:\Users\basti\source\repos\webdummy\WebDummy\fonts\Roboto-MediumItalic.ttf");
            privateFontCollection.AddFontFile(@"C:\Users\basti\source\repos\webdummy\WebDummy\fonts\Roboto-Regular.ttf");
            privateFontCollection.AddFontFile(@"C:\Users\basti\source\repos\webdummy\WebDummy\fonts\Roboto-Thin.ttf");
            privateFontCollection.AddFontFile(@"C:\Users\basti\source\repos\webdummy\WebDummy\fonts\Roboto-ThinItalic.ttf");

            return privateFontCollection;
        }
        private PrivateFontCollection GetRobotoMedium()
        {
            PrivateFontCollection privateFontCollection = new PrivateFontCollection();
            privateFontCollection.AddFontFile(@"C:\Users\basti\source\repos\webdummy\WebDummy\fonts\Roboto-Medium.ttf");
            return privateFontCollection;
        }
    }
}