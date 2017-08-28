using AutoMapper;
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
        public QuotationController(IQuotationBusiness quotationBusiness, ICustomerBusiness customerBusiness, ICompanyBusiness companyBusiness, IEmployeeBusiness employeeBusiness, ITaxTypeBusiness taxTypeBusiness)
        {
            _quotationBusiness = quotationBusiness;
            _customerBusiness = customerBusiness;
            _companyBusiness = companyBusiness;
            _employeeBusiness = employeeBusiness;
            _taxTypeBusiness = taxTypeBusiness;
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

                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Text = "Delete";
                    ToolboxViewModelObj.deletebtn.Title = "Delete";
                    ToolboxViewModelObj.deletebtn.Event = "DeleteInvoices();";

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

                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Disable = true;
                    ToolboxViewModelObj.deletebtn.Text = "Delete";
                    ToolboxViewModelObj.deletebtn.Title = "Delete";
                    ToolboxViewModelObj.deletebtn.Event = "DeleteInvoices();";
                    break;
                default:
                    return Content("Nochange");
            }
            return PartialView("ToolboxView", ToolboxViewModelObj);
        }

        #endregion
    }
}