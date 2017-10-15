using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.BusinessService.Contracts
{
   public interface IDashboardBusiness
    {
        DashboardStatus GetCountOfEnquiries(string duration);
        List<Enquiry> GetRecentEnquiryList( string BaseURL);
        EnquirySummary GetEnquirySummary();
        List<FollowUp> GetTodaysFollowUpDetails(DateTime onDate, string BaseURL);
        CustomerPOSummary GetCustomerPOSummary();
        QuotationSummary GetQuotationSummary();

    }
}
