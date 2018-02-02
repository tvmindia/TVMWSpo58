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
using UserInterface.Models;

namespace SPOffice.UserInterface.API
{
    public class EmployeeController : ApiController
    {
        AppConst c = new AppConst();
        string auth = System.Web.Configuration.WebConfigurationManager.AppSettings["APIkey"];
        #region Constructor_Injection

        IEmployeeBusiness _employeeBusiness;
        public EmployeeController(IEmployeeBusiness employeeBusiness)
        {
            _employeeBusiness = employeeBusiness;
        }
        #endregion Constructor_Injection




        #region Insert a Employee
        /// <summary>
        /// Insert a new product
        /// </summary>
        /// <param name="employeeVM"></param>
        /// <returns></returns>
        [HttpPost]
        public string InsertEmployee(EmployeeViewModel employeeVM)
        {
            object result = null;
            try
            {
                if (employeeVM.APIKey == auth)
                {
                    employeeVM.commonObj = new CommonViewModel();
                    employeeVM.commonObj.CreatedBy = employeeVM.UserName;
                    Common commObj = new Common();
                    employeeVM.commonObj.CreatedDate = commObj.GetCurrentDateTime();
                    if ((employeeVM.ID.ToString()) == Guid.Empty.ToString())
                    {
                        result = _employeeBusiness.InsertEmployee(Mapper.Map<EmployeeViewModel, Employee>(employeeVM));
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
        /// <param name="empObj"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateEmployee(EmployeeViewModel empObj)
        {
            object result = null;
            try
            {
                if (empObj.APIKey == auth)
                {
                    empObj.commonObj = new CommonViewModel();
                    empObj.commonObj.UpdatedBy = empObj.UserName;
                    Common commObj = new Common();
                    empObj.commonObj.UpdatedDate = commObj.GetCurrentDateTime();
                    result = _employeeBusiness.UpdateEmployee(Mapper.Map<EmployeeViewModel, Employee>(empObj));
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
