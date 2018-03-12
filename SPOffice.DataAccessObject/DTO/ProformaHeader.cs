using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SPOffice.DataAccessObject.DTO
{
    public class ProformaHeader
    {
        public Guid? ID { get; set; }
        public string QuotationNo { get; set; }
        public Guid? CustomerID { get; set; }
        public CustomerPO customer { get; set; }
        public string QuotationDate { get; set; }
        public string ValidTillDate { get; set; }
        public Guid? SalesPersonID { get; set; }
        public string QuoteFromCompCode { get; set; }
        public Company company { get; set; }
        public string Stage { get; set; }
        public ProformaStage quoteStage { get; set; }
        public string QuoteSubject { get; set; }
        public string SentToEmails { get; set; }
        public string ContactPerson { get; set; }
        public string SentToAddress { get; set; }
        public string BodyHead { get; set; }
        public string BodyFoot { get; set; }
        public decimal Discount { get; set; }
        public string TaxTypeCode { get; set; }
        public decimal TaxPercApplied { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal TaxAmount { get; set; }
        public string GeneralNotes { get; set; }
        public string EmailSentYN { get; set; }
        public decimal GrossAmount { get; set; }
        public Common commonObj { get; set; }
        public List<ProformaItem> quoteItemList { get; set; }
        public string DetailXML { get; set; }
        public string MailBody { get; set; }
        public Guid hdnFileID { get; set; }

        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }      
        public string Subject { get; set; }       
        public decimal Total { get; set; }
        public string OriginCompCode { get; set; }
        public string NewCustomer { get; set; }
    }

    public class ProformaItem
    {
        public Guid? ID { get; set; }
        public Guid? QuoteID { get; set; }
        public string ProductDescription { get; set; }
        public string UnitCode { get; set; }
        public string UnitDescription { get; set; }

        public Unit unit { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public Guid? ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductSearch { get; set; }
        public string OldProductCode { get; set; }
        public Product product { get; set; }
        public Common commonObj { get; set; }
        public Company company { get; set; }
    }
    public class ProformaStage
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public Common commonObj { get; set; }
    }
}