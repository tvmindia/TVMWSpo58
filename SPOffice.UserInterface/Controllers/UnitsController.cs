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
    public class UnitsController : Controller
    {


        AppConst c = new AppConst();
        IUnitsBusiness _unitsBusiness;
        public UnitsController(IUnitsBusiness unitsBusiness)
        {
            _unitsBusiness = unitsBusiness;
        }
        // GET: Units
        [AuthSecurityFilter(ProjectObject = "Units", Mode = "R")]
        public ActionResult Index()
        {
            return View();
        }

        #region GetAllUnits
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Units", Mode = "R")]
        public string GetAllUnits()
        {
            try
            {
                List<UnitsViewModel> unitsList = Mapper.Map<List<Units>, List<UnitsViewModel>>(_unitsBusiness.GetAllUnits());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = unitsList });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion  GetAllUnits

        #region GetUnitsDetails
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Units", Mode = "R")]
        public string GetUnitsDetails(string code)
        {
            try
            {

                UnitsViewModel unitsViewModelObj = Mapper.Map<Units, UnitsViewModel>(_unitsBusiness.GetUnitsDetails(code));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = unitsViewModelObj });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion  GetUnitsDetails

        #region InsertUpdateUnits
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthSecurityFilter(ProjectObject = "Units", Mode = "W")]
        public string InsertUpdateUnits(UnitsViewModel unitsViewModel)
        {
            object result = null;
            try
            {
                AppUA _appUA = Session["AppUAOffice"] as AppUA;
                unitsViewModel.commonObj = new CommonViewModel();
                unitsViewModel.commonObj.CreatedBy = _appUA.UserName;
                unitsViewModel.commonObj.CreatedDate = _appUA.DateTime;
                unitsViewModel.commonObj.UpdatedBy = unitsViewModel.commonObj.CreatedBy;
                unitsViewModel.commonObj.UpdatedDate = unitsViewModel.commonObj.CreatedDate;
                switch (string.IsNullOrEmpty(unitsViewModel.hdnCode))
                {
                    case true:
                        result = _unitsBusiness.InsertUnits(Mapper.Map<UnitsViewModel, Units>(unitsViewModel));
                        break;
                    case false:
                        result = _unitsBusiness.UpdateUnits(Mapper.Map<UnitsViewModel, Units>(unitsViewModel));
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
        #endregion InsertUpdateUnits

        #region DeleteUnits
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Units", Mode = "D")]
        public string DeleteUnits(string code)
        {

            try
            {
                object result = null;

                result = _unitsBusiness.DeleteUnits(code);
                return JsonConvert.SerializeObject(new { Result = "OK", Message = result });

            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }


        }

        #endregion DeleteUnits

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

                    ToolboxViewModelObj.addbtn.Visible = false;
                    ToolboxViewModelObj.addbtn.Text = "New";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    ToolboxViewModelObj.addbtn.Event = "openNav();";

                    break; 
                default:
                    return Content("Nochange");
            }
            return PartialView("ToolboxView", ToolboxViewModelObj);
        }
        #endregion ButtonStyling
    }
}