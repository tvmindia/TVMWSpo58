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
       public List<Requisition> GetUserRequisitionList(string LoginName, Guid AppID, bool IsAdminOrCeo, ReqAdvanceSearch ReqAdvanceSearchObj)
        {
            return _requisitionRepository.GetUserRequisitionList(LoginName,AppID,  IsAdminOrCeo,  ReqAdvanceSearchObj);
        }
        public List<RequisitionDetail> GetRequisitionDetailList(Guid ID)
        {
            return _requisitionRepository.GetRequisitionDetailList(ID);
        }
        public object InsertRequisition(Requisition RequisitionObj, bool isAdminOrCeo)
        {
            RequisitionObj.DetailXML = _commonBusiness.GetXMLfromRequisitionDetailList(RequisitionObj.RequisitionDetailList, "MaterialID");
            return _requisitionRepository.InsertRequisition(RequisitionObj,isAdminOrCeo);
        }
        public object UpdateRequisition(Requisition RequisitionObj)
        {
            RequisitionObj.DetailXML = _commonBusiness.GetXMLfromRequisitionDetailList(RequisitionObj.RequisitionDetailList, "MaterialID");
            return _requisitionRepository.UpdateRequisition(RequisitionObj);
        }
        public Requisition GetRequisitionDetails(Guid ID,string LoginName)
        {
            return _requisitionRepository.GetRequisitionDetails(ID,LoginName);
        }
        public object DeleteRequisitionDetailByID(Guid ID)
        {
            return _requisitionRepository.DeleteRequisitionDetailByID(ID);
        }
        public RequisitionOverViewCount GetRequisitionOverViewCount(string UserName, bool IsAdminORCeo)
        {
            return _requisitionRepository.GetRequisitionOverViewCount(UserName, IsAdminORCeo);
        }
        public Requisition ApproveRequisition(Requisition RequisitionObj, bool IsAdminORCeo)
        {
           return _requisitionRepository.ApproveRequisition(RequisitionObj,IsAdminORCeo);
        }
    }
}