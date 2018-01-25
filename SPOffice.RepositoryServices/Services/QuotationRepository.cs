using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

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
                                        _quoteHeader.customer = new Customer();
                                        {
                                            _quoteHeader.customer.ID = (Guid)_quoteHeader.CustomerID;
                                            _quoteHeader.customer.CompanyName= (sdr["CustomerName"].ToString() != "" ? sdr["CustomerName"].ToString() : _quoteHeader.customer.CompanyName);
                                            _quoteHeader.customer.BillingAddress= (sdr["CustomerAddress"].ToString() != "" ? sdr["CustomerAddress"].ToString() : _quoteHeader.customer.BillingAddress);
                                            _quoteHeader.customer.ContactEmail= (sdr["CustomerEmail"].ToString() != "" ? sdr["CustomerEmail"].ToString() : _quoteHeader.customer.ContactEmail);
                                        }
                                        _quoteHeader.QuotationDate = (sdr["QuotationDate"].ToString() != "" ? DateTime.Parse(sdr["QuotationDate"].ToString()).ToString(settings.dateformat) : _quoteHeader.QuotationDate);
                                        _quoteHeader.ValidTillDate = (sdr["ValidTillDate"].ToString() != "" ? DateTime.Parse(sdr["ValidTillDate"].ToString()).ToString(settings.dateformat) : _quoteHeader.ValidTillDate);

                                        _quoteHeader.SalesPersonID = (sdr["SalesPersonID"].ToString() != "" ? Guid.Parse(sdr["SalesPersonID"].ToString()) : _quoteHeader.SalesPersonID);
                                        _quoteHeader.QuoteFromCompCode = (sdr["QuoteFromCompCode"].ToString() != "" ? sdr["QuoteFromCompCode"].ToString() : _quoteHeader.QuoteFromCompCode);
                                        _quoteHeader.company = new Company();
                                        {
                                            _quoteHeader.company.Code = _quoteHeader.QuoteFromCompCode;
                                            _quoteHeader.company.Name= (sdr["QuoteFromCompanyName"].ToString() != "" ? sdr["QuoteFromCompanyName"].ToString() : _quoteHeader.company.Name);
                                            _quoteHeader.company.BillingAddress= (sdr["CompanyAddress"].ToString() != "" ? sdr["CompanyAddress"].ToString() : _quoteHeader.company.BillingAddress);
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
            SqlParameter outputStatus, outputID;
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
                        cmd.CommandText = "[Office].[InsertQuotation]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.UniqueIdentifier).Value = quoteHeader.CustomerID;
                        cmd.Parameters.Add("@QuotationDate", SqlDbType.DateTime).Value = quoteHeader.QuotationDate;
                        cmd.Parameters.Add("@ValidTillDate", SqlDbType.DateTime).Value = quoteHeader.ValidTillDate;
                        cmd.Parameters.Add("@SalesPersonID", SqlDbType.UniqueIdentifier).Value = quoteHeader.SalesPersonID;
                        cmd.Parameters.Add("@QuoteFromCompCode", SqlDbType.VarChar,10).Value = quoteHeader.QuoteFromCompCode;
                        cmd.Parameters.Add("@QuoteStage", SqlDbType.VarChar,10).Value = quoteHeader.Stage;
                        cmd.Parameters.Add("@QuoteSubject", SqlDbType.VarChar,500).Value = quoteHeader.QuoteSubject;
                       // cmd.Parameters.Add("@SentToEmails", SqlDbType.NVarChar,-1).Value = quoteHeader.SentToEmails;
                        cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar,100).Value = quoteHeader.ContactPerson;
                        cmd.Parameters.Add("@SentToAddress", SqlDbType.NVarChar,-1).Value = quoteHeader.SentToAddress;
                        cmd.Parameters.Add("@QuoteBodyHead", SqlDbType.NVarChar,-1).Value = quoteHeader.QuoteBodyHead;
                        cmd.Parameters.Add("@QuoteBodyFoot", SqlDbType.NVarChar,-1).Value = quoteHeader.QuoteBodyFoot;

                       // cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = quoteHeader.Discount;
                       // cmd.Parameters.Add("@TaxTypeCode", SqlDbType.VarChar,10).Value = quoteHeader.TaxTypeCode;
                       // cmd.Parameters.Add("@TaxPercApplied", SqlDbType.Decimal).Value = quoteHeader.TaxPercApplied;
                       // cmd.Parameters.Add("@TaxAmount", SqlDbType.Decimal).Value = quoteHeader.TaxAmount;
                        cmd.Parameters.Add("@GeneralNotes", SqlDbType.NVarChar,-1).Value = quoteHeader.GeneralNotes;
                        //   cmd.Parameters.Add("@EmailSentYN", SqlDbType.Bit).Value = quoteHeader.EmailSentYN;
                        cmd.Parameters.Add("@DetailXML", SqlDbType.Xml).Value = quoteHeader.DetailXML;
                        cmd.Parameters.Add("@FileDupID", SqlDbType.UniqueIdentifier).Value = quoteHeader.hdnFileID;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = quoteHeader.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = quoteHeader.commonObj.CreatedDate;
                        outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;
                        outputID = cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier);
                        outputID.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                    }
                }
                AppConst Cobj = new AppConst();
                switch (outputStatus.Value.ToString())
                {
                    case "0":
                        throw new Exception(Cobj.InsertFailure);
                    case "1":
                        //success
                        return new
                        {
                            ID = outputID.Value.ToString(),
                            Status = outputStatus.Value.ToString(),
                            Message = Cobj.InsertSuccess
                        };

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return new
            {
                ID = outputID.Value.ToString(),
                Status = outputStatus.Value.ToString(),
                Message = Cobj.InsertSuccess
            };
        }

        public object UpdateQuotation(QuoteHeader quoteHeader)
        {
            SqlParameter outputStatus = null;
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
                        cmd.CommandText = "[Office].[UpdateQuotation]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = quoteHeader.ID;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.UniqueIdentifier).Value = quoteHeader.CustomerID;
                        cmd.Parameters.Add("@QuotationDate", SqlDbType.DateTime).Value = quoteHeader.QuotationDate;
                        cmd.Parameters.Add("@ValidTillDate", SqlDbType.DateTime).Value = quoteHeader.ValidTillDate;
                        cmd.Parameters.Add("@SalesPersonID", SqlDbType.UniqueIdentifier).Value = quoteHeader.SalesPersonID;
                        cmd.Parameters.Add("@QuoteFromCompCode", SqlDbType.VarChar, 10).Value = quoteHeader.QuoteFromCompCode;
                        cmd.Parameters.Add("@QuoteStage", SqlDbType.VarChar, 10).Value = quoteHeader.Stage;
                        cmd.Parameters.Add("@QuoteSubject", SqlDbType.VarChar, 500).Value = quoteHeader.QuoteSubject;
                        cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar, 100).Value = quoteHeader.ContactPerson;
                        cmd.Parameters.Add("@SentToAddress", SqlDbType.NVarChar, -1).Value = quoteHeader.SentToAddress;
                        cmd.Parameters.Add("@QuoteBodyHead", SqlDbType.NVarChar, -1).Value = quoteHeader.QuoteBodyHead;
                        cmd.Parameters.Add("@QuoteBodyFoot", SqlDbType.NVarChar, -1).Value = quoteHeader.QuoteBodyFoot;
                        cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = quoteHeader.Discount;
                        cmd.Parameters.Add("@TaxTypeCode", SqlDbType.VarChar, 10).Value = quoteHeader.TaxTypeCode;
                        cmd.Parameters.Add("@TaxPercApplied", SqlDbType.Decimal).Value = quoteHeader.TaxPercApplied;
                        cmd.Parameters.Add("@TaxAmount", SqlDbType.Decimal).Value = quoteHeader.TaxAmount;
                        cmd.Parameters.Add("@GeneralNotes", SqlDbType.NVarChar, -1).Value = quoteHeader.GeneralNotes;
                        cmd.Parameters.Add("@DetailXML", SqlDbType.Xml).Value = quoteHeader.DetailXML;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = quoteHeader.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = quoteHeader.commonObj.UpdatedDate;
                        outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();


                    }
                }
                AppConst Cobj = new AppConst();
                switch (outputStatus.Value.ToString())
                {
                    case "0":

                        throw new Exception(Cobj.UpdateFailure);

                    case "1":

                        return new
                        {
                            Status = outputStatus.Value.ToString(),
                            Message = Cobj.UpdateSuccess
                        };
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return new
            {
                Status = outputStatus.Value.ToString(),
                Message = Cobj.UpdateSuccess
            };
        }



        //Delete Quotation

        public object DeleteQuotation(Guid? ID)
        {           
            try
            {
                SqlParameter outputStatus = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[Office].[DeleteQuotations]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = ID;
                        outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();


                    }
                }
                AppConst Cobj = new AppConst();
                switch (outputStatus.Value.ToString())
                {
                    case "0":
                        //Const Cobj = new Const();
                        throw new Exception(Cobj.DeleteFailure);
                    case "1":
                        return new
                        {
                            status = outputStatus.Value.ToString(),
                            Message = Cobj.DeleteSuccess
                        };


                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return new
            {

            };

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

        //-------------------Quote Items------------------
        //Begin
        #region GetAllQuoteItems
        public List<QuoteItem> GetAllQuoteItems(Guid? ID)
        {
            List<QuoteItem> quoteItemList = new List<QuoteItem>();
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
                        cmd.CommandText = "[Office].[GetQuotationItems]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QuoteID", SqlDbType.UniqueIdentifier).Value = ID;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                while (sdr.Read())
                                {
                                    QuoteItem quoteItem = new QuoteItem();
                                    {
                                        quoteItem.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : quoteItem.ID);
                                        quoteItem.QuoteID = (sdr["QuoteID"].ToString() != "" ? Guid.Parse(sdr["QuoteID"].ToString()) : quoteItem.QuoteID);
                                        quoteItem.ProductDescription = (sdr["ProductDesc"].ToString() != "" ? sdr["ProductDesc"].ToString() : quoteItem.ProductDescription);
                                        quoteItem.UnitCode = (sdr["UnitCode"].ToString() != "" ? sdr["UnitCode"].ToString() : quoteItem.UnitCode);
                                        quoteItem.UnitDescription= (sdr["UnitDescription"].ToString() != "" ? sdr["UnitDescription"].ToString() : quoteItem.UnitDescription);
                                        quoteItem.Quantity = (sdr["Qty"].ToString() != "" ? decimal.Parse(sdr["Qty"].ToString()) : quoteItem.Quantity);
                                        quoteItem.Rate = (sdr["Rate"].ToString() != "" ? decimal.Parse(sdr["Rate"].ToString()) : quoteItem.Rate);
                                        quoteItem.product = new Product()
                                        {
                                            ID = (sdr["ProductID"].ToString() != "" ? Guid.Parse(sdr["ProductID"].ToString()) : Guid.Empty),
                                            Code = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : string.Empty),
                                            Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : string.Empty)
                                        };
                                        quoteItem.ProductID = (sdr["ProductID"].ToString() != "" ? Guid.Parse(sdr["ProductID"].ToString()) : Guid.Empty);
                                        quoteItem.ProductCode = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : string.Empty);
                                        quoteItem.OldProductCode = (sdr["OldCode"].ToString() != "" ? sdr["OldCode"].ToString() : string.Empty);
                                        quoteItem.company = new Company();
                                        quoteItem.company.LogoURL = (sdr["LogoURL"].ToString() != "" ? sdr["LogoURL"].ToString() : quoteItem.company.LogoURL);
                                    }
                                    quoteItemList.Add(quoteItem);
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

            return quoteItemList;
        }


        #endregion GetQuotationDetails
        #region DeleteQuoteItem
        public object DeleteQuoteItem(Guid? ID)
        {
            SqlParameter outputStatus = null;
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
                        cmd.CommandText = "[Office].[DeleteQuoteItemByID]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = ID;
                        outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();


                    }
                }

                switch (outputStatus.Value.ToString())
                {
                    case "0":

                        throw new Exception(Cobj.DeleteFailure);

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return new
            {
                Status = outputStatus.Value.ToString(),
                Message = Cobj.DeleteSuccess
            };
        }


        #endregion DeleteQuoteItem

        public object UpdateQuoteMailStatus(QuoteHeader quoteHeader)
        {
            SqlParameter outputStatus = null;
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
                        cmd.CommandText = "[Office].[UpdateQuoteMailStatus]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = quoteHeader.ID;
                        cmd.Parameters.Add("@SentToEmails",SqlDbType.NVarChar, -1).Value = quoteHeader.SentToEmails;
                        cmd.Parameters.Add("@EmailSentYN", SqlDbType.Bit).Value = quoteHeader.EmailSentYN;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = quoteHeader.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = quoteHeader.commonObj.UpdatedDate;
                        outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                    }
                }
                AppConst Cobj = new AppConst();
                switch (outputStatus.Value.ToString())
                {
                    case "0":

                        throw new Exception(Cobj.UpdateFailure);

                    case "1":

                        return new
                        {
                            Status = outputStatus.Value.ToString(),
                            Message = Cobj.UpdateSuccess
                        };
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return new
            {
                Status = outputStatus.Value.ToString(),
                Message = Cobj.UpdateSuccess
            };
        }


        public QuotationSummary GetQuotationSummary()
        {
            QuotationSummary QS = new QuotationSummary();
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
                        cmd.CommandText = "[Office].[GetQuotationSummary]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {

                                if (sdr.Read())
                                {
                                    
                                    {
                                        QS.Total = (sdr["Total"].ToString() != "" ? int.Parse(sdr["Total"].ToString()) : 0);
                                        QS.Draft = (sdr["Draft"].ToString() != "" ? int.Parse(sdr["Draft"].ToString()) : 0);
                                        QS.OnHold = (sdr["OnHold"].ToString() != "" ? int.Parse(sdr["OnHold"].ToString()) : 0);
                                        QS.Closed = (sdr["Closed"].ToString() != "" ? int.Parse(sdr["Closed"].ToString()) : 0);
                                        QS.InProgress = (sdr["InProgress"].ToString() != "" ? int.Parse(sdr["InProgress"].ToString()) : 0);
                                    }
                                    
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

            return QS;
        }

        //End
        //-------------------Quote Items------------------
    }
}
    
