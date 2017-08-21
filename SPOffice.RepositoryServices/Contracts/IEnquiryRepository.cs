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
        object UpdateEnquiry(Enquiry _enquiriesObj);
        List<Enquiry>GetAllEnquiryList(Enquiry EqyObj);
        List<Enquiry> SearchEnquiriesList(Enquiry enqObj);
    }
}