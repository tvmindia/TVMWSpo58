using AutoMapper;
using Newtonsoft.Json;
using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.UserInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserInterface.Models;

namespace SPOffice.UserInterface.API
{
    public class DashBoardController : ApiController
    {
        #region Constructor_Injection 

        AppConst c = new AppConst();
        IDashboardBusiness _dashboardBusiness;


        public DashBoardController(IDashboardBusiness dashboardBusiness)
        {
            _dashboardBusiness = dashboardBusiness;

        }
        #endregion Constructor_Injection 

        [HttpPost]
        public string StatiticsForMobile(DashboardStatusViewModel homescreenObj)
        {
            try
            {
                DashboardStatusViewModel saleObj = Mapper.Map<DashboardStatus, DashboardStatusViewModel>(_dashboardBusiness.GetCountOfEnquiries(homescreenObj.duration));

                return JsonConvert.SerializeObject(new { Result = true, Records = saleObj });
            }
            catch (Exception ex)
            {

                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

    }
}
