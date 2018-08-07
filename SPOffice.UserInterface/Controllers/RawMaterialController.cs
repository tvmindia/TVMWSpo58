using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.UserInterface.Models;
using SPOffice.UserInterface.SecurityFilter;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace SPOffice.UserInterface.Controllers
{
    public class RawMaterialController : Controller
    {
        AppConst c = new AppConst();
        IRawMaterialBusiness _rawMaterialBusiness;
        IUnitsBusiness _unitsBusiness;
        //string Key = "X-Api-Key";
        //string Value = "JyFgHsICUOgloskIMuyM6PH4GYxyU30p";
        public RawMaterialController(IRawMaterialBusiness rawMaterialBusiness, IUnitsBusiness unitsBusiness)
        {
            _rawMaterialBusiness = rawMaterialBusiness;
            _unitsBusiness = unitsBusiness;
        }
        // GET: RawMaterial
        [AuthSecurityFilter(ProjectObject = "RawMaterial", Mode = "R")]
        public ActionResult Index()
        {
            RawMaterialViewModel rawMaterialViewModel = new RawMaterialViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            rawMaterialViewModel.materialTypeList = Mapper.Map<List<MaterialType>, List<MaterialTypeViewModel>>(_rawMaterialBusiness.GetAllMaterialType());
            if (rawMaterialViewModel.materialTypeList != null)
            {
                foreach (MaterialTypeViewModel uvm in rawMaterialViewModel.materialTypeList)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = uvm.Description,
                        Value = uvm.Code.ToString(),
                        Selected = false
                    });
                }
            }

            rawMaterialViewModel.RawMaterialList = selectListItem;

            selectListItem = new List<SelectListItem>();
            List<UnitsViewModel> unitsList = Mapper.Map<List<Units>, List<UnitsViewModel>>(_unitsBusiness.GetAllUnits());
            foreach (UnitsViewModel US in unitsList)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = US.Description,
                        Value = US.UnitsCode,
                        Selected = false
                    });

                }
        rawMaterialViewModel.UnitList = selectListItem;
            return View(rawMaterialViewModel);
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
                        //string Status = result.GetType().GetProperty("Status").GetValue(result, null).ToString();
                        //if (Status == "1")
                        //{


                        //    string ID = result.GetType().GetProperty("ID").GetValue(result, null).ToString();

                        //    string json_data;
                        //    string response;
                        //    if (rawMaterialViewModel.Type == "SP")
                        //    {
                        //        SpareAPI test = new SpareAPI();
                        //        test.id = "8";
                        //        test.name = rawMaterialViewModel.MaterialCode;
                        //        test.sku = rawMaterialViewModel.MaterialCode;
                        //        test.description = rawMaterialViewModel.Description;
                        //        test.is_enable_serial_no = "0";
                        //        test.uom = "1";

                        //        json_data = "{ \"spare\" :" + JsonConvert.SerializeObject(test) + " } ";
                        //        response = InvokePostRequest("http://secure.appdeal.in/sp2/rest/web/spare/save-spare#", json_data);
                        //        ResponseAPI res = new ResponseAPI();
                        //        res = JsonConvert.DeserializeObject<ResponseAPI>(response);
                        //        if (res.status == "0")
                        //        {
                        //            object DelteRaw = _rawMaterialBusiness.DeleteRawMaterial(Guid.Parse(ID));
                        //        }
                        //        // dynamic stuff = JsonConvert.DeserializeObject(response);

                        //    }
                        //    else if (rawMaterialViewModel.Type == "RM")
                        //    {
                        //        MaterialAPI material = new MaterialAPI();
                        //        material.id = "0";
                        //        material.product_id = null;
                        //        material.material_name = "dummy material";
                        //        material.sku = rawMaterialViewModel.MaterialCode;
                        //        material.description = rawMaterialViewModel.Description;
                        //        material.uom = "0";
                        //        material.msq = "200";

                        //        json_data = "{ \"Phase1Material\" :" + JsonConvert.SerializeObject(material) + " } ";
                        //        response = InvokePostRequest("http://secure.appdeal.in/sp2/rest/web/material/save-material#", json_data);
                        //        ResponseAPI res = new ResponseAPI();
                        //        res = JsonConvert.DeserializeObject<ResponseAPI>(response);
                        //        if (res.status == "0")
                        //        {
                        //            object DelteRaw = _rawMaterialBusiness.DeleteRawMaterial(Guid.Parse(ID));
                        //        }
                        //    }

                        //    //  string json_data = "{ \"spare\" :" + JsonConvert.SerializeObject(test) + " } ";

                        //    //  var response = InvokePostRequest("http://secure.appdeal.in/sp2/rest/web/spare/save-spare#", json_data);
                        //    //var  res = (JObject)JsonConvert.DeserializeObject(response);
                        //    //  var result1 = res.SelectToken("spare.status").ToString();
                        //    //string status = res.GetType().GetProperty("status").GetValue(res,null).ToString();
                        //}
                            break;
                    case false:
                        result = _rawMaterialBusiness.UpdateRawMaterial(Mapper.Map<RawMaterialViewModel, RawMaterial>(rawMaterialViewModel));
                        //string Status1 = result.GetType().GetProperty("Status").GetValue(result, null).ToString();
                        //if (Status1 == "1")
                        //{


                        //  //  string ID = result.GetType().GetProperty("ID").GetValue(result, null).ToString();

                        //    string json_data;
                        //    string response;
                        //    if (rawMaterialViewModel.Type == "SP")
                        //    {
                        //        SpareAPI test = new SpareAPI();
                        //        test.id = "8";
                        //        test.name = rawMaterialViewModel.MaterialCode;
                        //        test.sku = rawMaterialViewModel.MaterialCode;
                        //        test.description = rawMaterialViewModel.Description;
                        //        test.is_enable_serial_no = "0";
                        //        test.uom = "1";

                        //        json_data = "{ \"spare\" :" + JsonConvert.SerializeObject(test) + " } ";
                        //        response = InvokePostRequest("http://secure.appdeal.in/sp2/rest/web/spare/save-spare#", json_data);
                        //        ResponseAPI res = new ResponseAPI();
                        //        res = JsonConvert.DeserializeObject<ResponseAPI>(response);
                        //        //if (res.status == "0")
                        //        //{
                        //        //    object DelteRaw = _rawMaterialBusiness.DeleteRawMaterial(Guid.Parse(ID));
                        //        //}
                        //        // dynamic stuff = JsonConvert.DeserializeObject(response);

                        //    }
                        //    else if (rawMaterialViewModel.Type == "RM")
                        //    {
                        //        MaterialAPI material = new MaterialAPI();
                        //        material.id = "0";
                        //        material.product_id = null;
                        //        material.material_name = "dummy material";
                        //        material.sku = rawMaterialViewModel.MaterialCode;
                        //        material.description = rawMaterialViewModel.Description;
                        //        material.uom = "0";
                        //        material.msq = "200";

                        //        json_data = "{ \"Phase1Material\" :" + JsonConvert.SerializeObject(material) + " } ";
                        //        response = InvokePostRequest("http://secure.appdeal.in/sp2/rest/web/material/save-material#", json_data);
                        //        ResponseAPI res = new ResponseAPI();
                        //        res = JsonConvert.DeserializeObject<ResponseAPI>(response);
                        //        //if (res.status == "0")
                        //        //{
                        //        //    object DelteRaw = _rawMaterialBusiness.DeleteRawMaterial(Guid.Parse(ID));
                        //        //}
                        //    }
                        // }

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
        //public string InvokePostRequest(string requestUrl, string requestBody)
        //{


        //    try
        //    {
        //        var request = WebRequest.Create(requestUrl) as HttpWebRequest;
        //      //  request.Headers.Add("Key", Key);
        //        request.Headers.Add(Key, Value);
        //        request.ContentType = "application/json";
        //        request.Method = @"POST";

        //        var requestWriter = new StreamWriter(request.GetRequestStream());
        //        requestWriter.Write(requestBody);
        //        requestWriter.Close();
        //        var webResponse = (HttpWebResponse)request.GetResponse();

        //        var responseReader = new StreamReader(webResponse.GetResponseStream());
        //        return responseReader.ReadToEnd();
        //    }
        //    catch (WebException ex)
        //    {
        //        if (ex.Response != null)
        //        {
        //            //reading the custom messages sent by the server
        //            using (var reader = new StreamReader(ex.Response.GetResponseStream()))
        //            {
        //                return reader.ReadToEnd();
        //            }
        //        }
        //        return ex.Message + ex.InnerException + "--error. " + "RequestString:::::::::::: " + requestBody;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message + ex.InnerException + "--error. " + "RequestString:::::::::::: " + requestBody;
        //    }


        //}




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

                    ToolboxViewModelObj.PrintBtn.Visible = true;
                    ToolboxViewModelObj.PrintBtn.Text = "Export";
                    ToolboxViewModelObj.PrintBtn.Title = "Export";
                    ToolboxViewModelObj.PrintBtn.Event = "PrintReport();";
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