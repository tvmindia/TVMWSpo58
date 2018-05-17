using AutoMapper;
using Newtonsoft.Json;
using SAMTool.DataAccessObject.DTO;
using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.UserInterface.Models;
using SPOffice.UserInterface.SecurityFilter;
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
        public object GetUserRequisitionList(RequisitionViewModel ReqObj)
        {
            try
            {
                ReqAdvanceSearch ReqAdvanceSearchObj = new ReqAdvanceSearch();
                ReqAdvanceSearchObj = Mapper.Map<ReqAdvanceSearchViewModel, ReqAdvanceSearch>(ReqObj.ReqAdvSearchObj);
                if (ReqObj.ReqAdvSearchObj.ReqStatus == "ALL")
                {
                    ReqAdvanceSearchObj.ReqStatus = null;
                }
                bool isAdminOrCeo = false;
                Permission _permission = _userBusiness.GetSecurityCode(ReqObj.userObj.UserName, "Requisition");
                if (_permission.SubPermissionList != null)
                {
                    if (_permission.SubPermissionList.Exists(s => s.Name == "C_Approval") == false || _permission.SubPermissionList.First(s => s.Name == "C_Approval").AccessCode.Contains("R"))
                    {
                        isAdminOrCeo = true;
                    }
                }

                bool showApproved=false;
                if (ReqObj.ReqAdvSearchObj.FinalApproved == false)
                {
                    showApproved =true;
                }

                List<RequisitionViewModel> RequisitionObj = Mapper.Map<List<Requisition>, List<RequisitionViewModel>>(_requisitionBusiness.GetUserRequisitionList(ReqObj.userObj.UserName, (Guid)ReqObj.userObj.RoleObj.AppID, isAdminOrCeo, ReqAdvanceSearchObj, showApproved));

                return JsonConvert.SerializeObject(new { Result = true, Records = RequisitionObj });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }
        }
        #region Requisition detail by Id
        //Get requisition details by Id with parameters id and username
        [HttpPost]
        public string GetRequisitionDetailByID(RequisitionViewModel ReqObj)
        {
            try
            {
                RequisitionViewModel requisitionViewModelObj = Mapper.Map<Requisition, RequisitionViewModel>(_requisitionBusiness.GetRequisitionDetails(Guid.Parse(ReqObj.ID.ToString()), ReqObj.userObj.UserName));
                requisitionViewModelObj.RequisitionDetailList = Mapper.Map<List<RequisitionDetail>, List<RequisitionDetailViewModel>>(_requisitionBusiness.GetRequisitionDetailList((ReqObj.ID)));
                return JsonConvert.SerializeObject(new { Result = true, Records = requisitionViewModelObj });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }
        }
        #endregion  Requisition detail by Id
        //Get Material details by requisition ID
        #region To get the material details by id
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
                return JsonConvert.SerializeObject(new { Result = true, Records = RequisitionDetailList });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }

        }
        #endregion To get the material details by id

        #region Approve Pending Requisitions
        [HttpPost]
            public string ApproveRequisition(RequisitionViewModel RequisitionObj)
        {
            try
            {
                bool isAdminOrCeo = false;
                Permission _permission = _userBusiness.GetSecurityCode(RequisitionObj.userObj.UserName, "Requisition");
                if (_permission.SubPermissionList != null)
                {
                    if (_permission.SubPermissionList.Exists(s => s.Name == "C_Approval") == false || _permission.SubPermissionList.First(s => s.Name == "C_Approval").AccessCode.Contains("R"))
                    {
                        isAdminOrCeo = true;
                    }
                }
              
                RequisitionObj.CommonObj = new CommonViewModel();
                RequisitionObj.CommonObj.CreatedBy = RequisitionObj.userObj.UserName;
                DataAccessObject.DTO.Common commonobj = new DataAccessObject.DTO.Common();
                RequisitionObj.CommonObj.UpdatedDate = commonobj.GetCurrentDateTime();
                //RequisitionObj.ID = Guid.Parse(userObj.ID);
                RequisitionViewModel Requisition = Mapper.Map<Requisition, RequisitionViewModel>(_requisitionBusiness.ApproveRequisition(Mapper.Map<RequisitionViewModel, Requisition>(RequisitionObj), isAdminOrCeo));
                return JsonConvert.SerializeObject(new { Result =true, Records = Requisition, Message = "Approved" });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }

        }
        #endregion

        #region Insert and Update Requisitions
        [HttpPost]
        public string InsertUpdateRequisition(RequisitionViewModel RequisitionObj)
        {
            try
            {
                object result = null;
               
                {
                    bool isAdminOrCeo = false;
                    Permission _permission = _userBusiness.GetSecurityCode(RequisitionObj.userObj.UserName, "Requisition");
                    if (_permission.SubPermissionList != null)
                    {
                        if (_permission.SubPermissionList.Exists(s => s.Name == "C_Approval") == false || _permission.SubPermissionList.First(s => s.Name == "C_Approval").AccessCode.Contains("R"))
                        {
                            isAdminOrCeo = true;
                        }
                    }
                   
                    RequisitionObj.CommonObj = new CommonViewModel();
                    DataAccessObject.DTO.Common commonobj = new DataAccessObject.DTO.Common();
                    RequisitionObj.CommonObj.CreatedBy = RequisitionObj.userObj.UserName;
                    RequisitionObj.CommonObj.CreatedDate = commonobj.GetCurrentDateTime();
                    RequisitionObj.CommonObj.UpdatedBy = RequisitionObj.userObj.UserName;
                    RequisitionObj.CommonObj.UpdatedDate = commonobj.GetCurrentDateTime();
           
                    switch (RequisitionObj.ID == Guid.Empty)
                    {
                        case true:
                            result = _requisitionBusiness.InsertRequisition(Mapper.Map<RequisitionViewModel, Requisition>(RequisitionObj), isAdminOrCeo);
                            try
                            {
                                string reqNo = result.GetType().GetProperty("ReqNo").GetValue(result, null).ToString();
                                _requisitionBusiness.SendToFCMManager(RequisitionObj.Title, "Req No : " + reqNo + " Req Date : " + RequisitionObj.ReqDateFormatted, true, "");
                            }
                            catch (Exception)
                            {

                                
                            }
                          
                            break;
                        case false:
                            if (RequisitionObj.ReqForCompany == null)
                            {
                                RequisitionObj.ReqForCompany = RequisitionObj.hdnReqForCompany;
                            }
                            result = _requisitionBusiness.UpdateRequisition(Mapper.Map<RequisitionViewModel, Requisition>(RequisitionObj));
                            break;
                    }

                    return JsonConvert.SerializeObject(new { Result = true, Records = result });
                }
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }
        }
        #endregion Insert and Update Requisitions

        #region Delete Requisition
        [HttpPost]
        public string DeleteRequisitionDetailByID(RequisitionViewModel ReqObj)
        {
            object result = null;
            try
            {
                result = _requisitionBusiness.DeleteRequisitionDetailByID(ReqObj.ID);
                return JsonConvert.SerializeObject(new { Result = true, Records = result });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }
        }
        #endregion Delete Requisition


        #region Delete Requisition by ID
        [HttpPost]
        public string DeleteRequisitionByID(RequisitionViewModel ReqObj)
        {
            object result = null;
            try
            {
                    Permission _permission = _userBusiness.GetSecurityCode(ReqObj.userObj.UserName, "Requisition");
                    if (_permission.SubPermissionList != null)
                    {
                        if (_permission.SubPermissionList.Exists(s => s.Name == "ButtonDelete" && s.AccessCode!=""))
                        {
                        result = _requisitionBusiness.DeleteRequisitionByID(ReqObj.ID);
                      
                        }
                        else
                        {
                            result = "Access Denied";
                        }
                    
                }
                return JsonConvert.SerializeObject(new { Result = true, Records = result });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }
          
        }
        #endregion Delete Requisition by ID

        [HttpPost]
        public string SendRequisitionNotificationToManager(RequisitionViewModel reqObj)
        {
            object result = null;
            try
            {
                RequisitionViewModel requisitionViewModelObj = Mapper.Map<Requisition, RequisitionViewModel>(_requisitionBusiness.GetRequisitionDetails(Guid.Parse(reqObj.ID.ToString()), reqObj.userObj.UserName));
                string titleString = "Pending Requisition";
                string descriptionString = requisitionViewModelObj.ReqForCompany + ", Requisition: " + requisitionViewModelObj.ReqNo + ", RequisitionBy: " + requisitionViewModelObj.RequisitionBy + " ,RequisitionCreatedBy:"+ requisitionViewModelObj.CommonObj.CreatedBy+",RequisitionDate:"+ requisitionViewModelObj.ReqDate;
                Boolean isCommon = true;
                string CompanyCode = "";
                _requisitionBusiness.SendToFCMManager(titleString, descriptionString, isCommon, CompanyCode);
                //Update notification 
                result = _requisitionBusiness.UpdateNotification(Mapper.Map<RequisitionViewModel, Requisition>(reqObj));
                return JsonConvert.SerializeObject(new { Result = true, Message = c.NotificationSuccess, Records = result });

            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = "Failed!!Notification cannot be send" });
            }
        }


        public string SendRequisitionNotificationToCEO(RequisitionViewModel reqObj)
        {
            object result = null;
            try
            {
                RequisitionViewModel requisitionViewModelObj = Mapper.Map<Requisition, RequisitionViewModel>(_requisitionBusiness.GetRequisitionDetails(Guid.Parse(reqObj.ID.ToString()), reqObj.userObj.UserName));
                string titleString = "Pending Requisition";
                string descriptionString = requisitionViewModelObj.ReqForCompany + ", Requisition: " + requisitionViewModelObj.ReqNo + ", RequisitionBy: " + requisitionViewModelObj.RequisitionBy + " ,RequisitionCreatedBy:" + requisitionViewModelObj.CommonObj.CreatedBy + ",RequisitionDate:" + requisitionViewModelObj.ReqDate;
                Boolean isCommon = true;
                _requisitionBusiness.SendToFCMCEO(titleString, descriptionString, isCommon);
                //Update notification 
                result = _requisitionBusiness.UpdateNotification(Mapper.Map<RequisitionViewModel, Requisition>(reqObj));
                return JsonConvert.SerializeObject(new { Result = true, Message = c.NotificationSuccess, Records = result });

            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = "Failed!!Notification cannot be send" });
            }
        }

        [HttpPost]
        public string GetRequisitionCount(RequisitionOverViewCount reqObj)
        {
            try
            {
                bool isAdminOrCeo = false;
            Permission _permission = _userBusiness.GetSecurityCode(reqObj.UserName, "Requisition");
            if (_permission.SubPermissionList != null)
            {
                if (_permission.SubPermissionList.Exists(s => s.Name == "C_Approval") == false || _permission.SubPermissionList.First(s => s.Name == "C_Approval").AccessCode.Contains("R"))
                {
                    isAdminOrCeo = true;
                }
            }
                RequisitionOverViewCountViewModel requisitionOverviewCountObj = Mapper.Map<RequisitionOverViewCount, RequisitionOverViewCountViewModel>(_requisitionBusiness.GetRequisitionCount(reqObj, isAdminOrCeo));
                return JsonConvert.SerializeObject(new { Result = true, Records = requisitionOverviewCountObj });

            }

            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }
        }
        }
    }

