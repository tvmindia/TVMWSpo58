using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.UserInterface.Models
{
    public class SupplierViewModel
    {
        public Guid ID { get; set; }
        public string PODate { get; set; }
        public DateTime POIssuedDate { get; set; }
        public Guid SupplierID { get; set; }
        public string POFromCompCode { get; set; }
        public string SupplierMailingAddress { get; set; }
        public string ShipToAddress { get; set; }
        public string BodyHeader { get; set; }
        public string BodyFooter { get; set; }
        public decimal GrantTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxPercApplied { get; set; }
        public decimal TaxAmount { get; set; }
        public string TaxTypeCode { get; set; }
        public string GeneralNotes { get; set; }
        public string EmailSentYN { get; set; }
        public string ContactPerson { get; set; }
        public decimal Amount { get; set; }
        public string POStatus { get; set; }
        public string Description { get; set; }
    }
}