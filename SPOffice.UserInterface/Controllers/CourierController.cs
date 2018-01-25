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
    public class CourierController : Controller
    {
        AppConst c = new AppConst();
        ICourierBusiness _courierBusiness;
        public CourierController(ICourierBusiness courierBusiness)
        {
            _courierBusiness = courierBusiness;
        }
        // GET: Courier
        [AuthSecurityFilter(ProjectObject = "Courier", Mode = "R")]
        public ActionResult Index(string id)
        {
            ViewBag.value = id;
            CourierViewModel CourierVM = new CourierViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            List<CourierAgencyViewModel> courierAgencyList = Mapper.Map<List<CourierAgency>, List<CourierAgencyViewModel>>(_courierBusiness.GetAllCourierAgency());
            foreach (CourierAgencyViewModel courier in courierAgencyList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = courier.Name,
                    Value = courier.Code,
                    Selected = false
                });
            }
            CourierVM.AgencyList = selectListItem;
            selectListItem = new List<SelectListItem>();
            selectListItem.Add(new SelectListItem
            {
                Text = "Inward",
                Value = "Inward",
                Selected = false
            });
            selectListItem.Add(new SelectListItem
            {
                Text = "Outward",
                Value = "Outward",
                Selected = false
            });

            CourierVM.CourierTypeList = selectListItem;

            return View(CourierVM);
        }

        #region GetAllCourierAgencies
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Courier", Mode = "R")]
        public string GetAllCouriers()
        {
            try
            {
                List<CourierViewModel> courierList = Mapper.Map<List<Courier>, List<CourierViewModel>>(_courierBusiness.GetAllCouriers());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = courierList });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion  GetAllCourierAgencies

        #region GetCourierDetails
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Courier", Mode = "R")]
        public string GetCourierDetails(string ID)
        {
            try
            {

                CourierViewModel courierViewModel = Mapper.Map<Courier, CourierViewModel>(_courierBusiness.GetCourierDetails(Guid.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = courierViewModel });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion  GetCourierDetails

        #region InsertUpdateCourier
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthSecurityFilter(ProjectObject = "Courier", Mode = "W")]
        public string InsertUpdateCourier(CourierViewModel courierViewModel)
        {
            object result = null;
            try
            {
                AppUA _appUA = Session["AppUAOffice"] as AppUA;
                courierViewModel.commonObj = new CommonViewModel();
                courierViewModel.commonObj.CreatedBy = _appUA.UserName;
                courierViewModel.commonObj.CreatedDate =_appUA.DateTime;
                courierViewModel.commonObj.UpdatedBy = courierViewModel.commonObj.CreatedBy;
                courierViewModel.commonObj.UpdatedDate = courierViewModel.commonObj.CreatedDate;
                switch (string.IsNullOrEmpty(courierViewModel.ID.ToString()))
                {
                    case true:
                        result = _courierBusiness.InsertCourier(Mapper.Map<CourierViewModel, Courier>(courierViewModel));
                        break;
                    case false:
                        result = _courierBusiness.UpdateCourier(Mapper.Map<CourierViewModel, Courier>(courierViewModel));
                        break;
                }

                return JsonConvert.SerializeObject(new { Result = "OK", Record = result, Message="Successfull" });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion InsertUpdateCourier

        #region DeleteCourier
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Courier", Mode = "D")]
        public string DeleteCourier(string ID)
        {
            try
            {
                object result = null;
                result = _courierBusiness.DeleteCourier(Guid.Parse(ID));
                return JsonConvert.SerializeObject(new { Result = "OK", Message = result });

            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }


        }
        #endregion DeleteCourier

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

                    ToolboxViewModelObj.addbtn.Visible = true;
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