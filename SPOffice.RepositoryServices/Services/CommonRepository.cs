using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPOffice.DataAccessObject.DTO;
using System.Data.SqlClient;
using System.Data;

namespace SPOffice.RepositoryServices.Services
{
    public class CommonRepository : ICommonRepository
    {
        private IDatabaseFactory _databaseFactory;
        public CommonRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public List<POStatuses> GetAllPOStatuses()
        {
            List<POStatuses> StatusList = new List<POStatuses>();
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
                        cmd.CommandText = "[Office].[GetAllPOStatus]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {

                                while (sdr.Read())
                                {
                                    POStatuses _statusObj = new POStatuses();
                                    {
                                        _statusObj.Code = (sdr["Code"].ToString() != "" ? (sdr["Code"].ToString()) : _statusObj.Code);
                                        _statusObj.Description = (sdr["Status"].ToString() != "" ? sdr["Status"].ToString() : _statusObj.Description);
                                    }
                                    StatusList.Add(_statusObj);
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

            return StatusList;
        }
    }
}