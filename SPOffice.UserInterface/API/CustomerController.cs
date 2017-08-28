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

            ICustomerBusiness _customerBusiness;


            public CustomerController(ICustomerBusiness customerBusiness)
            {
                _customerBusiness = customerBusiness;

            }
            #endregion Constructor_Injection

            Const messages = new Const();

            [HttpPost]
            public object GetCustomerPODetail(CustomerPOViewModel customerObj)
            {
                try
                {
                    List<CustomerPOViewModel> CustomerList = Mapper.Map<List<CustomerPO>, List<CustomerPOViewModel>>(_customerBusiness.GetAllCustomerPOForMobile(customerObj.duration));
                    //if (CustomerList.Count == 0) throw new Exception(messages.NoItems);
                    return JsonConvert.SerializeObject(new { Result = true, Records = CustomerList });
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
                }
            }

        }
    }
