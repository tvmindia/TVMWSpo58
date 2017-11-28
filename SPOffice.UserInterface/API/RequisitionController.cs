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
//Get requisition details by Id with parameters id and username
        [HttpPost]
        public string GetRequisitionDetailByID(RequisitionViewModel ReqObj)
        { 
            try
            {
                RequisitionViewModel requisitionViewModelObj = Mapper.Map<Requisition, RequisitionViewModel>(_requisitionBusiness.GetRequisitionDetails( Guid.Parse(ReqObj.ID.ToString()) , ReqObj.userObj.UserName));
                requisitionViewModelObj.RequisitionDetailList = Mapper.Map<List<RequisitionDetail>, List<RequisitionDetailViewModel>>(_requisitionBusiness.GetRequisitionDetailList((ReqObj.ID)));
                return JsonConvert.SerializeObject(new { Result = true, Records = requisitionViewModelObj });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }
        }
//Get Material details by requisition ID
        [HttpPost]
        public string GetRequisitionDetail(RequisitionDetailViewModel ReqObj)
        {
            try
            {
                List<RequisitionDetailViewModel> RequisitionDetailList = new List<RequisitionDetailViewModel>();
                RequisitionDetailViewModel RequisitionDetailObj = new RequisitionDetailViewModel();
                if (ReqObj == null)
                {
                    RequisitionDetailObj.ID = Guid.Empty;
                    RequisitionDetailObj.MaterialID = Guid.Empty;
                    RequisitionDetailObj.ReqID = Guid.Empty;
                    RequisitionDetailObj.RawMaterialObj = new RawMaterialViewModel();
                    RequisitionDetailObj.RawMaterialObj.Description = "";
                    RequisitionDetailObj.RawMaterialObj.ID = Guid.Empty;
                    RequisitionDetailList.Add(RequisitionDetailObj);
                }
                else
                {
                    RequisitionDetailList = Mapper.Map<List<RequisitionDetail>, List<RequisitionDetailViewModel>>(_requisitionBusiness.GetRequisitionDetailList((ReqObj.ID)));
                }
                return JsonConvert.SerializeObject(new { Result = true, Records = RequisitionDetailList });//, Open = openCount, InProgress = inProgressCount, Closed = closedCount });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }
        }
    }
}
