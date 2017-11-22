using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace SPOffice.UserInterface.Models
{
    public class ReportViewModel
    {
        public Guid AppID { get; set; }
        public Guid ID { get; set; }
        public string ReportName { get; set; }
        public string ReportDescription { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ReportGroup { get; set; }
        public int GroupOrder { get; set; }
        public string SPName { get; set; }
        public string SQL { get; set; }
        public int ReportOrder { get; set; }
    }

    public class EnquiryReportViewModel
    {
        public Guid ID { get; set; }
        public string EnquiryNo { get; set; }
        public string EnquiryDate { get; set; }
        public string ContactTitle { get; set; }
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string LandLine { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string EnquirySource { get; set; }
        public string Industry { get; set; }
        public string GeneralNotes { get; set; }
        [Display(Name = "Enquiry Status")]
        public string EnquiryStatus { get; set; }
        public string Subject { get; set; }
        public string Search { get; set; }
        public EnquiryStatusViewModel enquiryStatusObj { get; set; }
    }


    public class CourierReportViewModel
    {
        public Guid? ID { get; set; }
        public Guid hdnFileID { get; set; }          
        public string Type { get; set; }
        public List<SelectListItem> CourierTypeList { get; set; }        
       
        public string TransactionDate { get; set; }
        public string Track { get; set; }    
       
        public string SourceName { get; set; }        
        public string SourceAddress { get; set; }      
       
        public string DestName { get; set; }        
        public string DestAddress { get; set; }     
        public string DistributedTo { get; set; }       
        public string DistributionDate { get; set; }      
       
        public string AgencyCode { get; set; }
        public CourierAgencyViewModel courierAgency { get; set; }
        public List<SelectListItem> AgencyList { get; set; }           
        public string TrackingRefNo { get; set; }     
       
        public string GeneralNotes { get; set; }         
        public string TrackingURL { get; set; }
        public CommonViewModel commonObj { get; set; }
    }

    public class QuotationReportViewModel
    {
        public Guid ID { get; set; }
        public string QuotationNo { get; set; }
        public string QuotationDate { get; set; }
        public string QuoteFromCompName { get; set; }
        public string QuoteStage { get; set; }
        public string QuoteSubject { get; set; }
        public string ContactPerson { get; set; }
        public string CompanyName { get; set; }
    }
}