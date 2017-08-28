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

        public object DeleteQuotation(Guid ID)
        {
            throw new NotImplementedException();
        }

        public List<QuoteHeader> GetAllQuotations()
        {
            List<QuoteHeader> quoteHeaderList = null;
            try
            {
                quoteHeaderList = _quotationRepository.GetAllQuotations();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return quoteHeaderList;
        }

        public List<QuoteStage> GetAllQuoteStages()
        {
            return _quotationRepository.GetAllQuoteStages();
        }

        public List<Quotation> GetQuotationDetails(string duration)
        {
            return _quotationRepository.GetQuotationDetails(duration);
        }

        public object InsertQuotation(QuoteHeader quoteHeader)
        {
            throw new NotImplementedException();
        }

        public object UpdateQuotation(QuoteHeader quoteHeader)
        {
            throw new NotImplementedException();
        }
    }
}