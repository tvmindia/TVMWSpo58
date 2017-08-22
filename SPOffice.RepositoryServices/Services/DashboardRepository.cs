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
    public class DashboardRepository: IDashboardRepository
    {
        AppConst Cobj = new AppConst();
        Settings s = new Settings();
        private IDatabaseFactory _databaseFactory;
        public DashboardRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public DashboardStatus GetCountOfEnquiries(string duration)
        {
            DashboardStatus dashboardObj = new DashboardStatus();
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
                        cmd.CommandText = "[Office].[GetEnquiryStatusCount]";
                        cmd.Parameters.Add("@duration", SqlDbType.Int).Value = duration;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                while (sdr.Read())
                                {
                                    dashboardObj.OpenEnquiryCount= (sdr["OE"].ToString() != "" ? int.Parse (sdr["OE"].ToString()) : dashboardObj.OpenEnquiryCount);
                                    dashboardObj.ConvertedEnquiryCount = (sdr["CE"].ToString() != "" ? int.Parse(sdr["CE"].ToString()) : dashboardObj.ConvertedEnquiryCount);
                                    dashboardObj.NonConvertedEnquiryCount = (sdr["NCE"].ToString() != "" ? int.Parse(sdr["NCE"].ToString()) : dashboardObj.NonConvertedEnquiryCount);

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

            return dashboardObj;
        }
    }
}