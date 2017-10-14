
using SPOffice.UserInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserInterface.Models
{
    public class DashboardViewModel
    {
        public string Code { get; set; }
        public string Type { get; set; }
        public string TypeDesc { get; set; }
        public string OpeningPaymentMode { get; set; }
        public decimal OpeningBalance { get; set; }
        public DateTime OpeningAsOfDate { get; set; }
        public bool ISEmploy { get; set; }
        public decimal Amount { get; set; }
        public string account { get; set; }
    }

    public class AdminDashboardViewModel
    {
    }


    public class DashboardStatusViewModel
    {
        public string duration { get; set; }
        public int OpenEnquiryCount { get; set; }
        public int ConvertedEnquiryCount { get; set; }
        public int NonConvertedEnquiryCount { get; set; }
    }

    public class RecentEnquiriesViewModel
    {
       public List<EnquiryViewModel> EnquiryList { get; set; }
        public string BaseUrl { get; set; }

    }






    }


 
