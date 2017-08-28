using SPOffice.BusinessService.Contracts;
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

        public List<SalesPerson> GetAllSalesPersons()
        {
            List<SalesPerson> salesPersonList = _employeeRepository.GetAllSalesPersons();
            return salesPersonList;
        }
    }
}