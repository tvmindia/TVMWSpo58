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
    public class CompanyRepository: ICompanyRepository
    {
        Settings s = new Settings();
        private IDatabaseFactory _databaseFactory;
        public CompanyRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        public List<Company> GetAllCompanies()
        {
            List<Company> companyList = null;
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
                        cmd.CommandText = "[Accounts].[GetCompanies]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                companyList = new List<Company>();
                                while (sdr.Read())
                                {
                                    Company _companyObj = new Company();
                                    {
                                        _companyObj.Code = (sdr["Code"].ToString() != "" ? (sdr["Code"].ToString()) : _companyObj.Code);
                                        _companyObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _companyObj.Name);
                                        _companyObj.ShippingAddress = (sdr["ShippingAddress"].ToString() != "" ? sdr["ShippingAddress"].ToString() : _companyObj.ShippingAddress);
                                        _companyObj.BillingAddress = (sdr["BillingAddress"].ToString() != "" ? sdr["BillingAddress"].ToString() : _companyObj.BillingAddress);
                                    }
                                    companyList.Add(_companyObj);
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

            return companyList;
        }
    }
}