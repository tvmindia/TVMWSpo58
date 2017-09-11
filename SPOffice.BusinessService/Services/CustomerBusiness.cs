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
          return  _customerRepository.InsertPurchaseOrder(customerPO);
        }

        public object UpdatePurchaseOrder(CustomerPO customerPO)
        {
            return _customerRepository.UpdatePurchaseOrder(customerPO);
        }

        public object DeletePurchaseOrder(Guid ID)
        {
            return _customerRepository.DeletePurchaseOrder(ID);
        }
        public List<CustomerPO> GetAllCustomerPurchaseOrders()
        {
            List<CustomerPO> CustomerPOList = null;
            try
            {
                CustomerPOList = _customerRepository.GetAllCustomerPurchaseOrders();
                CustomerPOList = CustomerPOList != null ? CustomerPOList.Select(Q => { Q.NetTaxableAmount = Q.GrossAmount - Q.Discount;Q.TotalAmount = Q.NetTaxableAmount + Q.TaxAmount; return Q; }).ToList() : new List<CustomerPO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CustomerPOList;
        }

        public CustomerPO GetCustomerPurchaseOrderByID(Guid ID)
        {
            List<CustomerPO> CustomerPOList = GetAllCustomerPurchaseOrders();
            CustomerPO customerPO = null;
            try
            {
               customerPO = CustomerPOList != null ? CustomerPOList.Where(Q => Q.ID == ID).SingleOrDefault() : null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
            return customerPO;
        }

        public Customer GetCustomerDetailsByID(Guid ID)
        {
            List<Customer> CustomerList = GetAllCustomers();
            Customer customer = null;
            try
            {
                customer = CustomerList != null ? CustomerList.Where(Q => Q.ID == ID).SingleOrDefault() : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customer;
        }
    }
}