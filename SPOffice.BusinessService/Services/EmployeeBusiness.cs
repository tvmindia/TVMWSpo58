﻿using SPOffice.BusinessService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;

namespace SPOffice.BusinessService.Services
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeBusiness(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public List<Employee> GetAllEmployees()
        {
            List<Employee> empList = new List<Employee>();
            empList = _employeeRepository.GetAllEmployees();
            empList = empList != null ? empList.Where(e => e.EmployeeType=="EMP").ToList() : null;
            return empList;
        }
        public List<Employee> GetAllRequisitionByEmployee()
        {
            List<Employee> empList = new List<Employee>();
            empList = _employeeRepository.GetAllRequisitionByEmployee();
            return empList;
        }
        
        public List<SalesPerson> GetAllSalesPersons()
        {
            List<SalesPerson> salesPersonList = _employeeRepository.GetAllSalesPersons();
            return salesPersonList;
        }


        public object InsertEmployee(Employee employeeObj)
        {
            object result = null;
            try
            {
                    result = _employeeRepository.InsertEmployee(employeeObj);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        public object UpdateEmployee(Employee empObj)
        {
            object result = null;
            try
            {
              result = _employeeRepository.UpdateEmployee(empObj);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}