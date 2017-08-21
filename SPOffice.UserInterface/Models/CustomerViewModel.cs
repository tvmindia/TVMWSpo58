using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.UserInterface.Models
{
    public class CustomerViewModel
    {
        public string PONo { get; set; }
        public DateTime PODate { get; set; }
        public DateTime POReceivedDate { get; set; }
        public Guid CustomerID { get; set; }
        public string POToCompCode { get; set; }
        public string POToCompAddress { get; set; }
        public string POTitle { get; set; }
        public string POContent { get; set; }
        public string POStatus { get; set; }
        public string POKeywords { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxPercApplied{ get; set; }
        public decimal TaxAmount { get; set; }
        public string TaxTypeCode { get; set; }
        public string GeneralNotes { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}