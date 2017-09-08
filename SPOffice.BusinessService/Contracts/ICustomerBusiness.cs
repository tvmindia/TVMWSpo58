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
        List<CustomerPO> GetAllCustomerPOForMobile(string duration);


        List<CustomerPO> GetAllCustomerPurchaseOrders();
        object InsertPurchaseOrder(CustomerPO customerPO);
        object UpdatePurchaseOrder(CustomerPO customerPO);
        object DeletePurchaseOrder(Guid ID);
    }
}
