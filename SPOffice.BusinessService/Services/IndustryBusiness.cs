using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class IndustryBusiness: IIndustryBusiness
    {
        private IIndustryRepository _industryRepository;

        public IndustryBusiness(IIndustryRepository industryRepository)
        {
            _industryRepository = industryRepository;
        }
        public List<Industry> GetAllIndustryList()
        {
            return _industryRepository.GetAllIndustryList();
        }
    }
}