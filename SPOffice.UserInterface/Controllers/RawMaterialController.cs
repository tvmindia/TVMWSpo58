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
    public class RawMaterialController : Controller
    {
        AppConst c = new AppConst();
        IRawMaterialBusiness _rawMaterialBusiness;
        public RawMaterialController(IRawMaterialBusiness rawMaterialBusiness)
        {
            _rawMaterialBusiness = rawMaterialBusiness;
        }
        // GET: RawMaterial
        [AuthSecurityFilter(ProjectObject = "RawMaterial", Mode = "R")]
        public ActionResult Index()
        {
            return View();
        }

        #region GetAllRawMaterials
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "RawMaterial", Mode = "R")]
        public string GetAllRawMaterials()
        {
            try
            {
                List<RawMaterialViewModel> rawMaterialList = Mapper.Map<List<RawMaterial>, List<RawMaterialViewModel>>(_rawMaterialBusiness.GetAllRawMaterial());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = rawMaterialList });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion  GetAllRawMaterials

        #region GetRawMaterialDetails
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "RawMaterial", Mode = "R")]
        public string GetRawMaterialDetails(string ID)
        {
            try
            {

                RawMaterialViewModel rawMaterialViewModelObj = Mapper.Map<RawMaterial, RawMaterialViewModel>(_rawMaterialBusiness.GetRawMaterialDetails(Guid.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = rawMaterialViewModelObj });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion  GetRawMaterialDetails

        #region InsertUpdateRawMaterial
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthSecurityFilter(ProjectObject = "RawMaterial", Mode = "W")]
        public string InsertUpdateRawMaterial(RawMaterialViewModel rawMaterialViewModel)
        {
            object result = null;
            try
            {
                AppUA _appUA = Session["AppUAOffice"] as AppUA;
                rawMaterialViewModel.commonObj = new CommonViewModel();
                rawMaterialViewModel.commonObj.CreatedBy = _appUA.UserName;
                rawMaterialViewModel.commonObj.CreatedDate = _appUA.DateTime;
                rawMaterialViewModel.commonObj.UpdatedBy = rawMaterialViewModel.commonObj.CreatedBy;
                rawMaterialViewModel.commonObj.UpdatedDate = rawMaterialViewModel.commonObj.CreatedDate;
                switch (string.IsNullOrEmpty(rawMaterialViewModel.ID.ToString()))
                {
                    case true:
                        result = _rawMaterialBusiness.InsertRawMaterial(Mapper.Map<RawMaterialViewModel, RawMaterial>(rawMaterialViewModel));
                        break;
                    case false:
                        result = _rawMaterialBusiness.UpdateRawMaterial(Mapper.Map<RawMaterialViewModel, RawMaterial>(rawMaterialViewModel));
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
        #endregion InsertUpdateRawMaterial

        #region DeleteRawMaterial
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "RawMaterial", Mode = "D")]
        public string DeleteRawMaterial(string ID)
        {

            try
            {
                object result = null;

                result = _rawMaterialBusiness.DeleteRawMaterial(Guid.Parse(ID));
                return JsonConvert.SerializeObject(new { Result = "OK", Message = result });

            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }


        }

        #endregion DeleteRawMaterial

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