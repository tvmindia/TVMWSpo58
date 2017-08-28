using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class Customer
    {
        public Guid ID { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactTitle { get; set; }
        public string Website { get; set; }
        public string LandLine { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string OtherPhoneNos { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentTermCode { get; set; }
        public string TaxRegNo { get; set; }
        public string PANNO { get; set; }
        public string GeneralNotes { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal OutStanding { get; set; }
        public Common commonObj { get; set; }
        public PaymentTerms PaymentTermsObj { get; set; }
    }


    public class CustomerPO
    {
        public Guid ID { get; set; }
        public string PONo { get; set; }
        public string PODate { get; set; }
        public string POReceivedDate { get; set; }
        public Guid CustomerID { get; set; }
        public string POToCompCode { get; set; }
        public string POToCompAddress { get; set; }
        public string POTitle { get; set; }
        public string POContent { get; set; }
        public string POStatus { get; set; }
        public string POKeywords { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxPercApplied { get; set; }
        public decimal TaxAmount { get; set; }
        public string TaxTypeCode { get; set; }
        public string GeneralNotes { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string duration { get; set; }
        public string CustomerName { get; set; }
        
        
    }

    public class PaymentTerms
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int NoOfDays { get; set; }
        public Common commonObj { get; set; }
    }


}