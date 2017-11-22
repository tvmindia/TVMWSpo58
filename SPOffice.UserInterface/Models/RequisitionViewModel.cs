using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public List<RequisitionDetailViewModel> RequisitionDetailList { get; set; }
        public CompanyViewModel CompanyObj { get; set; }
        public CommonViewModel CommonObj { get; set; }
        //Properties for client side functionalities
        public string ReqDateFormatted { get; set; }
        public string ManagerApprovalDateFormatted { get; set; }
        public string FinalApprovalDateFormatted { get; set; }
        public string DetailXML { get; set; }
        public string PendingRequisitionCount { get; set; }
    }
    public class RequisitionDetailViewModel
    {
        public Guid ID { get; set; }
        public Guid ReqID { get; set; }
        public Guid? MaterialID { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.MultilineText)]
        public string ExtendedDescription { get; set; }
        public string CurrStock { get; set; }
        public decimal AppxRate { get; set; }
        public string RequestedQty { get; set; }
        //External references
        public string RequisitionDetailObject { get; set; }
        public RawMaterialViewModel RawMaterialObj { get; set; }
    }

}