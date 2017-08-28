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
        object DeleteQuotation(Guid ID);

        List<QuoteStage> GetAllQuoteStages();
        QuoteHeader GetQuationDetailsByID(Guid ID);

    }
}
