using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SPOffice.RepositoryServices.Services
{
    public class QuotationRepository: IQuotationRepository
    {
        AppConst Cobj = new AppConst();
        Settings settings = new Settings();
        private IDatabaseFactory _databaseFactory;
        public QuotationRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public object DeleteQuotation(Guid ID)
        {
            throw new NotImplementedException();
        }

        public List<QuoteHeader> GetAllQuotations()
        {
            List<QuoteHeader> quoteHeaderList = null;
            try
            {
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[Office].[GetAllQuotations]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                quoteHeaderList = new List<QuoteHeader>();
                                while (sdr.Read())
                                {
                                    QuoteHeader _quoteHeader = new QuoteHeader();
                                    {
                                        _quoteHeader.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _quoteHeader.ID);
                                        _quoteHeader.QuotationNo = (sdr["QuotationNo"].ToString() != "" ? sdr["QuotationNo"].ToString() : _quoteHeader.QuotationNo);
                                        _quoteHeader.CustomerID = (sdr["CustomerID"].ToString() != "" ? Guid.Parse(sdr["CustomerID"].ToString()) : _quoteHeader.CustomerID);
                                        _quoteHeader.customer = new CustomerPO();
                                        {
                                            _quoteHeader.customer.ID = (Guid)_quoteHeader.CustomerID;
                                            _quoteHeader.customer.CustomerName= (sdr["CustomerName"].ToString() != "" ? sdr["CustomerName"].ToString() : _quoteHeader.customer.CustomerName);
                                        }
                                        _quoteHeader.QuotationDate = (sdr["QuotationDate"].ToString() != "" ? DateTime.Parse(sdr["QuotationDate"].ToString()).ToString(settings.dateformat) : _quoteHeader.QuotationDate);
                                        _quoteHeader.ValidTillDate = (sdr["ValidTillDate"].ToString() != "" ? DateTime.Parse(sdr["ValidTillDate"].ToString()).ToString(settings.dateformat) : _quoteHeader.ValidTillDate);

                                        _quoteHeader.SalesPersonID = (sdr["SalesPersonID"].ToString() != "" ? Guid.Parse(sdr["SalesPersonID"].ToString()) : _quoteHeader.SalesPersonID);
                                        _quoteHeader.QuoteFromCompCode = (sdr["QuoteFromCompCode"].ToString() != "" ? sdr["QuoteFromCompCode"].ToString() : _quoteHeader.QuoteFromCompCode);
                                        _quoteHeader.company = new Company();
                                        {
                                            _quoteHeader.company.Code = _quoteHeader.QuoteFromCompCode;
                                            _quoteHeader.company.Name= (sdr["QuoteFromCompanyName"].ToString() != "" ? sdr["QuoteFromCompanyName"].ToString() : _quoteHeader.company.Name);
                                        }
                                        _quoteHeader.Stage = (sdr["QuoteStage"].ToString() != "" ? sdr["QuoteStage"].ToString() : _quoteHeader.Stage);
                                        _quoteHeader.quoteStage = new QuoteStage();
                                        {
                                            _quoteHeader.quoteStage.Code = _quoteHeader.Stage;
                                            _quoteHeader.quoteStage.Description= (sdr["QuoteDescription"].ToString() != "" ? sdr["QuoteDescription"].ToString() : _quoteHeader.quoteStage.Description);
                                        }
                                        _quoteHeader.QuoteSubject = (sdr["QuoteSubject"].ToString() != "" ? sdr["QuoteSubject"].ToString() : _quoteHeader.QuoteSubject);
                                        _quoteHeader.SentToEmails = (sdr["SentToEmails"].ToString() != "" ? sdr["SentToEmails"].ToString() : _quoteHeader.SentToEmails);
                                        _quoteHeader.ContactPerson = (sdr["ContactPerson"].ToString() != "" ? sdr["ContactPerson"].ToString() : _quoteHeader.ContactPerson);
                                        _quoteHeader.SentToAddress = (sdr["SentToAddress"].ToString() != "" ? sdr["SentToAddress"].ToString() : _quoteHeader.SentToAddress);
                                        _quoteHeader.QuoteBodyHead = (sdr["QuoteBodyHead"].ToString() != "" ? sdr["QuoteBodyHead"].ToString() : _quoteHeader.QuoteBodyHead);
                                        _quoteHeader.QuoteBodyFoot = (sdr["QuoteBodyFoot"].ToString() != "" ? sdr["QuoteBodyFoot"].ToString() : _quoteHeader.QuoteBodyFoot);
                                        _quoteHeader.Discount = (sdr["Discount"].ToString() != "" ? decimal.Parse(sdr["Discount"].ToString()) : _quoteHeader.Discount);
                                        _quoteHeader.TaxTypeCode = (sdr["TaxTypeCode"].ToString() != "" ? sdr["TaxTypeCode"].ToString() : _quoteHeader.TaxTypeCode);
                                        _quoteHeader.TaxPercApplied = (sdr["TaxPercApplied"].ToString() != "" ? decimal.Parse(sdr["TaxPercApplied"].ToString()) : _quoteHeader.TaxPercApplied);
                                        _quoteHeader.TaxAmount = (sdr["TaxAmount"].ToString() != "" ? decimal.Parse(sdr["TaxAmount"].ToString()) : _quoteHeader.TaxAmount);
                                        _quoteHeader.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _quoteHeader.GeneralNotes);
                                        _quoteHeader.EmailSentYN = (sdr["EmailSentYN"].ToString() != "" ? sdr["EmailSentYN"].ToString() : _quoteHeader.EmailSentYN);
                                        _quoteHeader.GrossAmount = (sdr["GrossAmount"].ToString() != "" ? decimal.Parse(sdr["GrossAmount"].ToString()) : _quoteHeader.GrossAmount);
                                        _quoteHeader.commonObj = new Common();
                                        {
                                            _quoteHeader.commonObj.CreatedBy = (sdr["CreatedBy"].ToString() != "" ? sdr["CreatedBy"].ToString() : _quoteHeader.commonObj.CreatedBy);
                                            _quoteHeader.commonObj.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : _quoteHeader.commonObj.CreatedDate);
                                            _quoteHeader.commonObj.CreatedDateString = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()).ToString(settings.dateformat) : string.Empty);
                                        }
                                    }
                                    quoteHeaderList.Add(_quoteHeader);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return quoteHeaderList;
        }
        public object InsertQuotation(QuoteHeader quoteHeader)
        {
            throw new NotImplementedException();
        }

        public object UpdateQuotation(QuoteHeader quoteHeader)
        {
            throw new NotImplementedException();
        }
        #region GetQuotationDetails
        public List<Quotation> GetQuotationDetails(string duration)
        {
           List<Quotation> quotationsList = null;
            try
            {
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[Office].[GetQuotationList]";
                        cmd.Parameters.Add("@duration", SqlDbType.Int).Value = duration;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                quotationsList = new List<Quotation>();
                                while (sdr.Read())
                                {
                                    Quotation _quotationObj = new Quotation();
                                    {
                                        _quotationObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _quotationObj.ID);
                                        _quotationObj.SalesPersonID = (sdr["SalesPersonID"].ToString() != "" ? Guid.Parse(sdr["SalesPersonID"].ToString()) : _quotationObj.SalesPersonID);
                                        _quotationObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? Guid.Parse(sdr["CustomerID"].ToString()) : _quotationObj.CustomerID);
                                        _quotationObj.QuotationNo = (sdr["QuotationNo"].ToString() != "" ? sdr["QuotationNo"].ToString() : _quotationObj.QuotationNo);
                                        _quotationObj.QuoteStage = (sdr["QuoteStage"].ToString() != "" ? sdr["QuoteStage"].ToString() : _quotationObj.QuoteStage);
                                        _quotationObj.QuotationDate = (sdr["QuotationDate"].ToString() != "" ? DateTime.Parse(sdr["QuotationDate"].ToString()).ToString(settings.dateformat) : _quotationObj.QuotationDate);

                                        _quotationObj.ValidTillDate = (sdr["ValidTillDate"].ToString() != "" ? DateTime.Parse(sdr["ValidTillDate"].ToString()).ToString(settings.dateformat) : _quotationObj.ValidTillDate);
                                        _quotationObj.QuoteFromCompCode = (sdr["QuoteFromCompCode"].ToString() != "" ? sdr["QuoteFromCompCode"].ToString() : _quotationObj.QuoteFromCompCode);
                                        _quotationObj.QuoteSubject = (sdr["QuoteSubject"].ToString() != "" ? sdr["QuoteSubject"].ToString() : _quotationObj.QuoteSubject);
                                        _quotationObj.QuoteBodyHead = (sdr["QuoteBodyHead"].ToString() != "" ? sdr["QuoteBodyHead"].ToString() : _quotationObj.QuoteBodyHead);
                                        _quotationObj.QuoteBodyFoot = (sdr["QuoteBodyFoot"].ToString() != "" ? sdr["QuoteBodyFoot"].ToString() : _quotationObj.QuoteBodyFoot);
                                        _quotationObj.SentToAddress = (sdr["SentToAddress"].ToString() != "" ? sdr["SentToAddress"].ToString() : _quotationObj.SentToAddress);
                                        _quotationObj.SentToEmails = (sdr["SentToEmails"].ToString() != "" ? sdr["SentToEmails"].ToString() : _quotationObj.SentToEmails);
                                        _quotationObj.EmailSentYN = (sdr["EmailSentYN"].ToString() != "" ? sdr["EmailSentYN"].ToString() : _quotationObj.EmailSentYN);
                                        _quotationObj.ContactPerson = (sdr["ContactPerson"].ToString() != "" ? sdr["ContactPerson"].ToString() : _quotationObj.ContactPerson);
                                        _quotationObj.TaxTypeCode = (sdr["TaxTypeCode"].ToString() != "" ? sdr["TaxTypeCode"].ToString() : _quotationObj.TaxTypeCode);
                                        _quotationObj.TaxPercApplied = (sdr["TaxPercApplied"].ToString() != "" ?decimal.Parse(sdr["TaxPercApplied"].ToString()) : _quotationObj.TaxPercApplied);
                                        _quotationObj.TaxAmount = (sdr["TaxAmount"].ToString() != "" ? decimal.Parse(sdr["TaxAmount"].ToString()) : _quotationObj.TaxAmount);
                                        //_quotationObj.GrantTotal = (sdr["GrantTotal"].ToString() != "" ? decimal.Parse(sdr["GrantTotal"].ToString()) : _quotationObj.GrantTotal);
                                        _quotationObj.Discount = (sdr["Discount"].ToString() != "" ? decimal.Parse(sdr["Discount"].ToString()) : _quotationObj.Discount);
                                        _quotationObj.Amount = (sdr["Amount"].ToString() != "" ? Decimal.Parse(sdr["Amount"].ToString()) : _quotationObj.Amount);
                                        
                                        _quotationObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _quotationObj.GeneralNotes);
                                        _quotationObj.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _quotationObj.CompanyName);

                                    }
                                    quotationsList.Add(_quotationObj);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return quotationsList;
        }


        #endregion GetQuotationDetails

        public List<QuoteStage> GetAllQuoteStages()
        {
            List<QuoteStage> quoteStageList = new List<QuoteStage>();
            try
            {
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[Office].[GetAllQuoteStages]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                              
                                while (sdr.Read())
                                {
                                    QuoteStage quoteStage = new QuoteStage()
                                    {
                                        Code = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : string.Empty),
                                        Description = (sdr["Description"].ToString() != "" ? sdr["Description"].ToString() : string.Empty)
                                    };
                                    quoteStageList.Add(quoteStage);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return quoteStageList;
        }
    }
}
    
