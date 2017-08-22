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

        #region GetAllQuotation
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

        #endregion GetAllQuotation
    }
}
    
