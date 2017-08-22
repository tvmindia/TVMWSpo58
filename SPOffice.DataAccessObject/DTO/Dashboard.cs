using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class Dashboard
    {

    }


    public class DashboardStatus
    {

        public string duration { get; set; }  
        public int OpenEnquiryCount { get; set; }
        public int ConvertedEnquiryCount { get; set; }
        public int NonConvertedEnquiryCount { get; set; }
    }
}