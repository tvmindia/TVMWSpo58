using AutoMapper;
using Newtonsoft.Json;
using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.UserInterface.Models;
using SPOffice.UserInterface.SecurityFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace SPOffice.UserInterface.Controllers
{
    public class ProformaInvoiceController : Controller
    {
        AppConst c = new AppConst();
        IProformaInvoiceBusiness _proformaInvoiceBusiness;
        ICustomerBusiness _customerBusiness;
        ICompanyBusiness _companyBusiness;
        IEmployeeBusiness _employeeBusiness;
        ITaxTypeBusiness _taxTypeBusiness;
        IProductBusiness _productBusiness;
        public ProformaInvoiceController(IProformaInvoiceBusiness proformaInvoiceBusiness, ICustomerBusiness customerBusiness, ICompanyBusiness companyBusiness, IEmployeeBusiness employeeBusiness, ITaxTypeBusiness taxTypeBusiness, IProductBusiness productBusiness)
        {
            _proformaInvoiceBusiness = proformaInvoiceBusiness;
            _customerBusiness = customerBusiness;
            _companyBusiness = companyBusiness;
            _employeeBusiness = employeeBusiness;
            _taxTypeBusiness = taxTypeBusiness;
            _productBusiness = productBusiness;
        }


        #region GetAllProformaInvoice
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "ProformaInvoice", Mode = "R")]
        public string GetAllProformaInvoices()
        {
            try
            {
                List<ProformaHeaderViewModel> proformaHeaderViewModelList = Mapper.Map<List<ProformaHeader>, List<ProformaHeaderViewModel>>(_proformaInvoiceBusiness.GetAllProformaInvoices());
                
                return JsonConvert.SerializeObject(new { Result = "OK", Records = proformaHeaderViewModelList });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetAllProformaInvoice
        // GET: ProformaInvoice
        [AuthSecurityFilter(ProjectObject = "ProformaInvoice", Mode = "R")]
        public ActionResult Index()
        {
            ProformaHeaderViewModel proformaHeaderVM = new ProformaHeaderViewModel();
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
            proformaHeaderVM.CustomerList = selectListItem;

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

            proformaHeaderVM.TaxTypeList = selectListItem;

            proformaHeaderVM.CompanyList = new List<SelectListItem>();
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
            proformaHeaderVM.CompanyList = selectListItem;



            return View(proformaHeaderVM);
        }

        #region InsertUpdateProformaInvoice
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthSecurityFilter(ProjectObject = "ProformaInvoice", Mode = "W")]
        public string InsertUpdateProformaInvoices(ProformaHeaderViewModel proformaHeaderVM)
        {
            try
            {
                object result = null;
                if (ModelState.IsValid)
                {

                    AppUA _appUA = Session["AppUA"] as AppUA;
                    proformaHeaderVM.commonObj = new CommonViewModel();
                    proformaHeaderVM.commonObj.CreatedBy = _appUA.UserName;
                    proformaHeaderVM.commonObj.CreatedDate =_appUA.DateTime;
                    proformaHeaderVM.commonObj.UpdatedBy = proformaHeaderVM.commonObj.CreatedBy;
                    proformaHeaderVM.commonObj.UpdatedDate = proformaHeaderVM.commonObj.CreatedDate;
                    //Deserialize items
                    object ResultFromJS = JsonConvert.DeserializeObject(proformaHeaderVM.DetailJSON);
                    string ReadableFormat = JsonConvert.SerializeObject(ResultFromJS);
                    proformaHeaderVM.quoteItemList = JsonConvert.DeserializeObject<List<ProformaItemViewModel>>(ReadableFormat);
                    switch (Guid.Empty==Guid.Parse(proformaHeaderVM.ID.ToString()))
                    {
                        case true:
                            result = _proformaInvoiceBusiness.InsertProformaInvoices(Mapper.Map<ProformaHeaderViewModel, ProformaHeader>(proformaHeaderVM));
                            break;
                        case false:
                            result = _proformaInvoiceBusiness.UpdateProformaInvoices(Mapper.Map<ProformaHeaderViewModel, ProformaHeader>(proformaHeaderVM));
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
        #endregion InsertUpdateProformaInvoices


       


        #region GetAllProductCodes
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "ProformaInvoice", Mode = "R")]
        public string GetAllProductCodes()
        {
            try
            {
                List<ProductViewModel> productViewModelList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetAllProducts());
                if (productViewModelList != null)
                {
                    productViewModelList = productViewModelList.Select(P => new ProductViewModel { ID = P.ID, Code = P.Code, Description = P.Description, Rate = P.Rate }).OrderBy(x => x.Code).ToList();
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
        [AuthSecurityFilter(ProjectObject = "ProformaInvoice", Mode = "R")]
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
        [AuthSecurityFilter(ProjectObject = "ProformaInvoice", Mode = "R")]
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

        #region GetQuateItemsByQuateHeadID
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "ProformaInvoice", Mode = "R")]
        public string GetQuateItemsByQuateHeadID(string ID)
        {
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID required");
                }
                List<ProformaItemViewModel> proformaItemViewModelList = Mapper.Map<List<ProformaItem>, List<ProformaItemViewModel>>(_proformaInvoiceBusiness.GetAllQuoteItems(Guid.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = proformaItemViewModelList });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetQuateItemsByQuateHeadID


        #region GetProformaInvoiceDetailsByID
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "ProformaInvoice", Mode = "R")]
        public string GetProformaInvoiceDetailsByID(string ID)
        {
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID required");
                }
                ProformaHeaderViewModel proformaHeaderViewModel = Mapper.Map<ProformaHeader, ProformaHeaderViewModel>(_proformaInvoiceBusiness.GetQuationDetailsByID(Guid.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = proformaHeaderViewModel });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetProformaInvoiceDetailsByID


        #region  DeleteItemByID
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "ProformaInvoice", Mode = "D")]
        public string DeleteItemByID(string ID)
        {
            object result = null;
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID Missing");
                }
                result = _proformaInvoiceBusiness.DeleteQuoteItem(Guid.Parse(ID));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = result, Message = c.DeleteSuccess });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion DeleteItemByID


        #region GetMailPreview
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "ProformaInvoice", Mode = "R")]
        public ActionResult GetMailPreview(string ID)
        {
            ProformaMailPreviewViewModel proformaMailPreviewViewModel = null;
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID is missing");
                }
                proformaMailPreviewViewModel = new ProformaMailPreviewViewModel();
                proformaMailPreviewViewModel.proformaHeaderViewModel = Mapper.Map<ProformaHeader, ProformaHeaderViewModel>(_proformaInvoiceBusiness.GetMailPreview(Guid.Parse(ID)));
                if (proformaMailPreviewViewModel.proformaHeaderViewModel != null)
                {

                    proformaMailPreviewViewModel.proformaHeaderViewModel.quoteItemList = proformaMailPreviewViewModel.proformaHeaderViewModel.quoteItemList != null ? proformaMailPreviewViewModel.proformaHeaderViewModel.quoteItemList.Select(QI => { QI.Amount = decimal.Round(decimal.Multiply((decimal)QI.Rate, (decimal)QI.Quantity), 2); return QI; }).ToList() : null;
                    if (proformaMailPreviewViewModel.proformaHeaderViewModel.quoteItemList != null)
                    {
                        proformaMailPreviewViewModel.proformaHeaderViewModel.GrossAmount = (decimal)proformaMailPreviewViewModel.proformaHeaderViewModel.quoteItemList.Sum(q => q.Amount);
                        proformaMailPreviewViewModel.proformaHeaderViewModel.NetTaxableAmount = proformaMailPreviewViewModel.proformaHeaderViewModel.GrossAmount - proformaMailPreviewViewModel.proformaHeaderViewModel.Discount;
                        proformaMailPreviewViewModel.proformaHeaderViewModel.TotalAmount = proformaMailPreviewViewModel.proformaHeaderViewModel.NetTaxableAmount + proformaMailPreviewViewModel.proformaHeaderViewModel.TaxAmount;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView("_ProfomaMailPreview", proformaMailPreviewViewModel);
        }
        #endregion GetMailPreview

        #region SendProformaMail

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [AuthSecurityFilter(ProjectObject = "ProformaInvoice", Mode = "R")]
        public async Task<string> SendQuoteMail(ProformaHeaderViewModel proformaHeaderVM)
        {
            try
            {
                object result = null;
                if (!string.IsNullOrEmpty(proformaHeaderVM.ID.ToString()))
                {                   
                    proformaHeaderVM.commonObj = new CommonViewModel();
                    proformaHeaderVM.commonObj.CreatedBy = proformaHeaderVM.commonObj.CreatedBy;
                    proformaHeaderVM.commonObj.CreatedDate = DateTime.Now; ;//_appUA.DateTime;
                    proformaHeaderVM.commonObj.UpdatedBy = proformaHeaderVM.commonObj.CreatedBy;
                    proformaHeaderVM.commonObj.UpdatedDate = proformaHeaderVM.commonObj.CreatedDate;

                    bool sendsuccess = await _proformaInvoiceBusiness.QuoteEmailPush(Mapper.Map<ProformaHeaderViewModel, ProformaHeader>(proformaHeaderVM));
                    if (sendsuccess)
                    {
                        //1 is meant for mail sent successfully
                        proformaHeaderVM.EmailSentYN = sendsuccess.ToString();
                        result = _proformaInvoiceBusiness.UpdateQuoteMailStatus(Mapper.Map<ProformaHeaderViewModel, ProformaHeader>(proformaHeaderVM));
                    }
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = result, MailResult = sendsuccess,Message = c.MailSuccess });
                }
                else
                {

                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "ID is Missing" });
                }
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion SendProformaMail


        #region DeleteProformaInvoice()
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "ProformaInvoice", Mode = "D")]
        public string DeleteProformaInvoice(string ID)
        {
            object result = null;
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID Missing");
                }
                result = _proformaInvoiceBusiness.DeleteProformaInvoice(Guid.Parse(ID));
                return JsonConvert.SerializeObject(new { Result = "OK", Record =result, DeleteInvoice = c.DeleteSuccess});
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion DeleteProformaInvoice

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


                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Text = "Delete";
                    ToolboxViewModelObj.deletebtn.Title = "Delete";
                    ToolboxViewModelObj.deletebtn.Event = "DeleteClick();";


                    ToolboxViewModelObj.CloseBtn.Visible = true;
                    ToolboxViewModelObj.CloseBtn.Text = "Close";
                    ToolboxViewModelObj.CloseBtn.Title = "Close";
                    ToolboxViewModelObj.CloseBtn.Event = "closeNav();";

                    ToolboxViewModelObj.EmailBtn.Visible = true;
                    ToolboxViewModelObj.EmailBtn.Text = "Mail";
                    ToolboxViewModelObj.EmailBtn.Title = "Mail";
                    ToolboxViewModelObj.EmailBtn.Event = "PreviewMail();";

                   
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