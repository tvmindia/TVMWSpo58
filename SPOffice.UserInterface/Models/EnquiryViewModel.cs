﻿using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace SPOffice.UserInterface.Models
{
    public class EnquiryViewModel
    {

        public Guid ID { get; set; }
        public string EnquiryNo { get; set; }

        [Required(ErrorMessage = "Enquiry Date is missing")]
        [Display(Name = "Enquiry Date")]
        public string EnquiryDate { get; set; }

        [Required(ErrorMessage = "Title")]
        [Display(Name = "Title")]
        public string ContactTitle { get; set; }

        [Required(ErrorMessage = "Contact Name is missing")]
        [Display(Name = " Contact Name")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Company Name is missing")]
        [Display(Name = " Client Company")]
        public string CompanyName { get; set; }

        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        public string Website { get; set; }

       // [Required(ErrorMessage = "Email is missing")]
        [RegularExpression(@"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;,.]{0,1}\s*)+$", ErrorMessage = "Please enter a valid e-mail adress")]
        [MaxLength(150)]
        public string Email { get; set; }
        public string LandLine { get; set; }

        [Required(ErrorMessage = "Mobile No. is missing")]
        [RegularExpression("[- +()0-9]+", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }
        public string Fax { get; set; }

        [Display(Name = "Enquiry Source")]
        public string EnquirySource { get; set; }

        [Required(ErrorMessage = "IndustryName is missing")]
        [Display(Name = "Industry")]
        public string IndustryName { get; set; }
        public List<EnquiryViewModel> IndustryList { get; set; }
        public string DetailJSON { get; set; }
        public string IndustryCode { get; set; }

        [Display(Name = "Progress Status")]
        public string ProgressStatus { get; set; }

        [Required(ErrorMessage = "Owner Name is missing")]
        [Display(Name = "Lead Owner")]
        public Guid EnquiryOwnerID { get; set; }

       // [Required(ErrorMessage = "General Notes is missing")]
        [Display(Name = "GeneralNotes")]
        [DataType(DataType.MultilineText)]
        public string GeneralNotes { get; set; }

        [Required(ErrorMessage = "Enquiry Status is missing")]
        [Display(Name = "Enquiry Status")]
        public string EnquiryStatus { get; set; }
        public string EnqStatusDescription { get; set; }

        [Required(ErrorMessage = "Subject is missing")]
        [Display(Name = "Subject")]
        [DataType(DataType.MultilineText)]
        public string Subject { get; set; }

        //[Required(ErrorMessage = "DealConverted is missing")]
        [Display(Name = "Deal Converted")]

        public string DealConverted { get; set; }
        public CommonViewModel commonObj { get; set; }
        public string duration { get; set; }
        public string FilterWords { get; set; }
        public List<EnquiryViewModel> OpenList { get; set; }
        public List<EnquiryViewModel> ConvertList { get; set; }
        public List<EnquiryViewModel> NonConvertList { get; set; }
        public string URL { get; set; }
        public string EnquiryStatusCode { get; set; }
        public Guid hdnFileID { get; set; }
        public string LeadOwner { get; set; }

        public Guid SalesPersonID { get; set; }
        public SalesPersonViewModel salesPersonObj { get; set; }

        public Guid IndustryID { get; set; }
        public IndustryViewModel industryObj { get; set; }

        public Guid EnquirySourceID { get; set; }
        public EnquirySourceViewModel enquirySourceObj { get; set; }

        public Guid EnquiryStatusID { get; set; }
        public EnquiryStatusViewModel enquiryStatusObj { get; set; }

        public Guid ProgressStatusID { get; set; }
        public ProgressStatusViewModel progressStatusObj { get; set; }

        public FollowUpViewModel followUpObj { get; set; }

        public Guid TitleID { get; set; }
        public TitlesViewModel titleObj { get; set; }

        public List<FollowUpViewModel> FollowUpList { get; set; }

        public Guid ReminderTypeID { get; set; }
        public ReminderViewModel reminderObj { get; set; }

        public Guid PriorityID { get; set; }
        public PriorityViewModel priorityObj { get;set;}
        public MessageViewModel messageObj { get; set; }
        public EnquiryItemViewModel enquiryObjList { get; set; }
        public List<EnquiryItemViewModel> enquiryItemList { get; set; }


    }

    public class EnquirySummaryViewModel
    {

        public int Total { get; set; }
        public int Open { get; set; }
        public int Converted { get; set; }
        public int NotConverted { get; set; }
        public decimal OpenPercentage { get; set; }
        public int ConvertedPercentage { get; set; }
        public int NotConvertedPercentage { get; set; }
    }

    public class TitlesViewModel
    {
        public string Title { get; set; }
        public List<SelectListItem> TitleList { get; set; }
    }


    public class EnquiryItemViewModel
    {
        public Guid? ID { get; set; }
        public Guid? EnquiryID { get; set; }
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
        public List<SelectListItem> enquiryItemList { get; set; }
        public decimal TaxPerc { get; set; }
    }
}