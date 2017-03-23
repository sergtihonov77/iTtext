using iText.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace iText.Controllers
{
    public class HomeController : Controller
    {


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ColumnWidth_1, ColumnWidth_2, ColumnWidth_3, ColumnText_1, ColumnText_2, ColumnText_3")]Columns model)
        {
            if (ModelState.IsValid)
            {
                Document pdf = new Document(iTextSharp.text.PageSize.A4);
                FileStream st = new FileStream(Server.MapPath("~/Data/data.pdf"), FileMode.Create);
                PdfWriter wr = PdfWriter.GetInstance(pdf, st);

                pdf.Open();

                BaseFont baseFont = BaseFont.CreateFont(Server.MapPath("~/fonts/ARIAL.TTF"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

                PdfPTable table = new PdfPTable(3);

                PdfPCell cell1 = new PdfPCell(new Phrase(model.ColumnText_1.ToString(),font));
                cell1.Colspan = 1;
                cell1.HorizontalAlignment = 1;

                PdfPCell cell2 = new PdfPCell(new Phrase(model.ColumnText_2.ToString(),font));
                cell2.Colspan = 1;
                cell2.HorizontalAlignment = 1;

                PdfPCell cell3 = new PdfPCell(new Phrase(model.ColumnText_3.ToString(), font));
                cell3.Colspan = 1;
                cell3.HorizontalAlignment = 1;

                table.AddCell(cell1);
                table.AddCell(cell2);
                table.AddCell(cell3);

                float[] widths = new float[] { model.ColumnWidth_1, model.ColumnWidth_2, model.ColumnWidth_3};
                table.SetWidths(widths);
                pdf.Add(table);
                pdf.Close();


                ViewBag.Message = "Data is stored on the server in PDF format";
                return View("Create");

            }
            return View();
        }
    }
}