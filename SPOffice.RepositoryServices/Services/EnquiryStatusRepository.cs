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
    public class EnquiryStatusRepository: IEnquiryStatusRepository
    {
        private IDatabaseFactory _databaseFactory;
        public EnquiryStatusRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        #region GetAllEnquiryStatusList

        public List<EnquiryStatus> GetAllEnquiryStatusList()
        {
            List<EnquiryStatus> EnquiryStatusList = null;
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
                        cmd.CommandText = "[Office].[GetAllEnquiryStatus]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                EnquiryStatusList = new List<EnquiryStatus>();

                                while (sdr.Read())
                                {
                                    EnquiryStatus _enquiryObj = new EnquiryStatus();
                                    {
                                        _enquiryObj.StatusCode = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : _enquiryObj.StatusCode);
                                        _enquiryObj.Status = (sdr["Status"].ToString() != "" ? sdr["Status"].ToString() : _enquiryObj.Status);
                                    }
                                    EnquiryStatusList.Add(_enquiryObj);
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
            return EnquiryStatusList;
        }



        #endregion GetAllEnquiryStatusList
    }
}