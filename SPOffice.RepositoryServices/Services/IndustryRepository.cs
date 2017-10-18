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
    public class IndustryRepository:IIndustryRepository
    {
        private IDatabaseFactory _databaseFactory;
        public IndustryRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        #region GetAllIndustryList

        public List<Industry> GetAllIndustryList()
        {
            List<Industry> IndustryList = null;
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
                        cmd.CommandText = "[Office].[GetAllIndustries]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                IndustryList = new List<Industry>();

                                while (sdr.Read())
                                {
                                    Industry _industryObj = new Industry();
                                    {
                                        _industryObj.IndustryCode = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : _industryObj.IndustryCode);
                                        _industryObj.IndustryName = (sdr["IndustryName"].ToString() != "" ? sdr["IndustryName"].ToString() : _industryObj.IndustryName);
                                    }
                                    IndustryList.Add(_industryObj);
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
            return IndustryList;
        }



        #endregion GetAllIndustryList
    }
}