﻿using SPOffice.UserInterface.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace UserInterface.Models
{
    public class QuoteHeaderViewModel
    {
        public Guid? ID { get; set; }
        [Display(Name = "Quotation No")]
        public string QuotationNo { get; set; }
        [Required(ErrorMessage = "Customer required")]
        [Display(Name = "Company Name")]
        public Guid? CustomerID { get; set; }
        public string NewCustomer { get; set; }
        [Display(Name = "Is Regular Customer")]
        public string IsRegularCustomer { get; set; }
        public List<SelectListItem> CustomerList { get; set; }
        public CustomerViewModel customer { get; set; }
        [Required(ErrorMessage = "Quotation Date required")]
        [Display(Name = "Quotation Date")]
        public string QuotationDate { get; set; }
        [Display(Name = "Valid Till Date")]
        public string ValidTillDate { get; set; }
        [Display(Name = "Sales Person")]
        public Guid? SalesPersonID { get; set; }
        public List<SelectListItem> SalesPersonList { get; set; }
        [Required(ErrorMessage = "Company required")]
        [Display(Name = "Quote From Company")]
        public string QuoteFromCompCode { get; set; }
        public List<SelectListItem> CompanyList { get; set; }
        public CompanyViewModel company { get; set; }
        [Required(ErrorMessage = "Stage required")]
        [Display(Name = "Quote Stage")]
        public string Stage { get; set; }
        public QuoteStageViewModel quoteStage { get; set; }
        public List<SelectListItem> QuoteStageList { get; set; }
        [Required(ErrorMessage = "Quote Subject required")]
        [Display(Name = "Quote Subject")]
        public string QuoteSubject { get; set; }
        [Display(Name = "Send To")]
        public string SentToEmails { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        [Required(ErrorMessage = "Mailing Address required")]
        [Display(Name = "Customer Address")]
        [DataType(DataType.MultilineText)]
        public string SentToAddress { get; set; }
        [Required(ErrorMessage = "Quote Header required")]
        [Display(Name = "Quote Header (Mail)")]
        [DataType(DataType.MultilineText)]
        public string QuoteBodyHead { get; set; }
        //[Required(ErrorMessage = "Quote Footer required")]
        [Display(Name = "Quote Footer")]
        [DataType(DataType.MultilineText)]
        public string QuoteBodyFoot { get; set; }

        [Required(ErrorMessage = "Cash Discount required")]
        [Display(Name = "Cash Discount")]
        public decimal Discount { get; set; }

     
        [Display(Name = "Tax Type")]
        public string TaxTypeCode { get; set; }
        public List<SelectListItem> TaxTypeList { get; set; }
        [Required(ErrorMessage = "Tax Percentage required")]
        [Display(Name = "Tax Percentage Applied")]
        public decimal TaxPercApplied { get; set; }
        [Required(ErrorMessage = "Tax Amount required")]
        [Display(Name = "Tax Amount Applied")]
        public decimal TaxAmount { get; set; }
        [Display(Name = "General Notes")]
        [DataType(DataType.MultilineText)]
        public string GeneralNotes { get; set; }
        public string EmailSentYN { get; set; }
        [Required(ErrorMessage = "Gross Amount required")]
        [Display(Name = "Gross Amount")]
        public decimal GrossAmount { get; set; }
        [Required(ErrorMessage = "Total Amount required")]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }
        [Required(ErrorMessage = "Net Taxable required")]
        [Display(Name = "Net Taxable Amount")]
        public decimal NetTaxableAmount { get; set; }
        public Guid hdnFileID { get; set; }
        public string DetailJSON { get; set; }
        public string MailBody { get; set; }
        public CommonViewModel commonObj { get; set; }
        public QuoteItemViewModel quoteItemListObj { get; set; }
        public List<QuoteItemViewModel> quoteItemList { get; set; }
        public PDFTools pdfToolsObj { get; set; }
        [Display(Name = "Contact No.")]
        public string ContactNo { get; set; }
    }

    public class QuoteItemViewModel
    {
        public Guid? ID { get; set; }
        public Guid? QuoteID { get; set; }
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }
        public string UnitCode { get; set; }
        public string UnitDescription { get; set; }
        public UnitViewModel unit { get; set; }
        [Display(Name = "Quantity")]
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public Guid? ProductID { get; set; }
        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }
        [Display(Name = "Old Product Code")]
        public string OldProductCode { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Search Product")]
        public string ProductSearch { get; set; }
        public ProductViewModel product { get; set; }
        public CommonViewModel commonObj { get; set; }
        public CompanyViewModel company { get; set; }
        public List<SelectListItem> quoteItemList { get; set; }
    }
    public class QuoteStageViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public CommonViewModel commonObj { get; set; }
        public List<SelectListItem> quoteStageList { get; set; }


    }
}