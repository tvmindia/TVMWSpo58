using AutoMapper;
using Newtonsoft.Json;
using SAMTool.DataAccessObject.DTO;
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
        SAMTool.BusinessServices.Contracts.IUserBusiness _userBusiness;
        public RequisitionController(IRequisitionBusiness requisitionBusiness, SAMTool.BusinessServices.Contracts.IUserBusiness userBusiness)
        {
            _requisitionBusiness = requisitionBusiness;
            _userBusiness = userBusiness;
        }

        [HttpPost]
        public object GetUserRequisitionList(UserViewModel UserObj)
        {
            try
            {
                ReqAdvanceSearch ReqAdvanceSearchObj = new ReqAdvanceSearch();
                bool isAdminOrCeo = false;
                Permission _permission = _userBusiness.GetSecurityCode(UserObj.UserName, "Requisition");
                if (_permission.SubPermissionList != null)
                {
                    if (_permission.SubPermissionList.Exists(s => s.Name == "C_Approval") == false || _permission.SubPermissionList.First(s => s.Name == "C_Approval").AccessCode.Contains("R"))
                    {
                        isAdminOrCeo = true;
                    }
                }
                List<RequisitionViewModel> RequisitionObj = Mapper.Map<List<Requisition>, List<RequisitionViewModel>>(_requisitionBusiness.GetUserRequisitionList(UserObj.UserName, (Guid)UserObj.RoleObj.AppID, isAdminOrCeo, ReqAdvanceSearchObj));

                return JsonConvert.SerializeObject(new { Result = true, Records = RequisitionObj });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }
        }
    }
}
