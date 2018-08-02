using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Contracts
{
    public interface IRequisitionBusiness
    {
        List<Requisition> GetUserRequisitionList(string LoginName, Guid AppID,bool IsAdminOrCeo,ReqAdvanceSearch ReqAdvanceSearchObj, bool ShowFinalApproved = false,string duration="All");
        List<RequisitionDetail> GetRequisitionDetailList(Guid ID);
        object InsertRequisition(Requisition RequisitionObj,bool isAdminOrCeo);
        object UpdateRequisition(Requisition RequisitionObj);
        Requisition GetRequisitionDetails(Guid ID, string LoginName);
        object DeleteRequisitionDetailByID(Guid ID);
        object DeleteRequisitionByID(Guid ID);
        RequisitionOverViewCount GetRequisitionOverViewCount(string UserName, bool IsAdminORCeo);
        Requisition ApproveRequisition(Requisition RequisitionObj, bool IsAdminORCeo);
        void SendToFCMManager(string titleString, string descriptionString, Boolean isCommon, string CompanyCode = "", string click_action = "");
        void SendToFCMCEO(string titleString, string descriptionString, Boolean isCommon, string click_action = "");
        void SendToFCMMangerOnly(string titleString, string descriptionString, Boolean isCommon, string click_action = "");
        object UpdateNotification(Requisition requisitionObj);
        RequisitionOverViewCount GetRequisitionCount(RequisitionOverViewCount reqObj,bool isAdminOrCeo);
    }
}