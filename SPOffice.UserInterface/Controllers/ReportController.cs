using AutoMapper;
using Newtonsoft.Json;
using SAMTool.BusinessServices.Contracts;
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
    public class ReportController : Controller
    {
        IReportBusiness _reportBusiness;
        ICourierBusiness _courierBusiness;
        IEnquiryStatusBusiness _enquiryStatusBusiness;
        ICompanyBusiness _companyBusiness;
        IQuotationBusiness _quotationBusiness;
        ICustomerBusiness _customerBusiness;
        IUserBusiness _userBusiness;
        IProductBusiness _productBusiness;
        public ReportController(IReportBusiness reportBusiness, ICourierBusiness courierBusiness, IEnquiryStatusBusiness enquiryStatusBusiness, ICompanyBusiness companyBusiness, IQuotationBusiness quotationBusiness, ICustomerBusiness customerBusiness, IUserBusiness userBusiness, IProductBusiness productBusiness)
        {
            _reportBusiness = reportBusiness;
            _enquiryStatusBusiness = enquiryStatusBusiness;
            _courierBusiness = courierBusiness;
            _companyBusiness = companyBusiness;
            _quotationBusiness = quotationBusiness;
            _customerBusiness = customerBusiness;
            _userBusiness = userBusiness;
            _productBusiness=productBusiness;


        }


        // GET: Report
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Report", Mode = "R")]
        public ActionResult Index()
        {
            AppUA _appUA = Session["AppUAOffice"] as AppUA;
            List<ReportViewModel> ReportList = Mapper.Map<List<Reports>, List<ReportViewModel>>(_reportBusiness.GetAllSysReports(_appUA));
            ReportList = ReportList != null ? ReportList.OrderBy(s => s.GroupOrder).ToList() : null;
            return View(ReportList);
        }


        /// <summary>
        /// To Get Enquiry Details in Report
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "EnquiryReport", Mode = "R")]
        public ActionResult EnquiryReport()
        {
            AppUA _appUA = Session["AppUAOffice"] as AppUA;
            DateTime dt = _appUA.DateTime;
            ViewBag.fromdate = dt.AddDays(-90).ToString("dd-MMM-yyyy");
            ViewBag.todate = dt.ToString("dd-MMM-yyyy");
            EnquiryReportViewModel ERVM = new EnquiryReportViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();


            List<EnquiryStatusViewModel> enquiryStatusList = Mapper.Map<List<EnquiryStatus>, List<EnquiryStatusViewModel>>(_enquiryStatusBusiness.GetAllEnquiryStatusList()).ToList();
            if (enquiryStatusList != null)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = "All",
                    Value = "ALL",
                    Selected = false
                });

                foreach (EnquiryStatusViewModel esvm in enquiryStatusList)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = esvm.Status,
                        Value = esvm.StatusCode.ToString(),
                        Selected = false
                    });
                }
            }
            ERVM.enquiryStatusObj = new EnquiryStatusViewModel();
            ERVM.enquiryStatusObj.EnquiryStatusList = selectListItem;

            selectListItem = null;
            selectListItem = new List<SelectListItem>();
            //List<ProductViewModel> productViewModelList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetAllProducts());
            List<ProductViewModel> ProductList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetAllProducts());
            if (ProductList != null)
            {
                //selectListItem.Add(new SelectListItem
                //{
                //    Text = "All",
                //    Value = "ALL",
                //    Selected = false
                //});

                foreach (ProductViewModel pvm in ProductList)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = '['+pvm.Code +']'+ '-' + pvm.Name ,
                        Value = pvm.Name,
                        Selected = false
                    });
                }
            }
            ERVM.ProductObj = new ProductViewModel();
            ERVM.ProductObj.ProductLists= selectListItem;



            return View(ERVM);
        }

        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "EnquiryReport", Mode = "R")]
        public string GetEnquiryDetails(string FromDate, string ToDate, string EnquiryStatus, string search, string Product)
        {

            try
            {
                DateTime? FDate = string.IsNullOrEmpty(FromDate) ? (DateTime?)null : DateTime.Parse(FromDate);
                DateTime? TDate = string.IsNullOrEmpty(ToDate) ? (DateTime?)null : DateTime.Parse(ToDate);
                List<EnquiryReportViewModel> EnquiryReport = Mapper.Map<List<EnquiryReport>, List<EnquiryReportViewModel>>(_reportBusiness.GetEnquiryDetails(FDate, TDate, EnquiryStatus, search,  Product));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = EnquiryReport });

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }


        }

        /// <summary>
        /// To Get Quotation Details in Report
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "QuotationReport", Mode = "R")]
        public ActionResult QuotationReport()
        {
            AppUA _appUA = Session["AppUAOffice"] as AppUA;
            DateTime dt = _appUA.DateTime;
            ViewBag.fromdate = dt.AddDays(-90).ToString("dd-MMM-yyyy");
            ViewBag.todate = dt.ToString("dd-MMM-yyyy");

            QuotationReportViewModel QRVM = new QuotationReportViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            List<CompanyViewModel> CompaniesList = Mapper.Map<List<Company>, List<CompanyViewModel>>(_companyBusiness.GetAllCompanies());
            if (CompaniesList != null)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = "All",
                    Value = "ALL",
                    Selected = false
                });

                foreach (CompanyViewModel Cmp in CompaniesList)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = Cmp.Name,
                        Value = Cmp.Code,
                        Selected = false
                    });
                }
            }
            QRVM.CompanyObj = new CompanyViewModel();
            QRVM.CompanyObj.CompanyList = selectListItem;

            
             selectListItem = new List<SelectListItem>();
            List<QuoteStageViewModel> QuoteStageList = Mapper.Map<List<QuoteStage>, List<QuoteStageViewModel>>(_quotationBusiness.GetAllQuoteStages());
           
            if (QuoteStageList != null)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = "All",
                    Value = "ALL",
                    Selected = false
                });

                foreach (QuoteStageViewModel QS in QuoteStageList)
                {
                selectListItem.Add(new SelectListItem
                {
                    Text = QS.Description,
                    Value = QS.Code.ToString(),
                    Selected = false
                });

            }
            }
            QRVM.QuoteStageObj = new QuoteStageViewModel();
            QRVM.QuoteStageObj.quoteStageList = selectListItem;

            selectListItem = null;
            selectListItem = new List<SelectListItem>();
            //List<ProductViewModel> productViewModelList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetAllProducts());
            List<ProductViewModel> ProductList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetAllProducts());
            if (ProductList != null)
            {
                //selectListItem.Add(new SelectListItem
                //{
                //    Text = "All",
                //    Value = "ALL",
                //    Selected = false
                //});

                foreach (ProductViewModel pvm in ProductList)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = '[' + pvm.Code + ']' + '-' + pvm.Name,
                        Value = pvm.Name,
                        Selected = false
                    });
                }
            }
            QRVM.ProductObj = new ProductViewModel();
            QRVM.ProductObj.ProductLists = selectListItem;


            return View(QRVM);
        }

        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "QuotationReport", Mode = "R")]
        public string GetQuotationDetails(string AdvanceSearchObject)//string FromDate, string ToDate, string EnquiryStatus, string search)//ReportAdvanceSearch
        {

            try
            {
                ReportAdvanceSearch ReptAdvancedSearchObj = AdvanceSearchObject != null ? JsonConvert.DeserializeObject<ReportAdvanceSearch>(AdvanceSearchObject) : new ReportAdvanceSearch();
                List<QuotationReportViewModel> QuotationReport = Mapper.Map<List<QuotationReport>, List<QuotationReportViewModel>>(_reportBusiness.GetQuotationDetails(ReptAdvancedSearchObj));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = QuotationReport });

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }


        }

        #region ButtonStyling
        [HttpGet]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            Permission _permission = Session["UserRightsOffice"] as Permission;

            switch (ActionType)
            {
                case "List":
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Disable = false;
                    ToolboxViewModelObj.backbtn.Text = "Back";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    ToolboxViewModelObj.backbtn.DisableReason = "Not applicable";
                    ToolboxViewModelObj.backbtn.Event = "Back();";

                    ToolboxViewModelObj.PrintBtn.Visible = true;
                    ToolboxViewModelObj.PrintBtn.Text = "Export";
                    ToolboxViewModelObj.PrintBtn.Title = "Export";
                    ToolboxViewModelObj.PrintBtn.Event = "PrintReport();";

                    // ToolboxViewModelObj = _tool.SetToolbarAccess(ToolboxViewModelObj, _permission);
                    break;

                case "ListWithReset":
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Disable = false;
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    ToolboxViewModelObj.backbtn.Text = "Back";
                    ToolboxViewModelObj.backbtn.DisableReason = "Not applicable";
                    ToolboxViewModelObj.backbtn.Event = "Back();";

                    ToolboxViewModelObj.PrintBtn.Visible = true;
                    ToolboxViewModelObj.PrintBtn.Text = "Export";
                    ToolboxViewModelObj.PrintBtn.Title = "Export";
                    ToolboxViewModelObj.PrintBtn.Event = "PrintReport();";

                    ToolboxViewModelObj.resetbtn.Visible = true;
                    ToolboxViewModelObj.resetbtn.Title = "Reset";
                    ToolboxViewModelObj.resetbtn.Text = "Reset";
                    ToolboxViewModelObj.resetbtn.Event = "Reset();";


                    //ToolboxViewModelObj = _tool.SetToolbarAccess(ToolboxViewModelObj, _permission);

                    break;

                default:
                    return Content("Nochange");
            }
            return PartialView("ToolboxView", ToolboxViewModelObj);
        }

        #endregion
        //CourierReport

        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "CourierReport", Mode = "R")]
        public ActionResult CourierReport()
        {

            AppUA _appUA = Session["AppUAOffice"] as AppUA;
            DateTime dt = _appUA.DateTime;
            ViewBag.fromdate = dt.AddDays(-90).ToString("dd-MMM-yyyy");
            ViewBag.todate = dt.ToString("dd-MMM-yyyy");
            CourierReportViewModel courierReportViewModel = new CourierReportViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            List<CourierAgencyViewModel> AgencyList = Mapper.Map<List<CourierAgency>, List<CourierAgencyViewModel>>(_courierBusiness.GetAllCourierAgency());
            if (AgencyList != null)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = "All",
                    Value = "ALL",
                    Selected = false
                });


                foreach (CourierAgencyViewModel cvm in AgencyList)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = cvm.Name,
                        Value = cvm.Code.ToString(),
                        Selected = false
                    });
                }
            }

            courierReportViewModel.AgencyList = selectListItem;
            selectListItem = new List<SelectListItem>();

            selectListItem.Add(new SelectListItem
            {
                Text = "All",
                Value = "ALL",
                Selected = false
            });
            selectListItem.Add(new SelectListItem
            {
                Text = "Inbound",
                Value = "Inbound",
                Selected = false
            });
            selectListItem.Add(new SelectListItem
            {
                Text = "Outbound",
                Value = "Outbound",
                Selected = false
            });

            courierReportViewModel.CourierTypeList = selectListItem;

            return View(courierReportViewModel);
        }

        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "CourierReport", Mode = "R")]
        public string GetCourierDetails(string FromDate, string ToDate, string AgencyCode, string search, string Type)
        {
            //if (!string.IsNullOrEmpty(AgencyCode))
            //{
            try
            {
                DateTime? FDate = string.IsNullOrEmpty(FromDate) ? (DateTime?)null : DateTime.Parse(FromDate);
                DateTime? TDate = string.IsNullOrEmpty(ToDate) ? (DateTime?)null : DateTime.Parse(ToDate);
                List<CourierReportViewModel> CourierdetailObj = Mapper.Map<List<CourierReport>, List<CourierReportViewModel>>(_reportBusiness.GetCourierDetails(FDate, TDate, AgencyCode, search, Type));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = CourierdetailObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }



        //CustomerPoReport  

        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "CustomerPOReport", Mode = "R")]
        public ActionResult CustomerPOReport()
        {
            AppUA _appUA = Session["AppUAOffice"] as AppUA;
            DateTime dt = _appUA.DateTime;
            ViewBag.fromdate = dt.AddDays(-90).ToString("dd-MMM-yyyy");
            ViewBag.todate = dt.ToString("dd-MMM-yyyy");
            CustomerPOReportViewModel customerPOReportVMObj = new CustomerPOReportViewModel();
            customerPOReportVMObj.CustomerPOObj = new CustomerPOViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();

            List<CustomerViewModel> CustList = Mapper.Map<List<Customer>, List<CustomerViewModel>>(_customerBusiness.GetAllCustomers());
            selectListItem.Add(new SelectListItem
            {
                Text = "ALL",
                Value = "ALL",
                Selected = false
            });


            foreach (CustomerViewModel Cust in CustList)
            {
               
                selectListItem.Add(new SelectListItem
                {
                    Text = Cust.CompanyName,
                    Value = Cust.ID.ToString(),
                    Selected = false
                });
            }
            customerPOReportVMObj.CustomerPOObj.CustomerList = selectListItem;

            customerPOReportVMObj.CustomerPOObj.CompanyList = new List<SelectListItem>();
            selectListItem = new List<SelectListItem>();
            List<CompanyViewModel> CompaniesList = Mapper.Map<List<Company>, List<CompanyViewModel>>(_companyBusiness.GetAllCompanies());
            selectListItem.Add(new SelectListItem
            {
                Text = "ALL",
                Value = "ALL",
                Selected = false
            });
            foreach (CompanyViewModel Cmp in CompaniesList)
            {
               
                selectListItem.Add(new SelectListItem
                {
                    Text = Cmp.Name,
                    Value = Cmp.Code,
                    Selected = false
                });
            }
            customerPOReportVMObj.CustomerPOObj.CompanyList = selectListItem;

            selectListItem = new List<SelectListItem>();
            selectListItem.Add(new SelectListItem
            {
                Text = "ALL",
                Value = "ALL",
                Selected = false
            });
            selectListItem.Add(new SelectListItem
            {
                Text = "Closed",
                Value = "CSD",
                Selected = false
            });
            selectListItem.Add(new SelectListItem
            {
                Text = "Open",
                Value = "OPN",
                Selected = false
            });
            selectListItem.Add(new SelectListItem
            {
                Text = "In Progress",
                Value = "PGS",
                Selected = false
            });
            customerPOReportVMObj.CustomerPOObj.POStatusList= selectListItem;
            return View(customerPOReportVMObj);
        }

        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "CustomerPOReport", Mode = "R")]
        public string GetCustomerPODetails(string AdvanceSearchObject)//string FromDate, string ToDate, string POStatus, string search
        {
            try
            {
                ReportAdvanceSearch ReptAdvancedSearchObj = AdvanceSearchObject != null ? JsonConvert.DeserializeObject<ReportAdvanceSearch>(AdvanceSearchObject) : new ReportAdvanceSearch();             
                List<CustomerPOReportViewModel> CustomerdetailObj = Mapper.Map<List<CustomerPOReport>, List<CustomerPOReportViewModel>>(_reportBusiness.GetCustomerPODetails(ReptAdvancedSearchObj));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = CustomerdetailObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }

        //RequisitionReport

        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "RequisitionReport", Mode = "R")]
        public ActionResult RequisitionReport()
        {
            AppUA _appUA = Session["AppUAOffice"] as AppUA;
            DateTime dt = _appUA.DateTime;
            ViewBag.fromdate = dt.AddDays(-90).ToString("dd-MMM-yyyy");
            ViewBag.todate = dt.ToString("dd-MMM-yyyy");
            RequisitionReportViewModel requisitionReportVMObj = new RequisitionReportViewModel();
            requisitionReportVMObj.RequisitionObj = new RequisitionViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();

            requisitionReportVMObj.RequisitionObj.userObj = new UserViewModel();
            requisitionReportVMObj.RequisitionObj.userObj.userList = new List<SelectListItem>();


            List<UserViewModel> userList = Mapper.Map<List<User>, List<UserViewModel>>(_userBusiness.GetAllUsers());

            selectListItem.Add(new SelectListItem
            {
                Text = "ALL",
                Value = "ALL",
                Selected = false
            });

            foreach (UserViewModel U in userList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = U.LoginName,
                    Value = U.ID.ToString(),
                    Selected = false
                });
            }

            requisitionReportVMObj.RequisitionObj.userObj.userList = selectListItem;
           

            return View(requisitionReportVMObj);
        }


        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "RequisitionReport", Mode = "R")]
        public string GetRequisitionDetails(string AdvanceSearchObject)
        {
            try
            {
                ReportAdvanceSearch ReptAdvancedSearchObj = AdvanceSearchObject != null ? JsonConvert.DeserializeObject<ReportAdvanceSearch>(AdvanceSearchObject) : new ReportAdvanceSearch();
                List<RequisitionReportViewModel> RequisitiondetailObj = Mapper.Map<List<RequisitionReport>, List<RequisitionReportViewModel>>(_reportBusiness.GetRequisitionDetails(ReptAdvancedSearchObj));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = RequisitiondetailObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }

        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "EnquiryFollowUpReport", Mode = "R")]
        public ActionResult EnquiryFollowUpReport(string id)
        {
            AppUA _appUA = Session["AppUAOffice"] as AppUA;
            DateTime dt = _appUA.DateTime;
            ViewBag.fromdate = dt.AddDays(-30).ToString("dd-MMM-yyyy");
            ViewBag.todate = dt.AddDays(30).ToString("dd-MMM-yyyy");
            EnquiryFollowUpReportListViewModel result = new EnquiryFollowUpReportListViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            selectListItem = new List<SelectListItem>();
            selectListItem.Add(new SelectListItem { Text = "Open", Value = "Open", Selected = true });
            selectListItem.Add(new SelectListItem { Text = "Closed", Value = "Closed", Selected = false });
            selectListItem.Add(new SelectListItem { Text = "All", Value = "ALL", Selected = false });
            result.StatusFilter = selectListItem;
            selectListItem = new List<SelectListItem>();
            //result.customerObj = new CustomerViewModel();
            //List<CustomerViewModel> customerList = Mapper.Map<List<Customer>, List<CustomerViewModel>>(_customerBusiness.GetAllCustomers());
            //if (customerList != null)
            //{
            //    foreach (CustomerViewModel customerVM in customerList)
            //    {
            //        selectListItem.Add(new SelectListItem
            //        {
            //            Text = customerVM.CompanyName,
            //            Value = customerVM.CompanyName.ToString(),
            //            Selected = false
            //        });
            //    }
            //}
            //result.customerObj.CustomerList = selectListItem;
            return View(result);
        }
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "EnquiryFollowUpReport", Mode = "R")]
        public string GetEnquiryFollowUpDetails(string enquiryFollowupAdvanceSearchObj)
        {
            try
            {

                EnquiryFollowupReportAdvanceSearch followupAdvanceSearchObj = enquiryFollowupAdvanceSearchObj != null ? JsonConvert.DeserializeObject<EnquiryFollowupReportAdvanceSearch>(enquiryFollowupAdvanceSearchObj) : new EnquiryFollowupReportAdvanceSearch();
                AppUA appUA = Session["AppUAOffice"] as AppUA;
                DateTime dt = appUA.DateTime;
                if (enquiryFollowupAdvanceSearchObj == null)
                {
                    followupAdvanceSearchObj.FromDate = dt.AddDays(-30).ToString("dd-MMM-yyyy");
                    followupAdvanceSearchObj.ToDate = dt.AddDays(30).ToString("dd-MMM-yyyy");
                    // followupAdvanceSearchObj.ToDate = appUA.DateTime.ToString("dd-MMM-yyyy");
                    followupAdvanceSearchObj.Status = "Open";

                }
                EnquiryFollowUpReportListViewModel result = new EnquiryFollowUpReportListViewModel();

                result.FollowUpList = Mapper.Map<List<EnquiryFollowupReport>, List<EnquiryFollowupReportViewModel>>(_reportBusiness.GetEnquiryFollowUpDetails(followupAdvanceSearchObj));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = result });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }



    }
}