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
        /// <param name="EnquiryStatus"></param>
        /// <param name="search"></param>
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
                                        enquiryReport.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : enquiryReport.ID);
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

        /// <summary>
        ///To Get Quotation details in Report
        /// </summary>
        /// <param name="ReptAdvancedSearchObj"></param>
        /// <returns>List</returns>
        public List<QuotationReport> GetQuotationDetails(ReportAdvanceSearch ReptAdvancedSearchObj)
        {
            List<QuotationReport> quotationList = null;
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
                        cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = ReptAdvancedSearchObj.FromDate;
                        cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = ReptAdvancedSearchObj.ToDate;
                        cmd.Parameters.Add("@FromCompany", SqlDbType.NVarChar, 250).Value = ReptAdvancedSearchObj.FromCompany;
                        cmd.Parameters.Add("@QuoteStage", SqlDbType.NVarChar, 250).Value = ReptAdvancedSearchObj.QuoteStage;
                        cmd.Parameters.Add("@search", SqlDbType.NVarChar, 250).Value = ReptAdvancedSearchObj.Search;
                        cmd.CommandText = "[Office].[RPT_GetQuotationDetails]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                quotationList = new List<QuotationReport>();
                                while (sdr.Read())
                                {
                                    QuotationReport quotationReport = new QuotationReport();
                                    {
                                        quotationReport.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : quotationReport.ID);
                                        quotationReport.QuotationNo = (sdr["QuotationNo"].ToString() != "" ? sdr["QuotationNo"].ToString() : quotationReport.QuotationNo);
                                        quotationReport.QuotationDate = (sdr["QuotationDate"].ToString() != "" ? DateTime.Parse(sdr["QuotationDate"].ToString()).ToString(settings.dateformat) : quotationReport.QuotationDate);
                                        quotationReport.QuoteFromCompName = (sdr["QuoteFromCompanyName"].ToString() != "" ? sdr["QuoteFromCompanyName"].ToString() : quotationReport.QuoteFromCompName);
                                        quotationReport.QuoteStage = (sdr["QuoteStage"].ToString() != "" ? sdr["QuoteStage"].ToString() : quotationReport.QuoteStage);
                                        quotationReport.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : quotationReport.CompanyName);
                                        quotationReport.QuoteSubject = (sdr["QuoteSubject"].ToString() != "" ? sdr["QuoteSubject"].ToString() : quotationReport.QuoteSubject);
                                        quotationReport.ContactPerson = (sdr["ContactPerson"].ToString() != "" ? sdr["ContactPerson"].ToString() : quotationReport.ContactPerson);
                                      
                                    }
                                    quotationList.Add(quotationReport);
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
            return quotationList;
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
                                        courierDetail.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : courierDetail.ID);
                                        courierDetail.Type = (sdr["Type"].ToString() != "" ? sdr["Type"].ToString() : courierDetail.Type);
                                        courierDetail.TransactionDate = (sdr["TransactionDate"].ToString() != "" ? DateTime.Parse(sdr["TransactionDate"].ToString()).ToString(settings.dateformat) : courierDetail.TransactionDate);
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
                                        courierDetail.DistributionDate = (sdr["DistributionDate"].ToString() != "" ? DateTime.Parse(sdr["DistributionDate"].ToString()).ToString(settings.dateformat) : courierDetail.DistributionDate); 
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


        /// <summary>
        ///To Get CustomerPO details in Report
        /// </summary>
        /// <param name="ReptAdvancedSearchObj"></param>
        /// <returns>List</returns>
        public List<CustomerPOReport> GetCustomerPODetails(ReportAdvanceSearch ReptAdvancedSearchObj)
        {
            List<CustomerPOReport> CustomerDetailList = null;
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
                        cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = ReptAdvancedSearchObj.FromDate;
                        cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = ReptAdvancedSearchObj.ToDate;
                        cmd.Parameters.Add("@search", SqlDbType.NVarChar, 250).Value = ReptAdvancedSearchObj.Search;
                        cmd.Parameters.Add("@POStatus", SqlDbType.NVarChar, 50).Value = ReptAdvancedSearchObj.POStatus;
                        cmd.Parameters.Add("@Customer", SqlDbType.NVarChar).Value = ReptAdvancedSearchObj.Customer;
                        cmd.Parameters.Add("@Company", SqlDbType.NVarChar).Value = ReptAdvancedSearchObj.Company;
                        cmd.CommandText = "[Office].[RPT_GetAllCustomerDetail]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                CustomerDetailList = new List<CustomerPOReport>();
                                while (sdr.Read())
                                {
                                    CustomerPOReport customerDetail = new CustomerPOReport();
                                    customerDetail.CustomerPOObj = new CustomerPO();
                                    {
                                        customerDetail.CustomerPOObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : customerDetail.CustomerPOObj.ID);
                                        customerDetail.CustomerPOObj.PONo = (sdr["PONo"].ToString() != "" ? sdr["PONo"].ToString() : customerDetail.CustomerPOObj.PONo);
                                        customerDetail.CustomerPOObj.PODate = (sdr["PODate"].ToString() != "" ? DateTime.Parse(sdr["PODate"].ToString()).ToString(settings.dateformat) : customerDetail.CustomerPOObj.PODate);
                                        customerDetail.CustomerPOObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? Guid.Parse(sdr["CustomerID"].ToString()) : customerDetail.CustomerPOObj.CustomerID);
                                        customerDetail.CustomerPOObj.customer = new Customer();
                                        {
                                            customerDetail.CustomerPOObj.customer.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : customerDetail.CustomerPOObj.customer.CompanyName);
                                        }
                                        
                                        customerDetail.CustomerPOObj.POToCompCode = (sdr["POToCompCode"].ToString() != "" ? sdr["POToCompCode"].ToString() : customerDetail.CustomerPOObj.POToCompCode);

                                        customerDetail.CustomerPOObj.company = new Company();
                                        {
                                            customerDetail.CustomerPOObj.company.Name = (sdr["Company"].ToString() != "" ? sdr["Company"].ToString() : customerDetail.CustomerPOObj.company.Name);
                                            customerDetail.CustomerPOObj.company.Code = (sdr["POToCompCode"].ToString() != "" ? sdr["POToCompCode"].ToString() : customerDetail.CustomerPOObj.company.Code);
                                        }
                                         customerDetail.CustomerPOObj.POStatus = (sdr["Description"].ToString() != "" ? sdr["Description"].ToString() : customerDetail.CustomerPOObj.POStatus);
                                       
                                        customerDetail.CustomerPOObj.Amount = (sdr["Amount"].ToString() != "" ? decimal.Parse(sdr["Amount"].ToString()) : customerDetail.CustomerPOObj.Amount);
                                    }
                                    CustomerDetailList.Add(customerDetail);
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
            return CustomerDetailList;
        }

    }
}