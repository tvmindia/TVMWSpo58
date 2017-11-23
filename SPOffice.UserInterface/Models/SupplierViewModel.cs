using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPOffice.UserInterface.Models
{
    public class SupplierViewModel
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

    public class SupplierOrderViewModel
    {
        public Guid ID { get; set; }

        [Display(Name = "Purchase Order Number")]
        [Required(ErrorMessage = "PONo required")]
        public string PONo { get; set; }

        [Display(Name = "Purchase Order Date")]
        [Required(ErrorMessage = "PODate required")]
        public string PODate { get; set; }

        [Display(Name = "PO Issued Date")]
        [Required(ErrorMessage = "PO Issued Date required")]
        public string POIssuedDate { get; set; }

        [Display(Name = "Supplier")]
        [MaxLength(150)]
        [Required(ErrorMessage = "Supplier is missing")]
        public Guid SupplierID { get; set; }

        [Display(Name = "Order From Company")]
        [Required(ErrorMessage = "Company required")]
        public string POFromCompCode { get; set; }

        [Display(Name = "Supplier Mailing Address")]
        public string SupplierMailingAddress { get; set; }

        [Display(Name = "Ship To Address")]
        public string ShipToAddress { get; set; }
        public string BodyHeader { get; set; }
        public string BodyFooter { get; set; }

        [Display(Name = "Gross Amount")]
        public decimal GrossTotal { get; set; }

        public decimal Discount { get; set; }

        [Display(Name = "Tax Percentage")]
        public decimal TaxPercApplied { get; set; }

        [Display(Name = "Tax Amount")]
        public decimal TaxAmount { get; set; }

        [Display(Name = "Tax Type")]
        public string TaxTypeCode { get; set; }

        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }


        public string GeneralNotes { get; set; }
        public string EmailSentYN { get; set; }
        public string SupplierName { get; set; }
        public decimal Amount { get; set; }

        [Display(Name = "Order Status")]
        [Required(ErrorMessage = "Status required")]
        public string POStatus { get; set; }

        public string Description { get; set; }
        public string duration { get; set; }


        public string CompanyName { get; set; }

        //Lists
        public List<SelectListItem> SupplierList { get; set; }
        public List<SelectListItem> CompanyList { get; set; }
        public List<SelectListItem> TaxTypeList { get; set; }
        public List<SelectListItem> POStatusList { get; set; }
        

//class
SuppliersViewModel SuppliersObj { get; set; }

        //file upload 
        public List<FileUpload> AttachmentLists { get; set; }
        public Guid hdnFileID { get; set; }
    }

    public class SuppliersViewModel
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

}