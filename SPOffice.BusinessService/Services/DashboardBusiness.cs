using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
	public class DashboardBusiness:IDashboardBusiness
	{
        IDashboardRepository _dashboardrepository;
        public DashboardBusiness(IDashboardRepository dashboardrepository)
        {
            _dashboardrepository = dashboardrepository;
        }
        public DashboardStatus GetCountOfEnquiries(string duration)
        {
            return _dashboardrepository.GetCountOfEnquiries(duration);
        }

    }
}