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

        /// <summary>
        ///To Get EnquiryList in Report
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
     
        /// <returns>List</returns>
        public List<EnquiryReport> GetEnquiryDetails(DateTime? FromDate, DateTime? ToDate,string EnquiryStatus, string search)
        {
            List<EnquiryReport> enquiryList = null;
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
                        cmd.Parameters.Add("@EnquiryStatus", SqlDbType.NVarChar, 50).Value = EnquiryStatus != "" ? EnquiryStatus : null;
                        cmd.Parameters.Add("@search", SqlDbType.NVarChar, 250).Value = search != "" ? search : null;
                        cmd.CommandText = "[Office].[RPT_GetEnquiryList]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                enquiryList = new List<EnquiryReport>();
                                while (sdr.Read())
                                {
                                    EnquiryReport enquiryReport = new EnquiryReport();
                                    {
                                        enquiryReport.ID = (sdr["ID"].ToString() != "" ? sdr["ID"].ToString() : enquiryReport.ID);
                                        enquiryReport.EnquiryNo = (sdr["EnquiryNo"].ToString() != "" ? sdr["EnquiryNo"].ToString() : enquiryReport.EnquiryNo);
                                        enquiryReport.EnquiryDate = (sdr["EnquiryDate"].ToString() != "" ? DateTime.Parse(sdr["EnquiryDate"].ToString()).ToString(settings.dateformat) : enquiryReport.EnquiryDate);
                                        enquiryReport.ContactTitle = (sdr["ContactTitle"].ToString() != "" ? sdr["ContactTitle"].ToString() : enquiryReport.ContactTitle);
                                        enquiryReport.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : enquiryReport.ContactName);
                                        enquiryReport.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString():enquiryReport.CompanyName);
                                        enquiryReport.Address = (sdr["Address"].ToString() != "" ? sdr["Address"].ToString() : enquiryReport.Address);
                                        enquiryReport.Website = (sdr["Website"].ToString() != "" ? sdr["Website"].ToString() : enquiryReport.Website);
                                        enquiryReport.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : enquiryReport.Email);
                                        enquiryReport.LandLine = (sdr["LandLine"].ToString() != "" ? sdr["LandLine"].ToString() : enquiryReport.LandLine);
                                        enquiryReport.Mobile = (sdr["Mobile"].ToString() != "" ? sdr["Mobile"].ToString(): enquiryReport.Mobile);
                                        enquiryReport.Fax = (sdr["Fax"].ToString() != "" ? sdr["Fax"].ToString() : enquiryReport.Fax);
                                        enquiryReport.EnquirySource = (sdr["EnquirySource"].ToString() != "" ? sdr["EnquirySource"].ToString() : enquiryReport.EnquirySource);
                                        enquiryReport.Industry = (sdr["Industry"].ToString() != "" ? sdr["Industry"].ToString(): enquiryReport.Industry);
                                        enquiryReport.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : enquiryReport.GeneralNotes);
                                        enquiryReport.EnquiryStatus = (sdr["EnquiryStatus"].ToString() != "" ? sdr["EnquiryStatus"].ToString() : enquiryReport.EnquiryStatus);
                                        enquiryReport.Subject = (sdr["Subject"].ToString() != "" ? sdr["Subject"].ToString() : enquiryReport.Subject);
                                    }
                                    enquiryList.Add(enquiryReport);
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
            return enquiryList;
        }

    }
}