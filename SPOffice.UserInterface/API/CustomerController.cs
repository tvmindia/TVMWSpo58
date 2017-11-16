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

namespace SPOffice.UserInterface.API
{
    public class CustomerController : ApiController
    {

        #region Constructor_Injection
        AppConst c = new AppConst();
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


    }
}
