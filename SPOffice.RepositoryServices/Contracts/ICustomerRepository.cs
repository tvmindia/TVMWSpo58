using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.RepositoryServices.Contracts
{
    public interface ICustomerRepository
    {
        List<CustomerPO> GetAllCustomerPOForMobile(string duration);
        List<Customer> GetAllCustomers();


        List<CustomerPO> GetAllCustomerPurchaseOrders();
        object InsertPurchaseOrder(CustomerPO customerPO);
        object UpdatePurchaseOrder(CustomerPO customerPO);
        object DeletePurchaseOrder(Guid ID);
        CustomerPOSummary GetCustomerPOSummary();
    }
}
