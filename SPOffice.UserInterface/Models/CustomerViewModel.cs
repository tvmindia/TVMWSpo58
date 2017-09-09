﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace SPOffice.UserInterface.Models
{
    public class CustomerViewModel
    {
        [Required(ErrorMessage = "Customer is missing")]
        [Display(Name = "Customer")]
        public Guid ID { get; set; }
        [Display(Name = "Company Name")]
        [MaxLength(150)]
        [Required(ErrorMessage = "Company Name is missing")]
        public string CompanyName { get; set; }
        [Display(Name = "Contact Person Name")]
        [MaxLength(100)]
        public string ContactPerson { get; set; }
        [Display(Name = "Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Entered email is not valid.")]
        [MaxLength(150)]
        public string ContactEmail { get; set; }
        [Display(Name = "Title")]
        [MaxLength(10)]
        public string ContactTitle { get; set; }
        [Display(Name = "Website")]
        [MaxLength(500)]
        public string Website { get; set; }
        [Display(Name = "Phone")]
        [StringLength(50, MinimumLength = 5)]
        public string LandLine { get; set; }
        [Display(Name = "Mobile")]
        [StringLength(50, MinimumLength = 5)]
        public string Mobile { get; set; }
        [Display(Name = "Fax")]
        [StringLength(50, MinimumLength = 5)]
        public string Fax { get; set; }
        [Display(Name = "Other Number(s) if any")]
        [MaxLength(250)]
        public string OtherPhoneNos { get; set; }
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }
        [Display(Name = "Default Payment Term")]
        [MaxLength(10)]
        public string PaymentTermCode { get; set; }
        public List<SelectListItem> DefaultPaymentTermList { get; set; }
        [Display(Name = "Tax Registration Number")]
        [MaxLength(50)]
        public string TaxRegNo { get; set; }
        [Display(Name = "Pan Number")]
        [MaxLength(50)]
        public string PANNO { get; set; }
        [Display(Name = "General Notes")]
        public string GeneralNotes { get; set; }
        [Display(Name = "Available Advance Amount:")]
        public decimal AdvanceAmount { get; set; }
        public decimal OutStanding { get; set; }
        public CommonViewModel commonObj { get; set; }
        //public PaymentTermsViewModel PaymentTermsObj { get; set; }
        public List<SelectListItem> CustomerList { get; set; }
        public List<SelectListItem> TitlesList { get; set; }
    }
    public class CustomerPOViewModel
    {
        public Guid ID { get; set; }
        [Display(Name = "Purchase Order Number")]
        public string PONo { get; set; }
        [Display(Name = "Purchase Order Date")]
        public string PODate { get; set; }
        public string POReceivedDate { get; set; }
        [Display(Name = "Customer")]
        public Guid CustomerID { get; set; }
        public List<SelectListItem> CustomerList { get; set; }

        public CustomerViewModel customer { get; set; }
        [Display(Name = "Order To Company")]
        public string POToCompCode { get; set; }
        public string POToCompAddress { get; set; }
        public List<SelectListItem> CompanyList { get; set; }
        public CompanyViewModel company { get; set; }
        public string POTitle { get; set; }
        public string POContent { get; set; }
        public string POStatus { get; set; }
        public PurchaseOrderStatusViewModel purchaseOrderStatus { get; set; }
        public string POKeywords { get; set; }
        [Required(ErrorMessage = "Gross Amount required")]
        [Display(Name = "Gross Amount")]
        public decimal GrossAmount { get; set; }
        [Required(ErrorMessage = "Cash Discount required")]
        [Display(Name = "Cash Discount")]
        public decimal Discount { get; set; }
        [Required(ErrorMessage = "Tax Percentage required")]
        [Display(Name = "Tax Percentage Applied")]
        public decimal TaxPercApplied { get; set; }
        public decimal TaxAmount { get; set; }
        [Display(Name = "Tax Type")]
        public string TaxTypeCode { get; set; }
        public TaxTypeViewModel taxType { get; set; }
        public List<SelectListItem> TaxTypeList { get; set; }
        [Display(Name = "General Notes")]
        [DataType(DataType.MultilineText)]
        public string GeneralNotes { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string duration { get; set; }
        public string CustomerName { get; set; }
        [Display(Name = "Customer Address")]
        public string BillingAddress { get; set; }
        [Display(Name = "Consignee Address")]
        public string ShippingAddress { get; set; }

        public Guid hdnFileID { get; set; }
        [Required(ErrorMessage = "Net Taxable required")]
        [Display(Name = "Net Taxable Amount")]
        public decimal NetTaxableAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public CommonViewModel commonObj { get; set; }
    }

    public class PurchaseOrderStatusViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}