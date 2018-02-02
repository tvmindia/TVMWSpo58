using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.BusinessService.Contracts
{
    public interface IEmployeeBusiness
    {
        List<Employee> GetAllEmployees();
        List<SalesPerson> GetAllSalesPersons();
        object InsertEmployee(Employee employeeObj);
        object UpdateEmployee(Employee empObj);
    }
}
