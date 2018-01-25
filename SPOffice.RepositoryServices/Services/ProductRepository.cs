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
    public class ProductRepository: IProductRepository
    {
        AppConst Cobj = new AppConst();
        Settings settings = new Settings();
        private IDatabaseFactory _databaseFactory;
        public ProductRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public object DeleteProduct(Guid ID)
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
                        cmd.CommandText = "[Office].[DeleteProduct]";
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

        public List<Product> GetAllProducts()
        {
            List<Product> productList = null;
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
                        cmd.CommandText = "[Office].[GetAllProducts]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                productList = new List<Product>();
                                while (sdr.Read())
                                {
                                    Product _product = new Product();
                                    {
                                        _product.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _product.ID);
                                        _product.Code = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : _product.Code);
                                        _product.OldCode = (sdr["OldCode"].ToString() != "" ? sdr["OldCode"].ToString() : _product.OldCode);
                                        _product.Name= (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _product.Name);
                                        _product.Description = (sdr["Description"].ToString() != "" ? sdr["Description"].ToString() : _product.Description);
                                        _product.UnitCode = (sdr["UnitCode"].ToString() != "" ? sdr["UnitCode"].ToString() : _product.UnitCode);
                                        _product.Rate = (sdr["Rate"].ToString() != "" ? decimal.Parse(sdr["Rate"].ToString()) : _product.Rate);
                                        _product.commonObj = new Common();
                                        {
                                            _product.commonObj.CreatedBy = (sdr["CreatedBy"].ToString() != "" ? sdr["CreatedBy"].ToString() : _product.commonObj.CreatedBy);
                                            _product.commonObj.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : _product.commonObj.CreatedDate);
                                            _product.commonObj.CreatedDateString = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()).ToString(settings.dateformat) : string.Empty);
                                        }
                                        _product.unit = new Unit();
                                        {
                                            _product.unit.Description = (sdr["UnitDescription"].ToString() != "" ? sdr["UnitDescription"].ToString() : _product.unit.Description);
                                        }
                                    }
                                    productList.Add(_product);
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

            return productList;
        }

        public List<Unit> GetAllUnits()
        {
            List<Unit> UnitList = null;
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
                                UnitList = new List<Unit>();
                                while (sdr.Read())
                                {
                                    Unit unit = new Unit();
                                    {
                                        unit.Code = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : unit.Code);
                                        unit.Description = (sdr["Description"].ToString() != "" ? sdr["Description"].ToString() : unit.Description);
                                      
                                    }
                                    UnitList.Add(unit);
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

            return UnitList;
        }

        public object InsertProduct(Product product)
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
                        cmd.CommandText = "[Office].[InsertProduct]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Code", SqlDbType.VarChar, 10).Value = product.Code;
                        cmd.Parameters.Add("@OldCode", SqlDbType.VarChar, 20).Value = product.OldCode;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 250).Value = product.Name;
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, -1).Value = product.Description;
                        cmd.Parameters.Add("@UnitCode", SqlDbType.VarChar, 15).Value = product.UnitCode;
                        cmd.Parameters.Add("@Rate", SqlDbType.Decimal).Value = product.Rate;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = product.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = product.commonObj.CreatedDate;
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
                            ID= outputID.Value.ToString(),
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

        public object UpdateProduct(Product product)
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
                        cmd.CommandText = "[Office].[UpdateProduct]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = product.ID;
                        cmd.Parameters.Add("@Code", SqlDbType.VarChar, 10).Value = product.Code;
                        cmd.Parameters.Add("@OldCode", SqlDbType.VarChar, 20).Value = product.OldCode;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 250).Value = product.Name;
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, -1).Value = product.Description;
                        cmd.Parameters.Add("@UnitCode", SqlDbType.VarChar, 15).Value = product.UnitCode;
                        cmd.Parameters.Add("@Rate", SqlDbType.Decimal).Value = product.Rate;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = product.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = product.commonObj.UpdatedDate;
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