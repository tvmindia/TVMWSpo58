using AutoMapper;
using Newtonsoft.Json;
using SAMTool.DataAccessObject.DTO;
using SPOffice.BusinessService.Contracts;
using SPOffice.BusinessService.Services;
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
    public class SupplierController : ApiController
    {
        #region Constructor_Injection

        ISupplierBusiness _supplierBusiness;


        public SupplierController(SupplierBusiness supplierBusiness)
        {
            _supplierBusiness = supplierBusiness;

        }
        #endregion Constructor_Injection

        Const messages = new Const();

        #region GetAllSuppliersForMobile
        [HttpPost]
        public string GetAllSuppliersDetailForMobile()
        {
            try
            {
                List<SupplierViewModel> suppliersList = Mapper.Map<List<Supplier>, List<SupplierViewModel>>(_supplierBusiness.GetAllSuppliersForMobile());
                return JsonConvert.SerializeObject(new { Result = true, Records = suppliersList });
            }
            catch (Exception ex)
            {

                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        #endregion  GetAllSuppliersForMobile
    }
}
