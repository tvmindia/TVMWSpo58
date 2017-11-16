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
        public List<Requisition> GetUserRequisitionList(string LoginName,Guid AppID)
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
        public List<Requisition> GetRequisitionDetails(Guid ID)
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
                        cmd.CommandText = "[Office].[GetRequisitionDetails]";
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = ID;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                RequisitionList = new List<Requisition>();
                                while (sdr.Read())
                                {
                                    Requisition _requisitionObj = new Requisition();
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
        public object InsertRequisition(Requisition RequisitionObj)
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
                        cmd.CommandText = "[Office].[InsertRequisition]";
                       
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ReqNo", SqlDbType.VarChar, 250).Value = RequisitionObj.ReqNo;
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
                        cmd.CommandText = "[Office].[UpdateProformaInvoice]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = RequisitionObj.ID;
                        //cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar, 20).Value = RequisitionObj.InvoiceNo;
                        //cmd.Parameters.Add("@CustomerID", SqlDbType.UniqueIdentifier).Value = proformaHeader.CustomerID;
                        //cmd.Parameters.Add("@InvoiceDate", SqlDbType.DateTime).Value = RequisitionObj.InvoiceDate;
                        //cmd.Parameters.Add("@ValidTillDate", SqlDbType.DateTime).Value = proformaHeader.ValidTillDate;
                        //cmd.Parameters.Add("@Subject", SqlDbType.VarChar, 500).Value = proformaHeader.Subject;
                        //cmd.Parameters.Add("@OriginCompCode", SqlDbType.VarChar, 10).Value = proformaHeader.OriginCompCode;
                        //cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar, 100).Value = proformaHeader.ContactPerson;
                        //cmd.Parameters.Add("@SentToAddress", SqlDbType.NVarChar, -1).Value = proformaHeader.SentToAddress;
                        //cmd.Parameters.Add("@BodyHeader", SqlDbType.NVarChar, -1).Value = proformaHeader.BodyHead;
                        //cmd.Parameters.Add("@BodyFooter", SqlDbType.NVarChar, -1).Value = proformaHeader.BodyFoot;
                        //cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = proformaHeader.Discount;
                        //cmd.Parameters.Add("@TaxTypeCode", SqlDbType.VarChar, 10).Value = proformaHeader.TaxTypeCode;
                        //cmd.Parameters.Add("@TaxPercApplied", SqlDbType.Decimal).Value = proformaHeader.TaxPercApplied;
                        //cmd.Parameters.Add("@TaxAmount", SqlDbType.Decimal).Value = proformaHeader.TaxAmount;
                        //cmd.Parameters.Add("@DetailXML", SqlDbType.Xml).Value = proformaHeader.DetailXML;
                        //cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = proformaHeader.commonObj.UpdatedBy;
                        //cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = proformaHeader.commonObj.UpdatedDate;
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

    }
}