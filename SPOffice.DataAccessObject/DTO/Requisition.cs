using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class Requisition
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
        public RequisitionDetail RequisitionDetailObj { get; set; }
        public List<RequisitionDetail> RequisitionDetailList { get; set; }
        public Company CompanyObj { get; set; }
        public Common CommonObj { get; set; }
        //Properties for client side functionalities
        public string ReqDateFormatted { get; set; }
        public string ManagerApprovalDateFormatted { get; set; }
        public string FinalApprovalDateFormatted { get; set; }
        public string DetailXML { get; set; }
        public bool IsApprover { get; set; }
       
    }
    public class RequisitionDetail
    {
        public Guid ID { get; set; }
        public Guid ReqID { get; set; }
        public Guid MaterialID { get; set; }
        public string Description { get; set; }
        public string ExtendedDescription { get; set; }
        public string CurrStock { get; set; }
        public decimal AppxRate { get; set; }
        public string RequestedQty { get; set; }
        public string OrderedQty { get; set; }
        public string UnitCode { get; set; }
        public string POQty { get; set; }
        public string ReqNo { get; set; }
        //External references
        public RawMaterial RawMaterialObj { get; set; }
    }
    public class RequisitionOverViewCount
    {
        public int? OpenCount { get; set; }
        public int? AllCount { get; set; }
        public int? PendingManagerCount { get; set; }
        public int? PendingFinalCount { get; set; }
    }
    public class ReqAdvanceSearch
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ReqStatus { get; set; }
        public string ReqSearch { get; set; }
        public bool ManagerApproved { get; set; }
        public bool FinalApproved { get; set; }
    }
}