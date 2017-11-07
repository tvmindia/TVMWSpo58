using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.RepositoryServices.Contracts
{
  public  interface IProformaInvoiceRepository
    {



        /// <summary>
        /// For web
        /// </summary>
        /// <returns></returns>
        List<ProformaInvoice> GetAllProformaInvoices();
        //object InsertQuotation(ProformaHeader proformaHeader);
        //object UpdateQuotation(ProformaHeader proformaHeader);


        //List<ProformaStage> GetAllQuoteStages();
        //List<ProformaItem> GetAllQuoteItems(Guid? ID);
        //object DeleteQuoteItem(Guid? ID);
        //object UpdateQuoteMailStatus(ProformaHeader proformaHeader);
        //ProformaInvoiceSummary GetQuotationSummary();

    }
}
