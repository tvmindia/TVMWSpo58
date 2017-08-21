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
    public class ProformaRepository : IProformaRepository
    {
        AppConst Cobj = new AppConst();
        Settings s = new Settings();
        private IDatabaseFactory _databaseFactory;
        public ProformaRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        #region GetAllProforma
        public List<Proforma> GetProformaDetails(Proforma perObj)
        {
            List<Proforma> proformaList = null;
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
                        cmd.CommandText = "[Office].[GetAllProformaInvoiceList]";
                        cmd.Parameters.Add("@duration", SqlDbType.Int).Value = perObj.duration;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                proformaList = new List<Proforma>();
                                while (sdr.Read())
                                {
                                    Proforma _proformaObj = new Proforma();
                                    {
                                        _proformaObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _proformaObj.ID);
                                        //_proformaObj.OriginCompCode = (sdr["OriginCompCode"].ToString() != "" ? sdr["OriginCompCode"].ToString() : _proformaObj.OriginCompCode);
                                        _proformaObj.InvoiceNo = (sdr["InvoiceNo"].ToString() != "" ? sdr["InvoiceNo"].ToString() : _proformaObj.InvoiceNo);
                                        _proformaObj.InvoiceDate = (sdr["InvoiceDate"].ToString() != "" ? DateTime.Parse(sdr["InvoiceDate"].ToString()).ToString(s.dateformat) : _proformaObj.InvoiceDate);
                                        //_proformaObj.ValidTillDate = (sdr["ValidTillDate"].ToString() != "" ? DateTime.Parse(sdr["ValidTillDate"].ToString()) : _proformaObj.ValidTillDate);
                                        //_proformaObj.Subject = (sdr["Subject"].ToString() != "" ? sdr["Subject"].ToString() : _proformaObj.Subject);
                                        //_proformaObj.BodyHeader = (sdr["BodyHeader"].ToString() != "" ? sdr["BodyHeader"].ToString() : _proformaObj.BodyHeader);
                                        //_proformaObj.BodyFooter = (sdr["BodyFooter"].ToString() != "" ? sdr["BodyFooter"].ToString() : _proformaObj.BodyFooter);
                                        //_proformaObj.SentToAddress = (sdr["SentToAddress"].ToString() != "" ? sdr["SentToAddress"].ToString() : _proformaObj.SentToAddress);
                                        //_proformaObj.SentToEmails = (sdr["SentToEmails"].ToString() != "" ? sdr["SentToEmails"].ToString() : _proformaObj.SentToEmails);
                                        //_proformaObj.EmailSentYN = (sdr["EmailSentYN"].ToString() != "" ? sdr["EmailSentYN"].ToString() : _proformaObj.EmailSentYN);
                                        _proformaObj.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _proformaObj.CompanyName);
                                        _proformaObj.TaxTypeCode = (sdr["TaxTypeCode"].ToString() != "" ? sdr["TaxTypeCode"].ToString() : _proformaObj.TaxTypeCode);
                                        _proformaObj.TaxPercApplied = (sdr["TaxPercApplied"].ToString() != "" ? decimal.Parse(sdr["TaxPercApplied"].ToString()) : _proformaObj.TaxPercApplied);
                                        _proformaObj.TaxAmount = (sdr["TaxAmount"].ToString() != "" ? decimal.Parse(sdr["TaxAmount"].ToString()) : _proformaObj.TaxAmount);                         
                                        _proformaObj.Discount = (sdr["Discount"].ToString() != "" ? decimal.Parse(sdr["Discount"].ToString()) : _proformaObj.Discount);
                                        _proformaObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _proformaObj.GeneralNotes);
                                        _proformaObj.Amount = (sdr["Amount"].ToString() != "" ? Decimal.Parse(sdr["Amount"].ToString()) : _proformaObj.Amount);
                                    }
                                    proformaList.Add(_proformaObj);
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

            return proformaList;
            ;
        }

        #endregion GetAllProforma
    }
}