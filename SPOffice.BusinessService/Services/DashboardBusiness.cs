using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class DashboardBusiness : IDashboardBusiness
    {
        private IDashboardRepository _dashboardRepository;
        private ICommonBusiness _commonBusiness;
        public DashboardBusiness(IDashboardRepository dashboardRepository, ICommonBusiness commonBusiness)
        {
            _dashboardRepository = dashboardRepository;
            _commonBusiness = commonBusiness;
        }
       

    }
}