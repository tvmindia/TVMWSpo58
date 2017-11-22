using SPOffice.DataAccessObject;
using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.BusinessService.Contracts
{
   public interface IReportBusiness
    {
        List<Reports> GetAllSysReports(AppUA appUA);
        List<EnquiryReport> GetEnquiryDetails(DateTime? FromDate, DateTime? ToDate, string EnquiryStatus, string search);
        List<QuotationReport> GetQuotationDetails(DateTime? FromDate, DateTime? ToDate, string EnquiryStatus, string search);
    }
}
