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
    public class RequisitionRepository:IRequisitionRepository
    {
        Settings settings = new Settings();
        private IDatabaseFactory _databaseFactory;
        public RequisitionRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        public List<Requisition> GetUserRequisitionList(string LoginName,Guid AppID, bool IsAdminOrCeo, ReqAdvanceSearch ReqAdvanceSearchObj)
        {

            List<Requisition> RequisitionList = null;
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
                        cmd.CommandText = "[Office].[GetUserRequisitionList]";
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = LoginName;
                        cmd.Parameters.Add("@AppID", SqlDbType.UniqueIdentifier).Value = AppID;
                        cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = ReqAdvanceSearchObj.FromDate;
                        cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = ReqAdvanceSearchObj.ToDate;
                        cmd.Parameters.Add("@ReqStatus", SqlDbType.VarChar,250).Value = ReqAdvanceSearchObj.ReqStatus;
                        cmd.Parameters.Add("@ReqSearch", SqlDbType.NVarChar,250).Value = ReqAdvanceSearchObj.ReqSearch;
                        cmd.Parameters.Add("@IsAdminOrCeo", SqlDbType.Bit).Value = IsAdminOrCeo;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                RequisitionList = new List<Requisition>();
                                while (sdr.Read())
                                {
                                    Requisition _requisitionObj = new Requisition();
                                    _requisitionObj.CompanyObj = new Company();
                                    {
                                        _requisitionObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _requisitionObj.ID);
                                        _requisitionObj.ReqNo = (sdr["ReqNo"].ToString() != "" ? sdr["ReqNo"].ToString() : _requisitionObj.ReqNo);
                                        _requisitionObj.ReqDate = (sdr["ReqDate"].ToString() != "" ? DateTime.Parse(sdr["ReqDate"].ToString()) : _requisitionObj.ReqDate);
                                        _requisitionObj.ReqDateFormatted = (sdr["ReqDate"].ToString() != "" ? DateTime.Parse(sdr["ReqDate"].ToString()).ToString(settings.dateformat) : _requisitionObj.ReqDateFormatted);
                                        _requisitionObj.Title = (sdr["Title"].ToString() != "" ? sdr["Title"].ToString() : _requisitionObj.Title);
                                        _requisitionObj.CompanyObj.Code= (sdr["ReqForCompany"].ToString() != "" ? sdr["ReqForCompany"].ToString() : _requisitionObj.CompanyObj.Code);
                                        _requisitionObj.CompanyObj.Name= (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _requisitionObj.CompanyObj.Name);
                                        _requisitionObj.ReqForCompany = (sdr["ReqForCompany"].ToString() != "" ? sdr["ReqForCompany"].ToString() : _requisitionObj.ReqForCompany);
                                        _requisitionObj.ReqStatus = (sdr["ReqStatus"].ToString() != "" ? (sdr["ReqStatus"].ToString()) : _requisitionObj.ReqStatus);
                                        _requisitionObj.ManagerApproved = (sdr["ManagerApproved"].ToString() != "" ? bool.Parse(sdr["ManagerApproved"].ToString()) : _requisitionObj.ManagerApproved);
                                        _requisitionObj.ManagerApprovalDate = (sdr["ManagerApprovalDate"].ToString() != "" ? DateTime.Parse(sdr["ManagerApprovalDate"].ToString()) : _requisitionObj.ManagerApprovalDate);
                                        _requisitionObj.ManagerApprovalDateFormatted = (sdr["ManagerApprovalDate"].ToString() != "" ? DateTime.Parse(sdr["ManagerApprovalDate"].ToString()).ToString(settings.dateformat) : _requisitionObj.ManagerApprovalDateFormatted);
                                        _requisitionObj.FinalApproval = (sdr["FinalApproval"].ToString() != "" ? bool.Parse(sdr["FinalApproval"].ToString()) : _requisitionObj.FinalApproval);
                                        _requisitionObj.FinalApprovalDate = (sdr["FinalApprovalDate"].ToString() != "" ? DateTime.Parse(sdr["FinalApprovalDate"].ToString()) : _requisitionObj.FinalApprovalDate);
                                        _requisitionObj.FinalApprovalDateFormatted = (sdr["FinalApprovalDate"].ToString() != "" ? DateTime.Parse(sdr["FinalApprovalDate"].ToString()).ToString(settings.dateformat) : _requisitionObj.FinalApprovalDateFormatted);
                                    }
                                    RequisitionList.Add(_requisitionObj);
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

            return RequisitionList;
        }
        public List<RequisitionDetail> GetRequisitionDetailList(Guid ID)
        {
            List<RequisitionDetail> RequisitionDetailList = null;
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
                        cmd.CommandText = "[Office].[GetRequisitionDetailList]";
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = ID;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                RequisitionDetailList = new List<RequisitionDetail>();
                                while (sdr.Read())
                                {
                                    RequisitionDetail _requisitionDetailObj = new RequisitionDetail();
                                    _requisitionDetailObj.RawMaterialObj = new RawMaterial();
                                    {
                                        _requisitionDetailObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _requisitionDetailObj.ID);
                                        _requisitionDetailObj.ReqID = (sdr["ReqID"].ToString() != "" ? Guid.Parse(sdr["ReqID"].ToString()) : _requisitionDetailObj.ReqID);
                                        _requisitionDetailObj.MaterialID = (sdr["MaterialID"].ToString() != "" ? Guid.Parse(sdr["MaterialID"].ToString()) : _requisitionDetailObj.MaterialID);
                                        _requisitionDetailObj.Description = (sdr["Description"].ToString());
                                        _requisitionDetailObj.ExtendedDescription = (sdr["ExtendedDescription"].ToString());
                                        _requisitionDetailObj.RawMaterialObj.ID = (sdr["MaterialID"].ToString() != "" ? Guid.Parse(sdr["MaterialID"].ToString()) : _requisitionDetailObj.MaterialID);
                                        _requisitionDetailObj.RawMaterialObj.MaterialCode = (sdr["MaterialCode"].ToString());
                                        _requisitionDetailObj.CurrStock = (sdr["CurrStock"].ToString());
                                        _requisitionDetailObj.AppxRate = (sdr["AppxRate"].ToString() != "" ? decimal.Parse(sdr["AppxRate"].ToString()) : _requisitionDetailObj.AppxRate);
                                        _requisitionDetailObj.RequestedQty = (sdr["RequestedQty"].ToString());
                                        }
                                    RequisitionDetailList.Add(_requisitionDetailObj);
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

            return RequisitionDetailList;
        }
        public Requisition GetRequisitionDetails(Guid ID, string LoginName)
        {
            Requisition _requisitionObj = null;
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
                        cmd.CommandText = "[Office].[GetRequisitionDetails]";
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = ID;
                        cmd.Parameters.Add("@LoginName", SqlDbType.VarChar, 250).Value = LoginName;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows)&&(sdr.Read()))
                            {
                                     _requisitionObj = new Requisition();
                                    {
                                        _requisitionObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _requisitionObj.ID);
                                        _requisitionObj.ReqNo = (sdr["ReqNo"].ToString() != "" ? sdr["ReqNo"].ToString() : _requisitionObj.ReqNo);
                                        _requisitionObj.ReqDate = (sdr["ReqDate"].ToString() != "" ? DateTime.Parse(sdr["ReqDate"].ToString()) : _requisitionObj.ReqDate);
                                        _requisitionObj.ReqDateFormatted = (sdr["ReqDate"].ToString() != "" ? DateTime.Parse(sdr["ReqDate"].ToString()).ToString(settings.dateformat) : _requisitionObj.ReqDateFormatted);
                                        _requisitionObj.Title = (sdr["Title"].ToString() != "" ? sdr["Title"].ToString() : _requisitionObj.Title);
                                        //_requisitionObj.CompanyObj.Code = (sdr["ReqForCompany"].ToString() != "" ? sdr["ReqForCompany"].ToString() : _requisitionObj.CompanyObj.Code);
                                        //_requisitionObj.CompanyObj.Name = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _requisitionObj.CompanyObj.Name);
                                        _requisitionObj.ReqForCompany = (sdr["ReqForCompany"].ToString() != "" ? sdr["ReqForCompany"].ToString() : _requisitionObj.ReqForCompany);
                                        _requisitionObj.ReqStatus = (sdr["ReqStatus"].ToString() != "" ? (sdr["ReqStatus"].ToString()) : _requisitionObj.ReqStatus);
                                        _requisitionObj.ManagerApproved = (sdr["ManagerApproved"].ToString() != "" ? bool.Parse(sdr["ManagerApproved"].ToString()) : _requisitionObj.ManagerApproved);
                                        _requisitionObj.ManagerApprovalDate = (sdr["ManagerApprovalDate"].ToString() != "" ? DateTime.Parse(sdr["ManagerApprovalDate"].ToString()) : _requisitionObj.ManagerApprovalDate);
                                        _requisitionObj.ManagerApprovalDateFormatted = (sdr["ManagerApprovalDate"].ToString() != "" ? DateTime.Parse(sdr["ManagerApprovalDate"].ToString()).ToString(settings.dateformat) : _requisitionObj.ManagerApprovalDateFormatted);
                                        _requisitionObj.FinalApproval = (sdr["FinalApproval"].ToString() != "" ? bool.Parse(sdr["FinalApproval"].ToString()) : _requisitionObj.FinalApproval);
                                        _requisitionObj.FinalApprovalDate = (sdr["FinalApprovalDate"].ToString() != "" ? DateTime.Parse(sdr["FinalApprovalDate"].ToString()) : _requisitionObj.FinalApprovalDate);
                                        _requisitionObj.FinalApprovalDateFormatted = (sdr["FinalApprovalDate"].ToString() != "" ? DateTime.Parse(sdr["FinalApprovalDate"].ToString()).ToString(settings.dateformat) : _requisitionObj.FinalApprovalDateFormatted);
                                        _requisitionObj.IsApprover = (sdr["IsApprover"].ToString() != "" ? bool.Parse(sdr["IsApprover"].ToString()) : _requisitionObj.IsApprover);
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

            return _requisitionObj;
        }
        public object InsertRequisition(Requisition RequisitionObj, bool isAdminOrCeo)
        {
            SqlParameter outputStatus, outputID, outputReqNo,outputApprovedBy;
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
                        cmd.CommandText = "[Office].[InsertRequisition]";
                       
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IsAdminOrCeo", SqlDbType.Bit).Value = isAdminOrCeo;
                        cmd.Parameters.Add("@Title", SqlDbType.NVarChar,250).Value = RequisitionObj.Title;
                        cmd.Parameters.Add("@ReqDate", SqlDbType.DateTime).Value = RequisitionObj.ReqDateFormatted;
                        cmd.Parameters.Add("@ReqForCompany", SqlDbType.VarChar, 10).Value = RequisitionObj.ReqForCompany;
                        cmd.Parameters.Add("@ReqStatus", SqlDbType.VarChar, 50).Value = RequisitionObj.ReqStatus;
                        cmd.Parameters.Add("@DetailXML", SqlDbType.Xml).Value = RequisitionObj.DetailXML;
                        cmd.Parameters.Add("@ManagerApproved", SqlDbType.Bit).Value = RequisitionObj.ManagerApproved;
                        cmd.Parameters.Add("@FinalApproved", SqlDbType.Bit).Value = RequisitionObj.FinalApproval;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = RequisitionObj.CommonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = RequisitionObj.CommonObj.CreatedDate;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = RequisitionObj.CommonObj.UpdatedDate;
                        outputApprovedBy = cmd.Parameters.Add("@ApprovedBy", SqlDbType.NVarChar, 250);
                        outputApprovedBy.Direction = ParameterDirection.Output;
                        outputReqNo = cmd.Parameters.Add("@ReqNo", SqlDbType.VarChar, 250);
                        outputReqNo.Direction = ParameterDirection.Output;
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
                            ReqNo=outputReqNo.Value.ToString(),
                            ApprovedBy=outputApprovedBy.Value.ToString(),
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
                // Message = Cobj.InsertSuccess
            };
        }
        public object UpdateRequisition(Requisition RequisitionObj)
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
                        cmd.CommandText = "[Office].[UpdateRequisition]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = RequisitionObj.ID;
                        //cmd.Parameters.Add("@ReqNo", SqlDbType.VarChar, 250).Value = RequisitionObj.ReqNo;
                        cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 250).Value = RequisitionObj.Title;
                        cmd.Parameters.Add("@ReqDate", SqlDbType.DateTime).Value = RequisitionObj.ReqDateFormatted;
                        cmd.Parameters.Add("@ReqForCompany", SqlDbType.VarChar, 10).Value = RequisitionObj.ReqForCompany;
                        cmd.Parameters.Add("@ReqStatus", SqlDbType.VarChar, 50).Value = RequisitionObj.ReqStatus;
                        cmd.Parameters.Add("@DetailXML", SqlDbType.Xml).Value = RequisitionObj.DetailXML;
                        //cmd.Parameters.Add("@ManagerApproved", SqlDbType.Bit).Value = RequisitionObj.ManagerApproved;
                        //cmd.Parameters.Add("@FinalApproved", SqlDbType.Bit).Value = RequisitionObj.FinalApproval;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = RequisitionObj.CommonObj.UpdatedDate;
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
                // Message = Cobj.UpdateSuccess
            };
        }
        public object DeleteRequisitionDetailByID(Guid ID)
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
                        cmd.CommandText = "[Office].[DeleteRequisitionDetailByID]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = ID;
                        outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();


                    }
                }
            }

            catch (Exception ex)
            {

                throw ex;
            }
            return new
            {
                Status = outputStatus.Value.ToString(),

            };
        }
        public RequisitionOverViewCount GetRequisitionOverViewCount(string UserName, bool IsAdminORCeo)
        {
            RequisitionOverViewCount _requisitionOverviewCountObj = null;
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
                        cmd.CommandText = "[Office].[GetRequisitionOverViewCount]";
                        cmd.Parameters.Add("@UserName", SqlDbType.VarChar,250).Value = UserName;
                        cmd.Parameters.Add("@IsAdminORCeo", SqlDbType.Bit).Value = IsAdminORCeo;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows) && (sdr.Read()))
                            {
                                _requisitionOverviewCountObj = new RequisitionOverViewCount();
                                {
                                    _requisitionOverviewCountObj.OpenCount = (sdr["OpenCount"].ToString() != "" ? int.Parse(sdr["OpenCount"].ToString()) : -1);
                                    _requisitionOverviewCountObj.AllCount = (sdr["AllCount"].ToString() != "" ? int.Parse(sdr["AllCount"].ToString()) : -1);
                                    _requisitionOverviewCountObj.PendingManagerCount = (sdr["PendingManagerCount"].ToString() != "" ? int.Parse(sdr["PendingManagerCount"].ToString()) : -1);
                                    _requisitionOverviewCountObj.PendingFinalCount = (sdr["PendingFinalCount"].ToString() != "" ? int.Parse(sdr["PendingFinalCount"].ToString()) : -1);
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

            return _requisitionOverviewCountObj;
        }
        public Requisition ApproveRequisition(Requisition RequisitionObj, bool IsAdminORCeo)
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
                        cmd.CommandText = "[Office].[ApproveRequisition]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = RequisitionObj.ID;
                        cmd.Parameters.Add("@IsAdminOrCeo", SqlDbType.Bit).Value = IsAdminORCeo;
                        cmd.Parameters.Add("@ApprovalDate", SqlDbType.DateTime).Value = RequisitionObj.CommonObj.UpdatedDate;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 250).Value = RequisitionObj.CommonObj.CreatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = RequisitionObj.CommonObj.UpdatedDate;
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
                        if(IsAdminORCeo)
                        {
                            RequisitionObj.FinalApproval = true;
                        }
                        else
                        {
                            RequisitionObj.ManagerApproved = true;
                        }
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RequisitionObj;
        }
    }
}