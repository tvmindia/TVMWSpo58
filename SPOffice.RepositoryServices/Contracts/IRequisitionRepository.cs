using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.RepositoryServices.Contracts
{
    public interface IRequisitionRepository
    {
        List<Requisition> GetUserRequisitionList(string LoginName, Guid AppID);
        List<RequisitionDetail> GetRequisitionDetailList(Guid ID);
        object InsertRequisition(Requisition RequisitionObj);
        object UpdateRequisition(Requisition RequisitionObj);
        Requisition GetRequisitionDetails(Guid ID);
        object DeleteRequisitionDetailByID(Guid ID);
    }
}