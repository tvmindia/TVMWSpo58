﻿using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SPOffice.RepositoryServices.Services
{
    public class EnquiryRepository: IEnquiryRepository
    {
        AppConst Cobj = new AppConst();
        Settings s = new Settings();
        private IDatabaseFactory _databaseFactory;
        public EnquiryRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        #region InsertEnquiry
        public Enquiry InsertEnquiry(Enquiry _enquiriesObj)
        {
            try
            {
                SqlParameter outputStatus, outputID, outputEnquiryNo = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[Office].[InsertEnquiry]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@EnquiryDate", SqlDbType.DateTime).Value = _enquiriesObj.EnquiryDate;
                        cmd.Parameters.Add("@ContactName", SqlDbType.VarChar, 100).Value = _enquiriesObj.ContactName;
                        cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar, 100).Value = _enquiriesObj.CompanyName;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 150).Value = _enquiriesObj.Email;
                        cmd.Parameters.Add("@EnquiryStatus", SqlDbType.VarChar, 100).Value = _enquiriesObj.EnquiryStatus;
                        cmd.Parameters.Add("@ContactTitle", SqlDbType.VarChar, 10).Value = _enquiriesObj.ContactTitle;
                        cmd.Parameters.Add("@Mobile", SqlDbType.VarChar, 50).Value = _enquiriesObj.Mobile;
                        cmd.Parameters.Add("@GeneralNotes", SqlDbType.NVarChar, -1).Value = _enquiriesObj.GeneralNotes;

                        //----- new fields------ //
                        cmd.Parameters.Add("@Address", SqlDbType.NVarChar, -1).Value = _enquiriesObj.Address;
                        cmd.Parameters.Add("@Website", SqlDbType.NVarChar, 500).Value = _enquiriesObj.Website;
                        cmd.Parameters.Add("@LandLine", SqlDbType.VarChar, 50).Value = _enquiriesObj.LandLine;
                        cmd.Parameters.Add("@Fax", SqlDbType.VarChar, 50).Value = _enquiriesObj.Fax;
                        cmd.Parameters.Add("@EnquirySourceCode", SqlDbType.VarChar, 10).Value = _enquiriesObj.EnquirySource;
                        cmd.Parameters.Add("@IndustryCode", SqlDbType.VarChar, 10).Value = _enquiriesObj.IndustryCode;
                        cmd.Parameters.Add("@ProgressStatus", SqlDbType.VarChar, 10).Value = _enquiriesObj.ProgressStatus;
                        cmd.Parameters.Add("@EnquiryOwnerID", SqlDbType.UniqueIdentifier).Value = new Guid( _enquiriesObj.EnquiryOwnerID);
                        //-----------------------//

                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = _enquiriesObj.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = _enquiriesObj.commonObj.CreatedDate;
                        outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;
                        outputID = cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier);
                        outputEnquiryNo = cmd.Parameters.Add("@EnquiryNo", SqlDbType.VarChar,20);
                        outputID.Direction = ParameterDirection.Output;
                        outputEnquiryNo.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();


                    }
                }

                switch (outputStatus.Value.ToString())
                {
                    case "0":
                        AppConst Cobj = new AppConst();
                        throw new Exception(Cobj.InsertFailure);
                    case "1":
                        _enquiriesObj.ID = Guid.Parse(outputID.Value.ToString());
                        _enquiriesObj.EnquiryNo = outputEnquiryNo.Value.ToString();
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return _enquiriesObj;
        }
        #endregion InsertEnquiry

        #region UpdateEnquiry
        public object UpdateEnquiry(Enquiry _enquiriesObj)
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
                        cmd.CommandText = "[Office].[UpdateEnquiry]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = _enquiriesObj.ID;
                        cmd.Parameters.Add("@EnquiryDate", SqlDbType.DateTime).Value = _enquiriesObj.EnquiryDate;
                        cmd.Parameters.Add("@ContactName", SqlDbType.VarChar, 100).Value = _enquiriesObj.ContactName;
                        cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar, 100).Value = _enquiriesObj.CompanyName;
                        cmd.Parameters.Add("@EnquiryStatus", SqlDbType.VarChar, 100).Value = _enquiriesObj.EnquiryStatus;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 150).Value = _enquiriesObj.Email;
                        cmd.Parameters.Add("@ContactTitle", SqlDbType.VarChar, 10).Value = _enquiriesObj.ContactTitle;
                        cmd.Parameters.Add("@Mobile", SqlDbType.VarChar, 50).Value = _enquiriesObj.Mobile;
                        cmd.Parameters.Add("@GeneralNotes", SqlDbType.NVarChar, -1).Value = _enquiriesObj.GeneralNotes;

                        //----- new fields------ //
                        cmd.Parameters.Add("@Address", SqlDbType.NVarChar, -1).Value = _enquiriesObj.Address;
                        cmd.Parameters.Add("@Website", SqlDbType.NVarChar, 500).Value = _enquiriesObj.Website;
                        cmd.Parameters.Add("@LandLine", SqlDbType.VarChar, 50).Value = _enquiriesObj.LandLine;
                        cmd.Parameters.Add("@Fax", SqlDbType.VarChar, 50).Value = _enquiriesObj.Fax;
                        cmd.Parameters.Add("@EnquirySource", SqlDbType.VarChar, 10).Value = _enquiriesObj.EnquirySource;
                        cmd.Parameters.Add("@IndustryCode", SqlDbType.VarChar, 10).Value = _enquiriesObj.IndustryCode;
                        cmd.Parameters.Add("@ProgressStatus", SqlDbType.VarChar, 10).Value = _enquiriesObj.ProgressStatus;
                        cmd.Parameters.Add("@EnquiryOwnerID", SqlDbType.UniqueIdentifier).Value = _enquiriesObj.EnquiryOwnerID;
                        //-----------------------//

                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = _enquiriesObj.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = _enquiriesObj.commonObj.UpdatedDate;
                        outputStatus = cmd.Parameters.Add("@UpdateStatus", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();


                    }
                }

                switch (outputStatus.Value.ToString())
                {
                    case "0":

                        throw new Exception(Cobj.UpdateFailure);

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
        #endregion UpdateEnquiry

        #region GetAllEnquiryList
        public  List<Enquiry>GetAllEnquiryList(Enquiry EqyObj)
        {
            List<Enquiry> EnquiryList = null;
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
                        cmd.CommandText = "[Office].[GetAllEnquiryList]";
                        cmd.Parameters.Add("@duration", SqlDbType.Int).Value = EqyObj.duration;
                        cmd.Parameters.Add("@enquirystatus", SqlDbType.Char).Value = EqyObj.EnquiryStatus;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                EnquiryList = new List<Enquiry>();
                                while (sdr.Read())
                                {
                                    Enquiry _enquiryObj = new Enquiry();
                                    {
                                            _enquiryObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _enquiryObj.ID);
                                            _enquiryObj.ContactTitle = (sdr["ContactTitle"].ToString() != "" ? sdr["ContactTitle"].ToString() : _enquiryObj.ContactTitle);
                                            _enquiryObj.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : _enquiryObj.ContactName);
                                            _enquiryObj.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _enquiryObj.CompanyName);
                                            _enquiryObj.EnquiryNo = (sdr["EnquiryNo"].ToString() != "" ? sdr["EnquiryNo"].ToString() : _enquiryObj.EnquiryNo);
                                            _enquiryObj.Mobile = (sdr["Mobile"].ToString() != "" ? sdr["Mobile"].ToString() : _enquiryObj.Mobile);
                                            _enquiryObj.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : _enquiryObj.Email);
                                            _enquiryObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _enquiryObj.GeneralNotes);
                                            _enquiryObj.EnquiryDate = (sdr["EnquiryDate"].ToString() != "" ? DateTime.Parse(sdr["EnquiryDate"].ToString()).ToString(s.dateformat) : _enquiryObj.EnquiryDate);
                                            _enquiryObj.LandLine = (sdr["LandLine"].ToString() != "" ? sdr["LandLine"].ToString() : _enquiryObj.LandLine);
                                           _enquiryObj.EnquirySource = (sdr["EnquirySource"].ToString() != "" ? sdr["EnquirySource"].ToString() : _enquiryObj.EnquirySource);
                                        _enquiryObj.IndustryName = (sdr["IndustryName"].ToString() != "" ? sdr["IndustryName"].ToString() : _enquiryObj.IndustryName);
                                        _enquiryObj.EnquiryStatus = (sdr["EnquiryStatus"].ToString() != "" ? sdr["EnquiryStatus"].ToString() : _enquiryObj.EnquiryStatus);
                                        _enquiryObj.LeadOwner = (sdr["ownername"].ToString() != "" ? sdr["ownername"].ToString() : _enquiryObj.LeadOwner);
                                    }
                                    EnquiryList.Add(_enquiryObj);
                                }
                                }
                            }
                        }
                    } }
            
                    catch (Exception ex)
            {
                throw ex;
            }

            return EnquiryList;

        }
        #endregion GetAllEnquiryList

        #region GetRecentEnquiryList
        public List<Enquiry> GetRecentEnquiryList()
        {
            List<Enquiry> EnquiryList = null;
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
                        cmd.CommandText = "[Office].[GetRecentEnquiryList]";                      
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                EnquiryList = new List<Enquiry>();
                                while (sdr.Read())
                                {
                                    Enquiry _enquiryObj = new Enquiry();
                                    {
                                        _enquiryObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _enquiryObj.ID);
                                        _enquiryObj.ContactTitle = (sdr["ContactTitle"].ToString() != "" ? sdr["ContactTitle"].ToString() : _enquiryObj.ContactTitle);
                                        _enquiryObj.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : _enquiryObj.ContactName);
                                        _enquiryObj.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _enquiryObj.CompanyName);
                                        _enquiryObj.EnquiryNo = (sdr["EnquiryNo"].ToString() != "" ? sdr["EnquiryNo"].ToString() : _enquiryObj.EnquiryNo);
                                        _enquiryObj.Mobile = (sdr["Mobile"].ToString() != "" ? sdr["Mobile"].ToString() : _enquiryObj.Mobile);
                                        _enquiryObj.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : _enquiryObj.Email);
                                        _enquiryObj.EnquiryStatus = (sdr["EnquiryStatus"].ToString() != "" ? sdr["EnquiryStatus"].ToString() : _enquiryObj.CompanyName);
                                        _enquiryObj.EnquiryStatusCode = (sdr["EnquiryStatusCode"].ToString() != "" ? sdr["EnquiryStatusCode"].ToString() : _enquiryObj.EnquiryStatusCode);
                                        _enquiryObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _enquiryObj.GeneralNotes);
                                        _enquiryObj.EnquiryDate = (sdr["EnquiryDate"].ToString() != "" ? DateTime.Parse(sdr["EnquiryDate"].ToString()).ToString(s.dateformat) : _enquiryObj.EnquiryDate);
                                    }
                                    EnquiryList.Add(_enquiryObj);
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

            return EnquiryList;

        }
        #endregion GetRecentEnquiryList

        #region GetEnquirySummary
        public EnquirySummary GetEnquirySummary()
        {
            EnquirySummary ES = null;
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
                        cmd.CommandText = "[Office].[GetEnquirySummary]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                ES = new EnquirySummary();
                                while (sdr.Read())
                                {                                    
                                    {
                                        ES.Total = (sdr["Total"].ToString() != "" ? int.Parse(sdr["Total"].ToString()) : ES.Total);
                                        ES.Converted = (sdr["Converted"].ToString() != "" ? int.Parse(sdr["Converted"].ToString()) : ES.Converted);
                                        ES.Open = (sdr["Open"].ToString() != "" ? int.Parse(sdr["Open"].ToString()) : ES.Open);
                                        ES.NotConverted = (sdr["NotConverted"].ToString() != "" ? int.Parse(sdr["NotConverted"].ToString()) : ES.NotConverted);

                                    }
                                    
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

            return ES;

        }
        #endregion GetEnquirySummary

        #region SearchEnquiryList
        public List<Enquiry> SearchEnquiriesList(Enquiry EqyObj)
        {
            List<Enquiry> EnquiryList = null;
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
                        cmd.CommandText = "[Office].[SearchEnquiry]";
                        cmd.Parameters.Add("@FilterWords", SqlDbType.NVarChar,-1).Value = EqyObj.FilterWords;
                       
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                EnquiryList = new List<Enquiry>();
                                while (sdr.Read())
                                {
                                    Enquiry _enquiryObj = new Enquiry();
                                    {
                                        _enquiryObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _enquiryObj.ID);
                                        _enquiryObj.ContactTitle = (sdr["ContactTitle"].ToString() != "" ? sdr["ContactTitle"].ToString() : _enquiryObj.ContactTitle);
                                        _enquiryObj.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : _enquiryObj.ContactName);
                                        _enquiryObj.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _enquiryObj.CompanyName);
                                        _enquiryObj.EnquiryNo = (sdr["EnquiryNo"].ToString() != "" ? sdr["EnquiryNo"].ToString() : _enquiryObj.EnquiryNo);
                                        _enquiryObj.Mobile = (sdr["Mobile"].ToString() != "" ? sdr["Mobile"].ToString() : _enquiryObj.Mobile);
                                        _enquiryObj.EnquiryStatus = (sdr["EnquiryStatus"].ToString() != "" ? sdr["EnquiryStatus"].ToString() : _enquiryObj.EnquiryStatus); 
                                        _enquiryObj.EnquiryDate = (sdr["EnquiryDate"].ToString() != "" ? DateTime.Parse(sdr["EnquiryDate"].ToString()).ToString(s.dateformat) : _enquiryObj.EnquiryDate);
                                    }
                                    EnquiryList.Add(_enquiryObj);
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

            return EnquiryList;

        }
        #endregion SearchEnquiryList


    }
}