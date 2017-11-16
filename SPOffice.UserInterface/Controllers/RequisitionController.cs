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
    public class RequisitionController : Controller
    {
        AppConst c = new AppConst();
        IRequisitionBusiness _requisitionBusiness;
        public RequisitionController(IRequisitionBusiness requisitionBusiness)
        {
            _requisitionBusiness = requisitionBusiness;
        }
        // GET: Requisition
        public ActionResult Index()
        {
            RequisitionViewModel RVM = new RequisitionViewModel();
            RVM.CompanyObj = new CompanyViewModel();
            RVM.RequisitionDetailObj = new RequisitionDetailViewModel();
            List<SelectListItem> List = new List<SelectListItem>();
            RVM.CompanyObj.CompanyList = List;
            return View(RVM);
        }
        [HttpGet]
        public string GetUserRequisitionList()
        {
            try
            {
                AppUA _appUA = Session["AppUA"] as AppUA;
                List<RequisitionViewModel> RequisitionList = Mapper.Map<List<Requisition>, List<RequisitionViewModel>>(_requisitionBusiness.GetUserRequisitionList(_appUA.UserName,_appUA.AppID));

                //int openCount = customerPOViewModelList == null ? 0 : customerPOViewModelList.Where(Q => Q.purchaseOrderStatus.Code == "OPN").Select(T => T.ID).Count();
                //int closedCount = customerPOViewModelList == null ? 0 : customerPOViewModelList.Where(Q => Q.purchaseOrderStatus.Code == "CSD").Select(T => T.ID).Count();

                //if (filter != null)
                //{
                //    customerPOViewModelList = customerPOViewModelList.Where(Q => Q.purchaseOrderStatus.Code == filter).ToList();
                //}
                return JsonConvert.SerializeObject(new { Result = "OK", Records = RequisitionList });//, Open = openCount, InProgress = inProgressCount, Closed = closedCount });
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
                    break;
                case "Add":
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Text = "Add";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    ToolboxViewModelObj.addbtn.Event = "AddNew();";

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Text = "Save";
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    ToolboxViewModelObj.savebtn.Event = "";

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