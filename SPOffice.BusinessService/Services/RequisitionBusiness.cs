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
        ICommonBusiness _commonBusiness;
        public RequisitionBusiness(IRequisitionRepository requisitionRepository, ICommonBusiness commonBusiness)
        {
            _requisitionRepository = requisitionRepository;
            _commonBusiness = commonBusiness;
        }
       public List<Requisition> GetUserRequisitionList(string LoginName, Guid AppID)
        {
            return _requisitionRepository.GetUserRequisitionList(LoginName,AppID);
        }
        public object InsertRequisition(Requisition RequisitionObj)
        {
            RequisitionObj.DetailXML = _commonBusiness.GetXMLfromRequisitionDetailList(RequisitionObj.RequisitionDetailList, "MaterialID");
            return _requisitionRepository.InsertRequisition(RequisitionObj);
        }
        public object UpdateRequisition(Requisition RequisitionObj)
        {
            return _requisitionRepository.UpdateRequisition(RequisitionObj);
        }
        public List<Requisition> GetRequisitionDetails(Guid ID)
        {
            return _requisitionRepository.GetRequisitionDetails(ID);
        }
    }
}