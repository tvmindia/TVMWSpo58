using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.RepositoryServices.Contracts
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        List<SalesPerson> GetAllSalesPersons();
        Employee InsertEmployee(Employee employeeObj);
        object UpdateEmployee(Employee empObj);
        List<Employee> GetAllRequisitionByEmployee();
    }
}
