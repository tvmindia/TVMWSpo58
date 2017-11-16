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



        public List<ProformaHeader> GetAllProformaInvoices()
        {
            List<ProformaHeader> proformaHeaderList = null;
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
                                proformaHeaderList = new List<ProformaHeader>();
                                while (sdr.Read())
                                {
                                    ProformaHeader _proformaHeader = new ProformaHeader();
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
                                        _proformaHeader.OriginCompCode = (sdr["OriginCompCode"].ToString() != "" ? sdr["OriginCompCode"].ToString() : _proformaHeader.OriginCompCode);
                                        _proformaHeader.company = new Company();
                                        {
                                            _proformaHeader.company.Code = _proformaHeader.OriginCompCode;
                                            _proformaHeader.company.Name = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _proformaHeader.company.Name);
                                           _proformaHeader.company.BillingAddress = (sdr["CompanyAddress"].ToString() != "" ? sdr["CompanyAddress"].ToString() : _proformaHeader.company.BillingAddress);
                                        }
                                        _proformaHeader.Subject = (sdr["Subject"].ToString() != "" ? sdr["Subject"].ToString() : _proformaHeader.Subject);
                                        _proformaHeader.Discount = (sdr["Discount"].ToString() != "" ? decimal.Parse(sdr["Discount"].ToString()) : _proformaHeader.Discount);

                                        _proformaHeader.TaxAmount = (sdr["TaxAmount"].ToString() != "" ? decimal.Parse(sdr["TaxAmount"].ToString()) : _proformaHeader.TaxAmount);

                                        _proformaHeader.Total = (sdr["TotalAmount"].ToString() != "" ? decimal.Parse(sdr["TotalAmount"].ToString()) : _proformaHeader.Total);
                                        _proformaHeader.Subject = (sdr["Subject"].ToString() != "" ? sdr["Subject"].ToString() : _proformaHeader.Subject);
                                        _proformaHeader.SentToEmails = (sdr["SentToEmails"].ToString() != "" ? sdr["SentToEmails"].ToString() : _proformaHeader.SentToEmails);
                                        _proformaHeader.EmailSentYN = (sdr["EmailSentYN"].ToString() != "" ? sdr["EmailSentYN"].ToString() : _proformaHeader.EmailSentYN);
                                        _proformaHeader.ContactPerson = (sdr["ContactPerson"].ToString() != "" ? sdr["ContactPerson"].ToString() : _proformaHeader.ContactPerson);
                                        _proformaHeader.SentToAddress = (sdr["SentToAddress"].ToString() != "" ? sdr["SentToAddress"].ToString() : _proformaHeader.SentToAddress);
                                        _proformaHeader.BodyHead = (sdr["BodyHeader"].ToString() != "" ? sdr["BodyHeader"].ToString() : _proformaHeader.BodyHead);
                                        _proformaHeader.BodyFoot = (sdr["BodyFooter"].ToString() != "" ? sdr["BodyFooter"].ToString() : _proformaHeader.BodyFoot);
                                        _proformaHeader.TaxTypeCode = (sdr["TaxTypeCode"].ToString() != "" ? sdr["TaxTypeCode"].ToString() : _proformaHeader.TaxTypeCode);
                                        _proformaHeader.TaxPercApplied = (sdr["TaxPercApplied"].ToString() != "" ? decimal.Parse(sdr["TaxPercApplied"].ToString()) : _proformaHeader.TaxPercApplied);
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
        public object InsertProformaInvoices(ProformaHeader proformaHeader)
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
                        cmd.CommandText = "[Office].[InsertProformaInvoice]";
                        cmd.CommandType = CommandType.StoredProcedure;                       
                        cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar, 20).Value = proformaHeader.InvoiceNo;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.UniqueIdentifier).Value = proformaHeader.CustomerID;
                        cmd.Parameters.Add("@InvoiceDate", SqlDbType.DateTime).Value = proformaHeader.InvoiceDate;
                        cmd.Parameters.Add("@ValidTillDate", SqlDbType.DateTime).Value = proformaHeader.ValidTillDate;
                        cmd.Parameters.Add("@OriginCompCode", SqlDbType.VarChar, 10).Value = proformaHeader.OriginCompCode;
                        cmd.Parameters.Add("@Subject", SqlDbType.VarChar, 500).Value = proformaHeader.Subject;
                        cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar, 100).Value = proformaHeader.ContactPerson;
                        cmd.Parameters.Add("@SentToAddress", SqlDbType.NVarChar, -1).Value = proformaHeader.SentToAddress;
                        cmd.Parameters.Add("@BodyHeader", SqlDbType.NVarChar, -1).Value = proformaHeader.BodyHead;
                        cmd.Parameters.Add("@BodyFooter", SqlDbType.NVarChar, -1).Value = proformaHeader.BodyFoot;
                       // cmd.Parameters.Add("@SentToEmails", SqlDbType.NVarChar,-1).Value = proformaHeader.SentToEmails;
                        cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = proformaHeader.Discount;
                        cmd.Parameters.Add("@TaxTypeCode", SqlDbType.VarChar, 10).Value = proformaHeader.TaxTypeCode;
                        cmd.Parameters.Add("@TaxPercApplied", SqlDbType.Decimal).Value = proformaHeader.TaxPercApplied;
                        cmd.Parameters.Add("@TaxAmount", SqlDbType.Decimal).Value = proformaHeader.TaxAmount;
                       // cmd.Parameters.Add("@EmailSentYN", SqlDbType.Bit).Value = proformaHeader.EmailSentYN;
                        cmd.Parameters.Add("@DetailXML", SqlDbType.Xml).Value = proformaHeader.DetailXML;
                        cmd.Parameters.Add("@FileDupID", SqlDbType.UniqueIdentifier).Value = proformaHeader.hdnFileID;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = proformaHeader.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = proformaHeader.commonObj.CreatedDate;
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
                //Message = Cobj.InsertSuccess
            };
        }

        public object UpdateProformaInvoices(ProformaHeader proformaHeader)
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
                        cmd.CommandText = "[Office].[UpdateProformaInvoice]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = proformaHeader.ID;
                        cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar, 20).Value = proformaHeader.InvoiceNo;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.UniqueIdentifier).Value = proformaHeader.CustomerID;
                        cmd.Parameters.Add("@InvoiceDate", SqlDbType.DateTime).Value = proformaHeader.InvoiceDate;
                        cmd.Parameters.Add("@ValidTillDate", SqlDbType.DateTime).Value = proformaHeader.ValidTillDate;
                        cmd.Parameters.Add("@Subject", SqlDbType.VarChar, 500).Value = proformaHeader.Subject;
                        cmd.Parameters.Add("@OriginCompCode", SqlDbType.VarChar, 10).Value = proformaHeader.OriginCompCode;
                        cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar, 100).Value = proformaHeader.ContactPerson;
                        cmd.Parameters.Add("@SentToAddress", SqlDbType.NVarChar, -1).Value = proformaHeader.SentToAddress;
                        cmd.Parameters.Add("@BodyHeader", SqlDbType.NVarChar, -1).Value = proformaHeader.BodyHead;
                        cmd.Parameters.Add("@BodyFooter", SqlDbType.NVarChar, -1).Value = proformaHeader.BodyFoot;
                       // cmd.Parameters.Add("@SentToEmails", SqlDbType.NVarChar, -1).Value = proformaHeader.SentToEmails;
                        //cmd.Parameters.Add("@EmailSentYN", SqlDbType.Bit).Value = proformaHeader.EmailSentYN;
                        cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = proformaHeader.Discount;
                        cmd.Parameters.Add("@TaxTypeCode", SqlDbType.VarChar, 10).Value = proformaHeader.TaxTypeCode;
                        cmd.Parameters.Add("@TaxPercApplied", SqlDbType.Decimal).Value = proformaHeader.TaxPercApplied;
                        cmd.Parameters.Add("@TaxAmount", SqlDbType.Decimal).Value = proformaHeader.TaxAmount;
                        cmd.Parameters.Add("@DetailXML", SqlDbType.Xml).Value = proformaHeader.DetailXML;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = proformaHeader.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = proformaHeader.commonObj.UpdatedDate;
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
                // Message = Cobj.UpdateSuccess
            };
        }


        #region GetQuotationDetails
        public List<ProformaInvoice> GetQuotationDetails(string duration)
        {
            List<ProformaInvoice> quotationsList = null;
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
                        cmd.CommandText = "[Office].[ProformaInvoiceList]";
                        cmd.Parameters.Add("@duration", SqlDbType.Int).Value = duration;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                quotationsList = new List<ProformaInvoice>();
                                while (sdr.Read())
                                {
                                    ProformaInvoice _quotationObj = new ProformaInvoice();
                                    {
                                        _quotationObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _quotationObj.ID);
                                       
                                        _quotationObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? Guid.Parse(sdr["CustomerID"].ToString()) : _quotationObj.CustomerID);
                                        _quotationObj.InvoiceNo = (sdr["InvoiceNo"].ToString() != "" ? sdr["InvoiceNo"].ToString() : _quotationObj.InvoiceNo);
                                        //_quotationObj.QuoteStage = (sdr["QuoteStage"].ToString() != "" ? sdr["QuoteStage"].ToString() : _quotationObj.QuoteStage);
                                        _quotationObj.InvoiceDate = (sdr["InvoiceDate"].ToString() != "" ? DateTime.Parse(sdr["InvoiceDate"].ToString()).ToString(settings.dateformat) : _quotationObj.InvoiceDate);

                                        _quotationObj.ValidTillDate = (sdr["ValidTillDate"].ToString() != "" ? DateTime.Parse(sdr["ValidTillDate"].ToString()).ToString(settings.dateformat) : _quotationObj.ValidTillDate);

                                        _quotationObj.Subject = (sdr["Subject"].ToString() != "" ? sdr["Subject"].ToString() : _quotationObj.Subject);
                                        _quotationObj.BodyHead = (sdr["BodyHeader"].ToString() != "" ? sdr["BodyHeader"].ToString() : _quotationObj.BodyHead);
                                        _quotationObj.BodyFoot = (sdr["BodyFooter"].ToString() != "" ? sdr["BodyFooter"].ToString() : _quotationObj.BodyFoot);
                                        _quotationObj.SentToAddress = (sdr["SentToAddress"].ToString() != "" ? sdr["SentToAddress"].ToString() : _quotationObj.SentToAddress);
                                        //_quotationObj.SentToEmails = (sdr["SentToEmails"].ToString() != "" ? sdr["SentToEmails"].ToString() : _quotationObj.SentToEmails);
                                        // _quotationObj.EmailSentYN = (sdr["EmailSentYN"].ToString() != "" ? sdr["EmailSentYN"].ToString() : _quotationObj.EmailSentYN);
                                        _quotationObj.ContactPerson = (sdr["ContactPerson"].ToString() != "" ? sdr["ContactPerson"].ToString() : _quotationObj.ContactPerson);
                                        _quotationObj.TaxTypeCode = (sdr["TaxTypeCode"].ToString() != "" ? sdr["TaxTypeCode"].ToString() : _quotationObj.TaxTypeCode);
                                        _quotationObj.TaxPercApplied = (sdr["TaxPercApplied"].ToString() != "" ? decimal.Parse(sdr["TaxPercApplied"].ToString()) : _quotationObj.TaxPercApplied);
                                        _quotationObj.TaxAmount = (sdr["TaxAmount"].ToString() != "" ? decimal.Parse(sdr["TaxAmount"].ToString()) : _quotationObj.TaxAmount);
                                       
                                        _quotationObj.Discount = (sdr["Discount"].ToString() != "" ? decimal.Parse(sdr["Discount"].ToString()) : _quotationObj.Discount);
                                        _quotationObj.Amount = (sdr["Amount"].ToString() != "" ? Decimal.Parse(sdr["Amount"].ToString()) : _quotationObj.Amount);                                    
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

        #endregion GetProformaInvoiceDetails

        #region GetAllProformaInvoiceItems
        public List<ProformaItem> GetAllQuoteItems(Guid? ID)
        {
            List<ProformaItem> quoteItemList = new List<ProformaItem>();
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
                        cmd.CommandText = "[Office].[GetProformaInvoiceDetailItems]";
                        cmd.Parameters.Add("@InvoiceID", SqlDbType.UniqueIdentifier).Value =ID;                      
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                while (sdr.Read())
                                {
                                    ProformaItem proformaItem = new ProformaItem();
                                    {
                                        proformaItem.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : proformaItem.ID);
                                      
                                        proformaItem.ProductDescription = (sdr["ProductDesc"].ToString() != "" ? sdr["ProductDesc"].ToString() : proformaItem.ProductDescription);
                                        proformaItem.UnitCode = (sdr["UnitCode"].ToString() != "" ? sdr["UnitCode"].ToString() : proformaItem.UnitCode);
                                        proformaItem.UnitDescription = (sdr["UnitDescription"].ToString() != "" ? sdr["UnitDescription"].ToString() : proformaItem.UnitDescription);
                                        proformaItem.Quantity = (sdr["Qty"].ToString() != "" ? decimal.Parse(sdr["Qty"].ToString()) : proformaItem.Quantity);
                                        proformaItem.Rate = (sdr["Rate"].ToString() != "" ? decimal.Parse(sdr["Rate"].ToString()) : proformaItem.Rate);
                                        proformaItem.product = new Product()
                                        {
                                            ID = (sdr["ProductID"].ToString() != "" ? Guid.Parse(sdr["ProductID"].ToString()) : Guid.Empty),
                                            Code = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : string.Empty),
                                            Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : string.Empty)
                                        };
                                        proformaItem.ProductID = (sdr["ProductID"].ToString() != "" ? Guid.Parse(sdr["ProductID"].ToString()) : Guid.Empty);
                                        proformaItem.ProductCode = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : string.Empty);
                                    }
                                    quoteItemList.Add(proformaItem);
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

        #endregion GetProformaInvoiceItems


        #region DeleteProformaItem
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
                        cmd.CommandText = "[Office].[DeleteProformaInvoiceItemByID]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = ID;
                        outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();


                    }
                }
            }

            catch (Exception ex)
            {

                throw ex;
            }
            return new
            {
                Status = outputStatus.Value.ToString(),
                
            };
        }


        #endregion DeleteProformaItem



        public object UpdateQuoteMailStatus(ProformaHeader proformaHeader)
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
                        cmd.CommandText = "[Office].[UpdateProformaMailStatus]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = proformaHeader.ID;
                        cmd.Parameters.Add("@SentToEmails", SqlDbType.NVarChar, -1).Value = proformaHeader.SentToEmails;
                        cmd.Parameters.Add("@EmailSentYN", SqlDbType.Bit).Value = proformaHeader.EmailSentYN;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = proformaHeader.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = proformaHeader.commonObj.UpdatedDate;
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
               // Message = Cobj.UpdateSuccess
            };
        }

        //Delete ProformaInvoice

        public object DeleteProformaInvoice(Guid? ID)
        {
           // bool result = false;
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
                        cmd.CommandText = "[Office].[DeleteProformaInvoice]";
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
                        return new{
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

    }
}