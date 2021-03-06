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
   
        public class FollowUpRepository : IFollowUpRepository
    {
            AppConst Cobj = new AppConst();
            Settings s = new Settings();
            private IDatabaseFactory _databaseFactory;
            public FollowUpRepository(IDatabaseFactory databaseFactory)
            {
                _databaseFactory = databaseFactory;
            }

        #region InsertFollowUp
        public FollowUp InsertFollowUp(FollowUp _followupObj)
            {
                try
                {
                    SqlParameter outputStatus, outputID = null;
                    using (SqlConnection con = _databaseFactory.GetDBConnection())
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            cmd.Connection = con;
                            cmd.CommandText = "[Office].[InsertFollowUp]";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@FollowUpDate", SqlDbType.DateTime).Value = _followupObj.FollowUpDate;
                            if (_followupObj.HdnFollowUpTime == "" || _followupObj.HdnFollowUpTime == null)
                            {
                                _followupObj.HdnFollowUpTime = "10:00:00";
                            }

                            cmd.Parameters.Add("@FollowUpTime", SqlDbType.Time).Value =_followupObj.HdnFollowUpTime;
                            cmd.Parameters.Add("@EnquiryID", SqlDbType.UniqueIdentifier).Value = _followupObj.EnquiryID;
                            cmd.Parameters.Add("@Status", SqlDbType.VarChar, 150).Value = _followupObj.Status;
                            cmd.Parameters.Add("@ReminderType", SqlDbType.VarChar, 10).Value = _followupObj.ReminderType;
                            cmd.Parameters.Add("@ContactName", SqlDbType.VarChar, 10).Value = _followupObj.ContactName;
                            cmd.Parameters.Add("@Subject", SqlDbType.VarChar, 250).Value = _followupObj.Subject;

                            cmd.Parameters.Add("@Priority", SqlDbType.VarChar, 10).Value = _followupObj.Priority;
                            cmd.Parameters.Add("@RemindPriorTo", SqlDbType.SmallInt).Value = _followupObj.RemindPriorTo;
                            cmd.Parameters.Add("@GeneralNotes", SqlDbType.NVarChar, -1).Value = _followupObj.GeneralNotes;

                            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = _followupObj.commonObj.CreatedBy;
                            cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = _followupObj.commonObj.CreatedDate;



                            outputStatus = cmd.Parameters.Add("@InsertStatus", SqlDbType.SmallInt);
                            outputStatus.Direction = ParameterDirection.Output;
                            outputID = cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier);
                            outputID.Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();


                        }
                    }

                    switch (outputStatus.Value.ToString())
                    {
                        case "0":
                            AppConst Cobj = new AppConst();
                            throw new Exception(Cobj.InsertFailure);
                        case "1":
                        _followupObj.
                                ID = Guid.Parse(outputID.Value.ToString());
                            break;
                        default:
                            break;
                    }

                }
                catch (Exception ex)
                {

                    throw ex;
                }
                return _followupObj;
            }
        #endregion InsertFollowUp

        #region UpdateFollowUp
        public FollowUp UpdateFollowUp(FollowUp _followupObj)
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
                            cmd.CommandText = "[Office].[UpdateFollowUp]";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = _followupObj.ID;
                            cmd.Parameters.Add("@FollowUpDate", SqlDbType.DateTime).Value = _followupObj.FollowUpDate;
                            cmd.Parameters.Add("@EnquiryID", SqlDbType.UniqueIdentifier).Value = _followupObj.EnquiryID;
                            cmd.Parameters.Add("@FollowUpTime", SqlDbType.DateTime).Value = _followupObj.HdnFollowUpTime; 
                            cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = _followupObj.Status;
                            cmd.Parameters.Add("@ReminderType", SqlDbType.VarChar, 10).Value = _followupObj.ReminderType;
                            cmd.Parameters.Add("@ContactName", SqlDbType.VarChar, 50).Value = _followupObj.ContactName;
                            cmd.Parameters.Add("@Subject", SqlDbType.VarChar, 250).Value = _followupObj.Subject;

                            cmd.Parameters.Add("@Priority", SqlDbType.VarChar, 10).Value = _followupObj.Priority;
                            cmd.Parameters.Add("@RemindPriorTo", SqlDbType.SmallInt).Value = _followupObj.RemindPriorTo;
                            cmd.Parameters.Add("@GeneralNotes", SqlDbType.NVarChar, -1).Value = _followupObj.GeneralNotes;

                            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = _followupObj.commonObj.UpdatedBy;
                            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = _followupObj.commonObj.UpdatedDate;
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
            return _followupObj;
                //return new
                //{
                //    Status = outputStatus.Value.ToString(),
                //    Message = Cobj.UpdateSuccess
                //};
        }
        #endregion UpdateFollowUp



        #region GetAllFollowup

        public List<FollowUp> GetFollowUpDetails(Guid EnquiryID)
        {
            List<FollowUp> followUpList = null;
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
                        cmd.CommandText = "[Office].[GetFollowUpList]";
                        cmd.Parameters.Add("@EnquiryID", SqlDbType.UniqueIdentifier).Value = EnquiryID;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                followUpList = new List<FollowUp>();
                                while (sdr.Read())
                                {
                                    FollowUp _followUpObj = new FollowUp();
                                    {
                                        _followUpObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _followUpObj.ID);
                                        _followUpObj.EnquiryID = (sdr["EnquiryID"].ToString() != "" ? Guid.Parse(sdr["EnquiryID"].ToString()) : _followUpObj.EnquiryID);
                                        _followUpObj.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : _followUpObj.ContactName);
                                        _followUpObj.FollowUpDate = (sdr["FollowUpDate"].ToString() != "" ? DateTime.Parse(sdr["FollowUpDate"].ToString()).ToString(s.dateformat): _followUpObj.FollowUpDate);
                                        _followUpObj.FollowUpTime = (sdr["FollowUpTime"].ToString() != "" ? DateTime.Parse(sdr["FollowUpTime"].ToString()).ToString("hh:mm tt") : _followUpObj.FollowUpTime);
                                        _followUpObj.Priority = (sdr["Priority"].ToString() != "" ? sdr["Priority"].ToString() : _followUpObj.Priority);
                                        _followUpObj.PriorityDescription = sdr["PriorityDescription"].ToString();
                                        _followUpObj.Subject = (sdr["Subject"].ToString() != "" ? sdr["Subject"].ToString() : _followUpObj.Subject);
                                        _followUpObj.RemindPriorTo = (sdr["RemindPriorTo"].ToString() != "" ? sdr["RemindPriorTo"].ToString() : _followUpObj.RemindPriorTo);
                                        _followUpObj.ReminderType = (sdr["ReminderType"].ToString() != "" ? sdr["ReminderType"].ToString() : _followUpObj.ReminderType);
                                        _followUpObj.Status = (sdr["Status"].ToString() != "" ? sdr["Status"].ToString() : _followUpObj.Status);
                                        _followUpObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _followUpObj.GeneralNotes);
                                        
                                    }
                                    followUpList.Add(_followUpObj);
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

            return followUpList;
        }

        #endregion GetAllFollowup

        #region GetFollowupDetailsByFollowUpID

        public FollowUp GetFollowupDetailsByFollowUpID(Guid ID)
        {
            FollowUp _followUpObj = null;
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
                        cmd.CommandText = "[Office].[GetFollowUpDetailsByFollowUpId]";
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = ID;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                               
                                if (sdr.Read())
                                {
                                    _followUpObj = new FollowUp();
                                    {
                                        _followUpObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _followUpObj.ID);
                                        _followUpObj.EnquiryID = (sdr["EnquiryID"].ToString() != "" ? Guid.Parse(sdr["EnquiryID"].ToString()) : _followUpObj.EnquiryID);
                                        _followUpObj.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : _followUpObj.ContactName);
                                        _followUpObj.FollowUpDate = (sdr["FollowUpDate"].ToString() != "" ? DateTime.Parse(sdr["FollowUpDate"].ToString()).ToString(s.dateformat) : _followUpObj.FollowUpDate);
                                        _followUpObj.FollowUpTime = (sdr["FollowUpTime"].ToString() != "" ? DateTime.Parse(sdr["FollowUpTime"].ToString()).ToString("hh:mm tt") : _followUpObj.FollowUpTime);
                                        _followUpObj.Priority = (sdr["Priority"].ToString() != "" ? sdr["Priority"].ToString() : _followUpObj.Priority);
                                        _followUpObj.Subject = (sdr["Subject"].ToString() != "" ? sdr["Subject"].ToString() : _followUpObj.Subject);
                                        _followUpObj.RemindPriorTo = (sdr["RemindPriorTo"].ToString() != "" ? sdr["RemindPriorTo"].ToString() : _followUpObj.RemindPriorTo);
                                        _followUpObj.ReminderType = (sdr["ReminderType"].ToString() != "" ? sdr["ReminderType"].ToString() : _followUpObj.ReminderType);
                                        _followUpObj.Status = (sdr["Status"].ToString() != "" ? sdr["Status"].ToString() : _followUpObj.Status);
                                        _followUpObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _followUpObj.GeneralNotes);

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

            return _followUpObj;
        }

        #endregion GetFollowupDetailsByFollowUpID




        #region GetFollowuponaDate

        public List<FollowUp> GetFollowUpDetailsOnDate(DateTime onDate)
        {
            List<FollowUp> followUpList = null;
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
                        cmd.CommandText = "[Office].[GetFollowUpByDate]";
                        cmd.Parameters.Add("@OnDate", SqlDbType.Date).Value = onDate;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                followUpList = new List<FollowUp>();
                                while (sdr.Read())
                                {
                                    FollowUp _followUpObj = new FollowUp();
                                    {
                                        _followUpObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _followUpObj.ID);
                                        _followUpObj.EnquiryID = (sdr["EnquiryID"].ToString() != "" ? Guid.Parse(sdr["EnquiryID"].ToString()) : _followUpObj.EnquiryID);
                                        _followUpObj.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : _followUpObj.ContactName);
                                        _followUpObj.FollowUpDate = (sdr["FollowUpDate"].ToString() != "" ? DateTime.Parse(sdr["FollowUpDate"].ToString()).ToString(s.dateformat) : _followUpObj.FollowUpDate);
                                        _followUpObj.FollowUpTime = (sdr["FollowUpTime"].ToString() != "" ? DateTime.Parse(sdr["FollowUpTime"].ToString()).ToString("hh:mm tt") : _followUpObj.FollowUpTime);
                                        _followUpObj.Priority = (sdr["Priority"].ToString() != "" ? sdr["Priority"].ToString() : _followUpObj.Priority);
                                        _followUpObj.Subject = (sdr["Subject"].ToString() != "" ? sdr["Subject"].ToString() : _followUpObj.Subject);
                                        _followUpObj.RemindPriorTo = (sdr["RemindPriorTo"].ToString() != "" ? sdr["RemindPriorTo"].ToString() : _followUpObj.RemindPriorTo);
                                        _followUpObj.ReminderType = (sdr["ReminderType"].ToString() != "" ? sdr["ReminderType"].ToString() : _followUpObj.ReminderType);
                                        _followUpObj.Status = (sdr["Status"].ToString() != "" ? sdr["Status"].ToString() : _followUpObj.Status);
                                        _followUpObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _followUpObj.GeneralNotes);
                                        _followUpObj.Company = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _followUpObj.Company);
                                        _followUpObj.Contact = (sdr["Mobile"].ToString() != "" ? sdr["Mobile"].ToString() : _followUpObj.Contact);

                                    }
                                    followUpList.Add(_followUpObj);
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

            return followUpList;
        }

        #endregion GetAllFollowup



        #region GetRecentFollowUps
        public List<FollowUp> GetRecentFollowUpCount(DateTime? Today)
        {
            List<FollowUp> followUpList = null;
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
                        cmd.CommandText = "[Office].[GetRecentFollowUpList]";
                        cmd.Parameters.Add("@Today", SqlDbType.Date).Value = Today;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                followUpList = new List<FollowUp>();
                                while (sdr.Read())
                                {
                                    FollowUp _followUpObj = new FollowUp();
                                    {
                                        _followUpObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _followUpObj.ID);
                                        _followUpObj.EnquiryID = (sdr["EnquiryID"].ToString() != "" ? Guid.Parse(sdr["EnquiryID"].ToString()) : _followUpObj.EnquiryID);
                                        _followUpObj.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : _followUpObj.ContactName);
                                        _followUpObj.FollowUpDate = (sdr["FollowUpDate"].ToString() != "" ? DateTime.Parse(sdr["FollowUpDate"].ToString()).ToString(s.dateformat) : _followUpObj.FollowUpDate);
                                        _followUpObj.FollowUpTime = (sdr["FollowUpTime"].ToString() != "" ? DateTime.Parse(sdr["FollowUpTime"].ToString()).ToString("hh:mm tt") : _followUpObj.FollowUpTime);
                                        _followUpObj.Priority = (sdr["Priority"].ToString() != "" ? sdr["Priority"].ToString() : _followUpObj.Priority);
                                        _followUpObj.Subject = (sdr["Subject"].ToString() != "" ? sdr["Subject"].ToString() : _followUpObj.Subject);
                                        _followUpObj.RemindPriorTo = (sdr["RemindPriorTo"].ToString() != "" ? sdr["RemindPriorTo"].ToString() : _followUpObj.RemindPriorTo);
                                        _followUpObj.ReminderType = (sdr["ReminderType"].ToString() != "" ? sdr["ReminderType"].ToString() : _followUpObj.ReminderType);
                                        _followUpObj.Status = (sdr["Status"].ToString() != "" ? sdr["Status"].ToString() : _followUpObj.Status);
                                        _followUpObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _followUpObj.GeneralNotes);
                                        _followUpObj.Company = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _followUpObj.Company);
                                        _followUpObj.Contact = (sdr["Mobile"].ToString() != "" ? sdr["Mobile"].ToString() : _followUpObj.Contact);

                                    }
                                    followUpList.Add(_followUpObj);
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

            return followUpList;
        }
        #endregion GetRecentFollowUps

        #region DeleteFollowUp
        public object DeleteFollowUp(Guid ID)
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
                        cmd.CommandText = "[Office].[DeleteFollowUpDetails]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = ID;
                        outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                    }
                }
                AppConst Cobj = new AppConst();
                switch (outputStatus.Value.ToString())
                {
                    case "0":
                        //Const Cobj = new Const();
                        throw new Exception(Cobj.DeleteFailure);
                    case "1":
                        return new
                        {
                            status = outputStatus.Value.ToString(),
                            Message = Cobj.DeleteSuccess
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

            };

        }

        #endregion DeleteFollowUp


    }
}