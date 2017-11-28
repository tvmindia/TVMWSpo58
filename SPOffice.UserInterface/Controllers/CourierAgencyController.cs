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
    public class CourierAgencyController : Controller
    {
        AppConst c = new AppConst();
        ICourierBusiness _courierBusiness;
        public CourierAgencyController(ICourierBusiness courierBusiness)
        {
            _courierBusiness = courierBusiness;
        }

        // GET: Courier
        [AuthSecurityFilter(ProjectObject = "Courier", Mode = "R")]
        public ActionResult Index()
        {
            
            return View();
        }

        #region GetAllCourierAgencies
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Courier", Mode = "R")]
        public string GetAllCourierAgencies()
        {
            try
            {
                List<CourierAgencyViewModel> courierAgencyList = Mapper.Map<List<CourierAgency>, List<CourierAgencyViewModel>>(_courierBusiness.GetAllCourierAgency());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = courierAgencyList });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion  GetAllCourierAgencies

        #region GetCourierAgencyDetails
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Courier", Mode = "R")]
        public string GetCourierAgencyDetails(string Code)
        {
            try
            {

                CourierAgencyViewModel courierAgencyViewModel = Mapper.Map<CourierAgency, CourierAgencyViewModel>(_courierBusiness.GetCourierAgencyDetails(Code));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = courierAgencyViewModel });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion  GetCourierAgencyDetails

        #region InsertUpdateCourierAgency
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthSecurityFilter(ProjectObject = "Courier", Mode = "R")]
        public string InsertUpdateCourierAgency(CourierAgencyViewModel courierAgencyViewModel)
        {
            object result = null;
            try
            {
                AppUA _appUA = Session["AppUAOffice"] as AppUA;
                courierAgencyViewModel.commonObj = new CommonViewModel();
                courierAgencyViewModel.commonObj.CreatedBy = _appUA.UserName;
                courierAgencyViewModel.commonObj.CreatedDate = _appUA.DateTime;
                courierAgencyViewModel.commonObj.UpdatedBy = courierAgencyViewModel.commonObj.CreatedBy;
                courierAgencyViewModel.commonObj.UpdatedDate = courierAgencyViewModel.commonObj.CreatedDate;
                switch (courierAgencyViewModel.Operation)
                {
                    case "Insert":
                        result = _courierBusiness.InsertCourierAgency(Mapper.Map<CourierAgencyViewModel, CourierAgency>(courierAgencyViewModel));
                        break;
                    case "Update":
                        result = _courierBusiness.UpdateCourierAgency(Mapper.Map<CourierAgencyViewModel, CourierAgency>(courierAgencyViewModel));
                        break;
                }

                return JsonConvert.SerializeObject(new { Result = "OK", Record = result });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion InsertUpdateCourierAgency

        #region DeleteCourierAgency
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Courier", Mode = "D")]
        public string DeleteCourierAgency(string Code)
        {
         try
            {
                object result = null;
                result = _courierBusiness.DeleteCourierAgency(Code);
                return JsonConvert.SerializeObject(new { Result = "OK", Message = result });

            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }


        }
        #endregion DeleteCourierAgency

        #region ButtonStyling
        [HttpGet]
        //[AuthSecurityFilter(ProjectObject = "Department", Mode = "R")]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "List":
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Text = "Add";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    ToolboxViewModelObj.addbtn.Event = "openNav();";


                    break;
                case "Edit":

                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Text = "New";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    ToolboxViewModelObj.addbtn.Event = "openNav();";

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Text = "Save";
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    ToolboxViewModelObj.savebtn.Event = "Save();";

                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Text = "Delete";
                    ToolboxViewModelObj.deletebtn.Title = "Delete";
                    ToolboxViewModelObj.deletebtn.Event = "Delete()";

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

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Text = "Save";
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    ToolboxViewModelObj.savebtn.Event = "Save();";

                    ToolboxViewModelObj.CloseBtn.Visible = true;
                    ToolboxViewModelObj.CloseBtn.Text = "Close";
                    ToolboxViewModelObj.CloseBtn.Title = "Close";
                    ToolboxViewModelObj.CloseBtn.Event = "closeNav();";

                    ToolboxViewModelObj.resetbtn.Visible = false;
                    ToolboxViewModelObj.resetbtn.Text = "Reset";
                    ToolboxViewModelObj.resetbtn.Title = "Reset";
                    ToolboxViewModelObj.resetbtn.Event = "Reset();";

                    ToolboxViewModelObj.deletebtn.Visible = false;
                    ToolboxViewModelObj.deletebtn.Text = "Delete";
                    ToolboxViewModelObj.deletebtn.Title = "Delete";
                    ToolboxViewModelObj.deletebtn.Event = "Delete()";

                    ToolboxViewModelObj.addbtn.Visible = false;
                    ToolboxViewModelObj.addbtn.Text = "New";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    ToolboxViewModelObj.addbtn.Event = "openNav();";

                    break;
                case "AddSub":

                    break;
                case "tab1":

                    break;
                case "tab2":

                    break;
                default:
                    return Content("Nochange");
            }
            return PartialView("ToolboxView", ToolboxViewModelObj);
        }
        #endregion ButtonStyling
    }
}