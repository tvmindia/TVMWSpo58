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
    public class RawMaterialRepository : IRawMaterialRepository
    {

        AppConst Cobj = new AppConst();
        Settings setting = new Settings();
        private IDatabaseFactory _databaseFactory;
        public RawMaterialRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public object DeleteRawMaterial(Guid ID)
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
                        cmd.CommandText = "[Office].[DeleteRawMaterial]";
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

        public List<RawMaterial> GetAllRawMaterial(string Type)
        {
            List<RawMaterial> rawMaterialList = null;
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
                        cmd.CommandText = "[Office].[GetAllRawMaterials]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Type", SqlDbType.VarChar, 20).Value = Type != "" ? Type : null  ;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                rawMaterialList = new List<RawMaterial>();
                                while (sdr.Read())
                                {
                                    RawMaterial _rawMaterial = new RawMaterial();
                                    {
                                        _rawMaterial.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _rawMaterial.ID);
                                        _rawMaterial.MaterialCode = (sdr["MaterialCode"].ToString() != "" ? sdr["MaterialCode"].ToString() : _rawMaterial.MaterialCode);
                                        _rawMaterial.Code = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : _rawMaterial.Code);
                                        _rawMaterial.UnitsCode = (sdr["UnitsCode"].ToString() != "" ? sdr["UnitsCode"].ToString() : _rawMaterial.UnitsCode);
                                        _rawMaterial.Description = (sdr["Description"].ToString() != "" ? sdr["Description"].ToString() : _rawMaterial.Description);
                                        _rawMaterial.Unit = (sdr["Unit"].ToString() != "" ? sdr["Unit"].ToString() : _rawMaterial.Unit);
                                        _rawMaterial.ApproximateRate=(sdr["ApproximateRate"].ToString()!=""? decimal.Parse(sdr["ApproximateRate"].ToString()) :_rawMaterial.ApproximateRate);
                                        _rawMaterial.Type = (sdr["Type"].ToString());
                                        _rawMaterial.commonObj = new Common();
                                        {
                                            _rawMaterial.commonObj.CreatedBy = (sdr["CreatedBy"].ToString() != "" ? sdr["CreatedBy"].ToString() : _rawMaterial.commonObj.CreatedBy);
                                            _rawMaterial.commonObj.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : _rawMaterial.commonObj.CreatedDate);
                                            _rawMaterial.commonObj.CreatedDateString = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()).ToString(setting.dateformat) : string.Empty);
                                        }
                                   }
                                    rawMaterialList.Add(_rawMaterial);
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
            return rawMaterialList;
        }

        public object InsertRawMaterial(RawMaterial rawMaterial)
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
                        cmd.CommandText = "[Office].[InsertRawMaterial]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MaterialCode", SqlDbType.VarChar, 10).Value = rawMaterial.MaterialCode;
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, -1).Value = rawMaterial.Description;
                        cmd.Parameters.Add("@MaterialType", SqlDbType.VarChar, 250).Value = rawMaterial.Type;
                        cmd.Parameters.Add("@Unit", SqlDbType.VarChar, 50).Value = rawMaterial.Unit;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = rawMaterial.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = rawMaterial.commonObj.CreatedDate;
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

        public object UpdateRawMaterial(RawMaterial rawMaterial)
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
                        cmd.CommandText = "[Office].[UpdateRawMaterial]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = rawMaterial.ID;
                        cmd.Parameters.Add("@MaterialCode", SqlDbType.VarChar, 50).Value = rawMaterial.MaterialCode;
                        cmd.Parameters.Add("@MaterialType", SqlDbType.VarChar, 250).Value = rawMaterial.Type;
                        cmd.Parameters.Add("@Unit", SqlDbType.VarChar, 50).Value = rawMaterial.Unit;
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, -1).Value = rawMaterial.Description;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = rawMaterial.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = rawMaterial.commonObj.UpdatedDate;
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

        public List<MaterialType> GetAllMaterialType()
        {
            List<MaterialType> materialTypeList = null;
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
                        cmd.CommandText = "[Office].[GetAllMaterialsType]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                materialTypeList = new List<MaterialType>();
                                while (sdr.Read())
                                {
                                    MaterialType materialType = new MaterialType();
                                    {
                                        materialType.Code = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : materialType.Code);
                                        materialType.Description = (sdr["Description"].ToString() != "" ? sdr["Description"].ToString() : materialType.Description);

                                    }
                                    materialTypeList.Add(materialType);
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

            return materialTypeList;
        }
    }
}