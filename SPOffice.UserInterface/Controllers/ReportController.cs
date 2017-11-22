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
    public class ReportController : Controller
    {
        IReportBusiness _reportBusiness;
        ICourierBusiness _courierBusiness;
        public ReportController(IReportBusiness reportBusiness,ICourierBusiness courierBusiness)
        {
            _reportBusiness = reportBusiness;
            _courierBusiness = courierBusiness;

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


        //CourierReport

        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "CourierReport", Mode = "R")]
        public ActionResult CourierReport()
        {

            AppUA _appUA = Session["AppUA"] as AppUA;
            DateTime dt = _appUA.DateTime;
            ViewBag.fromdate = dt.AddDays(-90).ToString("dd-MMM-yyyy");
            ViewBag.todate = dt.ToString("dd-MMM-yyyy");
            CourierReportViewModel courierReportViewModel = new CourierReportViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
           List< CourierAgencyViewModel> AgencyList = Mapper.Map<List<CourierAgency>, List<CourierAgencyViewModel>>(_courierBusiness.GetAllCourierAgency());
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
                    List<CourierReportViewModel> CourierdetailObj = Mapper.Map<List<CourierReport>, List<CourierReportViewModel>>(_reportBusiness.GetCourierDetails(FDate,TDate, AgencyCode, search, Type));

                    return JsonConvert.SerializeObject(new { Result = "OK" ,Records = CourierdetailObj });
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }

            }
            //return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "AgencyCode is required" });
        }




    }

//#region ButtonStyling
//[HttpGet]
//public ActionResult ChangeButtonStyle(string ActionType)
//{
//    ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
//    //Permission _permission = Session["UserRights"] as Permission;

//    switch (ActionType)
//    {       

//        case "ListWithReset":
//            ToolboxViewModelObj.backbtn.Visible = true;
//            ToolboxViewModelObj.backbtn.Disable = false;
//            ToolboxViewModelObj.backbtn.Text = "Back";
//            ToolboxViewModelObj.backbtn.DisableReason = "Not applicable";
//            ToolboxViewModelObj.backbtn.Event = "Back();";

//            ToolboxViewModelObj.PrintBtn.Visible = true;
//            ToolboxViewModelObj.PrintBtn.Text = "Export";
//            ToolboxViewModelObj.PrintBtn.Event = "PrintReport();";

//            ToolboxViewModelObj.resetbtn.Visible = true;
//            ToolboxViewModelObj.resetbtn.Text = "Reset";
//            ToolboxViewModelObj.resetbtn.Event = "Reset();";


//            //ToolboxViewModelObj = _tool.SetToolbarAccess(ToolboxViewModelObj, _permission);

//            break;

//        default:
//            return Convert("Nochange");
//    }
//    return PartialViewResult("ToolboxView", ToolboxViewModelObj);
//}

//        #endregion ButtonStyling
