using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class Reports
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
    //public class EnquiryReport
    //{
    //   public string ID { get; set; }
		  //public string EnquiryNo { get; set; }
    //    public string EnquiryDate { get; set; }

    //    public string ContactTitle { get; set; }
    //    public string ContactName { get; set; }
		  // ,CompanyName { get; set; }
		  // ,E.[Address] { get; set; }
		  // ,Website { get; set; }
		  // ,Email
		  // ,LandLine
		  // ,Mobile
		  // ,Fax
		  // ,ES.[Description] EnquirySource
    //       , I.[Description]
    //    Industry
		  // ,E.GeneralNotes
		  // ,ESS.[Description] EnquiryStatus
    //       , E.[Subject]
    //}



    public class CourierReport
    {
        public Guid? ID { get; set; }
        public Guid hdnFileID { get; set; }
        public string Type { get; set; }
        public string TransactionDate { get; set; }
        public string Track { get; set; }
        public string SourceName { get; set; }
        public string SourceAddress { get; set; }
        public string DestName { get; set; }
        public string DestAddress { get; set; }
        public string DistributedTo { get; set; }
        public string DistributionDate { get; set; }
        public CourierAgency courierAgency { get; set; }
        public string AgencyCode { get; set; }
        public string TrackingRefNo { get; set; }
        public string GeneralNotes { get; set; }
        public string TrackingURL { get; set; }
        public Common commonObj { get; set; }      
        public List<CourierReport> courierDetailList { get; set; }
    }   


    public class EnquiryReport
    {
        public string ID { get; set; }
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
        public string  EnquiryStatus { get; set; }
        public string Subject { get; set; }
    }
}