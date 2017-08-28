using SPOffice.BusinessService.Contracts;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPOffice.DataAccessObject.DTO;

namespace SPOffice.BusinessService.Services
{
    public class TaxTypeBusiness: ITaxTypeBusiness
    {
        ITaxTypeRepository _taxTypeRepository;
        public TaxTypeBusiness(ITaxTypeRepository taxTypeRepository)
        {
            _taxTypeRepository = taxTypeRepository;
        }

        public List<TaxType> GetAllTaxTypes()
        {
            return _taxTypeRepository.GetAllTaxTypes(); 
        }
    }
}