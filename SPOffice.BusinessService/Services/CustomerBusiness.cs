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

        public List<Customer> GetAllCustomersForMobile(Customer duration)
        {
            return _customerRepository.GetAllCustomersForMobile(duration);
        }
    }
}