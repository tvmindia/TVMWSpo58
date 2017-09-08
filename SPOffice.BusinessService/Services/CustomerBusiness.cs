using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    
        public class CustomerBusiness : ICustomerBusiness
        {
            private ICustomerRepository _customerRepository;

            public CustomerBusiness(ICustomerRepository customerRepository)
            {
                _customerRepository = customerRepository;
            }

     

        public List<CustomerPO> GetAllCustomerPOForMobile(string duration)
        {
            return _customerRepository.GetAllCustomerPOForMobile(duration);
        }

        

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }

        public object InsertPurchaseOrder(CustomerPO customerPO)
        {
            throw new NotImplementedException();
        }

        public object UpdatePurchaseOrder(CustomerPO customerPO)
        {
            throw new NotImplementedException();
        }

        public object DeletePurchaseOrder(Guid ID)
        {
            throw new NotImplementedException();
        }
        public List<CustomerPO> GetAllCustomerPurchaseOrders()
        {
            throw new NotImplementedException();
        }
    }
}