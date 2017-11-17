using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.BusinessService.Contracts
{
   public interface ICustomerBusiness
    {
        List<Customer> GetAllCustomers();
        Customer GetCustomerDetailsByID(Guid ID);
        List<CustomerPO> GetAllCustomerPOForMobile(string duration);
        CustomerPO GetCustomerPODetailsByID(Guid ID);
        List<CustomerPO> GetAllCustomerPurchaseOrders();
        CustomerPO GetCustomerPurchaseOrderByID(Guid ID);
        object InsertPurchaseOrder(CustomerPO customerPO);
        object UpdatePurchaseOrder(CustomerPO customerPO);
        object DeletePurchaseOrder(Guid ID);
    }
}
