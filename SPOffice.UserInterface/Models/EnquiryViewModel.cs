using SPOffice.DataAccessObject.DTO;
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
        public string Email { get; set; }
        public string LandLine { get; set; }

        [Required(ErrorMessage = "Mobile No. is missing")]
        public string Mobile { get; set; }
        public string Fax { get; set; }

        [Display(Name = "Enquiry Source")]
        public string EnquirySource { get; set; }

        [Required(ErrorMessage = "IndustryName is missing")]
        [Display(Name = "Industry")]
        public string IndustryName { get; set; }
        public List<EnquiryViewModel> IndustryList{get;set;}

        public string IndustryCode { get; set; }

        [Display(Name = "Progress Status")]
        public string ProgressStatus { get; set; }

        [Required(ErrorMessage = "Owner Name is missing")]
        [Display(Name = "Lead Owner")]
        public string EnquiryOwnerID { get; set; }

        [Required(ErrorMessage = "General Notes is missing")]
        [Display(Name = "Subject")]
        [DataType(DataType.MultilineText)]
        public string GeneralNotes { get; set; }

        [Required(ErrorMessage = "Enquiry Status is missing")]
        [Display(Name = "Enquiry Status")]
        public string EnquiryStatus { get; set; }

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
}