using AutoMapper;
using Newtonsoft.Json;
using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.UserInterface.SecurityFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace SPOffice.UserInterface.Controllers
{
    public class ProductController : Controller
    {
        AppConst c = new AppConst();
        IProductBusiness _productBusiness;
        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }
        // GET: Product
        [AuthSecurityFilter(ProjectObject = "Product", Mode = "R")]
        public ActionResult Index()
        {
           
            ProductViewModel productViewModel = new ProductViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            productViewModel.unitViewModelList = Mapper.Map<List<Unit>, List<UnitViewModel>>(_productBusiness.GetAllUnits());
            if (productViewModel.unitViewModelList != null)
            {
              
                foreach (UnitViewModel uvm in productViewModel.unitViewModelList)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = uvm.Description,
                        Value = uvm.Code.ToString(),
                        Selected = false
                    });
                }
            }

            productViewModel.unitList = selectListItem;
            return View(productViewModel);
           
        }
        #region GetAllProducts
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Product", Mode = "R")]
        public string GetAllProducts()
        {
            try
            {
                List<ProductViewModel> productViewModelList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetAllProducts());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = productViewModelList });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion  GetAllProducts

        #region GetProductDetails
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Product", Mode = "R")]
        public string GetProductDetails(string ID)
        {
            try
            {

                ProductViewModel ProductObj = Mapper.Map<Product, ProductViewModel>(_productBusiness.GetProductDetails(Guid.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = ProductObj });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }
        }
        #endregion  GetProductDetails

        #region InsertUpdateProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthSecurityFilter(ProjectObject = "Product", Mode = "W")]
        public string InsertUpdateProduct(ProductViewModel productViewModel)
        {
            object result = null;
            try
            {
                AppUA _appUA = Session["AppUA"] as AppUA;
                productViewModel.commonObj = new CommonViewModel();
                productViewModel.commonObj.CreatedBy = _appUA.UserName;
                productViewModel.commonObj.CreatedDate = _appUA.DateTime;
                productViewModel.commonObj.UpdatedBy = productViewModel.commonObj.CreatedBy;
                productViewModel.commonObj.UpdatedDate = productViewModel.commonObj.CreatedDate;
                switch (string.IsNullOrEmpty(productViewModel.ID.ToString()))
                {
                    case true:
                        result = _productBusiness.InsertProduct(Mapper.Map<ProductViewModel, Product>(productViewModel));
                        break;
                    case false:
                        result = _productBusiness.UpdateProduct(Mapper.Map<ProductViewModel, Product>(productViewModel));
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
        #endregion InsertUpdateProduct

        #region DeleteProduct
        [HttpGet]
        [AuthSecurityFilter(ProjectObject = "Product", Mode = "D")]
        public string DeleteProduct(string ID)
        {

            try
            {
                object result = null;

                result = _productBusiness.DeleteProduct(Guid.Parse(ID));
                return JsonConvert.SerializeObject(new { Result = "OK", Message = result });

            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }


        }

        #endregion DeleteProduct

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