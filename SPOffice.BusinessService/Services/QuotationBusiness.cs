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
            return _quotationRepository.DeleteQuotation(ID);
        }

        public object DeleteQuoteItem(Guid? ID)
        {
            return _quotationRepository.DeleteQuoteItem(ID);
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

        public List<QuoteItem> GetAllQuoteItems(Guid? ID)
        {
            List<QuoteItem> quoteItemList = null;
            try
            {
               
                quoteItemList = _quotationRepository.GetAllQuoteItems(ID);
                quoteItemList = quoteItemList != null ? quoteItemList.Select(Q => { Q.Amount = Q.Quantity * Q.Rate; return Q; }).ToList() : new List<QuoteItem>();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return quoteItemList;
        }

        public List<QuoteStage> GetAllQuoteStages()
        {
            return _quotationRepository.GetAllQuoteStages();
        }

        public QuoteHeader GetQuationDetailsByID(Guid ID)
        {

            List<QuoteHeader> QuotList = GetAllQuotations();
            QuoteHeader quoteHeader = QuotList != null ? QuotList.Where(Q => Q.ID == ID).SingleOrDefault():null;
            return quoteHeader;
        }

        public List<Quotation> GetQuotationDetails(string duration)
        {
            return _quotationRepository.GetQuotationDetails(duration);
        }

        public object InsertQuotation(QuoteHeader quoteHeader)
        {
            return _quotationRepository.InsertQuotation(quoteHeader);
        }

        public object UpdateQuotation(QuoteHeader quoteHeader)
        {
            return _quotationRepository.UpdateQuotation(quoteHeader);
        }
    }
}