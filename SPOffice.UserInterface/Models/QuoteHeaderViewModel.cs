using SPOffice.UserInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserInterface.Models;

namespace UserInterface.Models
{
    public class QuoteHeaderViewModel
    {
        public Guid? ID { get; set; }
        public string QuotationNo { get; set; }
        public Guid? CustomerID { get; set; }
        public CustomerViewModel customer { get; set; }
        public string QuotationDate { get; set; }
        public string ValidTillDate { get; set; }
        public Guid? SalesPersonID { get; set; }
        public string QuoteFromCompCode { get; set; }
        public CompanyViewModel company { get; set; }
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
        public string GeneralNotes { get; set; }
        public string EmailSentYN { get; set; }
        public CommonViewModel commonObj { get; set; }
        public QuoteDetailViewModel quoteDetail { get; set; }
    }

    public class QuoteDetailViewModel
    {
        public Guid? ID { get; set; }
        public Guid? QuoteID { get; set; }
        public string ProductDescription { get; set; }
        public string UnitCode { get; set; }
        public UnitViewModel unit { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public Guid? ProductID { get; set; }
        public ProductViewModel product { get; set; }
        public CommonViewModel commonObj { get; set; }
    }
}