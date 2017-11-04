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
    public class PriorityRepository: IPriorityRepository
    {
        private IDatabaseFactory _databaseFactory;
        public PriorityRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        #region GetAllPriorityList

        public List<Priority> GetAllPriorityList()
        {
            List<Priority> PriorityList = null;
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
                        cmd.CommandText = "[Office].[GetAllPriorities]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                PriorityList = new List<Priority>();

                                while (sdr.Read())
                                {
                                    Priority _priorityObj = new Priority();
                                    {
                                        _priorityObj.PriorityCode = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : _priorityObj.PriorityCode);
                                        _priorityObj.PriorityDescription = (sdr["Priority"].ToString() != "" ? sdr["Priority"].ToString() : _priorityObj.PriorityDescription);
                                    }
                                    PriorityList.Add(_priorityObj);
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
            return PriorityList;
        }



        #endregion GetAllPriorityList
    }
}