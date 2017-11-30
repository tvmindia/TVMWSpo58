using AutoMapper;
using Newtonsoft.Json;
using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.UserInterface.Models;
using SPOffice.UserInterface.SecurityFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UserInterface.Models;

namespace SPOffice.UserInterface.Controllers
{
    public class CustomerOrderController : Controller
    {

        AppConst c = new AppConst();
        ICustomerBusiness _customerBusiness;
        ICompanyBusiness _companyBusiness;
        ITaxTypeBusiness _taxTypeBusiness;
        ICommonBusiness _commonBusiness;
        public CustomerOrderController(ICustomerBusiness customerBusiness, ICompanyBusiness companyBusiness,
            ITaxTypeBusiness taxTypeBusiness,ICommonBusiness commonBusiness)
        {
            _customerBusiness = customerBusiness;
            _companyBusiness = companyBusiness;
            _taxTypeBusiness = taxTypeBusiness;
            _commonBusiness = commonBusiness;
        }
        // GET: CustomerOrder
        [AuthSecurityFilter(ProjectObject = "CustomerOrder", Mode = "R")]
        public ActionResult Index(string id)
        {
            ViewBag.filter = id;
            ViewBag.value = id;
            CustomerPOViewModel customerPOlVM = new CustomerPOViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            List<CustomerViewModel> CustList = Mapper.Map<List<Customer>, List<CustomerViewModel>>(_customerBusiness.GetAllCustomers());
            foreach (CustomerViewModel Cust in CustList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = Cust.CompanyName,
                    Value = Cust.ID.ToString(),
                    Selected = false
                });
            }
            customerPOlVM.CustomerList = selectListItem;

            customerPOlVM.CompanyList = new List<SelectListItem>();
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
            customerPOlVM.CompanyList = selectListItem;

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

            customerPOlVM.TaxTypeList = selectListItem;
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
            customerPOlVM.POStatusList = selectListItem;
            return View(customerPOlVM);
        }


        #region GetAllPurchaseOrders
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "CustomerOrder", Mode = "R")]
        public string GetAllPurchaseOrders(string filter)
        {
            try
            {
                List<CustomerPOViewModel> customerPOViewModelList = Mapper.Map<List<CustomerPO>, List<CustomerPOViewModel>>(_customerBusiness.GetAllCustomerPurchaseOrders());
              
                int openCount = customerPOViewModelList == null ? 0 : customerPOViewModelList.Where(Q => Q.purchaseOrderStatus.Code == "OPN").Select(T => T.ID).Count();
                int inProgressCount = customerPOViewModelList == null ? 0 : customerPOViewModelList.Where(Q => Q.purchaseOrderStatus.Code == "PGS").Select(T => T.ID).Count();
                int closedCount = customerPOViewModelList == null ? 0 : customerPOViewModelList.Where(Q => Q.purchaseOrderStatus.Code == "CSD").Select(T => T.ID).Count();

                if (filter != null)
                {
                    customerPOViewModelList = customerPOViewModelList.Where(Q => Q.purchaseOrderStatus.Code == filter).ToList();
                }
                return JsonConvert.SerializeObject(new { Result = "OK", Records = customerPOViewModelList, Open = openCount, InProgress = inProgressCount, Closed = closedCount });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetAllPurchaseOrders
        #region GetPurchaseOrderByID
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "CustomerOrder", Mode = "R")]
        public string GetPurchaseOrderByID(string ID)
        {
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID required");
                }
                CustomerPOViewModel customerPOViewModel = Mapper.Map<CustomerPO, CustomerPOViewModel>(_customerBusiness.GetCustomerPurchaseOrderByID(Guid.Parse(ID)));
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
        [ValidateAntiForgeryToken]
        public string InsertUpdatePurchaseOrder(CustomerPOViewModel customerPOViewModel)
        {
            try
            {
                object result = null;
                if (ModelState.IsValid)
                {
                    AppUA _appUA = Session["AppUAOffice"] as AppUA;
                    customerPOViewModel.commonObj = new CommonViewModel();
                    customerPOViewModel.commonObj.CreatedBy =_appUA.UserName;
                    customerPOViewModel.commonObj.CreatedDate =_appUA.DateTime;
                    customerPOViewModel.commonObj.UpdatedBy = customerPOViewModel.commonObj.CreatedBy;
                    customerPOViewModel.commonObj.UpdatedDate = customerPOViewModel.commonObj.CreatedDate;
                   
                    switch (string.IsNullOrEmpty(customerPOViewModel.ID.ToString()))
                    {
                        case true:
                            result = _customerBusiness.InsertPurchaseOrder(Mapper.Map<CustomerPOViewModel, CustomerPO>(customerPOViewModel));
                            break;
                        case false:
                            result = _customerBusiness.UpdatePurchaseOrder(Mapper.Map<CustomerPOViewModel, CustomerPO>(customerPOViewModel));
                            break;
                    }

                    return JsonConvert.SerializeObject(new { Result = "OK", Record = result });
                }
                else
                {
                    List<string> modelErrors = new List<string>();
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var modelError in modelState.Errors)
                        {
                            modelErrors.Add(modelError.ErrorMessage);
                        }
                    }
                    return JsonConvert.SerializeObject(new { Result = "VALIDATION", Message = string.Join(",", modelErrors) });
                }
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
        [AuthSecurityFilter(ProjectObject = "CustomerOrder", Mode = "D")]
        public string DeletePurchaseOrder(string ID)
        {
            object result = null;
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID Missing");
                }
                result = _customerBusiness.DeletePurchaseOrder(Guid.Parse(ID));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = result });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion DeletePurchaseOrder


        #region GetCustomerDetailsByID
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "CustomerOrder", Mode = "R")]
        public string GetCustomerDetailsByID(string ID)
        {
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("CustomerID required");
                }
                CustomerViewModel customerViewModel = Mapper.Map<Customer, CustomerViewModel>(_customerBusiness.GetCustomerDetailsByID(Guid.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = customerViewModel });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetCustomerDetailsByID

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

                    //ToolboxViewModelObj.backbtn.Visible = true;
                    //ToolboxViewModelObj.backbtn.Disable = true;
                    //ToolboxViewModelObj.backbtn.Text = "Back";
                    //ToolboxViewModelObj.backbtn.DisableReason = "Not applicable";
                    //ToolboxViewModelObj.backbtn.Event = "Back();";  
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

                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Text = "Delete";
                    ToolboxViewModelObj.deletebtn.Title = "Delete";
                    ToolboxViewModelObj.deletebtn.Event = "DeleteCustomerPO()";

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