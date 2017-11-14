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
    }
}