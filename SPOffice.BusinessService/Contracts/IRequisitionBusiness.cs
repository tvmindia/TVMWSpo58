using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Contracts
{
    public interface IRequisitionBusiness
    {
        List<Requisition> GetUserRequisitionList(string LoginName, Guid AppID,bool IsAdminOrCeo,ReqAdvanceSearch ReqAdvanceSearchObj);
        List<RequisitionDetail> GetRequisitionDetailList(Guid ID);
        object InsertRequisition(Requisition RequisitionObj,bool isAdminOrCeo);
        object UpdateRequisition(Requisition RequisitionObj);
        Requisition GetRequisitionDetails(Guid ID, string LoginName);
        object DeleteRequisitionDetailByID(Guid ID);
        RequisitionOverViewCount GetRequisitionOverViewCount(string UserName, bool IsAdminORCeo);
        Requisition ApproveRequisition(Requisition RequisitionObj, bool IsAdminORCeo);
        void SendToFCMManager(string titleString, string descriptionString, Boolean isCommon, string CompanyCode = "");
        void SendToFCMCEO(string titleString, string descriptionString, Boolean isCommon);
        object UpdateNotification(Requisition requisitionObj);
    }
}