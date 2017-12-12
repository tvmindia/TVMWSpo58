using AutoMapper;
using Newtonsoft.Json;
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
    public class SupplierOrderController : Controller
    {

        AppConst c = new AppConst();
        ISupplierBusiness _supplierBusiness;
        ICompanyBusiness _companyBusiness;
        ITaxTypeBusiness _taxTypeBusiness;
        ICommonBusiness _commonBusiness;

        public SupplierOrderController(ISupplierBusiness supplierBusiness, ICompanyBusiness companyBusiness, 
            ITaxTypeBusiness taxTypeBusiness, ICommonBusiness commonBusiness)
        {
            _supplierBusiness = supplierBusiness;
            _companyBusiness = companyBusiness;
            _taxTypeBusiness = taxTypeBusiness;
            _commonBusiness = commonBusiness;
        }

        // GET: SupplierOrder
        [AuthSecurityFilter(ProjectObject = "CustomerOrder", Mode = "R")]
        public ActionResult Index(string id)
        {
            ViewBag.filter = id;
            SupplierOrderViewModel SPOVM = new SupplierOrderViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            List<SuppliersViewModel> SOList = Mapper.Map<List<Suppliers>, List<SuppliersViewModel>>(_supplierBusiness.GetAllSuppliers());
            foreach (SuppliersViewModel supp in SOList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = supp.CompanyName,
                    Value = supp.ID.ToString(),
                    Selected = false
                });
            }
            SPOVM.SupplierList = selectListItem;

            SPOVM.CompanyList = new List<SelectListItem>();
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
            SPOVM.CompanyList = selectListItem;

            selectListItem = new List<SelectListItem>();
            List<TaxTypeViewModel> TaxTypeList = Mapper.Map<List<TaxType>, List<TaxTypeViewModel>>(_taxTypeBusiness.GetAllTaxTypes());
            foreach (TaxTypeViewModel TT in TaxTypeList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = TT.Description,
                    Value = TT.Code.ToString(),
                    Selected = false
                });
            }

            SPOVM.TaxTypeList = selectListItem;

            selectListItem = new List<SelectListItem>();
            List<POStatusesViewModel> POStatusesList = Mapper.Map<List<POStatuses>, List<POStatusesViewModel>>(_commonBusiness.GetAllPOStatuses());
            foreach (POStatusesViewModel TT in POStatusesList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = TT.Description,
                    Value = TT.Code.ToString(),
                    Selected = false
                });
            }
            SPOVM.POStatusList = selectListItem;
            return View(SPOVM);
        }


        #region GetAllSupplierPurchaseOrders
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "CustomerOrder", Mode = "R")]
        public string GetAllSupplierPurchaseOrders(string filter)
        {
            try
            {
                List<SupplierOrderViewModel> SPOVMList = Mapper.Map<List<SupplierOrder>, List<SupplierOrderViewModel>>(_supplierBusiness.GetAllSupplierPurchaseOrders());

                int openCount = SPOVMList == null ? 0 : SPOVMList.Where(Q => Q.POStatus == "OPN").Select(T => T.ID).Count();
                int inProgressCount = SPOVMList == null ? 0 : SPOVMList.Where(Q => Q.POStatus == "PGS").Select(T => T.ID).Count();
                int closedCount = SPOVMList == null ? 0 : SPOVMList.Where(Q => Q.POStatus == "CSD").Select(T => T.ID).Count();

                if (filter != null)
                {
                    SPOVMList = SPOVMList.Where(Q => Q.POStatus == filter).ToList();
                }
                return JsonConvert.SerializeObject(new { Result = "OK", Records = SPOVMList, Open = openCount, InProgress = inProgressCount, Closed = closedCount });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetAllSupplierPurchaseOrders

        #region GetPurchaseOrderByID
        [HttpGet]
      //  [AuthSecurityFilter(ProjectObject = "CustomerOrder", Mode = "R")]
        public string GetPurchaseOrderByID(string ID)
        {
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID required");
                }
                SupplierOrderViewModel customerPOViewModel = Mapper.Map<SupplierOrder, SupplierOrderViewModel>(_supplierBusiness.GetSupplierPurchaseOrderByID(Guid.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = customerPOViewModel });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetPurchaseOrderByID

        #region InsertUpdatePurchaseOrder
        [HttpPost]
        [AuthSecurityFilter(ProjectObject = "CustomerOrder", Mode = "W")]
      //  [ValidateAntiForgeryToken]
        public string InsertUpdatePurchaseOrder(SupplierOrderViewModel SPOViewModel)
        {
            try
            {
                object result = null;
                //if (ModelState.IsValid)
                //{
                    AppUA _appUA = Session["AppUAOffice"] as AppUA;
                    SPOViewModel.commonObj = new CommonViewModel();
                    SPOViewModel.commonObj.CreatedBy = _appUA.UserName;
                    SPOViewModel.commonObj.CreatedDate = _appUA.DateTime;
                    SPOViewModel.commonObj.UpdatedBy = SPOViewModel.commonObj.CreatedBy;
                    SPOViewModel.commonObj.UpdatedDate = SPOViewModel.commonObj.CreatedDate;

                    switch (string.IsNullOrEmpty(SPOViewModel.ID.ToString()))
                    {
                        case true:
                            result = _supplierBusiness.InsertPurchaseOrder(Mapper.Map<SupplierOrderViewModel, SupplierOrder>(SPOViewModel));
                            break;
                        case false:
                            result = _supplierBusiness.UpdatePurchaseOrder(Mapper.Map<SupplierOrderViewModel, SupplierOrder>(SPOViewModel));
                            break;
                    }

                    return JsonConvert.SerializeObject(new { Result = "OK", Record = result });
                //}
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
        #endregion InsertUpdatePurchaseOrder

        #region  DeletePurchaseOrder
        [HttpGet]
      //  [AuthSecurityFilter(ProjectObject = "CustomerOrder", Mode = "D")]
        public string DeletePurchaseOrder(string ID)
        {
            object result = null;
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID Missing");
                }
                result = _supplierBusiness.DeletePurchaseOrder(Guid.Parse(ID));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = result });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion DeletePurchaseOrder

        #region GetPurchaseOrderDetailTable
        [HttpGet]
        //  [AuthSecurityFilter(ProjectObject = "CustomerOrder", Mode = "R")]
        public string GetPurchaseOrderDetailTable(string ID)
        {
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID required");
                }
                List<SupplierPODetailViewModel> SPOVMList = Mapper.Map<List<SupplierPODetail>, List<SupplierPODetailViewModel>>(_supplierBusiness.GetPurchaseOrderDetailTable(Guid.Parse(ID)));
                decimal GrossAmount = SPOVMList == null ? 0 : SPOVMList.Sum(Q => Q.Amount);

                return JsonConvert.SerializeObject(new { Result = "OK", Record = SPOVMList, GrossAmount= GrossAmount });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetPurchaseOrderDetailTable

        #region GetAllRequisitionHeaderForSupplierPO
        [HttpGet]
        //  [AuthSecurityFilter(ProjectObject = "CustomerOrder", Mode = "R")]
        public string GetAllRequisitionHeaderForSupplierPO()
        {
            try
            { 
                List<RequisitionViewModel> SPOVMList = Mapper.Map<List<Requisition>, List<RequisitionViewModel>>(_supplierBusiness.GetAllRequisitionHeaderForSupplierPO());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = SPOVMList});
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetAllRequisitionHeaderForSupplierPO


        #region GetRequisitionDetailsByIDs
        [HttpGet]
        //  [AuthSecurityFilter(ProjectObject = "CustomerOrder", Mode = "R")]
        public string GetRequisitionDetailsByIDs(string IDs)
        {
            try
            {
                List<RequisitionDetailViewModel> SPOVMList = Mapper.Map<List<RequisitionDetail>, List<RequisitionDetailViewModel>>(_supplierBusiness.GetRequisitionDetailsByIDs(IDs));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = SPOVMList });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetRequisitionDetailsByIDs

        #region ButtonStyling
        [HttpGet]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "List":
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Text = "Add";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    ToolboxViewModelObj.addbtn.Event = "AddNew();";

                    ToolboxViewModelObj.resetbtn.Visible = true;
                    ToolboxViewModelObj.resetbtn.Text = "Reset";
                    ToolboxViewModelObj.resetbtn.Title = "Reset";
                    ToolboxViewModelObj.resetbtn.Event = "FilterReset();";

                   
                    break;
                case "Edit":
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Text = "Add";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    ToolboxViewModelObj.addbtn.Event = "AddNew();"; 

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Text = "Save";
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    ToolboxViewModelObj.savebtn.Event = "Save();";

                    ToolboxViewModelObj.resetbtn.Visible = true;
                    ToolboxViewModelObj.resetbtn.Text = "Reset";
                    ToolboxViewModelObj.resetbtn.Title = "Reset";
                    ToolboxViewModelObj.resetbtn.Event = "Reset();";

                    ToolboxViewModelObj.CloseBtn.Visible = true;
                    ToolboxViewModelObj.CloseBtn.Text = "Close";
                    ToolboxViewModelObj.CloseBtn.Title = "Close";
                    ToolboxViewModelObj.CloseBtn.Event = "closeNav();";

                    ToolboxViewModelObj.EmailBtn.Visible = true;
                    ToolboxViewModelObj.EmailBtn.Text = "Mail";
                    ToolboxViewModelObj.EmailBtn.Title = "Mail";
                    ToolboxViewModelObj.EmailBtn.Event = "PreviewMail()";

                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Text = "Delete";
                    ToolboxViewModelObj.deletebtn.Title = "Delete";
                    ToolboxViewModelObj.deletebtn.Event = "DeleteSupplierPO()";

                    break;
                case "Add":

                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Text = "Add";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    ToolboxViewModelObj.addbtn.Event = "AddNew();";

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Text = "Save";
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    ToolboxViewModelObj.savebtn.Event = "Save();";

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