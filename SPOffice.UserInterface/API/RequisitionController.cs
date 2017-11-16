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
    public class RequisitionController : ApiController
    {
        AppConst c = new AppConst();
        IRequisitionBusiness _requisitionBusiness;
        public RequisitionController(IRequisitionBusiness requisitionBusiness)
        {
            _requisitionBusiness = requisitionBusiness;
        }

        [HttpPost]
        public object GetUserRequisitionList(UserViewModel UserObj)
        {
            try
            {  
                List<RequisitionViewModel> RequisitionObj = Mapper.Map<List<Requisition>, List<RequisitionViewModel>>(_requisitionBusiness.GetUserRequisitionList(UserObj.UserName, (Guid)UserObj.RoleObj.AppID));

                return JsonConvert.SerializeObject(new { Result = true, Records = RequisitionObj });//, Open = openCount, InProgress = inProgressCount, Closed = closedCount });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }
        }
    }
}
