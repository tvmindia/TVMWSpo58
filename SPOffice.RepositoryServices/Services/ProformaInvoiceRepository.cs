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
    public class ProformaInvoiceRepository : IProformaInvoiceRepository
    {
        //    AppConst Cobj = new AppConst();
        Settings settings = new Settings();
        private IDatabaseFactory _databaseFactory;
        public ProformaInvoiceRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }



        public List<ProformaInvoice> GetAllProformaInvoices()
        {
            List<ProformaInvoice> proformaHeaderList = null;
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
                        cmd.CommandText = "[Office].[GetAllProformaInvoice]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                proformaHeaderList = new List<ProformaInvoice>();
                                while (sdr.Read())
                                {
                                    ProformaInvoice _proformaHeader = new ProformaInvoice();
                                    {
                                        _proformaHeader.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _proformaHeader.ID);
                                        _proformaHeader.InvoiceNo = (sdr["InvoiceNo"].ToString() != "" ? sdr["InvoiceNo"].ToString() : _proformaHeader.InvoiceNo);
                                        _proformaHeader.CustomerID = (sdr["CustomerID"].ToString() != "" ? Guid.Parse(sdr["CustomerID"].ToString()) : _proformaHeader.CustomerID);
                                        _proformaHeader.customer = new CustomerPO();
                                        {
                                            _proformaHeader.customer.ID = (Guid)_proformaHeader.CustomerID;
                                            _proformaHeader.customer.CustomerName = (sdr["CustomerName"].ToString() != "" ? sdr["CustomerName"].ToString() : _proformaHeader.customer.CustomerName);

                                        }
                                        _proformaHeader.InvoiceDate = (sdr["InvoiceDate"].ToString() != "" ? DateTime.Parse(sdr["InvoiceDate"].ToString()).ToString(settings.dateformat) : _proformaHeader.InvoiceDate);
                                        _proformaHeader.ValidTillDate = (sdr["ValidTillDate"].ToString() != "" ? DateTime.Parse(sdr["ValidTillDate"].ToString()).ToString(settings.dateformat) : _proformaHeader.ValidTillDate);                                       
                                        
                                        _proformaHeader.Subject = (sdr["Subject"].ToString() != "" ? sdr["Subject"].ToString() : _proformaHeader.Subject);                                       
                                        _proformaHeader.Discount = (sdr["Discount"].ToString() != "" ? decimal.Parse(sdr["Discount"].ToString()) : _proformaHeader.Discount);
                                     
                                        _proformaHeader.TaxAmount = (sdr["TaxAmount"].ToString() != "" ? decimal.Parse(sdr["TaxAmount"].ToString()) : _proformaHeader.TaxAmount);
                                       
                                        _proformaHeader.Total = (sdr["TotalAmount"].ToString() != "" ? decimal.Parse(sdr["TotalAmount"].ToString()) : _proformaHeader.Total);
                                        
                                    }
                                    proformaHeaderList.Add(_proformaHeader);
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

            return proformaHeaderList;
        }
        //    public object InsertQuotation(ProformaHeader quoteHeader)
        //    {
        //        SqlParameter outputStatus, outputID;
        //        try
        //        {

        //            using (SqlConnection con = _databaseFactory.GetDBConnection())
        //            {
        //                using (SqlCommand cmd = new SqlCommand())
        //                {
        //                    if (con.State == ConnectionState.Closed)
        //                    {
        //                        con.Open();
        //                    }
        //                    cmd.Connection = con;
        //                    cmd.CommandText = "[Office].[InsertQuotation]";
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.Parameters.Add("@QuotationNo", SqlDbType.VarChar, 20).Value = quoteHeader.QuotationNo;
        //                    cmd.Parameters.Add("@CustomerID", SqlDbType.UniqueIdentifier).Value = quoteHeader.CustomerID;
        //                    cmd.Parameters.Add("@QuotationDate", SqlDbType.DateTime).Value = quoteHeader.QuotationDate;
        //                    cmd.Parameters.Add("@ValidTillDate", SqlDbType.DateTime).Value = quoteHeader.ValidTillDate;
        //                    cmd.Parameters.Add("@SalesPersonID", SqlDbType.UniqueIdentifier).Value = quoteHeader.SalesPersonID;
        //                    cmd.Parameters.Add("@QuoteFromCompCode", SqlDbType.VarChar, 10).Value = quoteHeader.QuoteFromCompCode;
        //                    cmd.Parameters.Add("@QuoteStage", SqlDbType.VarChar, 10).Value = quoteHeader.Stage;
        //                    cmd.Parameters.Add("@QuoteSubject", SqlDbType.VarChar, 500).Value = quoteHeader.QuoteSubject;
        //                    // cmd.Parameters.Add("@SentToEmails", SqlDbType.NVarChar,-1).Value = quoteHeader.SentToEmails;
        //                    cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar, 100).Value = quoteHeader.ContactPerson;
        //                    cmd.Parameters.Add("@SentToAddress", SqlDbType.NVarChar, -1).Value = quoteHeader.SentToAddress;
        //                    cmd.Parameters.Add("@QuoteBodyHead", SqlDbType.NVarChar, -1).Value = quoteHeader.QuoteBodyHead;
        //                    cmd.Parameters.Add("@QuoteBodyFoot", SqlDbType.NVarChar, -1).Value = quoteHeader.QuoteBodyFoot;

        //                    cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = quoteHeader.Discount;
        //                    cmd.Parameters.Add("@TaxTypeCode", SqlDbType.VarChar, 10).Value = quoteHeader.TaxTypeCode;
        //                    cmd.Parameters.Add("@TaxPercApplied", SqlDbType.Decimal).Value = quoteHeader.TaxPercApplied;
        //                    cmd.Parameters.Add("@TaxAmount", SqlDbType.Decimal).Value = quoteHeader.TaxAmount;
        //                    cmd.Parameters.Add("@GeneralNotes", SqlDbType.NVarChar, -1).Value = quoteHeader.GeneralNotes;
        //                    //   cmd.Parameters.Add("@EmailSentYN", SqlDbType.Bit).Value = quoteHeader.EmailSentYN;
        //                    cmd.Parameters.Add("@DetailXML", SqlDbType.Xml).Value = quoteHeader.DetailXML;
        //                    cmd.Parameters.Add("@FileDupID", SqlDbType.UniqueIdentifier).Value = quoteHeader.hdnFileID;
        //                    cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = quoteHeader.commonObj.CreatedBy;
        //                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = quoteHeader.commonObj.CreatedDate;
        //                    outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
        //                    outputStatus.Direction = ParameterDirection.Output;
        //                    outputID = cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier);
        //                    outputID.Direction = ParameterDirection.Output;
        //                    cmd.ExecuteNonQuery();
        //                }
        //            }
        //            AppConst Cobj = new AppConst();
        //            switch (outputStatus.Value.ToString())
        //            {
        //                case "0":
        //                    throw new Exception(Cobj.InsertFailure);
        //                case "1":
        //                    //success
        //                    return new
        //                    {
        //                        ID = outputID.Value.ToString(),
        //                        Status = outputStatus.Value.ToString(),
        //                        Message = Cobj.InsertSuccess
        //                    };

        //                default:
        //                    break;
        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //        return new
        //        {
        //            ID = outputID.Value.ToString(),
        //            Status = outputStatus.Value.ToString(),
        //            Message = Cobj.InsertSuccess
        //        };
        //    }

        //    public object UpdateQuotation(ProformaHeader quoteHeader)
        //    {
        //        SqlParameter outputStatus = null;
        //        try
        //        {

        //            using (SqlConnection con = _databaseFactory.GetDBConnection())
        //            {
        //                using (SqlCommand cmd = new SqlCommand())
        //                {
        //                    if (con.State == ConnectionState.Closed)
        //                    {
        //                        con.Open();
        //                    }
        //                    cmd.Connection = con;
        //                    cmd.CommandText = "[Office].[UpdateQuotation]";
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = quoteHeader.ID;
        //                    cmd.Parameters.Add("@QuotationNo", SqlDbType.VarChar, 20).Value = quoteHeader.QuotationNo;
        //                    cmd.Parameters.Add("@CustomerID", SqlDbType.UniqueIdentifier).Value = quoteHeader.CustomerID;
        //                    cmd.Parameters.Add("@QuotationDate", SqlDbType.DateTime).Value = quoteHeader.QuotationDate;
        //                    cmd.Parameters.Add("@ValidTillDate", SqlDbType.DateTime).Value = quoteHeader.ValidTillDate;
        //                    cmd.Parameters.Add("@SalesPersonID", SqlDbType.UniqueIdentifier).Value = quoteHeader.SalesPersonID;
        //                    cmd.Parameters.Add("@QuoteFromCompCode", SqlDbType.VarChar, 10).Value = quoteHeader.QuoteFromCompCode;
        //                    cmd.Parameters.Add("@QuoteStage", SqlDbType.VarChar, 10).Value = quoteHeader.Stage;
        //                    cmd.Parameters.Add("@QuoteSubject", SqlDbType.VarChar, 500).Value = quoteHeader.QuoteSubject;
        //                    cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar, 100).Value = quoteHeader.ContactPerson;
        //                    cmd.Parameters.Add("@SentToAddress", SqlDbType.NVarChar, -1).Value = quoteHeader.SentToAddress;
        //                    cmd.Parameters.Add("@QuoteBodyHead", SqlDbType.NVarChar, -1).Value = quoteHeader.QuoteBodyHead;
        //                    cmd.Parameters.Add("@QuoteBodyFoot", SqlDbType.NVarChar, -1).Value = quoteHeader.QuoteBodyFoot;
        //                    cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = quoteHeader.Discount;
        //                    cmd.Parameters.Add("@TaxTypeCode", SqlDbType.VarChar, 10).Value = quoteHeader.TaxTypeCode;
        //                    cmd.Parameters.Add("@TaxPercApplied", SqlDbType.Decimal).Value = quoteHeader.TaxPercApplied;
        //                    cmd.Parameters.Add("@TaxAmount", SqlDbType.Decimal).Value = quoteHeader.TaxAmount;
        //                    cmd.Parameters.Add("@GeneralNotes", SqlDbType.NVarChar, -1).Value = quoteHeader.GeneralNotes;
        //                    cmd.Parameters.Add("@DetailXML", SqlDbType.Xml).Value = quoteHeader.DetailXML;
        //                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = quoteHeader.commonObj.UpdatedBy;
        //                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = quoteHeader.commonObj.UpdatedDate;
        //                    outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
        //                    outputStatus.Direction = ParameterDirection.Output;
        //                    cmd.ExecuteNonQuery();


        //                }
        //            }
        //            AppConst Cobj = new AppConst();
        //            switch (outputStatus.Value.ToString())
        //            {
        //                case "0":

        //                    throw new Exception(Cobj.UpdateFailure);

        //                case "1":

        //                    return new
        //                    {
        //                        Status = outputStatus.Value.ToString(),
        //                        Message = Cobj.UpdateSuccess
        //                    };
        //                default:
        //                    break;
        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //        return new
        //        {
        //            Status = outputStatus.Value.ToString(),
        //            Message = Cobj.UpdateSuccess
        //        };
        //    }



        //    #region GetQuotationDetails
        //    public List<ProformaInvoice> GetQuotationDetails(string duration)
        //    {
        //        List<ProformaInvoice> quotationsList = null;
        //        try
        //        {
        //            using (SqlConnection con = _databaseFactory.GetDBConnection())
        //            {
        //                using (SqlCommand cmd = new SqlCommand())
        //                {
        //                    if (con.State == ConnectionState.Closed)
        //                    {
        //                        con.Open();
        //                    }
        //                    cmd.Connection = con;
        //                    cmd.CommandText = "[Office].[GetQuotationList]";
        //                    cmd.Parameters.Add("@duration", SqlDbType.Int).Value = duration;
        //                    cmd.CommandType = CommandType.StoredProcedure;

        //                    using (SqlDataReader sdr = cmd.ExecuteReader())
        //                    {
        //                        if ((sdr != null) && (sdr.HasRows))
        //                        {
        //                            quotationsList = new List<ProformaInvoice>();
        //                            while (sdr.Read())
        //                            {
        //                                ProformaInvoice _quotationObj = new ProformaInvoice();
        //                                {
        //                                    _quotationObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _quotationObj.ID);
        //                                    _quotationObj.SalesPersonID = (sdr["SalesPersonID"].ToString() != "" ? Guid.Parse(sdr["SalesPersonID"].ToString()) : _quotationObj.SalesPersonID);
        //                                    _quotationObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? Guid.Parse(sdr["CustomerID"].ToString()) : _quotationObj.CustomerID);
        //                                    _quotationObj.QuotationNo = (sdr["QuotationNo"].ToString() != "" ? sdr["QuotationNo"].ToString() : _quotationObj.QuotationNo);
        //                                    _quotationObj.QuoteStage = (sdr["QuoteStage"].ToString() != "" ? sdr["QuoteStage"].ToString() : _quotationObj.QuoteStage);
        //                                    _quotationObj.QuotationDate = (sdr["QuotationDate"].ToString() != "" ? DateTime.Parse(sdr["QuotationDate"].ToString()).ToString(settings.dateformat) : _quotationObj.QuotationDate);

        //                                    _quotationObj.ValidTillDate = (sdr["ValidTillDate"].ToString() != "" ? DateTime.Parse(sdr["ValidTillDate"].ToString()).ToString(settings.dateformat) : _quotationObj.ValidTillDate);
        //                                    _quotationObj.QuoteFromCompCode = (sdr["QuoteFromCompCode"].ToString() != "" ? sdr["QuoteFromCompCode"].ToString() : _quotationObj.QuoteFromCompCode);
        //                                    _quotationObj.QuoteSubject = (sdr["QuoteSubject"].ToString() != "" ? sdr["QuoteSubject"].ToString() : _quotationObj.QuoteSubject);
        //                                    _quotationObj.QuoteBodyHead = (sdr["QuoteBodyHead"].ToString() != "" ? sdr["QuoteBodyHead"].ToString() : _quotationObj.QuoteBodyHead);
        //                                    _quotationObj.QuoteBodyFoot = (sdr["QuoteBodyFoot"].ToString() != "" ? sdr["QuoteBodyFoot"].ToString() : _quotationObj.QuoteBodyFoot);
        //                                    _quotationObj.SentToAddress = (sdr["SentToAddress"].ToString() != "" ? sdr["SentToAddress"].ToString() : _quotationObj.SentToAddress);
        //                                    _quotationObj.SentToEmails = (sdr["SentToEmails"].ToString() != "" ? sdr["SentToEmails"].ToString() : _quotationObj.SentToEmails);
        //                                    _quotationObj.EmailSentYN = (sdr["EmailSentYN"].ToString() != "" ? sdr["EmailSentYN"].ToString() : _quotationObj.EmailSentYN);
        //                                    _quotationObj.ContactPerson = (sdr["ContactPerson"].ToString() != "" ? sdr["ContactPerson"].ToString() : _quotationObj.ContactPerson);
        //                                    _quotationObj.TaxTypeCode = (sdr["TaxTypeCode"].ToString() != "" ? sdr["TaxTypeCode"].ToString() : _quotationObj.TaxTypeCode);
        //                                    _quotationObj.TaxPercApplied = (sdr["TaxPercApplied"].ToString() != "" ? decimal.Parse(sdr["TaxPercApplied"].ToString()) : _quotationObj.TaxPercApplied);
        //                                    _quotationObj.TaxAmount = (sdr["TaxAmount"].ToString() != "" ? decimal.Parse(sdr["TaxAmount"].ToString()) : _quotationObj.TaxAmount);
        //                                    //_quotationObj.GrantTotal = (sdr["GrantTotal"].ToString() != "" ? decimal.Parse(sdr["GrantTotal"].ToString()) : _quotationObj.GrantTotal);
        //                                    _quotationObj.Discount = (sdr["Discount"].ToString() != "" ? decimal.Parse(sdr["Discount"].ToString()) : _quotationObj.Discount);
        //                                    _quotationObj.Amount = (sdr["Amount"].ToString() != "" ? Decimal.Parse(sdr["Amount"].ToString()) : _quotationObj.Amount);

        //                                    _quotationObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _quotationObj.GeneralNotes);
        //                                    _quotationObj.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _quotationObj.CompanyName);

        //                                }
        //                                quotationsList.Add(_quotationObj);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }

        //        return quotationsList;
        //    }


        //    #endregion GetQuotationDetails
        //    public List<ProformaStage> GetAllQuoteStages()
        //    {
        //        List<ProformaStage> quoteStageList = new List<ProformaStage>();
        //        try
        //        {
        //            using (SqlConnection con = _databaseFactory.GetDBConnection())
        //            {
        //                using (SqlCommand cmd = new SqlCommand())
        //                {
        //                    if (con.State == ConnectionState.Closed)
        //                    {
        //                        con.Open();
        //                    }
        //                    cmd.Connection = con;
        //                    cmd.CommandText = "[Office].[GetAllQuoteStages]";
        //                    cmd.CommandType = CommandType.StoredProcedure;

        //                    using (SqlDataReader sdr = cmd.ExecuteReader())
        //                    {
        //                        if ((sdr != null) && (sdr.HasRows))
        //                        {

        //                            while (sdr.Read())
        //                            {
        //                                QuoteStage quoteStage = new QuoteStage()
        //                                {
        //                                    Code = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : string.Empty),
        //                                    Description = (sdr["Description"].ToString() != "" ? sdr["Description"].ToString() : string.Empty)
        //                                };
        //                                quoteStageList.Add(quoteStage);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }

        //        return quoteStageList;
        //    }

        //    //-------------------Quote Items------------------
        //    //Begin
        //    #region GetAllQuoteItems
        //    public List<ProformaItem> GetAllQuoteItems(Guid? ID)
        //    {
        //        List<ProformaItem> quoteItemList = new List<ProformaItem>();
        //        try
        //        {
        //            using (SqlConnection con = _databaseFactory.GetDBConnection())
        //            {
        //                using (SqlCommand cmd = new SqlCommand())
        //                {
        //                    if (con.State == ConnectionState.Closed)
        //                    {
        //                        con.Open();
        //                    }
        //                    cmd.Connection = con;
        //                    cmd.CommandText = "[Office].[GetQuotationItems]";
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.Parameters.Add("@QuoteID", SqlDbType.UniqueIdentifier).Value = ID;
        //                    using (SqlDataReader sdr = cmd.ExecuteReader())
        //                    {
        //                        if ((sdr != null) && (sdr.HasRows))
        //                        {
        //                            while (sdr.Read())
        //                            {
        //                                ProformaItem quoteItem = new ProformaItem();
        //                                {
        //                                    quoteItem.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : quoteItem.ID);
        //                                    quoteItem.QuoteID = (sdr["QuoteID"].ToString() != "" ? Guid.Parse(sdr["QuoteID"].ToString()) : quoteItem.QuoteID);
        //                                    quoteItem.ProductDescription = (sdr["ProductDesc"].ToString() != "" ? sdr["ProductDesc"].ToString() : quoteItem.ProductDescription);
        //                                    quoteItem.UnitCode = (sdr["UnitCode"].ToString() != "" ? sdr["UnitCode"].ToString() : quoteItem.UnitCode);
        //                                    quoteItem.UnitDescription = (sdr["UnitDescription"].ToString() != "" ? sdr["UnitDescription"].ToString() : quoteItem.UnitDescription);
        //                                    quoteItem.Quantity = (sdr["Qty"].ToString() != "" ? decimal.Parse(sdr["Qty"].ToString()) : quoteItem.Quantity);
        //                                    quoteItem.Rate = (sdr["Rate"].ToString() != "" ? decimal.Parse(sdr["Rate"].ToString()) : quoteItem.Rate);
        //                                    quoteItem.product = new Product()
        //                                    {
        //                                        ID = (sdr["ProductID"].ToString() != "" ? Guid.Parse(sdr["ProductID"].ToString()) : Guid.Empty),
        //                                        Code = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : string.Empty),
        //                                        Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : string.Empty)
        //                                    };
        //                                    quoteItem.ProductID = (sdr["ProductID"].ToString() != "" ? Guid.Parse(sdr["ProductID"].ToString()) : Guid.Empty);
        //                                    quoteItem.ProductCode = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : string.Empty);
        //                                }
        //                                quoteItemList.Add(quoteItem);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }

        //        return quoteItemList;
        //    }


        //    #endregion GetQuotationDetails
        //    #region DeleteQuoteItem
        //    public object DeleteQuoteItem(Guid? ID)
        //    {
        //        SqlParameter outputStatus = null;
        //        try
        //        {
        //            using (SqlConnection con = _databaseFactory.GetDBConnection())
        //            {
        //                using (SqlCommand cmd = new SqlCommand())
        //                {
        //                    if (con.State == ConnectionState.Closed)
        //                    {
        //                        con.Open();
        //                    }
        //                    cmd.Connection = con;
        //                    cmd.CommandText = "[Office].[DeleteQuoteItemByID]";
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = ID;
        //                    outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
        //                    outputStatus.Direction = ParameterDirection.Output;
        //                    cmd.ExecuteNonQuery();


        //                }
        //            }

        //            switch (outputStatus.Value.ToString())
        //            {
        //                case "0":

        //                    throw new Exception(Cobj.DeleteFailure);

        //                default:
        //                    break;
        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //        return new
        //        {
        //            Status = outputStatus.Value.ToString(),
        //            Message = Cobj.DeleteSuccess
        //        };
        //    }


        //    #endregion DeleteQuoteItem

        //    public object UpdateQuoteMailStatus(ProformaHeader quoteHeader)
        //    {
        //        SqlParameter outputStatus = null;
        //        try
        //        {

        //            using (SqlConnection con = _databaseFactory.GetDBConnection())
        //            {
        //                using (SqlCommand cmd = new SqlCommand())
        //                {
        //                    if (con.State == ConnectionState.Closed)
        //                    {
        //                        con.Open();
        //                    }
        //                    cmd.Connection = con;
        //                    cmd.CommandText = "[Office].[UpdateQuoteMailStatus]";
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = quoteHeader.ID;
        //                    cmd.Parameters.Add("@SentToEmails", SqlDbType.NVarChar, -1).Value = quoteHeader.SentToEmails;
        //                    cmd.Parameters.Add("@EmailSentYN", SqlDbType.Bit).Value = quoteHeader.EmailSentYN;
        //                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = quoteHeader.commonObj.UpdatedBy;
        //                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = quoteHeader.commonObj.UpdatedDate;
        //                    outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
        //                    outputStatus.Direction = ParameterDirection.Output;
        //                    cmd.ExecuteNonQuery();
        //                }
        //            }
        //            AppConst Cobj = new AppConst();
        //            switch (outputStatus.Value.ToString())
        //            {
        //                case "0":

        //                    throw new Exception(Cobj.UpdateFailure);

        //                case "1":

        //                    return new
        //                    {
        //                        Status = outputStatus.Value.ToString(),
        //                        Message = Cobj.UpdateSuccess
        //                    };
        //                default:
        //                    break;
        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //        return new
        //        {
        //            Status = outputStatus.Value.ToString(),
        //            Message = Cobj.UpdateSuccess
        //        };
        //    }


        //    public ProformaInvoiceSummary GetQuotationSummary()
        //    {
        //        ProformaInvoiceSummary QS = new ProformaInvoiceSummary();
        //        try
        //        {
        //            using (SqlConnection con = _databaseFactory.GetDBConnection())
        //            {
        //                using (SqlCommand cmd = new SqlCommand())
        //                {
        //                    if (con.State == ConnectionState.Closed)
        //                    {
        //                        con.Open();
        //                    }
        //                    cmd.Connection = con;
        //                    cmd.CommandText = "[Office].[GetQuotationSummary]";
        //                    cmd.CommandType = CommandType.StoredProcedure;

        //                    using (SqlDataReader sdr = cmd.ExecuteReader())
        //                    {
        //                        if ((sdr != null) && (sdr.HasRows))
        //                        {

        //                            if (sdr.Read())
        //                            {

        //                                {
        //                                    QS.Total = (sdr["Total"].ToString() != "" ? int.Parse(sdr["Total"].ToString()) : 0);
        //                                    QS.Draft = (sdr["Draft"].ToString() != "" ? int.Parse(sdr["Draft"].ToString()) : 0);
        //                                    QS.OnHold = (sdr["OnHold"].ToString() != "" ? int.Parse(sdr["OnHold"].ToString()) : 0);
        //                                    QS.Closed = (sdr["Closed"].ToString() != "" ? int.Parse(sdr["Closed"].ToString()) : 0);
        //                                    QS.InProgress = (sdr["InProgress"].ToString() != "" ? int.Parse(sdr["InProgress"].ToString()) : 0);
        //                                }

        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }

        //        return QS;
        //    }

        //    //End
        //    //-------------------Quote Items------------------
        //}
    }
}
