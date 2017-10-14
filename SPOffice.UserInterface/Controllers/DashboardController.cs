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
        [AuthSecurityFilter(ProjectObject = "Dashboard", Mode = "R")]
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

        [AuthSecurityFilter(ProjectObject = "AdminDashboard", Mode = "R")]
        public ActionResult Admin()
        {
            return View();
        }

        


        private ActionResult RedirectToAdminDashboard()
        {
            return RedirectToAction("Admin", "DashBoard");
        }

        [AuthSecurityFilter(ProjectObject = "AdminDashboard", Mode = "R")]
        public ActionResult RecentEnquiries()
        {
            RecentEnquiriesViewModel data=new RecentEnquiriesViewModel();
            data.BaseUrl = "../Enquiries/Index/";
            data.EnquiryList = Mapper.Map<List<Enquiry>, List<EnquiryViewModel>>(_dashboardBusiness.GetRecentEnquiryList(data.BaseUrl));
            return PartialView("_RecentEnquiries", data);
        }


        [AuthSecurityFilter(ProjectObject = "AdminDashboard", Mode = "R")]
        public ActionResult EnquiriesSummary()
        {
            EnquirySummaryViewModel data = new EnquirySummaryViewModel();
           
            data = Mapper.Map<EnquirySummary, EnquirySummaryViewModel>(_dashboardBusiness.GetEnquirySummary());
            return PartialView("_EnquirySummary", data);
        }


    }
}