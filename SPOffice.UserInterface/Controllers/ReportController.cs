using AutoMapper;
using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.UserInterface.Models;
using SPOffice.UserInterface.SecurityFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPOffice.UserInterface.Controllers
{
    public class ReportController : Controller
    {
        IReportBusiness _reportBusiness;
        public ReportController(IReportBusiness reportBusiness)
        {
            _reportBusiness = reportBusiness;

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
    }
}