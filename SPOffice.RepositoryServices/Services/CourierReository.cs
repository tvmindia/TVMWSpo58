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


        public List<Courier> GetAllCouriers()
        {
            List<Courier> courierList = null;
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
                        cmd.CommandText = "[Office].[GetAllCouriers]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                courierList = new List<Courier>();
                                while (sdr.Read())
                                {
                                    Courier _courier = new Courier();
                                    {
                                        _courier.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _courier.ID);
                                        _courier.CourierNO = (sdr["CourierNO"].ToString() != "" ? sdr["CourierNO"].ToString() : _courier.CourierNO);
                                        _courier.Type = (sdr["Type"].ToString() != "" ? sdr["Type"].ToString() : _courier.Type);
                                        _courier.TransactionDate = (sdr["TransactionDate"].ToString() != "" ? DateTime.Parse(sdr["TransactionDate"].ToString()).ToString(s.dateformat) : _courier.TransactionDate);
                                        _courier.Track = (sdr["Track"].ToString() != "" ? sdr["Track"].ToString() : _courier.Track);
                                        _courier.SourceName = (sdr["SourceName"].ToString() != "" ? sdr["SourceName"].ToString() : _courier.SourceName);
                                        _courier.SourceAddress = (sdr["SourceAddress"].ToString() != "" ? sdr["SourceAddress"].ToString() : _courier.SourceAddress);
                                        _courier.DestName = (sdr["DestName"].ToString() != "" ? sdr["DestName"].ToString() : _courier.DestName);
                                        _courier.DestAddress = (sdr["DestAddress"].ToString() != "" ? sdr["DestAddress"].ToString() : _courier.DestAddress);
                                        _courier.DistributedTo = (sdr["DistributedTo"].ToString() != "" ? sdr["DistributedTo"].ToString() : _courier.DistributedTo);
                                        _courier.DistributionDate = (sdr["DistributionDate"].ToString() != "" ? DateTime.Parse(sdr["DistributionDate"].ToString()).ToString(s.dateformat) : _courier.DistributionDate);
                                        _courier.AgencyCode = (sdr["AgencyCode"].ToString() != "" ? sdr["AgencyCode"].ToString() : _courier.AgencyCode);
                                        _courier.courierAgency = new CourierAgency();
                                        {
                                            _courier.courierAgency.Code = _courier.AgencyCode;
                                            _courier.courierAgency.Name = (sdr["AgencyName"].ToString() != "" ? sdr["AgencyName"].ToString() : _courier.courierAgency.Name);
                                        }
                                        _courier.TrackingRefNo= (sdr["TrackingRefNo"].ToString() != "" ? sdr["TrackingRefNo"].ToString() : _courier.TrackingRefNo);
                                        _courier.GeneralNotes= (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _courier.GeneralNotes);
                                        _courier.TrackingURL = (sdr["TrackingURL"].ToString() != "" ? sdr["TrackingURL"].ToString() : _courier.TrackingURL);
                                        _courier.commonObj = new Common();
                                        {
                                            _courier.commonObj.CreatedBy = (sdr["CreatedBy"].ToString() != "" ? sdr["CreatedBy"].ToString() : _courier.commonObj.CreatedBy);
                                            _courier.commonObj.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : _courier.commonObj.CreatedDate);
                                            _courier.commonObj.CreatedDateString = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()).ToString(s.dateformat) : string.Empty);
                                        }
                                    }
                                    courierList.Add(_courier);
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

            return courierList;
        }

        public object InsertCourier(Courier courier)
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
                        cmd.CommandText = "[Office].[InsertCourier]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Type", SqlDbType.VarChar, 10).Value = courier.Type;
                        cmd.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = courier.TransactionDate;
                        cmd.Parameters.Add("@SourceName", SqlDbType.VarChar,250).Value = courier.SourceName;
                        cmd.Parameters.Add("@SourceAddress", SqlDbType.NVarChar,-1).Value = courier.SourceAddress;
                        cmd.Parameters.Add("@DestName", SqlDbType.VarChar,150).Value = courier.DestName;
                        cmd.Parameters.Add("@DestAddress", SqlDbType.NVarChar,-1).Value = courier.DestAddress;
                        cmd.Parameters.Add("@DistributedTo", SqlDbType.VarChar, 100).Value = courier.DistributedTo;
                        cmd.Parameters.Add("@DistributionDate", SqlDbType.DateTime).Value = courier.DistributionDate;
                        cmd.Parameters.Add("@AgencyCode", SqlDbType.VarChar,10).Value = courier.AgencyCode;
                        cmd.Parameters.Add("@TrackingRefNo", SqlDbType.VarChar, 50).Value = courier.TrackingRefNo;
                        
                        cmd.Parameters.Add("@GeneralNotes", SqlDbType.NVarChar, -1).Value = courier.GeneralNotes;
                        cmd.Parameters.Add("@TrackingURL", SqlDbType.VarChar, 500).Value = courier.TrackingURL;
                        cmd.Parameters.Add("@FileDupID", SqlDbType.UniqueIdentifier).Value = courier.hdnFileID;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = courier.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = courier.commonObj.CreatedDate;
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

        public object UpdateCourier(Courier courier)
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
                        cmd.CommandText = "[Office].[UpdateCourier]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = courier.ID;
                        cmd.Parameters.Add("@Type", SqlDbType.VarChar, 10).Value = courier.Type;
                        cmd.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = courier.TransactionDate;
                        cmd.Parameters.Add("@SourceName", SqlDbType.VarChar, 250).Value = courier.SourceName;
                        cmd.Parameters.Add("@SourceAddress", SqlDbType.NVarChar, -1).Value = courier.SourceAddress;
                        cmd.Parameters.Add("@DestName", SqlDbType.VarChar, 150).Value = courier.DestName;
                        cmd.Parameters.Add("@DestAddress", SqlDbType.NVarChar, -1).Value = courier.DestAddress;
                        cmd.Parameters.Add("@DistributedTo", SqlDbType.VarChar, 100).Value = courier.DistributedTo;
                        cmd.Parameters.Add("@DistributionDate", SqlDbType.DateTime).Value = courier.DistributionDate;
                        cmd.Parameters.Add("@AgencyCode", SqlDbType.VarChar, 10).Value = courier.AgencyCode;
                        cmd.Parameters.Add("@TrackingRefNo", SqlDbType.VarChar, 50).Value = courier.TrackingRefNo;
                        cmd.Parameters.Add("@GeneralNotes", SqlDbType.NVarChar, -1).Value = courier.GeneralNotes;
                        cmd.Parameters.Add("@TrackingURL", SqlDbType.VarChar, 500).Value = courier.TrackingURL;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = courier.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = courier.commonObj.UpdatedDate;
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

        public object DeleteCourier(Guid ID)
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
                        cmd.CommandText = "[Office].[DeleteCourier]";
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
    }
}