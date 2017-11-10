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

        public List<ProformaHeader> GetAllProformaInvoices()
        {
            List<ProformaHeader> proformaHeaderList = null;
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

        public object InsertProformaInvoices(ProformaHeader proformaHeader)
        {
            Object result = null;
            try
            {
                proformaHeader.DetailXML = _commonBusiness.GetXMLfromObj(proformaHeader.quoteItemList, "ProductCode");
                result = _proformaInvoiceRepository.InsertProformaInvoices(proformaHeader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public object UpdateProformaInvoices(ProformaHeader proformaHeader)
        {
            Object result = null;
            try
            {
                proformaHeader.DetailXML = _commonBusiness.GetXMLfromObj(proformaHeader.quoteItemList, "ProductCode");
                result = _proformaInvoiceRepository.UpdateProformaInvoices(proformaHeader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<ProformaItem> GetAllQuoteItems(Guid? ID)
        {
            List<ProformaItem> proformaItemList = null;
            try
            {

                proformaItemList = _proformaInvoiceRepository.GetAllQuoteItems(ID);
                proformaItemList = proformaItemList != null ? proformaItemList.Select(Q => { Q.Amount = Q.Quantity * Q.Rate; return Q; }).ToList() : new List<ProformaItem>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return proformaItemList;
        }

           public ProformaHeader GetQuationDetailsByID(Guid ID)
        {

            List<ProformaHeader> QuotList = GetAllProformaInvoices();
            ProformaHeader proformaHeader = QuotList != null ? QuotList.Where(Q => Q.ID == ID).SingleOrDefault():null;
            return proformaHeader;
        }

        public List<ProformaInvoice> GetQuotationDetails(string duration)
        {
            return _proformaInvoiceRepository.GetQuotationDetails(duration);
        }


        public object DeleteQuoteItem(Guid? ID)
        {
            return _proformaInvoiceRepository.DeleteQuoteItem(ID);
        }

        //public ProformaHeader GetMailPreview(Guid ID)
        //{
        //    ProformaHeader proformaHeader = null;
        //    try
        //    {
        //        proformaHeader = GetQuationDetailsByID(ID);
        //        if (proformaHeader != null)
        //        {
        //            if ((proformaHeader.ID != Guid.Empty) && (proformaHeader.ID != null))
        //            {
        //                proformaHeader.quoteItemList = GetAllQuoteItems(ID);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return proformaHeader;
        //}



    }
}