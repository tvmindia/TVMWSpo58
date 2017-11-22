﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace SPOffice.UserInterface.Models
{
    public class ReportViewModel
    {
        public Guid AppID { get; set; }
        public Guid ID { get; set; }
        public string ReportName { get; set; }
        public string ReportDescription { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ReportGroup { get; set; }
        public int GroupOrder { get; set; }
        public string SPName { get; set; }
        public string SQL { get; set; }
        public int ReportOrder { get; set; }
    }


    public class CourierReportViewModel
    {
        public Guid? ID { get; set; }
        public Guid hdnFileID { get; set; }          
        public string Type { get; set; }
        public List<SelectListItem> CourierTypeList { get; set; }        
       
        public string TransactionDate { get; set; }
        public string Track { get; set; }    
       
        public string SourceName { get; set; }        
        public string SourceAddress { get; set; }      
       
        public string DestName { get; set; }        
        public string DestAddress { get; set; }     
        public string DistributedTo { get; set; }       
        public string DistributionDate { get; set; }      
       
        public string AgencyCode { get; set; }
        public CourierAgencyViewModel courierAgency { get; set; }
        public List<SelectListItem> AgencyList { get; set; }           
        public string TrackingRefNo { get; set; }     
       
        public string GeneralNotes { get; set; }         
        public string TrackingURL { get; set; }
        public CommonViewModel commonObj { get; set; }
    }
}