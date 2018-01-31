using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAMTool.BusinessServices.Contracts;
using SAMTool.DataAccessObject.DTO;
using SPOffice.UserInterface.SecurityFilter;
using UserInterface.Models;
using AutoMapper;
using SPOffice.DataAccessObject.DTO;
using SPOffice.BusinessService.Contracts;
using SPOffice.UserInterface.Models;
using Newtonsoft.Json;

namespace UserInterface.Controllers
{
    public class DashboardController : Controller
    {

        #region Constructor_Injection 

        AppConst c = new AppConst();
        IDashboardBusiness _dashboardBusiness;
        ICommonBusiness _commonBusiness;

        public DashboardController(IDashboardBusiness dashboardBusiness , ICommonBusiness commonBusiness)
        {
            _dashboardBusiness = dashboardBusiness;
            _commonBusiness = commonBusiness;

        }
        #endregion Constructor_Injection 


        // GET: Dashboard
        [AuthSecurityFilter(ProjectObject = "DashBoard", Mode = "R")]
        public ActionResult Index()
        {
            //AppUA _appUA = Session["AppUA"] as AppUA;

            //if (("," +_appUA.RolesCSV+ ",").Contains(",SAdmin,") || _appUA.RolesCSV.Contains("CEO"))
            //{
            //    return RedirectToAdminDashboard();
            //}
            //else {
            //    return View();
            //}
            return View();
        }

        [AuthSecurityFilter(ProjectObject = "DashBoard", Mode = "R")]
        public ActionResult Admin()
        {
            return View();
        }

        


        private ActionResult RedirectToAdminDashboard()
        {
            return RedirectToAction("Admin", "DashBoard");
        }

        [AuthSecurityFilter(ProjectObject = "DashBoard", Mode = "R")]
        public ActionResult RecentEnquiries()
        {
            RecentEnquiriesViewModel data=new RecentEnquiriesViewModel();
            data.BaseUrl = "../Enquiry/Index/";
            data.EnquiryList = Mapper.Map<List<Enquiry>, List<EnquiryViewModel>>(_dashboardBusiness.GetRecentEnquiryList(data.BaseUrl));
            return PartialView("_RecentEnquiries", data);
        }


        [AuthSecurityFilter(ProjectObject = "DashBoard", Mode = "R")]
        public ActionResult EnquiriesSummary()
        {
            EnquirySummaryViewModel data = new EnquirySummaryViewModel();
           
            data = Mapper.Map<EnquirySummary, EnquirySummaryViewModel>(_dashboardBusiness.GetEnquirySummary());
            return PartialView("_EnquirySummary", data);
        }

        [AuthSecurityFilter(ProjectObject = "DashBoard", Mode = "R")]
        public ActionResult TodaysFollowUps()
        {
            TodaysFollowUpsViewModel data = new TodaysFollowUpsViewModel();
            SPOffice.DataAccessObject.DTO.Common C = new SPOffice.DataAccessObject.DTO.Common();
            data.BaseUrl = "../Enquiry/Index/";
            data.Day = C.GetCurrentDateTimeFormatted();
            data.FollowUpsList = Mapper.Map<List<FollowUp>, List<FollowUpViewModel>>(_dashboardBusiness.GetTodaysFollowUpDetails(C.GetCurrentDateTime(), data.BaseUrl));
            if (data.FollowUpsList != null) {
                data.open = data.FollowUpsList.Count(n => n.Status == "Open");
                data.closed = data.FollowUpsList.Count(n => n.Status == "Closed");
                if ((data.open + data.closed) > 0)
                {
                    data.openPerc = (data.open * 100) / (data.open + data.closed);
                    data.closedPerc = (data.closed * 100) / (data.open + data.closed);
                }
               
            }

            return PartialView("_TodaysFollowups", data);
        }


        [AuthSecurityFilter(ProjectObject = "DashBoard", Mode = "R")]
        public ActionResult POandQuoteSummary()
        {
            POandQuoteSummaryViewModel data = new POandQuoteSummaryViewModel();

            data.CustomerPOSummary = Mapper.Map<CustomerPOSummary, CustomerPOSummaryViewModel>(_dashboardBusiness.GetCustomerPOSummary());
            data.QuoteSummary = Mapper.Map<QuotationSummary, QuotationSummaryViewModel>(_dashboardBusiness.GetQuotationSummary());
            data.RequisitionSummary = Mapper.Map<RequisitionOverViewCount, RequisitionOverViewCountViewModel>(_dashboardBusiness.GetRequisitionSummary());
            return PartialView("_POandQuoteSummary", data);
        }


       
       
        
    }
}