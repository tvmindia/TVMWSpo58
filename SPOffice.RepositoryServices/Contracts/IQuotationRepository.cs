using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.RepositoryServices.Contracts
{
    public interface IQuotationRepository
    {
        /// <summary>
        /// For API
        /// </summary>
        /// <param name="duration"></param>
        /// <returns>List<Quotation></returns>
        List<Quotation> GetQuotationDetails(string duration);


        /// <summary>
        /// For web
        /// </summary>
        /// <returns></returns>
        List<QuoteHeader> GetAllQuotations();
        object InsertQuotation(QuoteHeader quoteHeader);
        object UpdateQuotation(QuoteHeader quoteHeader);
    

        List<QuoteStage> GetAllQuoteStages();
        List<QuoteItem> GetAllQuoteItems(Guid? ID);
        object DeleteQuoteItem(Guid? ID);
        object UpdateQuoteMailStatus(QuoteHeader quoteHeader);
        QuotationSummary GetQuotationSummary();

    }
}
