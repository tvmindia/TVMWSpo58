using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPOffice.DataAccessObject.DTO;


namespace SPOffice.RepositoryServices.Contracts
{
    public interface IEnquiryRepository
    {
        Enquiry InsertEnquiry(Enquiry _enquiriesObj);
        Enquiry UpdateEnquiry(Enquiry _enquiriesObj);
        List<Enquiry>GetAllEnquiryList(Enquiry EqyObj);
        List<Enquiry> SearchEnquiriesList(Enquiry enqObj);
        List<Enquiry> GetRecentEnquiryList();
        EnquirySummary GetEnquirySummary();
        List<Titles> GetAllTitles();
        Enquiry GetEnquiryDetailsById(Guid ID);
        object DeleteEnquiry(Guid ID);      
    }
}