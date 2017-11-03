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
        IFollowUpBusiness _followupBusiness;
        IReminderBusiness _reminderBusiness;

        public EnquiryController(IEnquiryBusiness enquiryBusiness, IEmployeeBusiness employeeBusiness, IIndustryBusiness industryBusiness,IEnquirySourceBusiness enquirySourceBusiness,IEnquiryStatusBusiness enquiryStatusBusiness, IProgressStatusBusiness progressStatusBusiness, IFollowUpBusiness followupBusiness,IReminderBusiness reminderBusiness)
        {
            _enquiryBusiness = enquiryBusiness;
            _industryBusiness = industryBusiness;
            _employeeBusiness = employeeBusiness;
            _enquirySourceBusiness = enquirySourceBusiness;
            _enquiryStatusBusiness = enquiryStatusBusiness;
            _progressStatusBusiness = progressStatusBusiness;
            _followupBusiness = followupBusiness;
            _reminderBusiness = reminderBusiness;
           
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

            selectListItem = new List<SelectListItem>();
            EVM.titleObj = new TitlesViewModel();
            List<TitlesViewModel> titlesList = Mapper.Map<List<Titles>, List<TitlesViewModel>>(_enquiryBusiness.GetAllTitles());
            //titlesList = titlesList == null ? null : titlesList.OrderBy(attset => attset.Title).ToList();
            foreach (TitlesViewModel tvm in titlesList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = tvm.Title,
                    Value = tvm.Title,
                    Selected = false
                });
            }
            EVM.titleObj.TitleList = selectListItem;

            selectListItem = new List<SelectListItem>();
            EVM.reminderObj = new ReminderViewModel();
            List<ReminderViewModel> reminderList = Mapper.Map<List<Reminder>, List<ReminderViewModel>>(_reminderBusiness.GetAllReminders());
            foreach (ReminderViewModel rvm in reminderList)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = rvm.ReminderDesc,
                    Value = rvm.Code,
                    Selected = false
                });
            }
            EVM.reminderObj.ReminderList = selectListItem;

            return View(EVM);
        }

        //------------To Get FollowUp List-----//


        [AuthSecurityFilter(ProjectObject = "Enquiry", Mode = "R")]
        public ActionResult FollowUps(FollowUpViewModel followObj)
        {

            List<FollowUpViewModel> followUpObj = Mapper.Map<List<FollowUp>, List<FollowUpViewModel>>(_followupBusiness.GetFollowUpDetails(followObj.EnquiryID != null && followObj.EnquiryID.ToString() != "" ? Guid.Parse(followObj.EnquiryID.ToString()) : Guid.Empty));
            int openCount = followUpObj == null ? 0 : followUpObj.Where(Q => Q.Status == "Open").Select(T => T.ID).Count();

            FollowUpListViewModel Result = new FollowUpListViewModel();
            Result.FollowUpList = followUpObj;
            Result.FlwID = followObj.ID;
            //Result.EnqID = followObj.EnquiryID;
            ViewBag.Count = openCount;


            return PartialView("_FollowUpList", Result);
        }
        //------------To Get FollowUp List-----//


        #region InsertUpdateFollowUp
        [HttpPost]
        public string InsertUpdateFollowUp(EnquiryViewModel _enquiryObj)
        {
            //FollowUpViewModel _followupObj = new FollowUpViewModel();
            try
            {
                AppUA _appUA = Session["AppUA"] as AppUA;
                _enquiryObj.followUpObj.commonObj = new CommonViewModel();
                _enquiryObj.followUpObj.commonObj.CreatedBy = _appUA.UserName;
                _enquiryObj.followUpObj.commonObj.CreatedDate = DateTime.Now;
                _enquiryObj.followUpObj.commonObj.UpdatedDate = DateTime.Now;
                _enquiryObj.followUpObj.commonObj.UpdatedBy = _appUA.UserName;
                FollowUpViewModel followupObj = Mapper.Map<FollowUp, FollowUpViewModel>(_followupBusiness.InsertUpdateFollowUp(Mapper.Map<FollowUpViewModel, FollowUp>(_enquiryObj.followUpObj)));

                if (_enquiryObj.followUpObj.ID == Guid.Empty)
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = followupObj, Message = "Insertion successfull" });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = followupObj, Message = "Updation successfull" });
                }


            }
            catch (Exception ex)
            {

                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }

        }

        #endregion InsertUpdateFollowUp


        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Enquiry", Mode = "R")]
        public string GetAllEnquiry(EnquiryViewModel data)
        {
            try
            {
                List<EnquiryViewModel> EnquiryList = Mapper.Map<List<Enquiry>, List<EnquiryViewModel>>(_enquiryBusiness.GetAllEnquiryList(Mapper.Map<EnquiryViewModel, Enquiry>(data)));
                int openCount = EnquiryList == null ? 0 : EnquiryList.Where(Q => Q.EnquiryStatus == "OE").Select(T => T.ID).Count();
                int convertedCount = EnquiryList == null ? 0 : EnquiryList.Where(Q => Q.EnquiryStatus == "CE").Select(T => T.ID).Count();
                int notconvertedCount = EnquiryList == null ? 0 : EnquiryList.Where(Q => Q.EnquiryStatus == "NCE").Select(T => T.ID).Count();

                return JsonConvert.SerializeObject(new { Result = "OK", Records = EnquiryList,Open= openCount,Converted= convertedCount,NotConverted= notconvertedCount });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }

        #region InsertUpdateEnquiry
        [HttpPost]
        public string InsertUpdateEnquiry(EnquiryViewModel _enquiriesObj)
         {
            //object resultFromBusiness = null;

            try
            {
                AppUA _appUA = Session["AppUA"] as AppUA;
                _enquiriesObj.commonObj = new CommonViewModel();
                _enquiriesObj.commonObj.CreatedBy = _appUA.UserName;
                _enquiriesObj.commonObj.CreatedDate = DateTime.Now;
                _enquiriesObj.commonObj.UpdatedDate = DateTime.Now;
                EnquiryViewModel enquiryObj = Mapper.Map <  Enquiry, EnquiryViewModel>(_enquiryBusiness.InsertUpdateEnquiry(Mapper.Map<EnquiryViewModel, Enquiry>(_enquiriesObj)));
               
                if (_enquiriesObj.ID == Guid.Empty)
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = enquiryObj, Message = "Insertion successfull" });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = enquiryObj, Message = "Updation successfull" });
                }


            }
            catch (Exception ex)
            {

                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }

        }

        #endregion InsertUpdateEnquiry


        #region GetEnquiryDetailsByID
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Enquiry", Mode = "R")]
        public string GetEnquiryDetailsByID(Guid ID)
        {
            EnquiryViewModel enquiryObj = Mapper.Map <Enquiry, EnquiryViewModel>(_enquiryBusiness.GetEnquiryDetailsByID(ID != null && ID.ToString() != "" ? Guid.Parse(ID.ToString()) : Guid.Empty));

            return JsonConvert.SerializeObject(new { Result = "OK", Records = enquiryObj });

        }
        #endregion GetEnquiryDetailsByID


        #region GetFollowUpDetails
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Enquiry", Mode = "R")]
        public string GetFollowUpDetails(Guid ID)
        {
            List<FollowUpViewModel> followupObj = Mapper.Map<List<FollowUp>, List<FollowUpViewModel>>(_followupBusiness.GetFollowUpDetails(ID != null && ID.ToString() != "" ? Guid.Parse(ID.ToString()) : Guid.Empty));

            return JsonConvert.SerializeObject(new { Result = "OK", Records = followupObj });

        }
        #endregion GetFollowUpDetails




        #region GetFollowUpDetailByFollowUpId
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Enquiry", Mode = "R")]
        public string GetFollowUpDetailByFollowUpId(Guid ID)
        {
            FollowUpViewModel followupObj = Mapper.Map< FollowUp,FollowUpViewModel> (_followupBusiness.GetFollowupDetailsByFollowUpID(ID != null && ID.ToString() != "" ? Guid.Parse(ID.ToString()) : Guid.Empty));

            return JsonConvert.SerializeObject(new { Result = "OK", Records = followupObj });

        }
        #endregion GetFollowUpDetailByFollowUpId

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
                    ToolboxViewModelObj.addbtn.Event = "Add();";


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