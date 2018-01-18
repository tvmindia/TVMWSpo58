using AutoMapper;
using Newtonsoft.Json;
using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserInterface.Models;

namespace SPOffice.UserInterface.API
{
    public class ProductController : ApiController
    {
        AppConst c = new AppConst();
    #region Constructor_Injection

        IProductBusiness _productBusiness;
        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }
        #endregion Constructor_Injection

     #region Insert or Update a product

        public string InsertUpdateProduct(ProductViewModel productViewModel)
        {
            object result = null;
            try
            {
                productViewModel.commonObj = new CommonViewModel();
                productViewModel.commonObj.CreatedBy = c.AppUser;
                productViewModel.commonObj.CreatedDate = DateTime.Now;
                productViewModel.commonObj.UpdatedBy = c.AppUser;
                productViewModel.commonObj.UpdatedDate = DateTime.Now;
                switch (string.IsNullOrEmpty(productViewModel.ID.ToString()))
                {
                    case true:
                        result = _productBusiness.InsertProduct(Mapper.Map<ProductViewModel, Product>(productViewModel));
                        break;
                    case false:
                        result = _productBusiness.UpdateProduct(Mapper.Map<ProductViewModel, Product>(productViewModel));
                        break;
                }

                return JsonConvert.SerializeObject(new { Result = true, Record = result });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }
        }

        #endregion Insert or Update a product
    }
}
