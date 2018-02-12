using SPOffice.UserInterface.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace SPOffice.UserInterface.Models
{
    public class ProformaHeaderViewModel
    {

        public Guid? ID { get; set; }
        [Required(ErrorMessage = "Invoice No required")]
        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }
        [Required(ErrorMessage = "Customer required")]
        [Display(Name = "Customer")]
        public Guid? CustomerID { get; set; }
        public List<SelectListItem> CustomerList { get; set; }
        public CustomerPOViewModel customer { get; set; }
        [Required(ErrorMessage = "Invoice Date required")]
        [Display(Name = "Invoice Date")]
        public string InvoiceDate { get; set; }

        [Required(ErrorMessage = "Valid Till Date required")]
        [Display(Name = "Valid Till Date")]
        public string ValidTillDate { get; set; }             
       
        [Required(ErrorMessage = "Proforma Invoice Subject required")]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Display(Name = "Send To")]
        public string SentToEmails { get; set; }

        [Required(ErrorMessage = "Customer Contact required")]
        [Display(Name = "Customer Contact")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Mailing Address required")]
        [Display(Name = "Customer Address")]
        [DataType(DataType.MultilineText)]
        public string SentToAddress { get; set; }

        [Required(ErrorMessage = "Body Header required")]
        [Display(Name = "Body Header")]
        [DataType(DataType.MultilineText)]
        public string BodyHead { get; set; }
       // [Required(ErrorMessage = "Body Footer required")]
        [Display(Name = "Body Footer")]
        [DataType(DataType.MultilineText)]
        public string BodyFoot { get; set; }

        [Required(ErrorMessage = "Cash Discount required")]
        [Display(Name = "Cash Discount")]
        public decimal Discount { get; set; }


        [Display(Name = "Tax Type")]
        public string TaxTypeCode { get; set; }

        public List<SelectListItem> TaxTypeList { get; set; }
        [Required(ErrorMessage = "Tax Percentage required")]
        [Display(Name = "Tax Percentage Applied")]
        public decimal TaxPercApplied { get; set; }  
        
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }    
        //[Display(Name = "General Notes")]
        //[DataType(DataType.MultilineText)]
        //public string GeneralNotes { get; set; }
        public string EmailSentYN { get; set; }
        [Required(ErrorMessage = "Gross Amount required")]
        [Display(Name = "Gross Amount")]
        public decimal GrossAmount { get; set; }

        [Required(ErrorMessage = "Total Amount required")]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }
        public string TotalAmountFormatted { get; set; }
        [Required(ErrorMessage = "Net Taxable required")]
        [Display(Name = "Net Taxable Amount")]
        public decimal NetTaxableAmount { get; set; }
        public Guid hdnFileID { get; set; }
        public string DetailJSON { get; set; }
        public string MailBody { get; set; }
        public CommonViewModel commonObj { get; set; }
        public CustomerViewModel cc { get; set; }
        public ProformaItemViewModel proformaItemListObj { get; set; }
        public List<ProformaItemViewModel> quoteItemList { get; set; }
        [Required(ErrorMessage = "Tax Amount required")]
        [Display(Name = "Tax Amount Applied")]
       public decimal TotalTaxAmount { get; set; }
        public decimal Total { get; set; }
        public decimal TaxAmount { get; set; }

        [Required(ErrorMessage = "Company Name required")]
        [Display(Name = "Company Name")]
        public string OriginCompCode { get; set; }
        public List<SelectListItem> CompanyList { get; set; }
        public CompanyViewModel company { get; set; }

    }

    public class ProformaItemViewModel
    {
        public Guid? ID { get; set; }
        public Guid? QuoteID { get; set; }
        public string ProductDescription { get; set; }

        [Display(Name ="Unit Code")]
        public string UnitCode { get; set; }

        public string UnitDescription { get; set; }
        public UnitViewModel unit { get; set; }

        [Display(Name ="Quantity")]
        [Required(ErrorMessage ="Quantity is missing")]
        public decimal? Quantity { get; set; }

        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public Guid? ProductID { get; set; }

        [Display(Name ="Product Code")]
        public string ProductCode { get; set; }

        [Display(Name ="Product Search")]
        public string ProductSearch { get; set; }

        [Display(Name ="Old Product Code")]
        public string OldProductCode { get; set; }

        public ProductViewModel product { get; set; }
        public CommonViewModel commonObj { get; set; }
        public List<SelectListItem> quoteItemList { get; set; }
        public CompanyViewModel company { get; set; }
    }

    public class ProformaStageViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public CommonViewModel commonObj { get; set; }
    }
}
