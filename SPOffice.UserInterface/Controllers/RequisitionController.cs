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
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace SPOffice.UserInterface.Controllers
{
    public class RequisitionController : Controller
    {
        AppConst c = new AppConst();
        IRequisitionBusiness _requisitionBusiness;
        ICompanyBusiness _companyBusiness;
        IRawMaterialBusiness _rawMaterialBusiness;
        SecurityFilter.ToolBarAccess _tool;
        public RequisitionController(IRequisitionBusiness requisitionBusiness, ICompanyBusiness companyBusiness, IRawMaterialBusiness rawMaterialBusiness, SecurityFilter.ToolBarAccess tool)
        {
            _requisitionBusiness = requisitionBusiness;
            _companyBusiness = companyBusiness;
            _rawMaterialBusiness = rawMaterialBusiness;
            _tool = tool;
        }
        // GET: Requisition
        [AuthSecurityFilter(ProjectObject = "Requisition", Mode = "R")]
        public ActionResult Index()
        {
            RequisitionViewModel RVM = new RequisitionViewModel();
            RVM.CompanyObj = new CompanyViewModel();
            RVM.RequisitionDetailObj = new RequisitionDetailViewModel();
            RVM.RequisitionDetailObj.RawMaterialObj = new RawMaterialViewModel();
            List<SelectListItem> selectListItem = null;

            RVM.CompanyObj.CompanyList = new List<SelectListItem>();
            selectListItem = new List<SelectListItem>();
            List<CompanyViewModel> CompaniesList = Mapper.Map<List<Company>, List<CompanyViewModel>>(_companyBusiness.GetAllCompanies());
            foreach (CompanyViewModel Cmp in CompaniesList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = Cmp.Name,
                    Value = Cmp.Code,
                    Selected = false
                });
            }
            RVM.CompanyObj.CompanyList = selectListItem;

            RVM.RequisitionDetailObj.RawMaterialObj.RawMaterialList = new List<SelectListItem>();
            selectListItem = new List<SelectListItem>();
            List<RawMaterialViewModel> RawMaterialList = Mapper.Map<List<RawMaterial>, List<RawMaterialViewModel>>(_rawMaterialBusiness.GetAllRawMaterial());
            foreach (RawMaterialViewModel Rwl in RawMaterialList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = Rwl.MaterialCode,
                    Value = Rwl.ID.ToString(),
                    Selected = false
                });
            }
            RVM.RequisitionDetailObj.RawMaterialObj.RawMaterialList = selectListItem;
            return View(RVM);
        }
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Requisition", Mode = "R")]
        public string GetUserRequisitionList(string AdvanceSearchObject)
        {
            try
            {
                ReqAdvanceSearch ReqAdvancedSearchObj = AdvanceSearchObject!=null?JsonConvert.DeserializeObject<ReqAdvanceSearch>(AdvanceSearchObject):new ReqAdvanceSearch();
                bool isAdminOrCeo = false;
                Permission _permission = Session["UserRights"] as Permission;
                if (_permission.SubPermissionList != null)
                {
                    if (_permission.SubPermissionList.Exists(s => s.Name == "C_Approval") == false || _permission.SubPermissionList.First(s => s.Name == "C_Approval").AccessCode.Contains("R"))
                    {
                        isAdminOrCeo = true;
                    }
                }
                AppUA _appUA = Session["AppUA"] as AppUA;
                List<RequisitionViewModel> RequisitionList = Mapper.Map<List<Requisition>, List<RequisitionViewModel>>(_requisitionBusiness.GetUserRequisitionList(_appUA.UserName,_appUA.AppID, isAdminOrCeo, ReqAdvancedSearchObj));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = RequisitionList });//, Open = openCount, InProgress = inProgressCount, Closed = closedCount });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Requisition", Mode = "R")]
        public string GetRequisitionDetail(string ID)
        {
            try
            {
                List<RequisitionDetailViewModel> RequisitionDetailList = new List<RequisitionDetailViewModel>();
                RequisitionDetailViewModel RequisitionDetailObj = new RequisitionDetailViewModel();
                if (ID=="0")
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
                    RequisitionDetailList= Mapper.Map<List<RequisitionDetail>, List<RequisitionDetailViewModel>>(_requisitionBusiness.GetRequisitionDetailList(Guid.Parse(ID)));
                }                
                return JsonConvert.SerializeObject(new { Result = "OK", Records = RequisitionDetailList });//, Open = openCount, InProgress = inProgressCount, Closed = closedCount });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Requisition", Mode = "R")]
        public string GetItemDetail(string MaterialID)
        {
            try
            {

                RawMaterialViewModel rawMaterialViewModelObj = Mapper.Map<RawMaterial, RawMaterialViewModel>(_rawMaterialBusiness.GetRawMaterialDetails(Guid.Parse(MaterialID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = rawMaterialViewModelObj });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Requisition", Mode = "R")]
        public string GetRequisitionDetailByID(string ID)
        {
            try
            {
                AppUA _appUA = Session["AppUA"] as AppUA;
                RequisitionViewModel requisitionViewModelObj = Mapper.Map<Requisition, RequisitionViewModel>(_requisitionBusiness.GetRequisitionDetails(Guid.Parse(ID),_appUA.UserName));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = requisitionViewModelObj });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        [HttpPost]
        [AuthSecurityFilter(ProjectObject = "Requisition", Mode = "R")]
        [ValidateAntiForgeryToken]
        public string InsertUpdateRequisition(RequisitionViewModel RequisitionObj)
        {
            try
            {
                object result = null;
                //if (ModelState.IsValid)
                {
                    bool isAdminOrCeo = false;
                    Permission _permission = Session["UserRights"] as Permission;
                    if (_permission.SubPermissionList != null)
                    {
                        if (_permission.SubPermissionList.Exists(s => s.Name == "C_Approval") == false || _permission.SubPermissionList.First(s => s.Name == "C_Approval").AccessCode.Contains("R"))
                        {
                            isAdminOrCeo = true;
                        }
                    }
                    AppUA _appUA = Session["AppUA"] as AppUA;
                    RequisitionObj.CommonObj = new CommonViewModel();
                    RequisitionObj.CommonObj.CreatedBy = _appUA.UserName;
                    RequisitionObj.CommonObj.CreatedDate = _appUA.DateTime;
                    RequisitionObj.CommonObj.UpdatedBy = _appUA.UserName;
                    RequisitionObj.CommonObj.UpdatedDate = _appUA.DateTime;
                    //Deserialize items
                    object ResultFromJS = JsonConvert.DeserializeObject(RequisitionObj.RequisitionDetailObj.RequisitionDetailObject);
                    string ReadableFormat = JsonConvert.SerializeObject(ResultFromJS);
                    RequisitionObj.RequisitionDetailList = JsonConvert.DeserializeObject<List<RequisitionDetailViewModel>>(ReadableFormat);
                    foreach(RequisitionDetailViewModel Obj in RequisitionObj.RequisitionDetailList)
                    {
                        Obj.MaterialID = (Obj.MaterialID != Guid.Empty) ? Obj.MaterialID : null;
                        Obj.RawMaterialObj = null;
                    }
                    switch (RequisitionObj.ID==Guid.Empty)
                    {
                        case true:
                            result = _requisitionBusiness.InsertRequisition(Mapper.Map<RequisitionViewModel, Requisition>(RequisitionObj), isAdminOrCeo);
                            break;
                        case false:
                            if(RequisitionObj.ReqForCompany==null)
                            {
                                RequisitionObj.ReqForCompany = RequisitionObj.hdnReqForCompany;
                            }
                            result = _requisitionBusiness.UpdateRequisition(Mapper.Map<RequisitionViewModel, Requisition>(RequisitionObj));
                            break;
                    }

                    return JsonConvert.SerializeObject(new
                    {
                        Result = "OK",
                        Record = result
                    });
                }
                //else
                //{
                //    List<string> modelErrors = new List<string>();
                //    foreach (var modelState in ModelState.Values)
                //    {
                //        foreach (var modelError in modelState.Errors)
                //        {
                //            modelErrors.Add(modelError.ErrorMessage);
                //        }
                //    }
                //    return JsonConvert.SerializeObject(new { Result = "VALIDATION", Message = string.Join(",", modelErrors) });
                //}
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }        
        [HttpGet]
        public string ApproveRequisition(string ID)
        {
            try
            {
                RequisitionViewModel RequisitionObj = new RequisitionViewModel();
                bool isAdminOrCeo = false;
                Permission _permission = Session["UserRights"] as Permission;
                if (_permission.SubPermissionList != null)
                {
                    if (_permission.SubPermissionList.Exists(s => s.Name == "C_Approval") == false || _permission.SubPermissionList.First(s => s.Name == "C_Approval").AccessCode.Contains("R"))
                    {
                        isAdminOrCeo = true;
                    }
                }
                AppUA _appUA = Session["AppUA"] as AppUA;
                RequisitionObj.CommonObj = new CommonViewModel();
                RequisitionObj.CommonObj.CreatedBy = _appUA.UserName;
                RequisitionObj.CommonObj.UpdatedDate = _appUA.DateTime;
                RequisitionObj.ID = Guid.Parse(ID);
                RequisitionViewModel Requisition = Mapper.Map <Requisition, RequisitionViewModel> (_requisitionBusiness.ApproveRequisition(Mapper.Map<RequisitionViewModel, Requisition>(RequisitionObj), isAdminOrCeo));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = Requisition, Message = "Approved" });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
           
        }
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Requisition", Mode = "R")]
        public string DeleteRequisitionDetailByID(string ID)
        {
            object result = null;
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID Missing");
                }
                result = _requisitionBusiness.DeleteRequisitionDetailByID(Guid.Parse(ID));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = result });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        [HttpGet]
        public string GetRequisitionOverViewCount()
        {
            try
            {
                bool isAdminOrCeo = false;
                AppUA _appUA = Session["AppUA"] as AppUA;
                Permission _permission = Session["UserRights"] as Permission;
                if(_permission.SubPermissionList!=null)
                {
                    if (_permission.SubPermissionList.Exists(s => s.Name == "C_Approval") == false || _permission.SubPermissionList.First(s => s.Name == "C_Approval").AccessCode.Contains("R"))
                    {
                        isAdminOrCeo = true;
                    }
                }
                RequisitionOverViewCountViewModel requisitionOverviewCountObj = Mapper.Map<RequisitionOverViewCount, RequisitionOverViewCountViewModel>(_requisitionBusiness.GetRequisitionOverViewCount(_appUA.UserName, isAdminOrCeo));
                requisitionOverviewCountObj.IsAdminOrCeo = isAdminOrCeo;
                return JsonConvert.SerializeObject(new { Result = "OK", Records = requisitionOverviewCountObj });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #region ButtonStyling
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Requisition", Mode = "R")]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            Permission _permission = Session["UserRights"] as Permission;
            switch (ActionType)
            {
                case "List":
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Text = "Add";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    ToolboxViewModelObj.addbtn.Event = "AddNew();"; 
                    break;
                case "Edit":
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Text = "Add";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    ToolboxViewModelObj.addbtn.Event = "AddNew();";

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Text = "Save";
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    ToolboxViewModelObj.savebtn.Event = "SaveRequisition()";

                    ToolboxViewModelObj.CloseBtn.Visible = true;
                    ToolboxViewModelObj.CloseBtn.Text = "Close";
                    ToolboxViewModelObj.CloseBtn.Title = "Close";
                    ToolboxViewModelObj.CloseBtn.Event = "closeNav();";

                    ToolboxViewModelObj.ApproveBtn.Visible = true;
                    ToolboxViewModelObj.ApproveBtn.Text = "Approve";
                    ToolboxViewModelObj.ApproveBtn.Title = "Approve";
                    ToolboxViewModelObj.ApproveBtn.Event = "ApproveRequsistion()";
                    ToolboxViewModelObj = _tool.SetToolbarAccess(ToolboxViewModelObj, _permission);
                    break;
                case "Add":
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Text = "Add";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    ToolboxViewModelObj.addbtn.Event = "AddNew();";

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Text = "Save";
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    ToolboxViewModelObj.savebtn.Event = "SaveRequisition()";

                    ToolboxViewModelObj.CloseBtn.Visible = true;
                    ToolboxViewModelObj.CloseBtn.Text = "Close";
                    ToolboxViewModelObj.CloseBtn.Title = "Close";
                    ToolboxViewModelObj.CloseBtn.Event = "closeNav();";
                    break;
                default:
                    return Content("Nochange");
            }
            return PartialView("ToolboxView", ToolboxViewModelObj);
        }

        #endregion
    }
}