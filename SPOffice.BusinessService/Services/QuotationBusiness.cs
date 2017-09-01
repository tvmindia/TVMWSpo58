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
    public class QuotationBusiness: IQuotationBusiness
    {
    
        private IQuotationRepository _quotationRepository;
        ICommonBusiness _commonBusiness;
        IMailBusiness _mailBusiness;
        public QuotationBusiness(IQuotationRepository quotationRepository, ICommonBusiness commonBusiness, IMailBusiness mailBusiness)
        {
            _quotationRepository = quotationRepository;
            _commonBusiness = commonBusiness;
            _mailBusiness = mailBusiness;
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
            Object result = null;
            try
            {
                quoteHeader.DetailXML = _commonBusiness.GetXMLfromObject(quoteHeader.quoteItemList, "ProductCode");
                result = _quotationRepository.InsertQuotation(quoteHeader);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public object UpdateQuotation(QuoteHeader quoteHeader)
        {
            Object result = null;
            try
            {
                quoteHeader.DetailXML= _commonBusiness.GetXMLfromObject(quoteHeader.quoteItemList, "ProductCode");
                result = _quotationRepository.UpdateQuotation(quoteHeader);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Quateheader with quate items
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>QuoteHeader</returns>
        public QuoteHeader GetMailPreview(Guid ID)
        {
            QuoteHeader quoteHeader = null;
            try
            {
                quoteHeader= GetQuationDetailsByID(ID);
                if(quoteHeader!=null)
                {
                    if ((quoteHeader.ID != Guid.Empty) && (quoteHeader.ID != null))
                    {
                        quoteHeader.quoteItemList = GetAllQuoteItems(ID);
                    }
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return quoteHeader;
        }

        public async Task<bool> QuoteEmailPush(QuoteHeader quoteHeader)
        {
            bool sendsuccess = false;
            try
            {
                Mail _mail = new Mail();
                _mail.Body = quoteHeader.MailBody;
                _mail.Subject = quoteHeader.QuoteSubject;
                _mail.To = quoteHeader.SentToEmails;
                sendsuccess = await _mailBusiness.MailSendAsync(_mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sendsuccess;
        }

        public object UpdateQuoteMailStatus(QuoteHeader quoteHeader)
        {
            Object result = null;
            try
            {
                
                result = _quotationRepository.UpdateQuoteMailStatus(quoteHeader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}