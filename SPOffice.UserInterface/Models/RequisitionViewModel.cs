using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserInterface.Models;

namespace SPOffice.UserInterface.Models
{
    public class RequisitionViewModel
    {
        public Guid ID { get; set; }
        public string ReqNo { get; set; }
        public string Title { get; set; }
        public DateTime ReqDate { get; set; }
        public string ReqForCompany { get; set; }
        public string ReqStatus { get; set; }
        public bool ManagerApproved { get; set; }
        public DateTime ManagerApprovalDate { get; set; }
        public bool FinalApproval { get; set; }
        public DateTime FinalApprovalDate { get; set; }
        //External references
        public RequisitionDetailViewModel RequisitionDetailObj { get; set; }
        public CompanyViewModel CompanyObj { get; set; }
        public CommonViewModel CommonObj { get; set; }
        //Properties for client side functionalities
        public string ReqDateFormatted { get; set; }
        public string ManagerApprovalDateFormatted { get; set; }
        public string FinalApprovalDateFormatted { get; set; }
    }
    public class RequisitionDetailViewModel
    {
        public Guid ID { get; set; }
        public Guid ReqID { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public string ExtendedDescription { get; set; }
        public int CurrStock { get; set; }
        public decimal AppxRate { get; set; }
        public int RequestedQty { get; set; }
        //External references
        public RawMaterialViewModel RawMaterialObj { get; set; }
    }

}