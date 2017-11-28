using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class Supplier
    {
        public Guid ID { get; set; }
        public string PONo { get; set; }
        public string PODate { get; set; }
        public string POIssuedDate { get; set; }
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
        public string SupplierName { get; set; }
        public decimal Amount { get; set; }
        public string POStatus { get; set; }
        public string Description { get; set; }
        public string duration { get; set; }
    }
    public class SupplierOrder
    {
        public Guid ID { get; set; }
        public Guid hdnFileID { get; set; }
        public string PONo { get; set; }
        public string PODate { get; set; }
        public string POIssuedDate { get; set; }
        public Guid SupplierID { get; set; }
        public string POFromCompCode { get; set; }
        public string SupplierMailingAddress { get; set; }
        public string ShipToAddress { get; set; }
        public string BodyHeader { get; set; }
        public string BodyFooter { get; set; }

        public decimal GrossTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxPercApplied { get; set; }
        public decimal TaxAmount { get; set; }
        public string TaxTypeCode { get; set; }
        public decimal TotalAmount { get; set; }

        public string GeneralNotes { get; set; }
        public string EmailSentYN { get; set; }
        //public string SupplierName { get; set; }
        public string POStatus { get; set; }
        public string Description { get; set; }
        public string duration { get; set; }

        public string CompanyName { get; set; }
        public Suppliers SuppliersObj { get; set; }
        public Company company { get; set; }
        public Common commonObj { get; set; }

    }

    public class Suppliers
    {
        public Guid? ID { get; set; }
        public string CompanyName { get; set; }
        public bool IsInternalComp { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactTitle { get; set; }
        public string Website { get; set; }
        public string Product { get; set; }
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
        public decimal OutStanding { get; set; }
        public Common commonObj { get; set; }
        public PaymentTerms PaymentTermsObj { get; set; }
        public decimal AdvanceAmount { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }
        public decimal MaxLimit { get; set; }

    }
    public class SupplierPODetail
    {
        public Guid ID { get; set; }
        public Guid POID { get; set; }
        public Guid MaterialID { get; set; }
        public string MaterialDesc { get; set; }
        public string UnitCode { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }

        public decimal Amount { get; set; }
        public Common commonObj { get; set; }
    }
}