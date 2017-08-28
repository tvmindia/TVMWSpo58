using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPOffice.DataAccessObject.DTO;
using System.Data.SqlClient;
using System.Data;

namespace SPOffice.RepositoryServices.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        AppConst Cobj = new AppConst();
        Settings settings = new Settings();
        private IDatabaseFactory _databaseFactory;
        public EmployeeRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        #region GetAllEmployees
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employeeList = null;
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
                        cmd.CommandText = "[Accounts].[GetAllEmployees]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                employeeList = new List<Employee>();
                                while (sdr.Read())
                                {
                                    Employee _employee = new Employee();
                                    {
                                        _employee.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _employee.ID);
                                        _employee.Code = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : _employee.Code);
                                        _employee.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _employee.Name);
                                        _employee.Department = (sdr["Department"].ToString() != "" ? sdr["Department"].ToString() : _employee.Department);
                                        _employee.EmployeeCategory = (sdr["EmployeeCategory"].ToString() != "" ? sdr["EmployeeCategory"].ToString() : _employee.EmployeeCategory);
                                        _employee.MobileNo = (sdr["MobileNo"].ToString() != "" ? sdr["MobileNo"].ToString() : _employee.MobileNo);
                                        _employee.Address = (sdr["Address"].ToString() != "" ? sdr["Address"].ToString() : _employee.Address);
                                        _employee.employeeTypeObj = new EmployeeType()
                                        {
                                            Code = (sdr["EmpType"].ToString() != "" ? sdr["EmpType"].ToString() : string.Empty),
                                            Name = (sdr["EmployeeType"].ToString() != "" ? sdr["EmployeeType"].ToString() : string.Empty)
                                        };
                                        _employee.ImageURL = (sdr["ImageURL"].ToString() != "" ? sdr["ImageURL"].ToString() : _employee.ImageURL);

                                        _employee.company = new Company()
                                        {
                                            Code = (sdr["CompanyID"].ToString() != "" ? (sdr["CompanyID"].ToString()) : string.Empty),
                                            Name = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : string.Empty)
                                        };

                                        _employee.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _employee.GeneralNotes);


                                        _employee.commonObj = new Common()
                                        {
                                            CreatedDateString = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()).ToString(settings.dateformat) : string.Empty)
                                        };

                                    }

                                    employeeList.Add(_employee);
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
            return employeeList;
        }


        #endregion GetAllEmployees

        public List<SalesPerson> GetAllSalesPersons()
        {
            List<SalesPerson> SalesPersonList = null;
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
                        cmd.CommandText = "[Office].[GetSalesPersons]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                SalesPersonList = new List<SalesPerson>();
                                while (sdr.Read())
                                {
                                    SalesPerson _salesPerson = new SalesPerson();
                                    {
                                        _salesPerson.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _salesPerson.ID);
                                        _salesPerson.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _salesPerson.Name);
                                    }
                                    SalesPersonList.Add(_salesPerson);
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
            return SalesPersonList;
        }
    }
}