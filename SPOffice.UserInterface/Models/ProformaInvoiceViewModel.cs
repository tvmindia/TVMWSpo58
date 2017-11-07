using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace SPOffice.UserInterface.Models
{
    public class ProformaInvoiceViewModel
    {
        public Guid ID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public Guid CustomerID { get; set; }
        public string ValidTillDate { get; set; }
        public string Subject { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }
        //public string ContactPerson { get; set; }
        //public string BodyHead { get; set; }
        //public string BodyFoot { get; set; }

        //public string TaxTypeCode { get; set; }
        //public decimal TaxPercApplied { get; set; }

        //public decimal GrantTotal { get; set; }
        ////public string EmailSentYN { get; set; }
        //public string CompanyName { get; set; }
        //public decimal Amount { get; set; }
        //public string duration { get; set; }
    }

    //public class ProformaInvoiceSummaryViewModel
    //{
    //    public int Total { get; set; }
    //    public int Closed { get; set; }
    //    public int Draft { get; set; }
    //    public int InProgress { get; set; }
    //    public int OnHold { get; set; }

    //}
}