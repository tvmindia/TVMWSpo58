using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SPOffice.DataAccessObject;
using System.Data;

namespace SPOffice.RepositoryServices.Services
{
    public class ReportRepository : IReportRepository
    {
        Settings settings = new Settings();
        private IDatabaseFactory _databaseFactory;
        public ReportRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public List<Reports> GetAllSysReports(AppUA appUA)
        {
            List<Reports> Reportlist = null;
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
                        cmd.Parameters.Add("@AppID", SqlDbType.UniqueIdentifier).Value = appUA.AppID;
                        cmd.CommandText = "[Office].[GetAllSys_Reports]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                Reportlist = new List<Reports>();
                                while (sdr.Read())
                                {
                                    Reports _ReportObj = new Reports();
                                    {
                                        _ReportObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _ReportObj.ID);
                                        _ReportObj.AppID = (sdr["AppID"].ToString() != "" ? Guid.Parse(sdr["AppID"].ToString()) : _ReportObj.AppID);
                                        _ReportObj.ReportName = (sdr["ReportName"].ToString() != "" ? (sdr["ReportName"].ToString()) : _ReportObj.ReportName);
                                        _ReportObj.ReportDescription = (sdr["ReportDescription"].ToString() != "" ? (sdr["ReportDescription"].ToString()) : _ReportObj.ReportDescription);
                                        _ReportObj.Controller = (sdr["Controller"].ToString() != "" ? sdr["Controller"].ToString() : _ReportObj.Controller);
                                        _ReportObj.Action = (sdr["Action"].ToString() != "" ? sdr["Action"].ToString() : _ReportObj.Action);
                                        _ReportObj.SPName = (sdr["SPName"].ToString() != "" ? sdr["SPName"].ToString() : _ReportObj.SPName);
                                        _ReportObj.SQL = (sdr["SQL"].ToString() != "" ? sdr["SQL"].ToString() : _ReportObj.SQL);
                                        _ReportObj.ReportOrder = (sdr["ReportOrder"].ToString() != "" ? int.Parse(sdr["ReportOrder"].ToString()) : _ReportObj.ReportOrder);
                                        _ReportObj.ReportGroup = (sdr["ReportGroup"].ToString() != "" ? sdr["ReportGroup"].ToString() : _ReportObj.ReportGroup);
                                        _ReportObj.GroupOrder = (sdr["GroupOrder"].ToString() != "" ? int.Parse(sdr["GroupOrder"].ToString()) : _ReportObj.GroupOrder);
                                    }
                                    Reportlist.Add(_ReportObj);
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
            return Reportlist;
        }

        public List<CourierReport> GetCourierDetails(DateTime? FromDate, DateTime? ToDate, string AgencyCode, string search, string Type)
        {
            List<CourierReport> CourierDetailList = null;
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
                        cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = FromDate;
                        cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = ToDate;
                        cmd.Parameters.Add("@AgencyCode", SqlDbType.NVarChar, 50).Value = AgencyCode;
                        cmd.Parameters.Add("@search", SqlDbType.NVarChar, 250).Value = search != "" ? search : null;
                        cmd.Parameters.Add("@Type", SqlDbType.NVarChar,50).Value = Type;
                        cmd.CommandText = "[Office].[RPT_GetCourierDetail]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                 CourierDetailList = new List<CourierReport>();
                                while (sdr.Read())
                                {
                                    CourierReport courierDetail = new CourierReport();
                                    {
                                        courierDetail.Type = (sdr["Type"].ToString() != "" ? sdr["Type"].ToString() : courierDetail.Type);
                                        courierDetail.TransactionDate = (sdr["TransactionDate"].ToString() != "" ? sdr["TransactionDate"].ToString() : courierDetail.TransactionDate);
                                        courierDetail.Track = (sdr["Track"].ToString() != "" ? sdr["Track"].ToString() : courierDetail.Track);
                                        courierDetail.TrackingURL = (sdr["TrackingURL"].ToString() != "" ? sdr["TrackingURL"].ToString() : courierDetail.TrackingURL);
                                        courierDetail.SourceName = (sdr["SourceName"].ToString() != "" ? sdr["SourceName"].ToString() : courierDetail.SourceName);
                                        courierDetail.DestName = (sdr["DestName"].ToString() != "" ? sdr["DestName"].ToString() : courierDetail.DestName);
                                        courierDetail.AgencyCode= (sdr["AgencyCode"].ToString() != "" ? sdr["AgencyCode"].ToString() : courierDetail.AgencyCode);
                                        courierDetail.courierAgency = new CourierAgency();
                                        {
                                            courierDetail.courierAgency.Code = courierDetail.AgencyCode;
                                            courierDetail.courierAgency.Name = (sdr["AgencyName"].ToString() != "" ? sdr["AgencyName"].ToString() : courierDetail.courierAgency.Name);
                                        }
                                        courierDetail.TrackingRefNo = (sdr["TrackingRefNo"].ToString() != "" ? sdr["TrackingRefNo"].ToString() : courierDetail.TrackingRefNo);
                                    }
                                    CourierDetailList.Add(courierDetail);
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
            return CourierDetailList;
        }
    }
}