﻿using System.IO;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Net.Mail;
using System.Net;
using System;
using UserInterface.Models;
using iTextSharp.text.pdf.draw;
using Newtonsoft.Json;
using iTextSharp.tool.xml.pipeline;

namespace UserInterface.Controllers
{
    public class PDFGeneratorController : Controller
    {
        // GET: PDFGenerator
        public ActionResult Index()
        {
            //string result= SendPDFDoc("");
            return View();
        }
        [HttpPost]
        //public string SendPDFDoc(string MailBody)
        //{
        //    try
        //    {
        //        string imageURL = Server.MapPath("~/Content/images/logo.png");
        //        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
        //        //Resize image depend upon your need
        //        jpg.ScaleToFit(70f, 60f);
        //        jpg.SpacingBefore = 10f;
        //        jpg.SpacingAfter = 1f;

        //        jpg.Alignment = Element.ALIGN_LEFT;
        //        string mailBody = MailBody.Replace("<br>", "<br/>").ToString();
        //        StringReader reader = new StringReader(mailBody.ToString());
        //        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //        using (MemoryStream memoryStream = new MemoryStream())
        //        {
        //            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
        //            pdfDoc.Open();
        //            jpg.SetAbsolutePosition(pdfDoc.Left, pdfDoc.Top - 60);
        //            pdfDoc.Add(jpg);

        //            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, reader);
        //            pdfDoc.Close();
        //            byte[] bytes = memoryStream.ToArray();
        //            memoryStream.Close();
        //            MailMessage mm = new MailMessage("gochurchmail@gmail.com", "tom.a4s.son@gmail.com");
        //            mm.Subject = "iTextSharp PDF";
        //            mm.Body = "iTextSharp PDF Attachment";
        //            mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "iTextSharpPDF.pdf"));
        //            mm.IsBodyHtml = true;
        //            SmtpClient smtp = new SmtpClient();
        //            smtp.Host = "smtp.gmail.com";
        //            smtp.EnableSsl = true;
        //            NetworkCredential NetworkCred = new NetworkCredential();
        //            NetworkCred.UserName = "gochurchmail@gmail.com";
        //            NetworkCred.Password = "thri@2015";
        //            smtp.UseDefaultCredentials = true;
        //            smtp.Credentials = NetworkCred;
        //            smtp.Port = 587;
        //            smtp.Send(mm);
        //            return "Email send successfully";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        public string PrintPDF(PDFTools pDFToolsObj)
        {
            //string imageURL = Server.MapPath("~/Content/images/logo.png");
            //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
            ////Resize image depend upon your need
            //jpg.ScaleToFit(70f, 60f);
            //jpg.SpacingBefore = 10f;
            //jpg.SpacingAfter = 1f;

            //jpg.Alignment = Element.ALIGN_LEFT;
            string htmlBody = pDFToolsObj.Content.Replace("<br>", "<br/>").ToString();
            StringReader reader = new StringReader(htmlBody.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 85f, 30f);
            byte[] bytes = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                Footer footobj = new Footer();
                footobj.imageURL = Server.MapPath("~/Content/images/logo.png");
                footobj.Header = XMLWorkerHelper.ParseToElementList(pDFToolsObj.Headcontent, null);
                footobj.Tableheader = "\n" + "Customer Payment Ledger Report" + "\n";
                writer.PageEvent = footobj;
                // Our custom Header and Footer is done using Event Handler
                //TwoColumnHeaderFooter PageEventHandler = new TwoColumnHeaderFooter();
                //writer.PageEvent = PageEventHandler;
                //// Define the page header
                //PageEventHandler.Title = "Column Header";
                //PageEventHandler.HeaderFont = FontFactory.GetFont(BaseFont.COURIER_BOLD, 10, Font.BOLD);
                //PageEventHandler.HeaderLeft = "Group";
                //PageEventHandler.HeaderRight = "1";
                pdfDoc.Open();
                //jpg.SetAbsolutePosition(pdfDoc.Left, pdfDoc.Top - 60);
                //pdfDoc.Add(jpg);
                //PdfContentByte cb = writer.DirectContent;
                //cb.MoveTo(pdfDoc.Left, pdfDoc.Top-60 );
                //cb.LineTo(pdfDoc.Right, pdfDoc.Top-60);
                //cb.SetLineWidth(1);
                //cb.SetColorStroke(new CMYKColor(0f, 12f, 0f, 7f));
                //cb.Stroke();
                //cb.MoveTo(pdfDoc.Left, pdfDoc.Top+5);
                //cb.LineTo(pdfDoc.Right, pdfDoc.Top+5);
                //cb.SetLineWidth(1);
                //cb.SetColorStroke(new CMYKColor(0f, 12f, 0f, 7f));
                //cb.Stroke();

                //Paragraph welcomeParagraph = new Paragraph("Hello, World!");
                // Our custom Header and Footer is done using Event Handler

                //pdfDoc.Add(welcomeParagraph);

                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, reader);
                //for (int i = 0; i <= 2; i++)
                //{
                //    // Define the page header
                //    PageEventHandler.HeaderRight = i.ToString();
                //    if (i != 1)
                //    {
                //        pdfDoc.NewPage();
                //    }
                //}
                pdfDoc.Close();
                bytes = memoryStream.ToArray();
                memoryStream.Close();
            }
            string fname = Path.Combine(Server.MapPath("~/Content/Uploads/"), "Report.pdf");
            System.IO.File.WriteAllBytes(fname, bytes);
            //File(bytes, "application/pdf", "Report.pdf").sa
            //bytes.SaveAs(fname);
            return JsonConvert.SerializeObject(new { Result = "OK", URL = "../Content/Uploads/Report.pdf" });

        }
        public partial class Footer : PdfPageEventHelper

        {
            public string imageURL { get; set; }
            public string Tableheader { get; set; }
            public ElementList Header;
            public override void OnEndPage(PdfWriter writer, Document doc)

            {
                Paragraph footer = new Paragraph("Report Generated on: " + DateTime.Now.ToString("dd-MMM-yyyy h:mm tt"), FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.ITALIC));
                footer.Alignment = Element.ALIGN_RIGHT;
                PdfPTable footerTbl = new PdfPTable(1);
                footerTbl.TotalWidth = 400;
                footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell = new PdfPCell(footer);
                cell.Border = 0;
                cell.PaddingLeft = 10;
                footerTbl.AddCell(cell);
                footerTbl.WriteSelectedRows(0, -1, 250, 30, writer.DirectContent);

            }
            public override void OnStartPage(PdfWriter writer, Document document)
            {
               // iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                //Resize image depend upon your need
                //jpg.ScaleToFit(50f, 40f);
                //jpg.SpacingBefore = 10f;
                //jpg.SpacingAfter = 1f;
                //jpg.Alignment = Element.ALIGN_LEFT;
                //jpg.SetAbsolutePosition(document.Left, document.Top - 60);
                Font companyFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 14, iTextSharp.text.Font.BOLD);
                Font documentFont = FontFactory.GetFont(FontFactory.TIMES, 14, iTextSharp.text.Font.BOLD);
                //string companyName = "Santhi Plastic Private Limited" + "\n";
                string documentName = Tableheader;

                Chunk chunkHeader = new Chunk(documentName);
                //chunkHeader.SetUnderline(1f, -6f);
                //Add Chunk to paragraph
                Paragraph header = new Paragraph(chunkHeader);
                Chunk linebreak = new Chunk(new LineSeparator(1f, document.PageSize.Width, BaseColor.BLACK, Element.ALIGN_CENTER, -3f));
                header.Add(linebreak);

                //Phrase phraseCompanyName=new Phrase(companyName, companyFont);
                Phrase phraseDocumentName = new Phrase(documentName, documentFont);
                //header.Add(phraseCompanyName);
                // header.Add(phraseDocumentName);
                header.Alignment = Element.ALIGN_LEFT;
                PdfPTable headerTbl = new PdfPTable(2);
                headerTbl.TotalWidth = document.PageSize.Width;
                //headerTbl.HeaderHeight = 60;
                headerTbl.HorizontalAlignment = Element.ALIGN_LEFT;
                float[] widths = new float[] { 180f, document.PageSize.Width - 100 };
                headerTbl.SetWidths(widths);
               // PdfPCell cell = new PdfPCell(jpg);
                //cell.Border = 0;
               // cell.PaddingLeft = 10;
               // headerTbl.AddCell(cell);
                PdfPCell cell1 = new PdfPCell(header);
                cell1.Border = 0;
                cell1.PaddingLeft = 50;
                cell1.PaddingTop = 40;
                //cell1.Width = document.PageSize.Width - 90;
                headerTbl.AddCell(cell1);
                ColumnText ct = new ColumnText(writer.DirectContent);
                ct.SetSimpleColumn(new Rectangle(10, 790, 559, 600));
                foreach (IElement e in Header)
                {
                    ct.AddElement(e);
                }
                ct.Go();

                headerTbl.WriteSelectedRows(0, -1, 0, 832, writer.DirectContent);
            }
        }

        public FileResult Download(PDFTools pDFToolsObj)
        {
            //jpg.Alignment = Element.ALIGN_LEFT;
            string htmlBody = pDFToolsObj.Content == null ? "" : pDFToolsObj.Content.Replace("<br>", "<br/>").ToString().Replace("workAround:image\">", "workAround:image\"/>");
            StringReader reader = new StringReader(htmlBody.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 85f, 30f);
            byte[] bytes = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);

                Footer footobj = new Footer();
                footobj.imageURL = Server.MapPath("~/Content/images/logo.png");
                footobj.Header = XMLWorkerHelper.ParseToElementList(pDFToolsObj.Headcontent==null?"":pDFToolsObj.Headcontent,null);
                footobj.Tableheader = pDFToolsObj.HeaderText;
                writer.PageEvent = footobj;

                // Our custom Header and Footer is done using Event Handler
                //TwoColumnHeaderFooter PageEventHandler = new TwoColumnHeaderFooter();
                //writer.PageEvent = PageEventHandler;
                //// Define the page header
                //PageEventHandler.Title = "Column Header";
                //PageEventHandler.HeaderFont = FontFactory.GetFont(BaseFont.COURIER_BOLD, 10, Font.BOLD);
                //PageEventHandler.HeaderLeft = "Group";
                //PageEventHandler.HeaderRight = "1";
                pdfDoc.Open();
                //jpg.SetAbsolutePosition(pdfDoc.Left, pdfDoc.Top - 60);
                //pdfDoc.Add(jpg);
                //PdfContentByte cb = writer.DirectContent;
                //cb.MoveTo(pdfDoc.Left, pdfDoc.Top-60 );
                //cb.LineTo(pdfDoc.Right, pdfDoc.Top-60);
                //cb.SetLineWidth(1);
                //cb.SetColorStroke(new CMYKColor(0f, 12f, 0f, 7f));
                //cb.Stroke();
                //cb.MoveTo(pdfDoc.Left, pdfDoc.Top+5);
                //cb.LineTo(pdfDoc.Right, pdfDoc.Top+5);
                //cb.SetLineWidth(1);
                //cb.SetColorStroke(new CMYKColor(0f, 12f, 0f, 7f));
                //cb.Stroke();

                //Paragraph welcomeParagraph = new Paragraph("Hello, World!");
                // Our custom Header and Footer is done using Event Handler

                //pdfDoc.Add(welcomeParagraph);

                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, reader);
                //for (int i = 0; i <= 2; i++)
                //{
                //    // Define the page header
                //    PageEventHandler.HeaderRight = i.ToString();
                //    if (i != 1)
                //    {
                //        pdfDoc.NewPage();
                //    }
                //}
                pdfDoc.Close();
                bytes = memoryStream.ToArray();
                memoryStream.Close();
            }
            string fname = Path.Combine(Server.MapPath("~/Content/Uploads/"), "Report.pdf");
            System.IO.File.WriteAllBytes(fname, bytes);
            string contentType = "application/pdf";
            //Parameters to file are
            //1. The File Path on the File Server
            //2. The content type MIME type
            //3. The parameter for the file save by the browser
            return File(fname, contentType, "Report.pdf");
        }

    }

}

