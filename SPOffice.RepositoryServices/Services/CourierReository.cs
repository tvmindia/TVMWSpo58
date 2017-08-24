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
    public class CourierRepository : ICourierRepository
    {
        AppConst Cobj = new AppConst();
        Settings s = new Settings();
        private IDatabaseFactory _databaseFactory;

        public CourierRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }


        public object DeleteCourierAgency(string Code)
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
                        cmd.CommandText = "[Office].[DeleteCourierAgency]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Code;
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

        public List<CourierAgency> GetAllCourierAgency()
        {
            List<CourierAgency> courierAgencyList = null;
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
                        cmd.CommandText = "[Office].[GetAllCourierAgencies]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                courierAgencyList = new List<CourierAgency>();
                                while (sdr.Read())
                                {
                                    CourierAgency _courierAgencyObj = new CourierAgency();
                                    {
                                        _courierAgencyObj.Code = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : _courierAgencyObj.Code);
                                        _courierAgencyObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _courierAgencyObj.Name);
                                        _courierAgencyObj.Website = (sdr["Website"].ToString() != "" ? sdr["Website"].ToString() : _courierAgencyObj.Website);
                                        _courierAgencyObj.Phone = (sdr["PhoneNos"].ToString() != "" ? sdr["PhoneNos"].ToString() : _courierAgencyObj.Phone);
                                        _courierAgencyObj.Fax = (sdr["Fax"].ToString() != "" ? sdr["Fax"].ToString() : _courierAgencyObj.Fax);
                                        _courierAgencyObj.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : _courierAgencyObj.Email);
                                        _courierAgencyObj.Address = (sdr["Address"].ToString() != "" ? sdr["Address"].ToString() : _courierAgencyObj.Address);
                                        _courierAgencyObj.commonObj = new Common();
                                        {
                                            _courierAgencyObj.commonObj.CreatedBy = (sdr["CreatedBy"].ToString() != "" ? sdr["CreatedBy"].ToString() : _courierAgencyObj.commonObj.CreatedBy);
                                            _courierAgencyObj.commonObj.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : _courierAgencyObj.commonObj.CreatedDate);
                                            _courierAgencyObj.commonObj.CreatedDateString = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()).ToString(s.dateformat) : string.Empty);
                                        }
                                    }
                                    courierAgencyList.Add(_courierAgencyObj);
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

            return courierAgencyList;
        }

        public object InsertCourierAgency(CourierAgency courierAgency)
        {
            SqlParameter outputStatus;
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
                        cmd.CommandText = "[Office].[InsertCourierAgency]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Code", SqlDbType.VarChar, 10).Value = courierAgency.Code;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 150).Value = courierAgency.Name;
                        cmd.Parameters.Add("@Website", SqlDbType.NVarChar, 500).Value = courierAgency.Website;
                        cmd.Parameters.Add("@PhoneNos", SqlDbType.VarChar, 250).Value = courierAgency.Phone;
                        cmd.Parameters.Add("@Fax", SqlDbType.VarChar, 50).Value = courierAgency.Fax;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 150).Value = courierAgency.Email;
                        cmd.Parameters.Add("@Address", SqlDbType.VarChar,-1).Value = courierAgency.Address;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = courierAgency.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = courierAgency.commonObj.CreatedDate;
                        outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();


                    }
                }
                AppConst Cobj = new AppConst();
                switch (outputStatus.Value.ToString())
                {
                    case "0":

                        throw new Exception(Cobj.InsertFailure);
                   
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return new
            {
                Status = outputStatus.Value.ToString(),
                Message = Cobj.InsertSuccess
            };
        }

        public object UpdateCourierAgency(CourierAgency courierAgency)
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
                        cmd.CommandText = "[Office].[UpdateCourierAgency]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Code", SqlDbType.VarChar, 10).Value = courierAgency.Code;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 150).Value = courierAgency.Name;
                        cmd.Parameters.Add("@Website", SqlDbType.NVarChar, 500).Value = courierAgency.Website;
                        cmd.Parameters.Add("@PhoneNos", SqlDbType.VarChar, 250).Value = courierAgency.Phone;
                        cmd.Parameters.Add("@Fax", SqlDbType.VarChar, 50).Value = courierAgency.Fax;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 150).Value = courierAgency.Email;
                        cmd.Parameters.Add("@Address", SqlDbType.NVarChar,-1).Value = courierAgency.Address;

                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = courierAgency.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = courierAgency.commonObj.UpdatedDate;
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
    }
}