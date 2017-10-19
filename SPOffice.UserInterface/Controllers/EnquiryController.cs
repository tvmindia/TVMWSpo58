using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.UserInterface.Models;
using UserInterface.Models;
using SPOffice.UserInterface.SecurityFilter;

namespace SPOffice.UserInterface.Controllers
{
    public class EnquiryController : Controller
    {
        AppConst c = new AppConst();
        IEnquiryBusiness _enquiryBusiness;
        IEmployeeBusiness _employeeBusiness;
        IIndustryBusiness _industryBusiness;
        IEnquirySourceBusiness _enquirySourceBusiness;
        IEnquiryStatusBusiness _enquiryStatusBusiness;
        IProgressStatusBusiness _progressStatusBusiness;


        public EnquiryController(IEnquiryBusiness enquiryBusiness, IEmployeeBusiness employeeBusiness, IIndustryBusiness industryBusiness,IEnquirySourceBusiness enquirySourceBusiness, IEnquiryStatusBusiness enquiryStatusBusiness, IProgressStatusBusiness progressStatusBusiness)
        {
            _enquiryBusiness = enquiryBusiness;
            _industryBusiness = industryBusiness;
            _employeeBusiness = employeeBusiness;
            _enquirySourceBusiness = enquirySourceBusiness;
            _enquiryStatusBusiness = enquiryStatusBusiness;
            _progressStatusBusiness = progressStatusBusiness;
        }


        // GET: Enquiry
        public ActionResult Index()
        {
            EnquiryViewModel EVM = new EnquiryViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            EVM.salesPersonObj = new SalesPersonViewModel();
            EVM.salesPersonObj.SalesPersonList = new List<SelectListItem>();


            List<SalesPersonViewModel> SalesPersonList = Mapper.Map<List<SalesPerson>, List<SalesPersonViewModel>>(_employeeBusiness.GetAllSalesPersons());

            foreach (SalesPersonViewModel SP in SalesPersonList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = SP.Name,
                    Value = SP.ID.ToString(),
                    Selected = false
                });
            }

            EVM.salesPersonObj.SalesPersonList = selectListItem;

            selectListItem = new List<SelectListItem>();
            EVM.industryObj = new IndustryViewModel();
            EVM.industryObj.IndustyList = new List<SelectListItem>();

            List<IndustryViewModel> industryList = Mapper.Map<List<Industry>, List<IndustryViewModel>>(_industryBusiness.GetAllIndustryList());

            foreach(IndustryViewModel IN in industryList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = IN.IndustryName,
                    Value = IN.IndustryCode.ToString(),
                    Selected = false
                });
            }
            EVM.industryObj.IndustyList = selectListItem;

            selectListItem = new List<SelectListItem>();
            EVM.enquirySourceObj = new EnquirySourceViewModel();
            EVM.enquirySourceObj.EnquirySourceList = new List<SelectListItem>();

            List<EnquirySourceViewModel> enquirySourceList = Mapper.Map<List<EnquirySource>, List<EnquirySourceViewModel>>(_enquirySourceBusiness.GetAllEnquirySourceList());

            foreach (EnquirySourceViewModel EN in enquirySourceList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = EN.Source,
                    Value = EN.SourceCode.ToString(),
                    Selected = false
                });
            }
            EVM.enquirySourceObj.EnquirySourceList = selectListItem;

            selectListItem = new List<SelectListItem>();
            EVM.enquiryStatusObj = new EnquiryStatusViewModel();
            EVM.enquiryStatusObj.EnquiryStatusList = new List<SelectListItem>();

            List<EnquiryStatusViewModel> enquiryStatusList = Mapper.Map<List<EnquiryStatus>, List<EnquiryStatusViewModel>>(_enquiryStatusBusiness.GetAllEnquiryStatusList());

            foreach (EnquiryStatusViewModel EN in enquiryStatusList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = EN.Status,
                    Value = EN.StatusCode.ToString(),
                    Selected = false
                });
            }
            EVM.enquiryStatusObj.EnquiryStatusList = selectListItem;

            selectListItem = new List<SelectListItem>();
            EVM.progressStatusObj = new ProgressStatusViewModel();
            EVM.progressStatusObj.ProgressStatusList = new List<SelectListItem>();

            List<ProgressStatusViewModel> progressStatusList = Mapper.Map<List<ProgressStatus>, List<ProgressStatusViewModel>>(_progressStatusBusiness.GetAllProgressStatusList());

            foreach (ProgressStatusViewModel PS in progressStatusList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = PS.Status,
                    Value = PS.StatusCode.ToString(),
                    Selected = false
                });
            }
            EVM.progressStatusObj.ProgressStatusList = selectListItem;

            return View(EVM);
        }

        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Enquiry", Mode = "R")]
        public string GetAllEnquiry(EnquiryViewModel data)
        {
            try
            {
                List<EnquiryViewModel> EnquiryList = Mapper.Map<List<Enquiry>, List<EnquiryViewModel>>(_enquiryBusiness.GetAllEnquiryList(Mapper.Map<EnquiryViewModel, Enquiry>(data)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = EnquiryList });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }


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