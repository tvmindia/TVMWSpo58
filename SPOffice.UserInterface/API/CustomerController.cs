using AutoMapper;
using Newtonsoft.Json;
using SAMTool.DataAccessObject.DTO;
using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.UserInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserInterface.Models;

namespace SPOffice.UserInterface.API
{
    public class CustomerController : ApiController
    {

        #region Constructor_Injection
        AppConst c = new AppConst();
        string auth = System.Web.Configuration.WebConfigurationManager.AppSettings["APIkey"];
        ICustomerBusiness _customerBusiness;


        public CustomerController(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;

        }
        #endregion Constructor_Injection

        Const messages = new Const();
        #region GetCustomerPODetails
        [HttpPost]
        public object GetCustomerPODetail(CustomerPOViewModel customerPOObj)
        {
            try
            {
                List<CustomerPOViewModel> CustomerList = Mapper.Map<List<CustomerPO>, List<CustomerPOViewModel>>(_customerBusiness.GetAllCustomerPOForMobile(customerPOObj.duration));
                //if (CustomerList.Count == 0) throw new Exception(messages.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CustomerList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        #endregion GetCustomerPODetails

        #region GetCustomerPODetailsByID
        [HttpPost]
        public string GetCustomerPODetailsByID(CustomerPOViewModel cust)
        {
            try
            {

                CustomerPOViewModel customerPOViewModel = Mapper.Map<CustomerPO, CustomerPOViewModel>(_customerBusiness.GetCustomerPODetailsByID(cust.ID != null && cust.ID.ToString() != "" ? Guid.Parse(cust.ID.ToString()) : Guid.Empty));
                return JsonConvert.SerializeObject(new { Result = true, Record = customerPOViewModel });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }
        }

        #endregion GetCustomerDetailsByID

        #region InsertCustomer
        [HttpPost]
        public string InsertCustomer(CustomerViewModel customersObj)
        {
            object result = null;
            try
            {
                if (customersObj.APIKey == auth)
                {
                    customersObj.commonObj = new CommonViewModel();
                    customersObj.commonObj.CreatedBy = customersObj.UserName;
                    SAMTool.DataAccessObject.DTO.Common commObj = new SAMTool.DataAccessObject.DTO.Common();
                    customersObj.commonObj.CreatedDate = commObj.GetCurrentDateTime();
                    if ((customersObj.ID.ToString()) == Guid.Empty.ToString()) 
                    {
                        result = _customerBusiness.InsertCustomer(Mapper.Map<CustomerViewModel, Customer>(customersObj));
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
        #endregion InsertCustomer

        #region UpdateCustomer
        [HttpPost]
        public string UpdateCustomer(CustomerViewModel customersObj)
        {
            object result = null;
            try
            {
                if (customersObj.APIKey == auth)
                {
                    customersObj.commonObj = new CommonViewModel();
                    customersObj.commonObj.UpdatedBy = customersObj.UserName;
                    SAMTool.DataAccessObject.DTO.Common commObj = new SAMTool.DataAccessObject.DTO.Common();
                    customersObj.commonObj.UpdatedDate = commObj.GetCurrentDateTime();

                    result = _customerBusiness.UpdateCustomer(Mapper.Map<CustomerViewModel, Customer>(customersObj));
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
        #endregion UpdateCustomer
    }
}