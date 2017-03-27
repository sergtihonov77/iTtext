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
        public ActionResult Create([Bind(Include = "ColumnWidth_1, ColumnWidth_3, ColumnText_1, ColumnText_2, ColumnText_3")]Columns model)
        {
            if (ModelState.IsValid)
            {
                Rectangle main = new Rectangle(1, 1, 1000, 1000);
                Document pdf = new Document(main);
                FileStream st = new FileStream(Server.MapPath("~/Data/data.pdf"), FileMode.Create);
                PdfWriter wr = PdfWriter.GetInstance(pdf, st);
                

                pdf.Open();

                BaseFont baseFont = BaseFont.CreateFont(Server.MapPath("~/fonts/ARIAL.TTF"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

                //Old
                /*
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
                */

                PdfContentByte cb = wr.DirectContent;

                var rec1 = new iTextSharp.text.Rectangle(10, 950, model.ColumnWidth_1, 850);
                rec1.Border = iTextSharp.text.Rectangle.BOX;
                rec1.BorderWidth = 1;
                rec1.BorderColor = new BaseColor(0, 0, 0);
                cb.Rectangle(rec1);              
                ColumnText column1 = new ColumnText(cb);
                column1.SetSimpleColumn(rec1);            
                column1.AddText(new Phrase(model.ColumnText_1.ToString(), font));
                column1.Go();

                var rec2 = new iTextSharp.text.Rectangle(10 + model.ColumnWidth_1, 950, 1000 - 20 - model.ColumnWidth_3, 850);
                rec2.Border = iTextSharp.text.Rectangle.BOX;
                rec2.BorderWidth = 1;
                rec2.BorderColor = new BaseColor(0, 0, 0);
                cb.Rectangle(rec2);
                ColumnText column2 = new ColumnText(cb);
                column2.SetSimpleColumn(rec2);
                column2.AddText(new Phrase(model.ColumnText_2.ToString(), font));
                column2.Go();

                var rec3 = new iTextSharp.text.Rectangle(990 - model.ColumnWidth_3, 950, 990, 850);
                rec3.Border = iTextSharp.text.Rectangle.BOX;
                rec3.BorderWidth = 1;
                rec3.BorderColor = new BaseColor(0, 0, 0);
                cb.Rectangle(rec3);
                ColumnText column3 = new ColumnText(cb);
                column3.SetSimpleColumn(rec3);
                column3.AddText(new Phrase(model.ColumnText_3.ToString(), font));
                column3.Go();
                

                pdf.Close();


                ViewBag.Message = "Data is stored on the server in PDF format";
                return View("Create");

            }
            return View();
        }
    }
}