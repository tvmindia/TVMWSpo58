using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class Enquiry
    {
       
        public Guid ID { get; set; }
        public int EnquiryNo { get; set; }
        public DateTime EnquiryDate { get; set; }
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
        public Common commonObj { get; set; }
    }
}