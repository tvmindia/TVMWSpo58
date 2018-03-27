using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.RepositoryServices.Contracts
{
    public interface IRequisitionRepository
    {
        List<Requisition> GetUserRequisitionList(string LoginName, Guid AppID, bool IsAdminOrCeo, ReqAdvanceSearch ReqAdvanceSearchObj);
        List<RequisitionDetail> GetRequisitionDetailList(Guid ID);
        object InsertRequisition(Requisition RequisitionObj, bool isAdminOrCeo);
        object UpdateRequisition(Requisition RequisitionObj);
        Requisition GetRequisitionDetails(Guid ID, string LoginName);
        object DeleteRequisitionDetailByID(Guid ID);
        object DeleteRequisitionByID(Guid ID);
        RequisitionOverViewCount GetRequisitionOverViewCount(string UserName, bool IsAdminORCeo);
        Requisition ApproveRequisition(Requisition RequisitionObj, bool IsAdminORCeo);
        object UpdateNotification(Requisition requisitionObj);
        RequisitionOverViewCount GetRequisitionSummary();
        RequisitionOverViewCount GetRequisitionCount(RequisitionOverViewCount reqObj,bool isAdminOrCeo);
    }
}