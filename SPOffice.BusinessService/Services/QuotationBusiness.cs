using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class QuotationBusiness: IQuotationBusiness
    {
    
        private IQuotationRepository _quotationRepository;

        public QuotationBusiness(IQuotationRepository quotationRepository)
        {
            _quotationRepository = quotationRepository;
        }
        public List<Quotation> GetQuotationDetails(string duration)
        {
            return _quotationRepository.GetQuotationDetails(duration);
        }
    }
}