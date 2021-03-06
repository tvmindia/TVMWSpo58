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
                                        _customerObj.PODate = (sdr["PODate"].ToString() != "" ? DateTime.Parse(sdr["PODate"].ToString()).ToString(s.dateformat) : _customerObj.PODate);
                                        _customerObj.POTitle = (sdr["POTitle"].ToString() != "" ? sdr["POTitle"].ToString() : _customerObj.POTitle);
                                        _customerObj.POStatus = (sdr["Description"].ToString() != "" ? sdr["Description"].ToString() : _customerObj.POStatus);
                                        _customerObj.POKeywords = (sdr["POKeywords"].ToString() != "" ? sdr["POKeywords"].ToString() : _customerObj.POKeywords);
                                        _customerObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _customerObj.GeneralNotes);
                                        _customerObj.POContent = (sdr["POContent"].ToString() != "" ? sdr["POContent"].ToString() : _customerObj.POContent);
                                        _customerObj.TaxTypeCode = (sdr["TaxTypeCode"].ToString() != "" ? sdr["TaxTypeCode"].ToString() : _customerObj.TaxTypeCode);
                                        _customerObj.POTitle = (sdr["POTitle"].ToString() != "" ? sdr["POTitle"].ToString() : _customerObj.POTitle);
                                        _customerObj.POToCompAddress = (sdr["CustomerBillingAddress"].ToString() != "" ? sdr["CustomerBillingAddress"].ToString() : _customerObj.POToCompAddress);
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

        public List<CustomerPO> GetAllCustomerPurchaseOrders()
        {
            List<CustomerPO> customerPOList = null;
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
                        cmd.CommandText = "[Office].[GetAllCustomerPurchaseOrders]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                customerPOList = new List<CustomerPO>();
                                while (sdr.Read())
                                {
                                    CustomerPO _customerObj = new CustomerPO();
                                    {
                                        _customerObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? Guid.Parse(sdr["CustomerID"].ToString()) : _customerObj.CustomerID);
                                        _customerObj.customer = new Customer();
                                        {
                                            _customerObj.customer.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _customerObj.customer.ID);
                                            _customerObj.customer.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _customerObj.customer.CompanyName);
                                            _customerObj.customer.BillingAddress = (sdr["CustBillingAddress"].ToString() != "" ? sdr["CustBillingAddress"].ToString() : _customerObj.customer.BillingAddress);
                                            _customerObj.customer.ShippingAddress = (sdr["CustShippingAddress"].ToString() != "" ? sdr["CustShippingAddress"].ToString() : _customerObj.customer.ShippingAddress);
                                        }
                                        _customerObj.BillingAddress = (sdr["CustomerBillingAddress"].ToString() != "" ? sdr["CustomerBillingAddress"].ToString() : _customerObj.BillingAddress);
                                        _customerObj.ShippingAddress = (sdr["CustomerShippingAddress"].ToString() != "" ? sdr["CustomerShippingAddress"].ToString() : _customerObj.ShippingAddress);
                                        _customerObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _customerObj.ID);
                                        _customerObj.PONo = (sdr["PONo"].ToString() != "" ? sdr["PONo"].ToString() : _customerObj.PONo);
                                        _customerObj.POReceivedDate = (sdr["POReceivedDate"].ToString() != "" ? DateTime.Parse(sdr["POReceivedDate"].ToString()).ToString(s.dateformat) : _customerObj.POReceivedDate);
                                        _customerObj.PODate = (sdr["PODate"].ToString() != "" ? DateTime.Parse(sdr["PODate"].ToString()).ToString(s.dateformat) : _customerObj.PODate);
                                        _customerObj.POTitle = (sdr["POTitle"].ToString() != "" ? sdr["POTitle"].ToString() : _customerObj.POTitle);
                                        _customerObj.POStatus = (sdr["POStatusDescription"].ToString() != "" ? sdr["POStatusDescription"].ToString() : _customerObj.POStatus);
                                        _customerObj.purchaseOrderStatus = new PurchaseOrderStatus();
                                        {
                                            _customerObj.purchaseOrderStatus.Code = (sdr["POStatus"].ToString() != "" ? sdr["POStatus"].ToString() : _customerObj.purchaseOrderStatus.Code);
                                            _customerObj.purchaseOrderStatus.Description = (sdr["POStatusDescription"].ToString() != "" ? sdr["POStatusDescription"].ToString() : _customerObj.purchaseOrderStatus.Description);
                                        }
                                        _customerObj.POKeywords = (sdr["POKeywords"].ToString() != "" ? sdr["POKeywords"].ToString() : _customerObj.POKeywords);
                                        _customerObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _customerObj.GeneralNotes);
                                        _customerObj.POContent = (sdr["POContent"].ToString() != "" ? sdr["POContent"].ToString() : _customerObj.POContent);
                                        _customerObj.TaxTypeCode = (sdr["TaxTypeCode"].ToString() != "" ? sdr["TaxTypeCode"].ToString() : _customerObj.TaxTypeCode);
                                        _customerObj.taxType = new TaxType();
                                        {
                                            _customerObj.taxType.Code = (sdr["TaxTypeCode"].ToString() != "" ? sdr["TaxTypeCode"].ToString() : _customerObj.taxType.Code);
                                            _customerObj.taxType.Description = (sdr["TaxTypeDescription"].ToString() != "" ? sdr["TaxTypeDescription"].ToString() : _customerObj.taxType.Description);
                                            _customerObj.taxType.Rate = (sdr["TaxRate"].ToString() != "" ? decimal.Parse(sdr["TaxRate"].ToString()) : _customerObj.taxType.Rate);
                                        }
                                        _customerObj.POTitle = (sdr["POTitle"].ToString() != "" ? sdr["POTitle"].ToString() : _customerObj.POTitle);

                                        _customerObj.POToCompCode = (sdr["POToCompCode"].ToString() != "" ? sdr["POToCompCode"].ToString() : _customerObj.POToCompCode);
                                        _customerObj.company = new Company();
                                        {
                                            _customerObj.company.Name = (sdr["Company"].ToString() != "" ? sdr["Company"].ToString() : _customerObj.company.Name);
                                            _customerObj.company.Code = (sdr["POToCompCode"].ToString() != "" ? sdr["POToCompCode"].ToString() : _customerObj.company.Code);
                                            _customerObj.company.BillingAddress = (sdr["POToBillingAddress"].ToString() != "" ? sdr["POToBillingAddress"].ToString() : _customerObj.company.BillingAddress);
                                            _customerObj.company.ShippingAddress = (sdr["POToShippingAddress"].ToString() != "" ? sdr["POToShippingAddress"].ToString() : _customerObj.company.ShippingAddress);
                                        }

                                        _customerObj.CustomerName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _customerObj.CustomerName);
                                     
                                        _customerObj.TaxAmount = (sdr["TaxAmount"].ToString() != "" ? decimal.Parse(sdr["TaxAmount"].ToString()) : _customerObj.TaxAmount);
                                        _customerObj.Discount = (sdr["Discount"].ToString() != "" ? decimal.Parse(sdr["Discount"].ToString()) : _customerObj.Discount);
                                        _customerObj.TaxPercApplied = (sdr["TaxPercApplied"].ToString() != "" ? decimal.Parse(sdr["TaxPercApplied"].ToString()) : _customerObj.TaxPercApplied);
                                        _customerObj.GrossAmount = (sdr["GrossAmount"].ToString() != "" ? decimal.Parse(sdr["GrossAmount"].ToString()) : _customerObj.GrossAmount);

                                    }
                                    customerPOList.Add(_customerObj);
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

            return customerPOList;
        }


        public object InsertPurchaseOrder(CustomerPO customerPO)
        {
            SqlParameter outputStatus, outputID;
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
                        cmd.CommandText = "[Office].[InsertCustomerPurchaseOrder]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PONo", SqlDbType.VarChar, 20).Value = customerPO.PONo;
                        cmd.Parameters.Add("@PODate", SqlDbType.DateTime).Value = customerPO.PODate;
                        cmd.Parameters.Add("@POReceivedDate", SqlDbType.DateTime).Value = customerPO.PODate;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.UniqueIdentifier).Value = customerPO.CustomerID;
                        cmd.Parameters.Add("@POToCompCode", SqlDbType.VarChar, 10).Value = customerPO.POToCompCode;
                        cmd.Parameters.Add("@CustomerBillingAddress", SqlDbType.NVarChar, -1).Value = customerPO.BillingAddress;
                        cmd.Parameters.Add("@CustomerShippingAddress", SqlDbType.NVarChar, -1).Value = customerPO.ShippingAddress;
                        cmd.Parameters.Add("@POTitle", SqlDbType.VarChar, 500).Value = customerPO.POTitle;
                        cmd.Parameters.Add("@POContent", SqlDbType.NVarChar, -1).Value = customerPO.POContent;

                        cmd.Parameters.Add("@POStatus", SqlDbType.VarChar, 10).Value = customerPO.POStatus;
                        cmd.Parameters.Add("@POKeywords", SqlDbType.VarChar, 250).Value = customerPO.POKeywords;
                        cmd.Parameters.Add("@GrossAmount", SqlDbType.Decimal).Value = customerPO.GrossAmount;
                        cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = customerPO.Discount;


                        cmd.Parameters.Add("@TaxTypeCode", SqlDbType.VarChar, 10).Value = customerPO.TaxTypeCode;
                        cmd.Parameters.Add("@TaxPercApplied", SqlDbType.Decimal).Value = customerPO.TaxPercApplied;
                        cmd.Parameters.Add("@TaxAmount", SqlDbType.Decimal).Value = customerPO.TaxAmount;
                        cmd.Parameters.Add("@GeneralNotes", SqlDbType.NVarChar, -1).Value = customerPO.GeneralNotes;
                        cmd.Parameters.Add("@FileDupID", SqlDbType.UniqueIdentifier).Value = customerPO.hdnFileID;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = customerPO.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = customerPO.commonObj.CreatedDate;
                        outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;
                        outputID = cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier);
                        outputID.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                    }
                }
                AppConst Cobj = new AppConst();
                switch (outputStatus.Value.ToString())
                {
                    case "0":
                        throw new Exception(Cobj.InsertFailure);
                    case "1":
                        //success
                        return new
                        {
                            ID = outputID.Value.ToString(),
                            Status = outputStatus.Value.ToString(),
                            Message = Cobj.InsertSuccess
                        };

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return new
            {
                ID = outputID.Value.ToString(),
                Status = outputStatus.Value.ToString(),
                Message = Cobj.InsertSuccess
            };
        }

        public object UpdatePurchaseOrder(CustomerPO customerPO)
        {
            SqlParameter outputStatus = null;
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
                        cmd.CommandText = "[Office].[UpdateCustomerPurchaseOrder]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = customerPO.ID;
                        cmd.Parameters.Add("@PONo", SqlDbType.VarChar, 20).Value = customerPO.PONo;
                        cmd.Parameters.Add("@PODate", SqlDbType.DateTime).Value = customerPO.PODate;
                        cmd.Parameters.Add("@POReceivedDate", SqlDbType.DateTime).Value = customerPO.PODate;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.UniqueIdentifier).Value = customerPO.CustomerID;
                        cmd.Parameters.Add("@POToCompCode", SqlDbType.VarChar, 10).Value = customerPO.POToCompCode;
                        cmd.Parameters.Add("@CustomerBillingAddress", SqlDbType.NVarChar, -1).Value = customerPO.BillingAddress;
                        cmd.Parameters.Add("@CustomerShippingAddress", SqlDbType.NVarChar, -1).Value = customerPO.ShippingAddress;
                        cmd.Parameters.Add("@POTitle", SqlDbType.VarChar, 500).Value = customerPO.POTitle;
                        cmd.Parameters.Add("@POContent", SqlDbType.NVarChar, -1).Value = customerPO.POContent;

                        cmd.Parameters.Add("@POStatus", SqlDbType.VarChar, 10).Value = customerPO.POStatus;
                        cmd.Parameters.Add("@POKeywords", SqlDbType.VarChar, 250).Value = customerPO.POKeywords;
                        cmd.Parameters.Add("@GrossAmount", SqlDbType.Decimal).Value = customerPO.GrossAmount;
                        cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = customerPO.Discount;


                        cmd.Parameters.Add("@TaxTypeCode", SqlDbType.VarChar, 10).Value = customerPO.TaxTypeCode;
                        cmd.Parameters.Add("@TaxPercApplied", SqlDbType.Decimal).Value = customerPO.TaxPercApplied;
                        cmd.Parameters.Add("@TaxAmount", SqlDbType.Decimal).Value = customerPO.TaxAmount;
                        cmd.Parameters.Add("@GeneralNotes", SqlDbType.NVarChar, -1).Value = customerPO.GeneralNotes;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = customerPO.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = customerPO.commonObj.UpdatedDate;
                        outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();


                    }
                }
                AppConst Cobj = new AppConst();
                switch (outputStatus.Value.ToString())
                {
                    case "0":

                        throw new Exception(Cobj.UpdateFailure);

                    case "1":

                        return new
                        {
                            Status = outputStatus.Value.ToString(),
                            Message = Cobj.UpdateSuccess
                        };
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return new
            {
                Status = outputStatus.Value.ToString(),
                Message = Cobj.UpdateSuccess
            };
        }
        public object DeletePurchaseOrder(Guid ID)
        {
            SqlParameter outputStatus = null;
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
                        cmd.CommandText = "[Office].[DeleteCustomerPurchaseOrder]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = ID;
                        outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();


                    }
                }

                switch (outputStatus.Value.ToString())
                {
                    case "0":

                        throw new Exception(Cobj.DeleteFailure);

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return new
            {
                Status = outputStatus.Value.ToString(),
                Message = Cobj.DeleteSuccess
            };
        }

        #region GetCustomerPOSummary
        public CustomerPOSummary GetCustomerPOSummary()
        {
            CustomerPOSummary CPOS = null;
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
                        cmd.CommandText = "[Office].[GetCustomerPOSummary]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                CPOS = new CustomerPOSummary();
                                if (sdr.Read())
                                {

                                    {
                                        CPOS.Total = (sdr["Total"].ToString() != "" ? int.Parse(sdr["Total"].ToString()) : CPOS.Total);
                                        CPOS.Closed = (sdr["Closed"].ToString() != "" ? int.Parse(sdr["Closed"].ToString()) : CPOS.Closed);
                                        CPOS.open = (sdr["Open"].ToString() != "" ? int.Parse(sdr["Open"].ToString()) : CPOS.open);
                                        CPOS.InProgress = (sdr["InProgress"].ToString() != "" ? int.Parse(sdr["InProgress"].ToString()) : CPOS.InProgress);
                                    }

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

            return CPOS;
        }
        #endregion GetAllCustomers


        public CustomerPO GetCustomerPODetailsByID(Guid ID)
        {
            CustomerPO _customerObj = new CustomerPO();
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
                        cmd.CommandText = "[Accounts].[GetCustomerPODetailByID]";
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = ID;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                while (sdr.Read())
                                {
                                        _customerObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? Guid.Parse(sdr["CustomerID"].ToString()) : _customerObj.CustomerID);
                                        _customerObj.customer = new Customer();
                                        {
                                            _customerObj.customer.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _customerObj.customer.ID);
                                            _customerObj.customer.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _customerObj.customer.CompanyName);
                                            _customerObj.customer.BillingAddress = (sdr["CustBillingAddress"].ToString() != "" ? sdr["CustBillingAddress"].ToString() : _customerObj.customer.BillingAddress);
                                            _customerObj.customer.ShippingAddress = (sdr["CustShippingAddress"].ToString() != "" ? sdr["CustShippingAddress"].ToString() : _customerObj.customer.ShippingAddress);
                                        }
                                        _customerObj.BillingAddress = (sdr["CustomerBillingAddress"].ToString() != "" ? sdr["CustomerBillingAddress"].ToString() : _customerObj.BillingAddress);
                                        _customerObj.ShippingAddress = (sdr["CustomerShippingAddress"].ToString() != "" ? sdr["CustomerShippingAddress"].ToString() : _customerObj.ShippingAddress);
                                        _customerObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _customerObj.ID);
                                        _customerObj.PONo = (sdr["PONo"].ToString() != "" ? sdr["PONo"].ToString() : _customerObj.PONo);
                                        _customerObj.POReceivedDate = (sdr["POReceivedDate"].ToString() != "" ? DateTime.Parse(sdr["POReceivedDate"].ToString()).ToString(s.dateformat) : _customerObj.POReceivedDate);
                                        _customerObj.PODate = (sdr["PODate"].ToString() != "" ? DateTime.Parse(sdr["PODate"].ToString()).ToString(s.dateformat) : _customerObj.PODate);
                                        _customerObj.POTitle = (sdr["POTitle"].ToString() != "" ? sdr["POTitle"].ToString() : _customerObj.POTitle);
                                        _customerObj.POStatus = (sdr["POStatusDescription"].ToString() != "" ? sdr["POStatusDescription"].ToString() : _customerObj.POStatus);
                                        _customerObj.purchaseOrderStatus = new PurchaseOrderStatus();
                                        {
                                            _customerObj.purchaseOrderStatus.Code = (sdr["POStatus"].ToString() != "" ? sdr["POStatus"].ToString() : _customerObj.purchaseOrderStatus.Code);
                                            _customerObj.purchaseOrderStatus.Description = (sdr["POStatusDescription"].ToString() != "" ? sdr["POStatusDescription"].ToString() : _customerObj.purchaseOrderStatus.Description);
                                        }
                                        _customerObj.POKeywords = (sdr["POKeywords"].ToString() != "" ? sdr["POKeywords"].ToString() : _customerObj.POKeywords);
                                        _customerObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _customerObj.GeneralNotes);
                                        _customerObj.POContent = (sdr["POContent"].ToString() != "" ? sdr["POContent"].ToString() : _customerObj.POContent);
                                        _customerObj.TaxTypeCode = (sdr["TaxTypeCode"].ToString() != "" ? sdr["TaxTypeCode"].ToString() : _customerObj.TaxTypeCode);
                                        _customerObj.taxType = new TaxType();
                                        {
                                            _customerObj.taxType.Code = (sdr["TaxTypeCode"].ToString() != "" ? sdr["TaxTypeCode"].ToString() : _customerObj.taxType.Code);
                                            _customerObj.taxType.Description = (sdr["TaxTypeDescription"].ToString() != "" ? sdr["TaxTypeDescription"].ToString() : _customerObj.taxType.Description);
                                            _customerObj.taxType.Rate = (sdr["TaxRate"].ToString() != "" ? decimal.Parse(sdr["TaxRate"].ToString()) : _customerObj.taxType.Rate);
                                        }
                                        _customerObj.POTitle = (sdr["POTitle"].ToString() != "" ? sdr["POTitle"].ToString() : _customerObj.POTitle);

                                        _customerObj.POToCompCode = (sdr["POToCompCode"].ToString() != "" ? sdr["POToCompCode"].ToString() : _customerObj.POToCompCode);
                                        _customerObj.company = new Company();
                                        {
                                            _customerObj.company.Name = (sdr["Company"].ToString() != "" ? sdr["Company"].ToString() : _customerObj.company.Name);
                                            _customerObj.company.Code = (sdr["POToCompCode"].ToString() != "" ? sdr["POToCompCode"].ToString() : _customerObj.company.Code);
                                            _customerObj.company.BillingAddress = (sdr["POToBillingAddress"].ToString() != "" ? sdr["POToBillingAddress"].ToString() : _customerObj.company.BillingAddress);
                                            _customerObj.company.ShippingAddress = (sdr["POToShippingAddress"].ToString() != "" ? sdr["POToShippingAddress"].ToString() : _customerObj.company.ShippingAddress);
                                        }

                                        _customerObj.CustomerName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _customerObj.CustomerName);
                                        _customerObj.TaxAmount = (sdr["TaxAmount"].ToString() != "" ? decimal.Parse(sdr["TaxAmount"].ToString()) : _customerObj.TaxAmount);
                                        _customerObj.Discount = (sdr["Discount"].ToString() != "" ? decimal.Parse(sdr["Discount"].ToString()) : _customerObj.Discount);
                                        _customerObj.TaxPercApplied = (sdr["TaxPercApplied"].ToString() != "" ? decimal.Parse(sdr["TaxPercApplied"].ToString()) : _customerObj.TaxPercApplied);
                                        _customerObj.GrossAmount = (sdr["GrossAmount"].ToString() != "" ? decimal.Parse(sdr["GrossAmount"].ToString()) : _customerObj.GrossAmount);

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

            return _customerObj;
        }



        #region InsertCustomer
        public Customer InsertCustomer(Customer customerObj)
        {
            try
            {
                SqlParameter outputStatus, outputID = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[Accounts].[InsertCustomers]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar, 150).Value = customerObj.CompanyName;
                        cmd.Parameters.Add("@IsInternalComp", SqlDbType.Bit).Value = customerObj.IsInternalComp;
                        cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar, 100).Value = customerObj.ContactPerson;
                        cmd.Parameters.Add("@ContactEmail", SqlDbType.VarChar, 150).Value = customerObj.ContactEmail;
                        cmd.Parameters.Add("@ContactTitle", SqlDbType.VarChar, 10).Value = customerObj.ContactTitle;
                        cmd.Parameters.Add("@Website", SqlDbType.NVarChar, 500).Value = customerObj.Website;
                        cmd.Parameters.Add("@LandLine", SqlDbType.VarChar, 50).Value = customerObj.LandLine;
                        cmd.Parameters.Add("@Mobile", SqlDbType.VarChar, 50).Value = customerObj.Mobile;
                        cmd.Parameters.Add("@Fax", SqlDbType.VarChar, 50).Value = customerObj.Fax;
                        cmd.Parameters.Add("@OtherPhoneNos", SqlDbType.VarChar, 250).Value = customerObj.OtherPhoneNos;
                        cmd.Parameters.Add("@BillingAddress", SqlDbType.NVarChar, -1).Value = customerObj.BillingAddress;
                        cmd.Parameters.Add("@ShippingAddress", SqlDbType.NVarChar, -1).Value = customerObj.ShippingAddress;
                        cmd.Parameters.Add("@PaymentTermCode", SqlDbType.VarChar, 10).Value = customerObj.PaymentTermCode;
                        cmd.Parameters.Add("@TaxRegNo", SqlDbType.VarChar, 50).Value = customerObj.TaxRegNo;
                        cmd.Parameters.Add("@PANNo", SqlDbType.VarChar, 50).Value = customerObj.PANNO;
                        cmd.Parameters.Add("@GeneralNotes", SqlDbType.NVarChar, -1).Value = customerObj.GeneralNotes;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = customerObj.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = customerObj.commonObj.CreatedDate;
                        outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;
                        outputID = cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier);
                        outputID.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();


                    }
                }

                switch (outputStatus.Value.ToString())
                {
                    case "0":
                        AppConst Cobj = new AppConst();
                        throw new Exception(Cobj.InsertFailure);
                    case "1":
                        customerObj.ID = Guid.Parse(outputID.Value.ToString());
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return customerObj;
        }
        #endregion InsertCustomer

        #region UpdateCustomer
        public object UpdateCustomer(Customer _customerObj)
        {
            SqlParameter outputStatus = null;
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
                        cmd.CommandText = "[Accounts].[UpdateCustomer]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = _customerObj.ID;
                        cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar, 150).Value = _customerObj.CompanyName;
                        cmd.Parameters.Add("@IsInternalComp", SqlDbType.Bit).Value = _customerObj.IsInternalComp;
                        cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar, 100).Value = _customerObj.ContactPerson;
                        cmd.Parameters.Add("@ContactEmail", SqlDbType.VarChar, 150).Value = _customerObj.ContactEmail;
                        cmd.Parameters.Add("@ContactTitle", SqlDbType.VarChar, 10).Value = _customerObj.ContactTitle;
                        cmd.Parameters.Add("@Website", SqlDbType.NVarChar, 500).Value = _customerObj.Website;
                        cmd.Parameters.Add("@LandLine", SqlDbType.VarChar, 50).Value = _customerObj.LandLine;
                        cmd.Parameters.Add("@Mobile", SqlDbType.VarChar, 50).Value = _customerObj.Mobile;
                        cmd.Parameters.Add("@Fax", SqlDbType.VarChar, 50).Value = _customerObj.Fax;
                        cmd.Parameters.Add("@OtherPhoneNos", SqlDbType.VarChar, 250).Value = _customerObj.OtherPhoneNos;
                        cmd.Parameters.Add("@BillingAddress", SqlDbType.NVarChar, -1).Value = _customerObj.BillingAddress;
                        cmd.Parameters.Add("@ShippingAddress", SqlDbType.NVarChar, -1).Value = _customerObj.ShippingAddress;
                        cmd.Parameters.Add("@PaymentTermCode", SqlDbType.VarChar, 10).Value = _customerObj.PaymentTermCode;
                        cmd.Parameters.Add("@TaxRegNo", SqlDbType.VarChar, 50).Value = _customerObj.TaxRegNo;
                        cmd.Parameters.Add("@PANNo", SqlDbType.VarChar, 50).Value = _customerObj.PANNO;
                        cmd.Parameters.Add("@GeneralNotes", SqlDbType.NVarChar, -1).Value = _customerObj.GeneralNotes;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = _customerObj.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = _customerObj.commonObj.UpdatedDate;
                        outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();


                    }
                }

                switch (outputStatus.Value.ToString())
                {
                    case "0":

                        throw new Exception(Cobj.UpdateFailure);

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return new
            {
                Status = outputStatus.Value.ToString(),
                Message = Cobj.UpdateSuccess
            };
        }
        #endregion UpdateCustomer
    }
}