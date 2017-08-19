using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class Quotation
    {
        public Guid ID { get; set; }
        public string QuotationNo { get; set; }
        public Guid CustomerID { get; set; }
        public string QuotationDate { get; set; }
        public string ValidTillDate { get; set; }
        public Guid SalesPersonID { get; set; }
        public string QuoteFromCompCode { get; set; }
        public string QuoteStage { get; set; }
        public string QuoteSubject { get; set; }
        public string SentToEmails { get; set; }
        public string ContactPerson { get; set; }
        public string SentToAddress { get; set; }
        public string QuoteBodyHead { get; set; }
        public string QuoteBodyFoot { get; set; }
        public decimal Discount { get; set; }
        public string TaxTypeCode { get; set; }
        public decimal TaxPercApplied { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal GrantTotal { get; set; }
        public string GeneralNotes { get; set; }
        public string EmailSentYN { get; set; }
        public string CompanyName { get; set; }
        public decimal Amount { get; set; }
        public string duration { get; set; }
    }
}