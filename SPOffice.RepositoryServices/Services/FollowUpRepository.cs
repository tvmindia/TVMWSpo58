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
                            cmd.Parameters.Add("@FollowUpTime", SqlDbType.DateTime).Value = _followupObj.FollowUpTime;
                            cmd.Parameters.Add("@EnquiryID", SqlDbType.UniqueIdentifier).Value = _followupObj.EnquiryID;
                            cmd.Parameters.Add("@Status", SqlDbType.VarChar, 150).Value = _followupObj.Status;
                            cmd.Parameters.Add("@ReminderType", SqlDbType.VarChar, 10).Value = _followupObj.ReminderType;
                            cmd.Parameters.Add("@ContactName", SqlDbType.VarChar, 10).Value = _followupObj.ContactName;
                            cmd.Parameters.Add("@Subject", SqlDbType.VarChar, 250).Value = _followupObj.Subject;
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
        public object UpdateFollowUp(FollowUp _followupObj)
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
                            cmd.Parameters.Add("@FollowUpTime", SqlDbType.DateTime).Value = _followupObj.FollowUpTime;
                            cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = _followupObj.Status;
                            cmd.Parameters.Add("@ReminderType", SqlDbType.VarChar, 10).Value = _followupObj.ReminderType;
                            cmd.Parameters.Add("@ContactName", SqlDbType.VarChar, 50).Value = _followupObj.ContactName;
                            cmd.Parameters.Add("@Subject", SqlDbType.VarChar, 250).Value = _followupObj.Subject;
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
                return new
                {
                    Status = outputStatus.Value.ToString(),
                    Message = Cobj.UpdateSuccess
                };
            }
        #endregion UpdateFollowUp



        #region GetAllFollowup

        public FollowUp GetFollowUpDetails(Guid EnquiryID)
        {
            FollowUp _followUpObj = new FollowUp();
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
                                while (sdr.Read())
                                {
                                 
                                    {
                                        _followUpObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _followUpObj.ID);
                                        _followUpObj.EnquiryID = (sdr["EnquiryID"].ToString() != "" ? Guid.Parse(sdr["EnquiryID"].ToString()) : _followUpObj.EnquiryID);
                                        _followUpObj.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : _followUpObj.ContactName);
                                        _followUpObj.FollowUpDate = (sdr["FollowUpDate"].ToString() != "" ? DateTime.Parse(sdr["FollowUpDate"].ToString()): _followUpObj.FollowUpDate);
                                        _followUpObj.FollowUpTime = (sdr["FollowUpTime"].ToString() != "" ? sdr["FollowUpTime"].ToString() : _followUpObj.FollowUpTime);
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

        #endregion GetAllFollowup
    }
}