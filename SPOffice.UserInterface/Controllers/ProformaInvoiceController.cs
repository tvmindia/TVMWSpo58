using AutoMapper;
using Newtonsoft.Json;
using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.UserInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace SPOffice.UserInterface.Controllers
{
    public class ProformaInvoiceController : Controller
    {
        AppConst c = new AppConst();
        IProformaInvoiceBusiness _proformaInvoiceBusiness;
        //ICustomerBusiness _customerBusiness;
        //ICompanyBusiness _companyBusiness;
        //IEmployeeBusiness _employeeBusiness;
        //ITaxTypeBusiness _taxTypeBusiness;
        //IProductBusiness _productBusiness;
        public ProformaInvoiceController(IProformaInvoiceBusiness proformaInvoiceBusiness)
        {
            _proformaInvoiceBusiness = proformaInvoiceBusiness;
            //_customerBusiness = customerBusiness;
            //_companyBusiness = companyBusiness;
            //_employeeBusiness = employeeBusiness;
            //_taxTypeBusiness = taxTypeBusiness;
            //_productBusiness = productBusiness;
        }


        #region GetAllProformaInvoice
        [HttpGet]
        public string GetAllProformaInvoices()
        {
            try
            {
                List<ProformaInvoiceViewModel> proformaHeaderViewModelList = Mapper.Map<List<ProformaInvoice>, List<ProformaInvoiceViewModel>>(_proformaInvoiceBusiness.GetAllProformaInvoices());
                
                return JsonConvert.SerializeObject(new { Result = "OK", Records = proformaHeaderViewModelList });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion GetAllProformaInvoice
        // GET: ProformaInvoice
        public ActionResult Index()
        {
            return View();
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

                    //ToolboxViewModelObj.backbtn.Visible = true;
                    //ToolboxViewModelObj.backbtn.Disable = true;
                    //ToolboxViewModelObj.backbtn.Text = "Back";
                    //ToolboxViewModelObj.backbtn.DisableReason = "Not applicable";
                    //ToolboxViewModelObj.backbtn.Event = "Back();";  
                    break;
                case "Edit":
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Text = "Add";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    ToolboxViewModelObj.addbtn.Event = "AddNew();";



                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Text = "Save";
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    ToolboxViewModelObj.savebtn.Event = "saveInvoices();";

                    ToolboxViewModelObj.resetbtn.Visible = true;
                    ToolboxViewModelObj.resetbtn.Text = "Reset";
                    ToolboxViewModelObj.resetbtn.Title = "Reset";
                    ToolboxViewModelObj.resetbtn.Event = "Reset();";

                    ToolboxViewModelObj.CloseBtn.Visible = true;
                    ToolboxViewModelObj.CloseBtn.Text = "Close";
                    ToolboxViewModelObj.CloseBtn.Title = "Close";
                    ToolboxViewModelObj.CloseBtn.Event = "closeNav();";

                    ToolboxViewModelObj.EmailBtn.Visible = true;
                    ToolboxViewModelObj.EmailBtn.Text = "Mail";
                    ToolboxViewModelObj.EmailBtn.Title = "Mail";
                    ToolboxViewModelObj.EmailBtn.Event = "PreviewMail()";

                    break;
                case "Add":

                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Text = "Add";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    ToolboxViewModelObj.addbtn.Event = "AddNew();";

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Text = "Save";
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    ToolboxViewModelObj.savebtn.Event = " saveInvoices();";

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