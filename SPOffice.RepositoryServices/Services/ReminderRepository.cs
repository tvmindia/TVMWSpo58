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
    public class ReminderRepository: IReminderRepository
    {
        private IDatabaseFactory _databaseFactory;
        public ReminderRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        public List<Reminder> GetAllReminders()
        {
           List<Reminder> ReminderList = null;
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
                        cmd.CommandText = "[Office].[GetAllRemainderTypes]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                ReminderList = new List<Reminder>();

                                while (sdr.Read())
                                {
                                    Reminder _reminderObj = new Reminder();
                                    {
                                        _reminderObj.Code = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : _reminderObj.Code);
                                        _reminderObj.ReminderDesc = (sdr["Reminder"].ToString() != "" ? sdr["Reminder"].ToString() : _reminderObj.ReminderDesc);

                                    }
                                    ReminderList.Add(_reminderObj);
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

            return ReminderList;
        }

    }
}