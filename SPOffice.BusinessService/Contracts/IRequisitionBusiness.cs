using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Contracts
{
    public interface IRequisitionBusiness
    {
        List<Requisition> GetUserRequisitionList(string LoginName, Guid AppID);
        object InsertRequisition(Requisition RequisitionObj);
        object UpdateRequisition(Requisition RequisitionObj);
        List<Requisition> GetRequisitionDetails(Guid ID);
    }
}