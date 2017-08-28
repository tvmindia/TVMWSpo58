using SPOffice.UserInterface.Models;
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
        [Required(ErrorMessage = "Quotation No required")]
        [Display(Name = "Quotation No")]
        public string QuotationNo { get; set; }
        [Required(ErrorMessage = "Customer required")]
        [Display(Name = "Customer")]
        public Guid? CustomerID { get; set; }
        public List<SelectListItem> CustomerList { get; set; }
        public CustomerPOViewModel customer { get; set; }
        [Required(ErrorMessage = "Quotation Date required")]
        [Display(Name = "Quotation Date")]
        public string QuotationDate { get; set; }
        [Required(ErrorMessage = "Valid Till Date required")]
        [Display(Name = "Valid Till Date")]
        public string ValidTillDate { get; set; }
        [Required(ErrorMessage = "Sales Person required")]
        [Display(Name = "Sales Person")]
        public Guid? SalesPersonID { get; set; }
        [Required(ErrorMessage = "Company required")]
        [Display(Name = "Quote From Company")]
        public string QuoteFromCompCode { get; set; }
        public CompanyViewModel company { get; set; }
        [Required(ErrorMessage = "Stage required")]
        [Display(Name = "Quote Stage")]
        public string Stage { get; set; }
        public QuoteStageViewModel quoteStage { get; set; }
        [Required(ErrorMessage = "Quote Subject required")]
        [Display(Name = "Quote Subject")]
        public string QuoteSubject { get; set; }
     
        public string SentToEmails { get; set; }
        [Required(ErrorMessage = "Customer Contact required")]
        [Display(Name = "Customer Contact")]
        public string ContactPerson { get; set; }
        [Required(ErrorMessage = "Mailing Address required")]
        [Display(Name = "Mailing Address")]
        public string SentToAddress { get; set; }
        [Required(ErrorMessage = "Quote Header required")]
        [Display(Name = "Quote Header")]
        public string QuoteBodyHead { get; set; }

        public string QuoteBodyFoot { get; set; }

        [Required(ErrorMessage = "Cash Discount required")]
        [Display(Name = "Cash Discount")]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Tax Type required")]
        [Display(Name = "Tax Type")]
        public string TaxTypeCode { get; set; }
        [Required(ErrorMessage = "Tax Percentage required")]
        [Display(Name = "Tax Percentage Applied")]
        public decimal TaxPercApplied { get; set; }
        [Required(ErrorMessage = "Tax Amount required")]
        [Display(Name = "Tax Amount Applied")]
        public decimal TaxAmount { get; set; }
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
    public class QuoteStageViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public CommonViewModel commonObj { get; set; }
    }
}