﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class QuoteHeader
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
        public QuoteStage quoteStage { get; set; }
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
        public decimal GrossAmount { get; set; }
        public Common commonObj { get; set; }
        public QuoteDetail quoteDetail { get; set; }
    }

    public class QuoteDetail
    {
        public Guid? ID { get; set; }
        public Guid? QuoteID { get; set; }
        public string ProductDescription { get; set; }
        public string UnitCode { get; set; }
        public Unit unit { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public Guid? ProductID { get; set; }
        public Product product { get; set; }
        public Common commonObj { get; set; }
    }
    public class QuoteStage
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public Common commonObj { get; set; }
    }
}