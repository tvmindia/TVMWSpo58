using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class Enquiry
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
        public string IndustryCode { get; set; }
        public string IndustryName { get; set; }
        public string ProgressStatus { get; set; }
        public Guid EnquiryOwnerID { get; set; }
        public string LeadOwner { get; set; }
        public string GeneralNotes { get; set; }
        public string EnquiryStatus { get; set; }
        public string EnqStatusDescription { get; set; }
        public string DealConverted { get; set; }
        public string FilterWords { get; set; }
        public Common commonObj { get; set; }
        public string duration { get; set; }
        public List<Enquiry> OpenList { get; set; }
        public List<Enquiry> ConvertList { get; set; }
        public List<Enquiry> NonConvertList { get; set; }
        public List<Enquiry> IndustryList { get; set; }
        public string URL { get; set; }
        public string EnquiryStatusCode { get; set; }
        public string Subject { get; set; }
        public Message messageObj { get; set; }
        public string DetailXML { get; set; }
        public List<EnquiryItem> enquiryItemList { get; set; }
    }

 

    public class EnquirySummary {

        public int Total { get; set; }
        public int Open { get; set; }
        public int Converted { get; set; }
        public int NotConverted { get; set; }
        public decimal OpenPercentage { get; set; }
        public int ConvertedPercentage { get; set; }
        public int NotConvertedPercentage { get; set; }
    }
    public class Titles
    {
        public string Title { get; set; }
    }

    public class EnquiryItem
    {
        public Guid? ID { get; set; }
        public Guid? EnquiryID { get; set; }
        public string ProductDescription { get; set; }
        public string UnitCode { get; set; }
        public string UnitDescription { get; set; }
        public Unit unit { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public Guid? ProductID { get; set; }
        public string ProductCode { get; set; }
        public string OldProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductSearch { get; set; }
        public Product product { get; set; }
        public Common commonObj { get; set; }
        public Company company { get; set; }
       
    }
}