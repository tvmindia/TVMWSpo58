using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class ReportBusiness : IReportBusiness
    {
        IReportRepository _reportRepository;
        
        public ReportBusiness(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
           
        }

        public List<Reports> GetAllSysReports(AppUA appUA)
        {
            List<Reports> systemReportList = null;
            try
            {
                systemReportList = _reportRepository.GetAllSysReports(appUA);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return systemReportList;
        }
    }
    }