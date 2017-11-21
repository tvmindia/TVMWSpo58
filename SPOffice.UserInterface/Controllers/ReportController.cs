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
    public class ReportController : Controller
    {
        IReportBusiness _reportBusiness;
        IEnquiryStatusBusiness _enquiryStatusBusiness;

        public ReportController(IReportBusiness reportBusiness, IEnquiryStatusBusiness enquiryStatusBusiness)
        {
            _reportBusiness = reportBusiness;
            _enquiryStatusBusiness = enquiryStatusBusiness;

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
            AppUA _appUA = Session["AppUA"] as AppUA;
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
            AppUA _appUA = Session["AppUA"] as AppUA;
            DateTime dt = _appUA.DateTime;
            ViewBag.fromdate = dt.AddDays(-90).ToString("dd-MMM-yyyy");
            ViewBag.todate = dt.ToString("dd-MMM-yyyy");
             EnquiryReportViewModel ERVM = new EnquiryReportViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();


            List<EnquiryStatusViewModel>enquiryStatusList = Mapper.Map<List<EnquiryStatus>, List<EnquiryStatusViewModel>>(_enquiryStatusBusiness.GetAllEnquiryStatusList()).ToList();
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

            return View(ERVM);
        }

        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "EnquiryReport", Mode = "R")]
        public string GetEnquiryDetails(string FromDate, string ToDate,string EnquiryStatus, string search)
        {
            
                try
                {
                    DateTime? FDate = string.IsNullOrEmpty(FromDate) ? (DateTime?)null : DateTime.Parse(FromDate);
                    DateTime? TDate = string.IsNullOrEmpty(ToDate) ? (DateTime?)null : DateTime.Parse(ToDate);
                    List<EnquiryReportViewModel> EnquiryReport = Mapper.Map<List<EnquiryReport>, List<EnquiryReportViewModel>>(_reportBusiness.GetEnquiryDetails(FDate, TDate, EnquiryStatus, search));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = EnquiryReport });

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
            Permission _permission = Session["UserRights"] as Permission;

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
    }
}