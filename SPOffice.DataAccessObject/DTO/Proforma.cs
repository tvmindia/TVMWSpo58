using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class Proforma
    {
        public string OriginCompCode { get; set; }
        public string InvoiceNo { get; set; }
      
        public Guid ID { get; set; }
        public String InvoiceDate { get; set; }
        public DateTime ValidTillDate { get; set; }
        public string Subject { get; set; }
        public string SentToEmails { get; set; }
        public string ContactPerson { get; set; }
        public string SentToAddress { get; set; }
        public string BodyHeader { get; set; }
        public string BodyFooter { get; set; }
        public Decimal Discount { get; set; }
        public string TaxTypeCode { get; set; }
        public Decimal TaxPercApplied { get; set; }
        public Decimal TaxAmount { get; set; }
        public string GeneralNotes { get; set; }
        public string EmailSentYN { get; set; }
        public decimal Amount { get; set; }
        public string duration { get; set; }
        public string CompanyName { get; set; }
    }
}