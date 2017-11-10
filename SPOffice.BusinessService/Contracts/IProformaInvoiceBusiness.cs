using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.BusinessService.Contracts
{
   public interface IProformaInvoiceBusiness
    {
        List<ProformaInvoice> GetQuotationDetails(string duration);
        List<ProformaHeader> GetAllProformaInvoices();
        object InsertProformaInvoices(ProformaHeader proformaHeader);
        object UpdateProformaInvoices(ProformaHeader proformaHeader);
        List<ProformaItem> GetAllQuoteItems(Guid? ID);
        ProformaHeader GetQuationDetailsByID(Guid ID);
        object DeleteQuoteItem(Guid? ID);
        ProformaHeader GetMailPreview(Guid ID);
        object UpdateQuoteMailStatus(ProformaHeader proformaHeader);
        Task<bool> QuoteEmailPush(ProformaHeader proformaHeader);


    }
}
