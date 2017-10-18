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
    public class EnquirySourceRepository
    {
        private IDatabaseFactory _databaseFactory;
        public EnquirySourceRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        #region GetAllIndustryList

        public List<EnquirySource> GetAllEnquirySourceList()
        {
            List<EnquirySource> EnquirySourceList = null;
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
                        cmd.CommandText = "[Office].[GetAllEnquirySources]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                EnquirySourceList = new List<EnquirySource>();

                                while (sdr.Read())
                                {
                                    EnquirySource _enquiryObj = new EnquirySource();
                                    {
                                        _enquiryObj.SourceCode = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : _enquiryObj.SourceCode);
                                        _enquiryObj.Source = (sdr["Source"].ToString() != "" ? sdr["Source"].ToString() : _enquiryObj.Source);
                                    }
                                    EnquirySourceList.Add(_enquiryObj);
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
            return EnquirySourceList;
        }



        #endregion GetAllIndustryList
    }
}