using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    
        public class FollowUpBusiness : IFollowUpBusiness
    {
            private IFollowUpRepository _followupRepository;

            public FollowUpBusiness(IFollowUpRepository followupRepository)
            {
            _followupRepository = followupRepository;
            }
            public object InsertUpdateFollowUp(FollowUp _followupObj)
            {
                object result = null;
                try
                {
                    if (_followupObj.ID == Guid.Empty)
                    {
                        result = _followupRepository.InsertFollowUp(_followupObj);
                    }
                    else
                    {
                        result = _followupRepository.UpdateFollowUp(_followupObj);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return result;
            }


        public List<FollowUp> GetFollowUpDetails(Guid EnquiryID)
        {
            return _followupRepository.GetFollowUpDetails(EnquiryID);
        }
    }
}