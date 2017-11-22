using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.BusinessService.Contracts
{
   public interface IQuotationBusiness
    {

        List<Quotation> GetQuotationDetails(string duration);


        List<QuoteHeader> GetAllQuotations();
        object InsertQuotation(QuoteHeader quoteHeader);
        object UpdateQuotation(QuoteHeader quoteHeader);     

        List<QuoteStage> GetAllQuoteStages();
        QuoteHeader GetQuationDetailsByID(Guid ID);
        List<QuoteItem> GetAllQuoteItems(Guid? ID);
        object DeleteQuoteItem(Guid? ID);
        QuoteHeader GetMailPreview(Guid ID);
        object UpdateQuoteMailStatus(QuoteHeader quoteHeader);
        Task<bool> QuoteEmailPush(QuoteHeader quoteHeader);
        object DeleteQuotation(Guid? ID);
    }
}
