﻿using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SPOffice.RepositoryServices.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        AppConst Cobj = new AppConst();
        Settings s = new Settings();
        private IDatabaseFactory _databaseFactory;
        public CustomerRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        #region GetAllCustomerMobile
        public List<CustomerPO> GetAllCustomerPOForMobile(string duration)
        {
            List<CustomerPO> customerList = null;
            try
            {
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[Office].[GetAllCustomerOrder]";
                        cmd.Parameters.Add("@duration", SqlDbType.Int).Value = duration;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                customerList = new List<CustomerPO>();
                                while (sdr.Read())
                                {
                                    CustomerPO _customerObj = new CustomerPO();
                                    {
                                        _customerObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? Guid.Parse(sdr["CustomerID"].ToString()) : _customerObj.CustomerID);
                                        _customerObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _customerObj.ID);
                                        _customerObj.PONo = (sdr["PONo"].ToString() != "" ? sdr["PONo"].ToString() : _customerObj.PONo);
                                        _customerObj.POReceivedDate = (sdr["POReceivedDate"].ToString() != "" ? (sdr["POReceivedDate"].ToString()) : _customerObj.POReceivedDate);
                                        _customerObj.PODate = (sdr["PODate"].ToString() != "" ? DateTime.Parse(sdr["PODate"].ToString()).ToString(s.dateformat): _customerObj.PODate);
                                        _customerObj.POTitle = (sdr["POTitle"].ToString() != "" ? sdr["POTitle"].ToString() : _customerObj.POTitle);
                                        _customerObj.POStatus = (sdr["Description"].ToString() != "" ? sdr["Description"].ToString() : _customerObj.POStatus);
                                        _customerObj.POKeywords = (sdr["POKeywords"].ToString() != "" ? sdr["POKeywords"].ToString() : _customerObj.POKeywords);
                                        _customerObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _customerObj.GeneralNotes);
                                        _customerObj.POContent = (sdr["POContent"].ToString() != "" ? sdr["POContent"].ToString() : _customerObj.POContent);
                                        _customerObj.TaxTypeCode = (sdr["TaxTypeCode"].ToString() != "" ? sdr["TaxTypeCode"].ToString() : _customerObj.TaxTypeCode);
                                        _customerObj.POTitle = (sdr["POTitle"].ToString() != "" ? sdr["POTitle"].ToString() : _customerObj.POTitle);
                                        _customerObj.POToCompAddress= (sdr["POToCompAddress"].ToString() != "" ? sdr["POToCompAddress"].ToString() : _customerObj.POToCompAddress);
                                        _customerObj.POToCompCode = (sdr["POToCompCode"].ToString() != "" ? sdr["POToCompCode"].ToString() : _customerObj.POToCompCode);
                                        _customerObj.CustomerName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _customerObj.CustomerName);
                                        _customerObj.TaxAmount = (sdr["TaxAmount"].ToString() != "" ? decimal.Parse(sdr["TaxAmount"].ToString()) : _customerObj.TaxAmount);
                                        _customerObj.Discount = (sdr["Discount"].ToString() != "" ? decimal.Parse(sdr["Discount"].ToString()) : _customerObj.Discount);
                                        _customerObj.TaxPercApplied = (sdr["TaxPercApplied"].ToString() != "" ? decimal.Parse(sdr["TaxPercApplied"].ToString()) : _customerObj.TaxPercApplied);
                                        _customerObj.GrossAmount = (sdr["GrossAmount"].ToString() != "" ? decimal.Parse(sdr["GrossAmount"].ToString()) : _customerObj.GrossAmount);
                                        _customerObj.Amount = (sdr["Amount"].ToString() != "" ? decimal.Parse(sdr["Amount"].ToString()) : _customerObj.Amount);
                                    }
                                    customerList.Add(_customerObj);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return customerList;
        }


        #endregion GetAllCustomerMobile

        #region GetAllCustomers
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customerList = null;
            try
            {
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[Accounts].[GetCustomers]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                customerList = new List<Customer>();
                                while (sdr.Read())
                                {
                                    Customer _customerObj = new Customer();
                                    {
                                        _customerObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _customerObj.ID);
                                        _customerObj.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _customerObj.CompanyName);
                                        _customerObj.ContactPerson = (sdr["ContactPerson"].ToString() != "" ? sdr["ContactPerson"].ToString() : _customerObj.ContactPerson);
                                        _customerObj.ContactEmail = (sdr["ContactEmail"].ToString() != "" ? sdr["ContactEmail"].ToString() : _customerObj.ContactEmail);
                                        _customerObj.ContactTitle = (sdr["ContactTitle"].ToString() != "" ? sdr["ContactTitle"].ToString() : _customerObj.ContactTitle);
                                        _customerObj.Website = (sdr["Website"].ToString() != "" ? sdr["Website"].ToString() : _customerObj.Website);
                                        _customerObj.LandLine = (sdr["LandLine"].ToString() != "" ? sdr["LandLine"].ToString() : _customerObj.LandLine);
                                        _customerObj.Mobile = (sdr["Mobile"].ToString() != "" ? sdr["Mobile"].ToString() : _customerObj.Mobile);
                                        _customerObj.Fax = (sdr["Fax"].ToString() != "" ? sdr["Fax"].ToString() : _customerObj.Fax);
                                        _customerObj.OtherPhoneNos = (sdr["OtherPhoneNos"].ToString() != "" ? sdr["OtherPhoneNos"].ToString() : _customerObj.OtherPhoneNos);
                                        _customerObj.BillingAddress = (sdr["BillingAddress"].ToString() != "" ? sdr["BillingAddress"].ToString() : _customerObj.BillingAddress);
                                        _customerObj.ShippingAddress = (sdr["ShippingAddress"].ToString() != "" ? sdr["ShippingAddress"].ToString() : _customerObj.ShippingAddress);
                                        _customerObj.PaymentTermCode = (sdr["PaymentTermCode"].ToString() != "" ? sdr["PaymentTermCode"].ToString() : _customerObj.PaymentTermCode);
                                        _customerObj.TaxRegNo = (sdr["TaxRegNo"].ToString() != "" ? sdr["TaxRegNo"].ToString() : _customerObj.TaxRegNo);
                                        _customerObj.PANNO = (sdr["PANNO"].ToString() != "" ? sdr["PANNO"].ToString() : _customerObj.PANNO);
                                        _customerObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _customerObj.GeneralNotes);
                                        _customerObj.commonObj = new Common();
                                        _customerObj.commonObj.CreatedBy = (sdr["CreatedBy"].ToString() != "" ? sdr["CreatedBy"].ToString() : _customerObj.commonObj.CreatedBy);
                                        _customerObj.commonObj.CreatedDateString = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()).ToString(s.dateformat) : _customerObj.commonObj.CreatedDateString);
                                        _customerObj.commonObj.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : _customerObj.commonObj.CreatedDate);
                                        _customerObj.commonObj.UpdatedBy = (sdr["UpdatedBy"].ToString() != "" ? sdr["UpdatedBy"].ToString() : _customerObj.commonObj.UpdatedBy);
                                        _customerObj.commonObj.UpdatedDate = (sdr["UpdatedDate"].ToString() != "" ? DateTime.Parse(sdr["UpdatedDate"].ToString()) : _customerObj.commonObj.UpdatedDate);
                                        _customerObj.commonObj.UpdatedDateString = (sdr["UpdatedDate"].ToString() != "" ? DateTime.Parse(sdr["UpdatedDate"].ToString()).ToString(s.dateformat) : _customerObj.commonObj.UpdatedDateString);

                                    }
                                    customerList.Add(_customerObj);
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return customerList;
        }
        #endregion GetAllCustomers

    }
}