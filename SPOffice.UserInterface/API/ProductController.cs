﻿using AutoMapper;
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
        string auth = System.Web.Configuration.WebConfigurationManager.AppSettings["APIkey"];
        #region Constructor_Injection

        IProductBusiness _productBusiness;
        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }
        #endregion Constructor_Injection

     #region Insert a product
        /// <summary>
        /// Insert a new product
        /// </summary>
        /// <param name="productVM"></param>
        /// <returns></returns>
        public string InsertProduct(ProductViewModel productVM)
        {
            object result = null;
            try
            {
                if (productVM.AuthenticationKey == auth)
                {
                    productVM.commonObj = new CommonViewModel();
                    productVM.commonObj.CreatedBy = productVM.userObj.UserName;
                    Common commObj = new Common();
                    productVM.commonObj.CreatedDate = commObj.GetCurrentDateTime();
                    if (string.IsNullOrEmpty(productVM.ID.ToString()))
                    {
                        result = _productBusiness.InsertProduct(Mapper.Map<ProductViewModel, Product>(productVM));
                    }
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = false, Message = "Authentication Failed" });
                }
                    return JsonConvert.SerializeObject(new { Result = true, Record = result });
                
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }
        }

        #endregion Insert a product

       #region Update a product
        /// <summary>
        /// Update an existing product
        /// </summary>
        /// <param name="proObj"></param>
        /// <returns></returns>
        public string UpdateProduct(ProductViewModel proObj)
        {
            object result = null;
            try
            {
                if (proObj.AuthenticationKey == auth)
                {
                    proObj.commonObj = new CommonViewModel();
                    proObj.commonObj.UpdatedBy =proObj.userObj.UserName;
                    Common commObj = new Common();
                    proObj.commonObj.UpdatedDate = commObj.GetCurrentDateTime();
                    result = _productBusiness.UpdateProduct(Mapper.Map<ProductViewModel, Product>(proObj));
                }
                 else
                {
                    return JsonConvert.SerializeObject(new { Result = false, Message = "Authentication Failed" });
                }
                return JsonConvert.SerializeObject(new { Result = true, Record = result });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }
        }
     #endregion Update a product

    }
}
