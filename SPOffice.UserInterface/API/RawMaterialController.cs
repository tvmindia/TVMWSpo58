using AutoMapper;
using Newtonsoft.Json;
using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.UserInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SPOffice.UserInterface.API
{
    public class RawMaterialController : ApiController
    {
        AppConst c = new AppConst();
        IRawMaterialBusiness _rawMaterialBusiness;
        IUnitsBusiness _unitsBusiness;
        public RawMaterialController(IRawMaterialBusiness rawMaterialBusiness, IUnitsBusiness unitsBusiness)
        {
            _rawMaterialBusiness = rawMaterialBusiness;
            _unitsBusiness = unitsBusiness;
        }
        #region GetAllMaterialType
        [HttpPost]
        public string GetAllRawMaterials()
        {
            try
            {
                List<RawMaterialViewModel> rawMaterialList = Mapper.Map<List<RawMaterial>, List<RawMaterialViewModel>>(_rawMaterialBusiness.GetAllRawMaterial());
                return JsonConvert.SerializeObject(new { Result = true, Records = rawMaterialList });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }
        }
        #endregion  GetAllMaterialType
    }
}