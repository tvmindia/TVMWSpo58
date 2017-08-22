using SPOffice.DataAccessObject.DTO;
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
        public List<Customer> GetAllCustomersForMobile(string duration)
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
                        cmd.CommandText = "[Office].[GetAllCustomerOrder]";
                        cmd.Parameters.Add("@duration", SqlDbType.Int).Value = duration;
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
                                        _customerObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? Guid.Parse(sdr["CustomerID"].ToString()) : _customerObj.CustomerID);
                                        _customerObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _customerObj.ID);
                                        _customerObj.PONo = (sdr["PONo"].ToString() != "" ? sdr["PONo"].ToString() : _customerObj.PONo);
                                        _customerObj.POReceivedDate = (sdr["POReceivedDate"].ToString() != "" ? DateTime.Parse(sdr["POReceivedDate"].ToString()) : _customerObj.POReceivedDate);
                                        _customerObj.PODate = (sdr["PODate"].ToString() != "" ? DateTime.Parse(sdr["PODate"].ToString()) : _customerObj.PODate);
                                        _customerObj.POTitle = (sdr["POTitle"].ToString() != "" ? sdr["POTitle"].ToString() : _customerObj.POTitle);
                                        _customerObj.Description = (sdr["Description"].ToString() != "" ? sdr["Description"].ToString() : _customerObj.Description);
                                        _customerObj.POKeywords = (sdr["POKeywords"].ToString() != "" ? sdr["POKeywords"].ToString() : _customerObj.POKeywords);
                                        _customerObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _customerObj.GeneralNotes);
                                        _customerObj.POContent = (sdr["POContent"].ToString() != "" ? sdr["POContent"].ToString() : _customerObj.POContent);
                                        _customerObj.TaxTypeCode = (sdr["TaxTypeCode"].ToString() != "" ? sdr["TaxTypeCode"].ToString() : _customerObj.TaxTypeCode);
                                        _customerObj.POTitle = (sdr["POTitle"].ToString() != "" ? sdr["POTitle"].ToString() : _customerObj.POTitle);
                                        _customerObj.POToCompAddress= (sdr["POToCompAddress"].ToString() != "" ? sdr["POToCompAddress"].ToString() : _customerObj.POToCompAddress);
                                        _customerObj.POToCompCode = (sdr["POToCompCode"].ToString() != "" ? sdr["POToCompCode"].ToString() : _customerObj.POToCompCode);
                                        _customerObj.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _customerObj.CompanyName);
                                        _customerObj.Description = (sdr["Description"].ToString() != "" ? sdr["Description"].ToString() : _customerObj.Description);
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
    }
}