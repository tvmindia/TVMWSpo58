using SPOffice.DataAccessObject;
using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.RepositoryServices.Contracts
{
    public interface IReportRepository
    {
        List<Reports> GetAllSysReports(AppUA appUA);
        List<EnquiryReport> GetEnquiryDetails(DateTime? FromDate, DateTime? ToDate,string EnquiryStatus, string search, string Product);
        List<CourierReport> GetCourierDetails(DateTime? FromDate, DateTime? ToDate, string AgencyCode, string search, string Type);
        List<QuotationReport> GetQuotationDetails(ReportAdvanceSearch ReptAdvancedSearchObj);
        List<CustomerPOReport> GetCustomerPODetails(ReportAdvanceSearch ReptAdvancedSearchObj);
        List<RequisitionReport> GetRequisitionDetails(ReportAdvanceSearch ReptAdvancedSearchObj);
        List<EnquiryFollowupReport> GetEnquiryFollowUpDetails(EnquiryFollowupReportAdvanceSearch advanceSearchObject);
    }
}
