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
    public class ProgressStatusRepository: IProgressStatusRepository
    {
        private IDatabaseFactory _databaseFactory;
        public ProgressStatusRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        #region GetAllProgressStatusList

        public List<ProgressStatus> GetAllProgressStatusList()
        {
            List<ProgressStatus> ProgressStatusList = null;
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
                        cmd.CommandText = "[Office].[GetAllProgressStatus]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                ProgressStatusList = new List<ProgressStatus>();

                                while (sdr.Read())
                                {
                                    ProgressStatus _statusObj = new ProgressStatus();
                                    {
                                        _statusObj.StatusCode = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : _statusObj.StatusCode);
                                        _statusObj.Status = (sdr["Status"].ToString() != "" ? sdr["Status"].ToString() : _statusObj.Status);
                                    }
                                    ProgressStatusList.Add(_statusObj);
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
            return ProgressStatusList;
        }

        #endregion GetAllProgressStatusList

    }
}