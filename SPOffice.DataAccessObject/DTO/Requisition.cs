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
        //External references
        public RawMaterial RawMaterialObj { get; set; }
    }
}