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

    }
}