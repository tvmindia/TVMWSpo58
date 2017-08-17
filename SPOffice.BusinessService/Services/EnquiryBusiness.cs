using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class EnquiryBusiness: IEnquiryBusiness
    {
        private IEnquiryRepository _enquiryRepository;

        public EnquiryBusiness(IEnquiryRepository enquiryRepository)
        {
            _enquiryRepository = enquiryRepository;
        }
        public object InsertUpdateEnquiry(Enquiry _enquiriesObj)
        {
            object result = null;
            try
            {
                if (_enquiriesObj.ID == Guid.Empty)
                {
                    result = _enquiryRepository.InsertEnquiry(_enquiriesObj);
                }
                else
                {
                    result = _enquiryRepository.UpdateEnquiry(_enquiriesObj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}