using AutoMapper;
using Newtonsoft.Json;
using SAMTool.DataAccessObject.DTO;
using SPOffice.BusinessService.Contracts;
using SPOffice.BusinessService.Services;
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
    public class SupplierController : ApiController
    {
        #region Constructor_Injection
        AppConst c = new AppConst();
        ISupplierBusiness _supplierBusiness;
        SAMTool.BusinessServices.Contracts.IUserBusiness _userBusiness;

        public SupplierController(SupplierBusiness supplierBusiness, SAMTool.BusinessServices.Contracts.IUserBusiness userBusiness)
        {
            _supplierBusiness = supplierBusiness;
            _userBusiness = userBusiness;
        }
        #endregion Constructor_Injection

        Const messages = new Const();

        #region GetAllApprovedSupplierOrderForMobile
        [HttpPost]
        public string GetAllSupplierPODetail(SupplierOrderViewModel supplierObj)
        {
            try
            {
                List<SupplierOrderViewModel> suppliersList = Mapper.Map<List<SupplierOrder>, List<SupplierOrderViewModel>>(_supplierBusiness.GetAllSupplierPOForMobile(supplierObj.duration));
                return JsonConvert.SerializeObject(new { Result = true, Records = suppliersList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        #endregion  GetAllApprovedSupplierOrderForMobile

        #region GetAllSupplierPendingPurchaseOrders
        [HttpPost]
        public string GetAllSupplierPurchaseOrdersList(SupplierOrderViewModel SupplierObj)
        {
            try
            {
                bool isAdminOrCeo = false;
                Permission _permission = _userBusiness.GetSecurityCode(SupplierObj.userObj.UserName, "SupplierOrder");
                if (_permission.SubPermissionList != null)
                {
                    if (_permission.SubPermissionList.Exists(s => s.Name == "C_Approval") == false || _permission.SubPermissionList.First(s => s.Name == "C_Approval").AccessCode.Contains("R"))
                    {
                        isAdminOrCeo = true;
                    }
                }
                List<SupplierOrderViewModel> SPOVMList = Mapper.Map<List<SupplierOrder>, List<SupplierOrderViewModel>>(_supplierBusiness.GetAllSupplierPurchaseOrdersList(Mapper.Map<SupplierOrderViewModel,SupplierOrder>(SupplierObj)));
                return JsonConvert.SerializeObject(new { Result = true, Records = SPOVMList});
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        #endregion GetAllSupplierPendingPurchaseOrders

        #region GetPurchaseOrderByID
        [HttpPost]
        public string GetPurchaseOrderByID(SupplierOrderViewModel suppObj)
        {
            try
            {
                bool IsApprover = false;
                Permission _permission = _userBusiness.GetSecurityCode(suppObj.userObj.UserName, "SupplierOrder");
                if (_permission.SubPermissionList != null)
                {
                    if (_permission.SubPermissionList.Exists(s => s.Name == "CEO") == false || _permission.SubPermissionList.First(s => s.Name == "SAdmin").AccessCode.Contains("R"))
                    {
                        IsApprover = true;
                    }
                }
                SupplierOrderViewModel supplierPOViewModel = Mapper.Map<SupplierOrder, SupplierOrderViewModel>(_supplierBusiness.GetSupplierPurchaseOrderByID(suppObj.ID != null && suppObj.ID.ToString() != "" ? Guid.Parse(suppObj.ID.ToString()) : Guid.Empty));
                return JsonConvert.SerializeObject(new { Result = true, Records = supplierPOViewModel });
            }
            catch (Exception ex)
            {
              
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        #endregion GetPurchaseOrderByID


        #region Approve PO
        [HttpPost]
        public string ApproveSupplierOrder(SupplierOrderViewModel suppObj)
        {
            try
            {
                bool isAdminOrCeo = false;
                Permission _permission = _userBusiness.GetSecurityCode(suppObj.userObj.UserName, "SupplierOrder");
                if (_permission.SubPermissionList != null)
                {
                    if (_permission.SubPermissionList.Exists(s => s.Name == "C_Approval") == false || _permission.SubPermissionList.First(s => s.Name == "C_Approval").AccessCode.Contains("R"))
                    {
                        isAdminOrCeo = true;
                    }
                }

                suppObj.commonObj = new CommonViewModel();
                suppObj.commonObj.CreatedBy = suppObj.userObj.UserName;
                DataAccessObject.DTO.Common commonobj = new DataAccessObject.DTO.Common();
                suppObj.commonObj.UpdatedDate = commonobj.GetCurrentDateTime();
                object supplierObj =_supplierBusiness.ApproveSupplierOrder((Guid)suppObj.ID,commonobj.GetCurrentDateTime());
                return JsonConvert.SerializeObject(new { Result = true, Records = supplierObj , Message="Approved" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result =false, Message = ex.Message });
            }
        }
        #endregion Approve PO


        #region PO pending for approval list
        [HttpPost]
        public string GetAllPendingSupplierPurchaseOrders(SupplierOrderViewModel supplierObj)
        {
            try
            {
                List<SupplierOrderViewModel> SPOVMList = null;
                Permission _permission = _userBusiness.GetSecurityCode(supplierObj.userObj.UserName, "SupplierOrder");
                if (_permission.SubPermissionList.Exists(s => s.Name == "ApproveBtn") == false || _permission.SubPermissionList.First(s => s.Name == "ApproveBtn").AccessCode.Contains("R"))
                {
                    SPOVMList = Mapper.Map<List<SupplierOrder>, List<SupplierOrderViewModel>>(_supplierBusiness.GetAllPendingSupplierPurchaseOrders());
                }
                else
                {
                    SPOVMList = new List<SupplierOrderViewModel>();
                }
                return JsonConvert.SerializeObject(new { Result = true, Records = SPOVMList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        #endregion PO pending for approval list


        #region GetPurchaseOrderDetailTable
        [HttpPost]
        public string GetPurchaseOrderDetailTable(SupplierPODetailViewModel suppObj)
        {

            try
            {
                List<SupplierPODetailViewModel> suppliersList = Mapper.Map<List<SupplierPODetail>, List<SupplierPODetailViewModel>>(_supplierBusiness.GetPurchaseOrderDetailTable((Guid)suppObj.POID));
                return JsonConvert.SerializeObject(new { Result = true, Records = suppliersList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
            
        }
        #endregion GetPurchaseOrderDetailTable

        #region Delete Supplier PO
        [HttpPost]
        public string DeletePurchaseOrder(SupplierOrderViewModel supplierObj)
        {
            object result = null;
            try
            {
                result = _supplierBusiness.DeletePurchaseOrder((Guid)supplierObj.ID);
                return JsonConvert.SerializeObject(new { Result = true, Records = result });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        #endregion Delete Supplier PO

        [HttpPost]
        public string SendSupplierNotificationToCEO(SupplierOrderViewModel suppObj)
        {
            object result = null;
            try
            {
                SupplierOrderViewModel supplierPO = Mapper.Map<SupplierOrder, SupplierOrderViewModel>(_supplierBusiness.GetSupplierPurchaseOrderByID(suppObj.ID != null && suppObj.ID.ToString() != "" ? Guid.Parse(suppObj.ID.ToString()) : Guid.Empty));
                string titleString = "Pending Supplier PO";
                string descriptionString = supplierPO.company.Name + ", Purchase Order: " + supplierPO.PONo + ", Amount: " + supplierPO.TotalAmount + ",PurchaseOrderCreatedBy:"+ supplierPO.SupplierName+",Purchase Order:"+ supplierPO.PODate;
                Boolean isCommonForCEO = true;
                _supplierBusiness.SendToFCMToCEO(titleString, descriptionString, isCommonForCEO);
                //Update notification 
                result = _supplierBusiness.UpdateNotificationToCEO(Mapper.Map<SupplierOrderViewModel, SupplierOrder>(suppObj));
                return JsonConvert.SerializeObject(new { Result = true, Message = c.NotificationSuccess, Records = result });

            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = "Failed!!Notification cannot be send" });
            }
        }
    }

}
