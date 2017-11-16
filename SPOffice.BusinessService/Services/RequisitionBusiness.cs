using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class RequisitionBusiness:IRequisitionBusiness
    {
        IRequisitionRepository _requisitionRepository;
        public RequisitionBusiness(IRequisitionRepository requisitionRepository)
        {
            _requisitionRepository = requisitionRepository;
        }
       public List<Requisition> GetUserRequisitionList(string LoginName, Guid AppID)
        {
            return _requisitionRepository.GetUserRequisitionList(LoginName,AppID);
        }
    }
}