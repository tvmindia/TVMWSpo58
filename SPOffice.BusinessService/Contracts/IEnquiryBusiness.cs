using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Contracts
{
    public interface IEnquiryBusiness
    {
        object InsertUpdateEnquiry(Enquiry _enquiriesObj);
        List<Enquiry>GetAllEnquiryList(Enquiry EqyObj);
    }
}