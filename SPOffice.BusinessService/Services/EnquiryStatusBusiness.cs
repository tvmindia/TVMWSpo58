using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class EnquiryStatusBusiness: IEnquiryStatusBusiness
    {
        private IEnquiryStatusRepository _enquiryStatusRepository;

        public EnquiryStatusBusiness(IEnquiryStatusRepository enquiryStatusRepository)
        {
            _enquiryStatusRepository = enquiryStatusRepository;
        }
        public List<EnquiryStatus> GetAllEnquiryStatusList()
        {
            return _enquiryStatusRepository.GetAllEnquiryStatusList();
        }
    }
}