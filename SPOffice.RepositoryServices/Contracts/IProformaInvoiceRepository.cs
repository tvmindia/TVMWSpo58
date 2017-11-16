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
        ///   
        /// 
         List<ProformaInvoice> GetQuotationDetails(string duration);

        List<ProformaHeader> GetAllProformaInvoices();
        object InsertProformaInvoices(ProformaHeader proformaHeader);
        object UpdateProformaInvoices(ProformaHeader proformaHeader);
        List<ProformaItem> GetAllQuoteItems(Guid? ID);
        object DeleteQuoteItem(Guid? ID);
        object UpdateQuoteMailStatus(ProformaHeader proformaHeader);
        //bool DeleteProformaInvoice(Guid? ID);
        object DeleteProformaInvoice(Guid? ID);
    }
}
