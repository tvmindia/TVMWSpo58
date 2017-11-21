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
    public class UnitsRepository : IUnitsRepository
    {
        AppConst Cobj = new AppConst();
        Settings setting = new Settings();
        private IDatabaseFactory _databaseFactory;
        public UnitsRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }



        public object DeleteUnits(string code)
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
                        cmd.CommandText = "[Office].[DeleteUnits]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@code", SqlDbType.VarChar,15).Value = code;
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

        public List<Units> GetAllUnits()
        {
            List<Units> unitsList = null;
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
                        cmd.CommandText = "[Office].[GetAllUnits]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                unitsList = new List<Units>();
                                while (sdr.Read())
                                {
                                    Units _units = new Units();
                                    {
                                        _units.UnitsCode = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : _units.UnitsCode);
                                        _units.Description = (sdr["Description"].ToString() != "" ? sdr["Description"].ToString() : _units.Description);
                                        _units.commonObj = new Common();
                                        {
                                            _units.commonObj.CreatedBy = (sdr["CreatedBy"].ToString() != "" ? sdr["CreatedBy"].ToString() : _units.commonObj.CreatedBy);
                                            _units.commonObj.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : _units.commonObj.CreatedDate);
                                            _units.commonObj.CreatedDateString = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()).ToString(setting.dateformat) : string.Empty);
                                        }
                                    }
                                    unitsList.Add(_units);
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
            return unitsList;
        } 

        public object InsertUnits(Units unitsObj)
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
                        cmd.CommandText = "[Office].[InsertUnits]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@code", SqlDbType.VarChar, 15).Value = unitsObj.UnitsCode;
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, -1).Value = unitsObj.Description;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = unitsObj.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = unitsObj.commonObj.CreatedDate;
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

                    case "1":
                        return new
                        {
                            Code = unitsObj.UnitsCode,
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
                ID = unitsObj.UnitsCode,
                Status = outputStatus.Value.ToString(),
                Message = Cobj.InsertSuccess
            };
        }

        public object UpdateUnits(Units unitsObj)
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
                        cmd.CommandText = "[Office].[UpdateUnits]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@code", SqlDbType.VarChar, 15).Value = unitsObj.hdnCode;
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, -1).Value = unitsObj.Description;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = unitsObj.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = unitsObj.commonObj.UpdatedDate;
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
                            Code = unitsObj.hdnCode,
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
                Code = unitsObj.hdnCode,
                Status = outputStatus.Value.ToString(),
                Message = Cobj.UpdateSuccess
            };
        }
    }
}