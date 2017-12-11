using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Contracts
{
    public interface IEnquiryBusiness
    {
        Enquiry InsertUpdateEnquiry(Enquiry _enquiriesObj);
        List<Enquiry>GetAllEnquiryList(Enquiry EqyObj);
        Enquiry SearchEnquiriesList(Enquiry enqObj);
        List<Titles> GetAllTitles();
        Enquiry GetEnquiryDetailsByID(Guid ID);
        object DeleteEnquiry(Guid ID);
        string SendEnquiryMessage(string mobileNumber);
    }
}