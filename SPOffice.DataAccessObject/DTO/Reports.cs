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
}