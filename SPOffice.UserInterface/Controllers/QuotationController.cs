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
using System.Net;
using System.Net.Mail;

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
        [AuthSecurityFilter(ProjectObject = "Quotation", Mode = "R")]
        public ActionResult Index(string id)
        {
            if (id == "Draft" || id == "Negotiation" || id == "Converted" || id == "Lost") {
                ViewBag.filter = id;
            }

            else if(id=="Quotation")
            {
                ViewBag.filter = id;
            }
            else if (id != "")
            {
                ViewBag.value = id;
            }
          
           
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

            selectListItem = new List<SelectListItem>();
            List<ProductViewModel> productViewModelList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetAllProducts());
            foreach (ProductViewModel PVML in productViewModelList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = PVML.Name+'-'+ PVML.Code+'-'+ PVML.OldCode,
                    Value = PVML.ID.ToString(),
                    Selected = false
                });
            }
            quoteHeaderVM.quoteItemListObj = new QuoteItemViewModel();
            quoteHeaderVM.quoteItemListObj.quoteItemList = selectListItem;


            return View(quoteHeaderVM);
        }
        #region GetAllQuotations
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Quotation", Mode = "R")]
        public string GetAllQuotations(string filter)
        {
            try
            {
                List<QuoteHeaderViewModel> quoteHeaderViewModelList = Mapper.Map<List<QuoteHeader>, List<QuoteHeaderViewModel>>(_quotationBusiness.GetAllQuotations());
                int draftCount = quoteHeaderViewModelList == null ? 0 : quoteHeaderViewModelList.Where(Q=>Q.Stage== "DFT").Select(T => T.ID).Count();
                int convertedCount = quoteHeaderViewModelList == null ? 0 : quoteHeaderViewModelList.Where(Q => Q.Stage == "CVD").Select(T => T.ID).Count();
                int negotiationCount = quoteHeaderViewModelList == null ? 0 : quoteHeaderViewModelList.Where(Q => Q.Stage == "NGT").Select(T => T.ID).Count();
                int lostCount = quoteHeaderViewModelList == null ? 0 : quoteHeaderViewModelList.Where(Q => Q.Stage == "LST").Select(T => T.ID).Count();


                if (filter != null)
                {
                    string[] filterCodes = filter.Split(',');
                    if(filterCodes.Length>1)
                    {
                        quoteHeaderViewModelList = quoteHeaderViewModelList.Where(Q => Q.Stage == filterCodes[1] || Q.Stage==filterCodes[0]).ToList();
                    }
                    else
                    {
                        quoteHeaderViewModelList = quoteHeaderViewModelList.Where(Q => Q.Stage == filter).ToList();
                    }                
                    
                }
                return JsonConvert.SerializeObject(new { Result = "OK", Records = quoteHeaderViewModelList,Draft= draftCount, Converted= convertedCount, Negotiation = negotiationCount, Lost= lostCount});
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
        [AuthSecurityFilter(ProjectObject = "Quotation", Mode = "R")]
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
        [AuthSecurityFilter(ProjectObject = "Quotation", Mode = "W")]
        public string InsertUpdateQuotaion(QuoteHeaderViewModel quoteHeaderVM)
        {
            try
            {
                object result = null;
                //if (ModelState.IsValid)
                //{
                    AppUA _appUA = Session["AppUAOffice"] as AppUA;
                    quoteHeaderVM.commonObj = new CommonViewModel();
                    quoteHeaderVM.commonObj.CreatedBy = _appUA.UserName;
                    quoteHeaderVM.commonObj.CreatedDate =_appUA.DateTime;
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
            catch(Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }



        }
        #endregion InsertUpdateQuotaion



        #region GetQuateItemsByQuateHeadID
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Quotation", Mode = "R")]
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
        #endregion GetQuateItemsByQuateHeadID

        #region  DeleteItemByID
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Quotation", Mode = "D")]
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
                return JsonConvert.SerializeObject(new { Result = "OK", Record = result,Message = c.DeleteSuccess });
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
        [AuthSecurityFilter(ProjectObject = "Quotation", Mode = "R")]
        public string GetAllProductCodes()
        {
            try
            {
                List<ProductViewModel> productViewModelList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetAllProducts());
                if(productViewModelList!=null)
                {
                 productViewModelList = productViewModelList.Select(P => new ProductViewModel { ID = P.ID, Code = P.Code,Description=P.Description,Rate=P.Rate }).OrderBy(x => x.Code).ToList();
                }
                return JsonConvert.SerializeObject(new { Result = "OK", Records = productViewModelList,Message =c.DeleteSuccess });
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
        [AuthSecurityFilter(ProjectObject = "Quotation", Mode = "R")]
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
        [AuthSecurityFilter(ProjectObject = "Quotation", Mode = "R")]
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
        [AuthSecurityFilter(ProjectObject = "Quotation", Mode = "R")]
        public ActionResult GetMailPreview(string ID)
        {
            QuoteMailPreviewViewModel quoteMailPreviewViewModel = null;
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID is missing");
                }
                quoteMailPreviewViewModel = new QuoteMailPreviewViewModel();
                quoteMailPreviewViewModel.quoteHeaderViewModel = Mapper.Map<QuoteHeader, QuoteHeaderViewModel>(_quotationBusiness.GetMailPreview(Guid.Parse(ID)));
                if (quoteMailPreviewViewModel.quoteHeaderViewModel!=null)
                {
                    if (quoteMailPreviewViewModel.quoteHeaderViewModel.QuoteBodyHead != null)
                        quoteMailPreviewViewModel.quoteHeaderViewModel.QuoteBodyHead = quoteMailPreviewViewModel.quoteHeaderViewModel.QuoteBodyHead.Replace(Environment.NewLine,"<br/>");
                    if (quoteMailPreviewViewModel.quoteHeaderViewModel.QuoteBodyFoot != null)
                        quoteMailPreviewViewModel.quoteHeaderViewModel.QuoteBodyFoot = quoteMailPreviewViewModel.quoteHeaderViewModel.QuoteBodyFoot.Replace(Environment.NewLine,"<br/>");

                    quoteMailPreviewViewModel.quoteHeaderViewModel.company.BillingAddress = quoteMailPreviewViewModel.quoteHeaderViewModel.company.BillingAddress.Replace(Environment.NewLine, "<br/>");
                    quoteMailPreviewViewModel.quoteHeaderViewModel.SentToAddress = quoteMailPreviewViewModel.quoteHeaderViewModel.SentToAddress.Replace(Environment.NewLine, "<br/>");
                                      
                    quoteMailPreviewViewModel.quoteHeaderViewModel.quoteItemList = quoteMailPreviewViewModel.quoteHeaderViewModel.quoteItemList != null ? quoteMailPreviewViewModel.quoteHeaderViewModel.quoteItemList.Select(QI => {return QI; }).ToList() : null;

                    ViewBag.path = "http://"+HttpContext.Request.Url.Authority+quoteMailPreviewViewModel.quoteHeaderViewModel.quoteItemList[0].company.LogoURL;

                    // quoteMailPreviewViewModel.quoteHeaderViewModel.quoteItemList = quoteMailPreviewViewModel.quoteHeaderViewModel.quoteItemList != null ? quoteMailPreviewViewModel.quoteHeaderViewModel.quoteItemList.Select(QI => { QI.Amount = decimal.Round(decimal.Multiply((decimal)QI.Rate, (decimal)QI.Quantity),2); return QI; }).ToList() : null;
                    //if(quoteMailPreviewViewModel.quoteHeaderViewModel.quoteItemList!=null)
                    //{
                    //    quoteMailPreviewViewModel.quoteHeaderViewModel.GrossAmount = (decimal)quoteMailPreviewViewModel.quoteHeaderViewModel.quoteItemList.Sum(q => q.Amount);
                    //    quoteMailPreviewViewModel.quoteHeaderViewModel.NetTaxableAmount = quoteMailPreviewViewModel.quoteHeaderViewModel.GrossAmount - quoteMailPreviewViewModel.quoteHeaderViewModel.Discount;
                    //    quoteMailPreviewViewModel.quoteHeaderViewModel.TotalAmount = quoteMailPreviewViewModel.quoteHeaderViewModel.NetTaxableAmount + quoteMailPreviewViewModel.quoteHeaderViewModel.TaxAmount;
                    //}


                }
             
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return PartialView("_QuoteMailPreview", quoteMailPreviewViewModel);
        }
        #endregion GetMailPreview

        #region SendQuoteMail

        [HttpPost,ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [AuthSecurityFilter(ProjectObject = "Quotation", Mode = "R")]
        public async Task<string> SendQuoteMail(QuoteHeaderViewModel quoteHeaderVM)
        {
            try
            {
                object result = null;
                if (!string.IsNullOrEmpty(quoteHeaderVM.ID.ToString()))
                {
                    AppUA _appUA = Session["AppUAOffice"] as AppUA;
                    quoteHeaderVM.commonObj = new CommonViewModel();
                    quoteHeaderVM.commonObj.CreatedBy = _appUA.UserName;
                    quoteHeaderVM.commonObj.CreatedDate = _appUA.DateTime;
                    quoteHeaderVM.commonObj.UpdatedBy = quoteHeaderVM.commonObj.CreatedBy;
                    quoteHeaderVM.commonObj.UpdatedDate = quoteHeaderVM.commonObj.CreatedDate;
                    
                    bool sendsuccess = await _quotationBusiness.QuoteEmailPush(Mapper.Map<QuoteHeaderViewModel, QuoteHeader>(quoteHeaderVM));
                    if (sendsuccess)
                    {
                        //1 is meant for mail sent successfully
                        quoteHeaderVM.EmailSentYN = sendsuccess.ToString();
                        result = _quotationBusiness.UpdateQuoteMailStatus(Mapper.Map<QuoteHeaderViewModel, QuoteHeader>(quoteHeaderVM));
                    return JsonConvert.SerializeObject(new { Result = "OK",MailResult= sendsuccess ,Message = c.MailSuccess});
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { Result = "ERROR", MailResult = sendsuccess, Message = c.MailFailure });

                    }
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
        #endregion SendQuoteMail


        #region GetCustomerDetailsByID
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Quotation", Mode = "R")]
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


        #region DeleteQuotation
        [AuthSecurityFilter(ProjectObject = "Quotation", Mode = "D")]
        public string DeleteQuotation(string ID)
        {
            object result = null;
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("ID Missing");
                }
                result = _quotationBusiness.DeleteQuotation(Guid.Parse(ID));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = result, DeleteQuote = c.DeleteSuccess });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }

        #endregion DeleteQuotation




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
                    ToolboxViewModelObj.savebtn.Event = "saveInvoices();";


                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Text = "Delete";
                    ToolboxViewModelObj.deletebtn.Title = "Delete";
                    ToolboxViewModelObj.deletebtn.Event = "DeleteClick();";

                    ToolboxViewModelObj.resetbtn.Visible = true;
                    ToolboxViewModelObj.resetbtn.Text = "Reset";
                    ToolboxViewModelObj.resetbtn.Title = "Reset";
                    ToolboxViewModelObj.resetbtn.Event = "Reset();";                   

                    ToolboxViewModelObj.EmailBtn.Visible = true;
                    ToolboxViewModelObj.EmailBtn.Text = "Mail";
                    ToolboxViewModelObj.EmailBtn.Title = "Mail";
                    ToolboxViewModelObj.EmailBtn.Event = "PreviewMail()";

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

                 
                    break;
                default:
                    return Content("Nochange");
            }
            return PartialView("ToolboxView", ToolboxViewModelObj);
        }

        #endregion


    }
}