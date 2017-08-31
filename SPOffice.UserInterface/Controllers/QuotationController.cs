﻿using AutoMapper;
using Newtonsoft.Json;
using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.UserInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace SPOffice.UserInterface.Controllers
{
    public class QuotationController : Controller
    {
        AppConst c = new AppConst();
        IQuotationBusiness _quotationBusiness;
        ICustomerBusiness _customerBusiness;
        ICompanyBusiness _companyBusiness;
        IEmployeeBusiness _employeeBusiness;
        ITaxTypeBusiness _taxTypeBusiness;
        IProductBusiness _productBusiness;
        public QuotationController(IQuotationBusiness quotationBusiness, ICustomerBusiness customerBusiness, ICompanyBusiness companyBusiness, IEmployeeBusiness employeeBusiness, ITaxTypeBusiness taxTypeBusiness, IProductBusiness productBusiness)
        {
            _quotationBusiness = quotationBusiness;
            _customerBusiness = customerBusiness;
            _companyBusiness = companyBusiness;
            _employeeBusiness = employeeBusiness;
            _taxTypeBusiness = taxTypeBusiness;
            _productBusiness = productBusiness;
        }
        // GET: Quotation
        public ActionResult Index()
        {
            QuoteHeaderViewModel quoteHeaderVM = new QuoteHeaderViewModel();
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
            quoteHeaderVM.CustomerList = selectListItem;

            quoteHeaderVM.CompanyList = new List<SelectListItem>();
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
            quoteHeaderVM.CompanyList = selectListItem;


            quoteHeaderVM.SalesPersonList = new List<SelectListItem>();
            selectListItem = new List<SelectListItem>();
            List<SalesPersonViewModel> EmployeeList = Mapper.Map<List<SalesPerson>, List<SalesPersonViewModel>>(_employeeBusiness.GetAllSalesPersons());
            
            foreach (SalesPersonViewModel SP in EmployeeList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = SP.Name,
                    Value = SP.ID.ToString(),
                    Selected = false
                });
            }

            quoteHeaderVM.SalesPersonList = selectListItem;



            selectListItem = new List<SelectListItem>();
            List<QuoteStageViewModel> QuoteStageList = Mapper.Map<List<QuoteStage>, List<QuoteStageViewModel>>(_quotationBusiness.GetAllQuoteStages());
            foreach (QuoteStageViewModel QS in QuoteStageList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = QS.Description,
                    Value = QS.Code.ToString(),
                    Selected = false
                });
             
            }
            //default draft selection in drop down
            var _selected = selectListItem.Where(x => x.Value == "DFT").FirstOrDefault();
            if(_selected!=null)
            {
                _selected.Selected = true;
            }
             

            quoteHeaderVM.QuoteStageList = selectListItem;


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

            quoteHeaderVM.TaxTypeList = selectListItem;
            return View(quoteHeaderVM);
        }
        #region GetAllQuotations
        [HttpGet]
        public string GetAllQuotations()
        {
            try
            {
                List<QuoteHeaderViewModel> quoteHeaderViewModelList = Mapper.Map<List<QuoteHeader>, List<QuoteHeaderViewModel>>(_quotationBusiness.GetAllQuotations());
                int draftCount = quoteHeaderViewModelList == null ? 0 : quoteHeaderViewModelList.Where(Q=>Q.Stage== "DFT").Select(T => T.ID).Count();
                int deliveredCount = quoteHeaderViewModelList == null ? 0 : quoteHeaderViewModelList.Where(Q => Q.Stage == "DVD").Select(T => T.ID).Count();
                int inProgressCount = quoteHeaderViewModelList == null ? 0 : quoteHeaderViewModelList.Where(Q => Q.Stage == "NGT").Select(T => T.ID).Count();
                int closedCount = quoteHeaderViewModelList == null ? 0 : quoteHeaderViewModelList.Where(Q => Q.Stage == "CLT" || Q.Stage== "CWN").Select(T => T.ID).Count();
                return JsonConvert.SerializeObject(new { Result = "OK", Records = quoteHeaderViewModelList,Draft= draftCount,Delivered= deliveredCount,InProgress= inProgressCount,Closed=closedCount });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetAllQuotations
        #region GetQuationDetailsByID
        [HttpGet]
        public string GetQuationDetailsByID(string ID)
        {
            try
            {
                if(string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID required");
                }
                 QuoteHeaderViewModel quoteHeaderViewModel = Mapper.Map<QuoteHeader,QuoteHeaderViewModel>(_quotationBusiness.GetQuationDetailsByID(Guid.Parse(ID)));
                 return JsonConvert.SerializeObject(new { Result = "OK", Record = quoteHeaderViewModel });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetQuationDetailsByID

        #region InsertUpdateQuotaion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string InsertUpdateQuotaion(QuoteHeaderViewModel quoteHeaderVM)
        {
            try
            {
                object result = null;
                if (ModelState.IsValid)
                {
                    //AppUA _appUA = Session["AppUA"] as AppUA;
                    quoteHeaderVM.commonObj = new CommonViewModel();
                    quoteHeaderVM.commonObj.CreatedBy = "Albert Thomson";//_appUA.UserName;
                    quoteHeaderVM.commonObj.CreatedDate = DateTime.Now;//_appUA.DateTime;
                    quoteHeaderVM.commonObj.UpdatedBy = quoteHeaderVM.commonObj.CreatedBy;
                    quoteHeaderVM.commonObj.UpdatedDate = quoteHeaderVM.commonObj.CreatedDate;
                    //Deserialize items
                    object ResultFromJS = JsonConvert.DeserializeObject(quoteHeaderVM.DetailJSON);
                    string ReadableFormat = JsonConvert.SerializeObject(ResultFromJS);
                    quoteHeaderVM.quoteItemList = JsonConvert.DeserializeObject<List<QuoteItemViewModel>>(ReadableFormat);
                    switch (string.IsNullOrEmpty(quoteHeaderVM.ID.ToString()))
                    {
                        case true:
                            result = _quotationBusiness.InsertQuotation(Mapper.Map<QuoteHeaderViewModel, QuoteHeader>(quoteHeaderVM));
                            break;
                        case false:
                            result = _quotationBusiness.UpdateQuotation(Mapper.Map<QuoteHeaderViewModel, QuoteHeader>(quoteHeaderVM));
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
            catch(Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }



        }
        #endregion InsertUpdateQuotaion

        #region GetQuationDetailsByID
        [HttpGet]
        public string GetQuateItemsByQuateHeadID(string ID)
        {
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID required");
                }
                List<QuoteItemViewModel> quoteItemViewModelList = Mapper.Map<List<QuoteItem>, List<QuoteItemViewModel>>(_quotationBusiness.GetAllQuoteItems(Guid.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = quoteItemViewModelList });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetQuationDetailsByID

        #region  DeleteItemByID
        [HttpGet]
        public string DeleteItemByID(string ID)
        {
            object result = null;
            try
            {
                if(string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID Missing");
                }
                result=_quotationBusiness.DeleteQuoteItem(Guid.Parse(ID));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = result });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion DeleteItemByID

        #region GetAllProductCodes
        [HttpGet]
        public string GetAllProductCodes()
        {
            try
            {
                List<ProductViewModel> productViewModelList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetAllProducts());
                if(productViewModelList!=null)
                {
                 productViewModelList = productViewModelList.Select(P => new ProductViewModel { ID = P.ID, Code = P.Code,Description=P.Description,Rate=P.Rate }).OrderBy(x => x.Code).ToList();
                }
                return JsonConvert.SerializeObject(new { Result = "OK", Records = productViewModelList });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetAllProductCodes

        #region GetAllUnitCodes
        [HttpGet]
        public string GetAllUnitCodes()
        {
            try
            {
                List<UnitViewModel> unitViewModelList = Mapper.Map<List<Unit>, List<UnitViewModel>>(_productBusiness.GetAllUnits());
                if (unitViewModelList != null)
                {
                    unitViewModelList = unitViewModelList.Select(P => new UnitViewModel { Code = P.Code, Description = P.Description }).OrderBy(x => x.Code).ToList();
                }
                return JsonConvert.SerializeObject(new { Result = "OK", Records = unitViewModelList });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetAllUnitCodes
        #region GetTaxRate
       
        [HttpGet]
        public string GetTaxRate(string Code)
        {
            try
            {
                TaxTypeViewModel taxTypesObj = Mapper.Map<TaxType, TaxTypeViewModel>(_taxTypeBusiness.GetTaxTypeDetailsByCode(Code));
                decimal Rate = taxTypesObj.Rate;
                return JsonConvert.SerializeObject(new { Result = "OK", Records = Rate });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetTaxRate

        #region GetMailPreview
        [HttpGet]
        public ActionResult GetMailPreview(string ID)
        {
            QuoteMailPreviewViewModel quoteMailPreviewViewModel = null;
            try
            {
                if(string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID is missing");
                }
                quoteMailPreviewViewModel = new QuoteMailPreviewViewModel();
                quoteMailPreviewViewModel.quoteHeaderViewModel = Mapper.Map<QuoteHeader, QuoteHeaderViewModel>(_quotationBusiness.GetMailPreview(Guid.Parse(ID)));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return PartialView("_QuoteMailPreview", quoteMailPreviewViewModel);
        }
        #endregion GetMailPreview

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
                    ToolboxViewModelObj.savebtn.Event = "saveInvoices();";

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

                    break;
                case "Add":

                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Text = "Add";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    ToolboxViewModelObj.addbtn.Event = "AddNew();";

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Text = "Save";
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    ToolboxViewModelObj.savebtn.Event = " saveInvoices();";

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