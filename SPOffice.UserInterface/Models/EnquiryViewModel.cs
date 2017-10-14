using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserInterface.Models;

namespace SPOffice.UserInterface.Models
{
    public class EnquiryViewModel
    {

        public Guid ID { get; set; }
        public string  EnquiryNo { get; set; }
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
        public string IndustryCode { get; set; }
        public string ProgressStatus { get; set; }
        public string EnquiryOwnerID { get; set; }
        public string GeneralNotes { get; set; }
        public string EnquiryStatus { get; set; }
        public string DealConverted { get; set; }
        public CommonViewModel commonObj { get; set; }
        public string duration { get; set; }
        public string FilterWords { get; set; }
        public List<EnquiryViewModel> OpenList { get; set; }
        public List<EnquiryViewModel> ConvertList { get; set; }
        public List<EnquiryViewModel> NonConvertList { get; set; }
        public string URL { get; set; }
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