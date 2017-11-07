using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class ProformaInvoiceBusiness : IProformaInvoiceBusiness
    {
        private IProformaInvoiceRepository _proformaInvoiceRepository;
        ICommonBusiness _commonBusiness;
        IMailBusiness _mailBusiness;
        public ProformaInvoiceBusiness(IProformaInvoiceRepository proformaInvoiceRepository, ICommonBusiness commonBusiness, IMailBusiness mailBusiness)
        {
            _proformaInvoiceRepository = proformaInvoiceRepository;
            _commonBusiness = commonBusiness;
            _mailBusiness = mailBusiness;
        }

        public List<ProformaInvoice> GetAllProformaInvoices()
        {
            List<ProformaInvoice> proformaHeaderList = null;
            try
            {
                proformaHeaderList = _proformaInvoiceRepository.GetAllProformaInvoices();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return proformaHeaderList;
        }
    }
}