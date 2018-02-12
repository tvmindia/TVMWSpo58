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
        [Display(Name = "Requisition No.")]
        public string ReqNo { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is missing")]
        public string Title { get; set; }        
        public DateTime ReqDate { get; set; }
        [Display(Name = "Requested Company")]
        [Required(ErrorMessage = "Company is missing")]
        public string ReqForCompany { get; set; }
        public string hdnReqForCompany { get; set; }
        [Display(Name = "Requisition Status")]
        public string ReqStatus { get; set; }
        public bool ManagerApproved { get; set; }
        public DateTime ManagerApprovalDate { get; set; }
        public bool FinalApproval { get; set; }
        public DateTime FinalApprovalDate { get; set; }
        public string RequisitionBy { get; set; }
        //External references
        public RequisitionDetailViewModel RequisitionDetailObj { get; set; }
        public UserViewModel userObj { get; set; }
        public List<RequisitionDetailViewModel> RequisitionDetailList { get; set; }
        public CompanyViewModel CompanyObj { get; set; }
        public CommonViewModel CommonObj { get; set; }
        //Properties for client side functionalities
        [Display(Name = "Requisition Date")]
        [Required(ErrorMessage = "Date is missing")]
        public string ReqDateFormatted { get; set; }
        public string ManagerApprovalDateFormatted { get; set; }
        public string FinalApprovalDateFormatted { get; set; }
        public string DetailXML { get; set; }
        public bool IsApprover { get; set; }
        public bool ViewOnly { get; set; }
        public ReqAdvanceSearchViewModel ReqAdvSearchObj { get; set; }

    }
    public class RequisitionDetailViewModel
    {
        public Guid ID { get; set; }
        public Guid ReqID { get; set; }
        public Guid? MaterialID { get; set; }
        public Guid LinkID { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.MultilineText)]
        public string ExtendedDescription { get; set; }
        public string CurrStock { get; set; }
        public decimal? AppxRate { get; set; }
        public string RequestedQty { get; set; }
        public string OrderedQty { get; set; }
        public string UnitCode { get; set; }
        public string POQty { get; set; }
        public string ReqNo { get; set; }
        //External references
        public string RequisitionDetailObject { get; set; }
        public RawMaterialViewModel RawMaterialObj { get; set; }
    }
    public class RequisitionOverViewCountViewModel
    {
        public int? OpenCount { get; set; }
        public int? AllCount { get; set; }
        public int? CloseCount { get; set; }
        public int? PendingManagerCount { get; set; }
        public int? PendingFinalCount { get; set; }
        public bool IsAdminOrCeo { get; set; }
        public UserViewModel userObj { get; set; }
        public string duration { get; set; }
    }

    public class ReqAdvanceSearchViewModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ReqStatus { get; set; }
        public string ReqSearch { get; set; }
        public bool ManagerApproved { get; set; }
        public bool FinalApproved { get; set; }
    }

    public  class RequisitionSummaryViewModel
    {
        public int Open { get; set; }
        public int All { get; set; }
    }

}