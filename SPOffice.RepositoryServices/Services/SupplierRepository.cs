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
    public class SupplierRepository : ISupplierRepository
    {
        AppConst Cobj = new AppConst();
        Settings s = new Settings();
        private IDatabaseFactory _databaseFactory;
        public SupplierRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        } 

        #region GetAllSupplierMobile
        public List<Supplier> GetAllSupplierPOForMobile(string duration)
        {
            List<Supplier> supplierList = null;
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
                        cmd.CommandText = "[Office].[GetAllSupplierOrder]";
                        cmd.Parameters.Add("@duration", SqlDbType.Int).Value = duration;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                supplierList = new List<Supplier>();
                                while (sdr.Read())
                                {
                                    Supplier _supplierObj = new Supplier();
                                    {
                                        _supplierObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _supplierObj.ID);
                                        _supplierObj.SupplierID = (sdr["SupplierID"].ToString() != "" ? Guid.Parse(sdr["SupplierID"].ToString()) : _supplierObj.SupplierID);
                                        _supplierObj.PONo = (sdr["PONo"].ToString() != "" ? sdr["PONo"].ToString() : _supplierObj.PONo);
                                        //_supplierObj.POIssuedDate = (sdr["POIssuedDate"].ToString() != "" ? DateTime.Parse(sdr["POIssuedDate"].ToString()) : _supplierObj.POIssuedDate);
                                        _supplierObj.PODate = (sdr["PODate"].ToString() != "" ? DateTime.Parse(sdr["PODate"].ToString()).ToString(s.dateformat) : _supplierObj.PODate);
                                        //_supplierObj.POFromCompCode = (sdr["POFromCompCode"].ToString() != "" ? sdr["POFromCompCode"].ToString() : _supplierObj.POFromCompCode);
                                        //_supplierObj.SupplierMailingAddress = (sdr["SupplierMailingAddress"].ToString() != "" ? sdr["SupplierMailingAddress"].ToString() : _supplierObj.SupplierMailingAddress);
                                        //_supplierObj.ShipToAddress = (sdr["ShipToAddress"].ToString() != "" ? sdr["ShipToAddress"].ToString() : _supplierObj.ShipToAddress);
                                        //_supplierObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _supplierObj.GeneralNotes);
                                        //_supplierObj.BodyHeader = (sdr["BodyHeader"].ToString() != "" ? sdr["BodyHeader"].ToString() : _supplierObj.BodyHeader);
                                        //_supplierObj.BodyFooter = (sdr["BodyFooter"].ToString() != "" ? sdr["BodyFooter"].ToString() : _supplierObj.BodyFooter);
                                        _supplierObj.SupplierName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _supplierObj.SupplierName);
                                        _supplierObj.POStatus = (sdr["Description"].ToString() != "" ? sdr["Description"].ToString() : _supplierObj.POStatus);
                                        _supplierObj.TaxAmount = (sdr["TaxAmount"].ToString() != "" ? decimal.Parse(sdr["TaxAmount"].ToString()) : _supplierObj.TaxAmount);
                                        _supplierObj.Discount = (sdr["Discount"].ToString() != "" ? decimal.Parse(sdr["Discount"].ToString()) : _supplierObj.Discount);
                                        _supplierObj.TaxPercApplied = (sdr["TaxPercApplied"].ToString() != "" ? decimal.Parse(sdr["TaxPercApplied"].ToString()) : _supplierObj.TaxPercApplied);
                                        _supplierObj.Amount = (sdr["Amount"].ToString() != "" ? decimal.Parse(sdr["Amount"].ToString()) : _supplierObj.Amount);
                                    }
                                    supplierList.Add(_supplierObj);
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

            return supplierList;
        }
        #endregion GetAllSupplierMobile

        public List<Suppliers> GetAllSuppliers()
        {
            List<Suppliers> suppliersList = null;
            Settings settings = new Settings();
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
                        cmd.CommandText = "[Accounts].[GetAllSuppliers]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                suppliersList = new List<Suppliers>();
                                while (sdr.Read())
                                {
                                    Suppliers _SupplierObj = new Suppliers();
                                    {
                                        _SupplierObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _SupplierObj.ID);
                                        _SupplierObj.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _SupplierObj.CompanyName);
                                        _SupplierObj.IsInternalComp = (sdr["IsInternalComp"].ToString() != "" ? bool.Parse(sdr["IsInternalComp"].ToString()) : _SupplierObj.IsInternalComp);
                                        _SupplierObj.ContactPerson = (sdr["ContactPerson"].ToString() != "" ? sdr["ContactPerson"].ToString() : _SupplierObj.ContactPerson);
                                        _SupplierObj.ContactEmail = (sdr["ContactEmail"].ToString() != "" ? sdr["ContactEmail"].ToString() : _SupplierObj.ContactEmail);
                                        _SupplierObj.ContactTitle = (sdr["ContactTitle"].ToString() != "" ? sdr["ContactTitle"].ToString() : _SupplierObj.ContactTitle);
                                        _SupplierObj.Product = (sdr["Product"].ToString() != "" ? sdr["Product"].ToString() : _SupplierObj.Product);
                                        _SupplierObj.Website = (sdr["Website"].ToString() != "" ? sdr["Website"].ToString() : _SupplierObj.Website);
                                        _SupplierObj.LandLine = (sdr["LandLine"].ToString() != "" ? sdr["LandLine"].ToString() : _SupplierObj.LandLine);
                                        _SupplierObj.Mobile = (sdr["Mobile"].ToString() != "" ? sdr["Mobile"].ToString() : _SupplierObj.Mobile);
                                        _SupplierObj.Fax = (sdr["Fax"].ToString() != "" ? sdr["Fax"].ToString() : _SupplierObj.Fax);
                                        _SupplierObj.OtherPhoneNos = (sdr["OtherPhoneNos"].ToString() != "" ? sdr["OtherPhoneNos"].ToString() : _SupplierObj.OtherPhoneNos);
                                        _SupplierObj.BillingAddress = (sdr["BillingAddress"].ToString() != "" ? sdr["BillingAddress"].ToString() : _SupplierObj.BillingAddress);
                                        _SupplierObj.ShippingAddress = (sdr["ShippingAddress"].ToString() != "" ? sdr["ShippingAddress"].ToString() : _SupplierObj.ShippingAddress);
                                        _SupplierObj.PaymentTermCode = (sdr["PaymentTermCode"].ToString() != "" ? sdr["PaymentTermCode"].ToString() : _SupplierObj.PaymentTermCode);
                                        _SupplierObj.TaxRegNo = (sdr["TaxRegNo"].ToString() != "" ? sdr["TaxRegNo"].ToString() : _SupplierObj.TaxRegNo);
                                        _SupplierObj.PANNO = (sdr["PANNo"].ToString() != "" ? sdr["PANNo"].ToString() : _SupplierObj.PANNO);
                                        _SupplierObj.OutStanding = (sdr["OutStanding"].ToString() != "" ? decimal.Parse(sdr["OutStanding"].ToString()) : _SupplierObj.OutStanding);
                                        _SupplierObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _SupplierObj.GeneralNotes);
                                    }
                                    suppliersList.Add(_SupplierObj);
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
            return suppliersList;
        }



        public List<SupplierOrder> GetAllSupplierPurchaseOrders()
        {
            List<SupplierOrder> SPOList = null;
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
                        cmd.CommandText = "[Office].[GetAllSupplierPurchaseOrders]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                SPOList = new List<SupplierOrder>();
                                while (sdr.Read())
                                {
                                    SupplierOrder _SuppObj = new SupplierOrder();
                                    {
                                        
                                        _SuppObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _SuppObj.ID);
                                        _SuppObj.PONo = (sdr["PONo"].ToString() != "" ? sdr["PONo"].ToString() : _SuppObj.PONo);
                                        _SuppObj.PODate = (sdr["PODate"].ToString() != "" ? DateTime.Parse(sdr["PODate"].ToString()).ToString(s.dateformat) : _SuppObj.PODate);
                                        _SuppObj.POStatus = (sdr["POStatus"].ToString() != "" ? sdr["POStatus"].ToString() : _SuppObj.POStatus);
                                        _SuppObj.POStatusDesc = (sdr["POStatusDesc"].ToString() != "" ? sdr["POStatusDesc"].ToString() : _SuppObj.POStatusDesc);
                                        _SuppObj.TotalAmount = (sdr["TotalAmount"].ToString() != "" ? decimal.Parse(sdr["TotalAmount"].ToString()) : _SuppObj.TotalAmount);
                                        _SuppObj.POFromCompCode = (sdr["POFromCompCode"].ToString() != "" ? sdr["POFromCompCode"].ToString() : _SuppObj.POFromCompCode);
                                        _SuppObj.IsFinalApproved= (sdr["FinalApproved"].ToString() != "" ? bool.Parse(sdr["FinalApproved"].ToString()) : _SuppObj.IsFinalApproved);
                                        _SuppObj.FinalApprovedDateString = (sdr["FinalApprovedDate"].ToString() != "" ? DateTime.Parse(sdr["FinalApprovedDate"].ToString()).ToString(s.dateformat) : _SuppObj.FinalApprovedDateString);
                                        // _SuppObj.POIssuedDate = (sdr["POIssuedDate"].ToString() != "" ? DateTime.Parse(sdr["POIssuedDate"].ToString()).ToString(s.dateformat) : _SuppObj.POIssuedDate);
                                        //_SuppObj.BodyHeader = (sdr["BodyHeader"].ToString() != "" ? sdr["BodyHeader"].ToString() : _SuppObj.BodyHeader);
                                        //_SuppObj.BodyFooter = (sdr["BodyFooter"].ToString() != "" ? sdr["BodyFooter"].ToString() : _SuppObj.BodyFooter);
                                        //_SuppObj.SupplierMailingAddress = (sdr["SupplierMailingAddress"].ToString() != "" ? sdr["SupplierMailingAddress"].ToString() : _SuppObj.SupplierMailingAddress);
                                        //_SuppObj.ShipToAddress = (sdr["ShipToAddress"].ToString() != "" ? sdr["ShipToAddress"].ToString() : _SuppObj.ShipToAddress);
                                        //_SuppObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _SuppObj.GeneralNotes);
                                        //_SuppObj.EmailSentYN = (sdr["EmailSentYN"].ToString() != "" ? sdr["EmailSentYN"].ToString() : _SuppObj.EmailSentYN);
                                        // _SuppObj.TaxAmount = (sdr["TaxAmount"].ToString() != "" ? decimal.Parse(sdr["TaxAmount"].ToString()) : _SuppObj.TaxAmount);
                                        // _SuppObj.Discount = (sdr["Discount"].ToString() != "" ? decimal.Parse(sdr["Discount"].ToString()) : _SuppObj.Discount);
                                        // _SuppObj.TaxPercApplied = (sdr["TaxPercApplied"].ToString() != "" ? decimal.Parse(sdr["TaxPercApplied"].ToString()) : _SuppObj.TaxPercApplied);
                                        // _SuppObj.GrossTotal = (sdr["GrossTotal"].ToString() != "" ? decimal.Parse(sdr["GrossTotal"].ToString()) : _SuppObj.GrossTotal);
                                        //_SuppObj.TaxTypeCode = (sdr["TaxTypeCode"].ToString() != "" ? sdr["TaxTypeCode"].ToString() : _SuppObj.TaxTypeCode);

                                        _SuppObj.SuppliersObj = new Suppliers();
                                        {
                                            _SuppObj.SuppliersObj.ID = (sdr["SupplierID"].ToString() != "" ? Guid.Parse(sdr["SupplierID"].ToString()) : _SuppObj.SuppliersObj.ID);
                                            _SuppObj.SuppliersObj.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _SuppObj.SuppliersObj.CompanyName);
                                          //  _SuppObj.SuppliersObj.BillingAddress = (sdr["CustBillingAddress"].ToString() != "" ? sdr["CustBillingAddress"].ToString() : _SuppObj.SuppliersObj.BillingAddress);
                                          //  _SuppObj.SuppliersObj.ShippingAddress = (sdr["CustShippingAddress"].ToString() != "" ? sdr["CustShippingAddress"].ToString() : _SuppObj.SuppliersObj.ShippingAddress);
                                        }
                                        _SuppObj.company = new Company();
                                        {
                                            _SuppObj.company.Name = (sdr["Company"].ToString() != "" ? sdr["Company"].ToString() : _SuppObj.company.Name);
                                            _SuppObj.company.Code = (sdr["POFromCompCode"].ToString() != "" ? sdr["POFromCompCode"].ToString() : _SuppObj.company.Code);
                                           // _SuppObj.company.BillingAddress = (sdr["POToBillingAddress"].ToString() != "" ? sdr["POToBillingAddress"].ToString() : _SuppObj.company.BillingAddress);
                                           // _SuppObj.company.ShippingAddress = (sdr["POToShippingAddress"].ToString() != "" ? sdr["POToShippingAddress"].ToString() : _SuppObj.company.ShippingAddress);
                                        }  
                                    }
                                    SPOList.Add(_SuppObj);
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

            return SPOList;

        }


        public List<SupplierOrder> GetAllSupplierPurchaseOrdersList(SupplierOrder SupplierObj)
        {
            List<SupplierOrder> SPOList = null;
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
                        cmd.CommandText = "[Office].[GetAllSupplierPOList]";
                        cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = SupplierObj.FromDate != "" ? SupplierObj.FromDate:null;
                        cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = SupplierObj.ToDate != "" ? SupplierObj.ToDate : null;
                        cmd.Parameters.Add("@Status", SqlDbType.NVarChar,50).Value = SupplierObj.Status != "" ? SupplierObj.Status : null;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = SupplierObj.ID;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                SPOList = new List<SupplierOrder>();
                                while (sdr.Read())
                                {
                                    SupplierOrder _SuppObj = new SupplierOrder();
                                    {

                                        _SuppObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _SuppObj.ID);
                                        _SuppObj.PONo = (sdr["PONo"].ToString() != "" ? sdr["PONo"].ToString() : _SuppObj.PONo);
                                        _SuppObj.PODate = (sdr["PODate"].ToString() != "" ? DateTime.Parse(sdr["PODate"].ToString()).ToString(s.dateformat) : _SuppObj.PODate);
                                        _SuppObj.POStatus = (sdr["POStatus"].ToString() != "" ? sdr["POStatus"].ToString() : _SuppObj.POStatus);
                                        _SuppObj.TotalAmount = (sdr["TotalAmount"].ToString() != "" ? decimal.Parse(sdr["TotalAmount"].ToString()) : _SuppObj.TotalAmount);
                                        _SuppObj.POFromCompCode = (sdr["POFromCompCode"].ToString() != "" ? sdr["POFromCompCode"].ToString() : _SuppObj.POFromCompCode);
                                        _SuppObj.IsFinalApproved = (sdr["FinalApproved"].ToString() != "" ? bool.Parse(sdr["FinalApproved"].ToString()) : _SuppObj.IsFinalApproved);
                                        _SuppObj.FinalApprovedDateString = (sdr["FinalApprovedDate"].ToString() != "" ? DateTime.Parse(sdr["FinalApprovedDate"].ToString()).ToString(s.dateformat) : _SuppObj.FinalApprovedDateString);

                                        _SuppObj.SuppliersObj = new Suppliers();
                                        {
                                            _SuppObj.SuppliersObj.ID = (sdr["SupplierID"].ToString() != "" ? Guid.Parse(sdr["SupplierID"].ToString()) : _SuppObj.SuppliersObj.ID);
                                            _SuppObj.SuppliersObj.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _SuppObj.SuppliersObj.CompanyName);
                                        }
                                        _SuppObj.company = new Company();
                                        {
                                            _SuppObj.company.Name = (sdr["Company"].ToString() != "" ? sdr["Company"].ToString() : _SuppObj.company.Name);
                                            _SuppObj.company.Code = (sdr["POFromCompCode"].ToString() != "" ? sdr["POFromCompCode"].ToString() : _SuppObj.company.Code);
                                        }
                                    }
                                    SPOList.Add(_SuppObj);
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

            return SPOList;

        }


        public List<SupplierOrder> GetAllPendingSupplierPurchaseOrders()
        {
            List<SupplierOrder> SPOList = null;
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
                        cmd.CommandText = "[Office].[GetAllPendingSupplierOrder]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                SPOList = new List<SupplierOrder>();
                                while (sdr.Read())
                                {
                                    SupplierOrder _SuppObj = new SupplierOrder();
                                    {

                                        _SuppObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _SuppObj.ID);
                                        _SuppObj.PONo = (sdr["PONo"].ToString() != "" ? sdr["PONo"].ToString() : _SuppObj.PONo);
                                        _SuppObj.PODate = (sdr["PODate"].ToString() != "" ? DateTime.Parse(sdr["PODate"].ToString()).ToString(s.dateformat) : _SuppObj.PODate);
                                        _SuppObj.POStatus = (sdr["POStatus"].ToString() != "" ? sdr["POStatus"].ToString() : _SuppObj.POStatus);
                                        _SuppObj.TotalAmount = (sdr["TotalAmount"].ToString() != "" ? decimal.Parse(sdr["TotalAmount"].ToString()) : _SuppObj.TotalAmount);
                                        _SuppObj.POFromCompCode = (sdr["POFromCompCode"].ToString() != "" ? sdr["POFromCompCode"].ToString() : _SuppObj.POFromCompCode);
                                        _SuppObj.IsFinalApproved = (sdr["FinalApproved"].ToString() != "" ? bool.Parse(sdr["FinalApproved"].ToString()) : _SuppObj.IsFinalApproved);
                                        _SuppObj.FinalApprovedDateString = (sdr["FinalApprovedDate"].ToString() != "" ? DateTime.Parse(sdr["FinalApprovedDate"].ToString()).ToString(s.dateformat) : _SuppObj.FinalApprovedDateString);

                                        _SuppObj.SuppliersObj = new Suppliers();
                                        {
                                            _SuppObj.SuppliersObj.ID = (sdr["SupplierID"].ToString() != "" ? Guid.Parse(sdr["SupplierID"].ToString()) : _SuppObj.SuppliersObj.ID);
                                            _SuppObj.SuppliersObj.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _SuppObj.SuppliersObj.CompanyName);
                                        }
                                        _SuppObj.company = new Company();
                                        {
                                            _SuppObj.company.Name = (sdr["Company"].ToString() != "" ? sdr["Company"].ToString() : _SuppObj.company.Name);
                                            _SuppObj.company.Code = (sdr["POFromCompCode"].ToString() != "" ? sdr["POFromCompCode"].ToString() : _SuppObj.company.Code);
                                        }
                                    }
                                    SPOList.Add(_SuppObj);
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

            return SPOList;

        }

        public SupplierOrder GetSupplierPurchaseOrderByID(Guid ID)
        {
            SupplierOrder _SuppObj = new SupplierOrder();
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
                        cmd.CommandText = "[Office].[GetSupplierPurchaseOrderByID]";
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = ID;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                while (sdr.Read())
                                {
                                    _SuppObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _SuppObj.ID);
                                    _SuppObj.SupplierID = (sdr["SupplierID"].ToString() != "" ? Guid.Parse(sdr["SupplierID"].ToString()) : _SuppObj.SupplierID);
                                    _SuppObj.SuppliersObj = new Suppliers();
                                    _SuppObj.SuppliersObj.CompanyName = (sdr["CompanyName"].ToString() != "" ? sdr["CompanyName"].ToString() : _SuppObj.SuppliersObj.CompanyName);
                                    _SuppObj.SuppliersObj.ContactEmail = (sdr["ContactEmail"].ToString() != "" ? sdr["ContactEmail"].ToString() : _SuppObj.SuppliersObj.ContactEmail);
                                    _SuppObj.company = new Company();
                                    _SuppObj.company.Name = (sdr["Company"].ToString() != "" ? sdr["Company"].ToString() : _SuppObj.company.Name);
                                    _SuppObj.PONo = (sdr["PONo"].ToString() != "" ? sdr["PONo"].ToString() : _SuppObj.PONo);
                                    _SuppObj.PODate = (sdr["PODate"].ToString() != "" ? DateTime.Parse(sdr["PODate"].ToString()).ToString(s.dateformat) : _SuppObj.PODate);
                                    _SuppObj.POIssuedDate = (sdr["POIssuedDate"].ToString() != "" ? DateTime.Parse(sdr["POIssuedDate"].ToString()).ToString(s.dateformat) : _SuppObj.POIssuedDate);
                                    _SuppObj.POStatus = (sdr["POStatus"].ToString() != "" ? sdr["POStatus"].ToString() : _SuppObj.POStatus);
                                    _SuppObj.BodyHeader = (sdr["BodyHeader"].ToString() != "" ? sdr["BodyHeader"].ToString() : _SuppObj.BodyHeader);
                                    _SuppObj.BodyFooter = (sdr["BodyFooter"].ToString() != "" ? sdr["BodyFooter"].ToString() : _SuppObj.BodyFooter);
                                    _SuppObj.SupplierMailingAddress = (sdr["SupplierMailingAddress"].ToString() != "" ? sdr["SupplierMailingAddress"].ToString() : _SuppObj.SupplierMailingAddress);
                                    _SuppObj.ShipToAddress = (sdr["ShipToAddress"].ToString() != "" ? sdr["ShipToAddress"].ToString() : _SuppObj.ShipToAddress);
                                    _SuppObj.POFromCompCode = (sdr["POFromCompCode"].ToString() != "" ? sdr["POFromCompCode"].ToString() : _SuppObj.POFromCompCode);
                                    _SuppObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _SuppObj.GeneralNotes);
                                    _SuppObj.EmailSentYN = (sdr["EmailSentYN"].ToString() != "" ? sdr["EmailSentYN"].ToString() : _SuppObj.EmailSentYN);
                                    _SuppObj.TotalAmount = (sdr["TotalAmount"].ToString() != "" ? decimal.Parse(sdr["TotalAmount"].ToString()) : _SuppObj.TotalAmount);


                                     _SuppObj.TaxAmount = (sdr["TaxAmount"].ToString() != "" ? decimal.Parse(sdr["TaxAmount"].ToString()) : _SuppObj.TaxAmount);
                                     _SuppObj.Discount = (sdr["Discount"].ToString() != "" ? decimal.Parse(sdr["Discount"].ToString()) : _SuppObj.Discount);
                                     _SuppObj.TaxPercApplied = (sdr["TaxPercApplied"].ToString() != "" ? decimal.Parse(sdr["TaxPercApplied"].ToString()) : _SuppObj.TaxPercApplied);
                                     _SuppObj.GrossTotal = (sdr["TotalAmount"].ToString() != "" ? decimal.Parse(sdr["TotalAmount"].ToString()) : _SuppObj.GrossTotal);
                                    _SuppObj.TaxTypeCode = (sdr["TaxTypeCode"].ToString() != "" ? sdr["TaxTypeCode"].ToString() : _SuppObj.TaxTypeCode);
                                    _SuppObj.IsFinalApproved= (sdr["FinalApproved"].ToString() != "" ? bool.Parse(sdr["FinalApproved"].ToString()) : _SuppObj.IsFinalApproved);
                                    _SuppObj.FinalApprovedDate= (sdr["FinalApprovedDate"].ToString() != "" ? DateTime.Parse(sdr["FinalApprovedDate"].ToString()) : _SuppObj.FinalApprovedDate);
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

            return _SuppObj;
        }

        public object InsertPurchaseOrder(SupplierOrder SPO)
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
                        cmd.CommandText = "[Office].[InsertSupplierPurchaseOrder]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PONo", SqlDbType.VarChar, 20).Value = SPO.PONo;
                        cmd.Parameters.Add("@PODate", SqlDbType.DateTime).Value = SPO.PODate;
                        cmd.Parameters.Add("@POIssuedDate", SqlDbType.DateTime).Value = SPO.POIssuedDate;
                        cmd.Parameters.Add("@SupplierID", SqlDbType.UniqueIdentifier).Value = SPO.SupplierID;
                        cmd.Parameters.Add("@POFromCompCode", SqlDbType.VarChar, 10).Value = SPO.POFromCompCode;
                        cmd.Parameters.Add("@SupplierMailingAddress", SqlDbType.NVarChar, -1).Value = SPO.SupplierMailingAddress;
                        cmd.Parameters.Add("@ShipToAddress", SqlDbType.NVarChar, -1).Value = SPO.ShipToAddress;
                        cmd.Parameters.Add("@BodyFooter", SqlDbType.VarChar, 500).Value = SPO.BodyFooter;
                        cmd.Parameters.Add("@BodyHeader", SqlDbType.NVarChar, -1).Value = SPO.BodyHeader;
                        cmd.Parameters.Add("@POStatus", SqlDbType.VarChar, 10).Value = SPO.POStatus;
                        cmd.Parameters.Add("@GrossAmount", SqlDbType.Decimal).Value = SPO.GrossTotal;
                        cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = SPO.Discount;
                        cmd.Parameters.Add("@TaxTypeCode", SqlDbType.VarChar, 10).Value = SPO.TaxTypeCode;
                        cmd.Parameters.Add("@TaxPercApplied", SqlDbType.Decimal).Value = SPO.TaxPercApplied;
                        cmd.Parameters.Add("@TaxAmount", SqlDbType.Decimal).Value = SPO.TaxAmount;
                        //cmd.Parameters.Add("@EmailSentYN", SqlDbType.Decimal).Value = SPO.EmailSentYN;
                        cmd.Parameters.Add("@GeneralNotes", SqlDbType.NVarChar, -1).Value = SPO.GeneralNotes;
                        cmd.Parameters.Add("@FileDupID", SqlDbType.UniqueIdentifier).Value = SPO.hdnFileID;

                        cmd.Parameters.Add("@reqDetailLinkObjXML", SqlDbType.NVarChar, -1).Value = SPO.reqDetailLinkObjXML;
                        cmd.Parameters.Add("@reqDetailObjXML", SqlDbType.NVarChar, -1).Value = SPO.reqDetailObjXML;

                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = SPO.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = SPO.commonObj.CreatedDate;
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

        public object UpdatePurchaseOrder(SupplierOrder SPO)
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
                        cmd.CommandText = "[Office].[UpdateSupplierPurchaseOrder]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = SPO.ID;
                        cmd.Parameters.Add("@PONo", SqlDbType.VarChar, 20).Value = SPO.PONo;
                        cmd.Parameters.Add("@PODate", SqlDbType.DateTime).Value = SPO.PODate;
                        cmd.Parameters.Add("@POIssuedDate", SqlDbType.DateTime).Value = SPO.POIssuedDate;
                        cmd.Parameters.Add("@SupplierID", SqlDbType.UniqueIdentifier).Value = SPO.SupplierID;
                        cmd.Parameters.Add("@POFromCompCode", SqlDbType.VarChar, 10).Value = SPO.POFromCompCode;
                        cmd.Parameters.Add("@SupplierMailingAddress", SqlDbType.NVarChar, -1).Value = SPO.SupplierMailingAddress;
                        cmd.Parameters.Add("@ShipToAddress", SqlDbType.NVarChar, -1).Value = SPO.ShipToAddress;
                        cmd.Parameters.Add("@BodyFooter", SqlDbType.VarChar, 500).Value = SPO.BodyFooter;
                        cmd.Parameters.Add("@BodyHeader", SqlDbType.NVarChar, -1).Value = SPO.BodyHeader;
                        cmd.Parameters.Add("@POStatus", SqlDbType.VarChar, 10).Value = SPO.POStatus;
                        cmd.Parameters.Add("@GrossAmount", SqlDbType.Decimal).Value = SPO.TotalAmount;
                        cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = SPO.Discount;
                        cmd.Parameters.Add("@TaxTypeCode", SqlDbType.VarChar, 10).Value = SPO.TaxTypeCode;
                        cmd.Parameters.Add("@TaxPercApplied", SqlDbType.Decimal).Value = SPO.TaxPercApplied;
                        cmd.Parameters.Add("@TaxAmount", SqlDbType.Decimal).Value = SPO.TaxAmount;
                        cmd.Parameters.Add("@GeneralNotes", SqlDbType.NVarChar, -1).Value = SPO.GeneralNotes;

                        cmd.Parameters.Add("@reqDetailLinkObjXML", SqlDbType.NVarChar, -1).Value = SPO.reqDetailLinkObjXML;
                        cmd.Parameters.Add("@reqDetailObjXML", SqlDbType.NVarChar, -1).Value = SPO.reqDetailObjXML;

                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = SPO.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = SPO.commonObj.UpdatedDate;
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

        public object UpdatePurchaseOrderDetailLink(SupplierOrder SPO)
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
                        cmd.CommandText = "[Office].[UpdatePurchaseOrderDetailLink]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@POID", SqlDbType.UniqueIdentifier).Value = SPO.ID;
                        cmd.Parameters.Add("@reqDetailLinkObjXML", SqlDbType.NVarChar, -1).Value = SPO.reqDetailLinkObjXML;
                        cmd.Parameters.Add("@reqDetailObjXML", SqlDbType.NVarChar, -1).Value = SPO.reqDetailObjXML;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = SPO.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = SPO.commonObj.UpdatedDate;
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
        public object DeletePurchaseOrder(Guid ID)
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
                        cmd.CommandText = "[Office].[DeleteSupplierOrder]";
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
        public object DeletePurchaseOrderDetail(Guid ID)
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
                        cmd.CommandText = "[Office].[DeleteSupplierOrderDetail]";
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

        public List<SupplierPODetail> GetPurchaseOrderDetailTable(Guid ID)
        {
            List<SupplierPODetail> SPODList = null;
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
                        cmd.CommandText = "[Office].[GetPurchaseOrderDetails]";
                        cmd.Parameters.Add("@SupplierPOID", SqlDbType.UniqueIdentifier).Value = ID;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                SPODList = new List<SupplierPODetail>();
                                while (sdr.Read())
                                {
                                    SupplierPODetail _SuppObj = new SupplierPODetail();
                                    {

                                        _SuppObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _SuppObj.ID);
                                        _SuppObj.MaterialID = (sdr["MaterialID"].ToString() != "" ? Guid.Parse(sdr["MaterialID"].ToString()) : _SuppObj.MaterialID);
                                        _SuppObj.MaterialDesc = (sdr["MaterialDesc"].ToString() != "" ? sdr["MaterialDesc"].ToString() : _SuppObj.MaterialDesc);
                                        _SuppObj.MaterialCode = (sdr["MaterialCode"].ToString() != "" ? sdr["MaterialCode"].ToString() : _SuppObj.MaterialCode);
                                        _SuppObj.UnitCode = (sdr["UnitCode"].ToString() != "" ? sdr["UnitCode"].ToString() : _SuppObj.UnitCode);
                                        _SuppObj.Qty = (sdr["Qty"].ToString() != "" ? decimal.Parse(sdr["Qty"].ToString()) : _SuppObj.Qty);
                                        _SuppObj.Rate = (sdr["Rate"].ToString() != "" ? decimal.Parse(sdr["Rate"].ToString()) : _SuppObj.Rate);
                                        _SuppObj.Amount = (sdr["Amount"].ToString() != "" ? decimal.Parse(sdr["Amount"].ToString()) : _SuppObj.Amount);
                                        _SuppObj.Particulars=(sdr["Particulars"].ToString() != "" ? sdr["Particulars"].ToString() : _SuppObj.Particulars);
                                    }
                                    SPODList.Add(_SuppObj);
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

            return SPODList;

        }

        public List<Requisition> GetAllRequisitionHeaderForSupplierPO()
        {
            List<Requisition> Req_List = null;
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
                        cmd.CommandText = "[Office].[GetAllRequisitionHeaderForSupplierPO]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                Req_List = new List<Requisition>();
                                while (sdr.Read())
                                {
                                    Requisition _ReqObj = new Requisition();
                                    {

                                        _ReqObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _ReqObj.ID);
                                        _ReqObj.Title = (sdr["Title"].ToString() != "" ? sdr["Title"].ToString() : _ReqObj.Title);
                                        _ReqObj.ReqNo = (sdr["ReqNo"].ToString() != "" ? sdr["ReqNo"].ToString() : _ReqObj.ReqNo);
                                        _ReqObj.ReqStatus = (sdr["ReqStatus"].ToString() != "" ? sdr["ReqStatus"].ToString() : _ReqObj.ReqStatus);
                                        _ReqObj.ReqDateFormatted = (sdr["ReqDate"].ToString() != "" ? DateTime.Parse(sdr["ReqDate"].ToString()).ToString(s.dateformat) : _ReqObj.ReqDateFormatted);
                                        _ReqObj.FinalApprovalDateFormatted = (sdr["FinalApprovalDate"].ToString() != "" ? DateTime.Parse(sdr["FinalApprovalDate"].ToString()).ToString(s.dateformat) : _ReqObj.FinalApprovalDateFormatted);
                                        _ReqObj.CommonObj = new Common();
                                        _ReqObj.CommonObj.CreatedBy = (sdr["CreatedBy"].ToString() != "" ? sdr["CreatedBy"].ToString() : _ReqObj.CommonObj.CreatedBy);

                                    }
                                    Req_List.Add(_ReqObj);
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

            return Req_List;

        }

        public List<RequisitionDetail> GetRequisitionDetailsByIDs(string IDs, string SPOID)
        {
            List<RequisitionDetail> Req_List = null;
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
                        cmd.CommandText = "[Office].[GetRequisitionDetailsByIDs]";
                        cmd.Parameters.Add("@IDs", SqlDbType.NVarChar,-1).Value = @IDs;
                        if(SPOID!="")
                        cmd.Parameters.Add("@POID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(SPOID);
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                Req_List = new List<RequisitionDetail>();
                                while (sdr.Read())
                                {
                                    RequisitionDetail _ReqObj = new RequisitionDetail();
                                    {
                                        _ReqObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _ReqObj.ID);
                                        _ReqObj.ReqID = (sdr["ReqID"].ToString() != "" ? Guid.Parse(sdr["ReqID"].ToString()) : _ReqObj.ReqID);
                                        _ReqObj.MaterialID = (sdr["MaterialID"].ToString() != "" ? Guid.Parse(sdr["MaterialID"].ToString()) : _ReqObj.MaterialID);
                                        _ReqObj.ReqNo = (sdr["ReqNo"].ToString() != "" ? sdr["ReqNo"].ToString() : _ReqObj.Description);
                                        _ReqObj.RawMaterialObj = new RawMaterial();
                                        _ReqObj.RawMaterialObj.MaterialCode = (sdr["MaterialCode"].ToString() != "" ? sdr["MaterialCode"].ToString() : _ReqObj.RawMaterialObj.MaterialCode);
                                        _ReqObj.Description = (sdr["Description"].ToString() != "" ? sdr["Description"].ToString() : _ReqObj.Description);
                                        _ReqObj.ExtendedDescription = (sdr["ExtendedDescription"].ToString() != "" ? sdr["ExtendedDescription"].ToString() : _ReqObj.ExtendedDescription);
                                        _ReqObj.CurrStock = (sdr["CurrStock"].ToString() != "" ? sdr["CurrStock"].ToString() : _ReqObj.CurrStock);
                                        _ReqObj.RequestedQty = (sdr["RequestedQty"].ToString() != "" ? sdr["RequestedQty"].ToString() : _ReqObj.RequestedQty);
                                        _ReqObj.OrderedQty = (sdr["OrderedQty"].ToString() != "" ? sdr["OrderedQty"].ToString() : _ReqObj.OrderedQty);
                                        _ReqObj.POQty = (decimal.Parse(_ReqObj.RequestedQty) - decimal.Parse(_ReqObj.OrderedQty)).ToString();
                                        _ReqObj.UnitCode = (sdr["UnitCode"].ToString() != "" ? sdr["UnitCode"].ToString() : _ReqObj.UnitCode);
                                        _ReqObj.AppxRate = (sdr["AppxRate"].ToString() != "" ? decimal.Parse(sdr["AppxRate"].ToString()) : _ReqObj.AppxRate);
                                    }
                                    Req_List.Add(_ReqObj);
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

            return Req_List;

        }

        public List<RequisitionDetail> EditPurchaseOrderDetail(string ID)
        {
            List<RequisitionDetail> Req_List = null;
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
                        cmd.CommandText = "[Office].[GetPurchaseOrderDetailByID]";
                        cmd.Parameters.Add("@ID", SqlDbType.NVarChar, -1).Value = @ID;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                Req_List = new List<RequisitionDetail>();
                                while (sdr.Read())
                                {
                                    RequisitionDetail _ReqObj = new RequisitionDetail();
                                    {
                                        _ReqObj.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _ReqObj.ID);
                                        _ReqObj.LinkID = (sdr["LinkID"].ToString() != "" ? Guid.Parse(sdr["LinkID"].ToString()) : _ReqObj.ID);
                                        _ReqObj.ReqID = (sdr["ReqID"].ToString() != "" ? Guid.Parse(sdr["ReqID"].ToString()) : _ReqObj.ReqID);
                                        _ReqObj.MaterialID = (sdr["MaterialID"].ToString() != "" ? Guid.Parse(sdr["MaterialID"].ToString()) : _ReqObj.MaterialID);
                                        _ReqObj.ReqNo = (sdr["ReqNo"].ToString() != "" ? sdr["ReqNo"].ToString() : _ReqObj.Description);
                                        _ReqObj.RawMaterialObj = new RawMaterial();
                                        _ReqObj.RawMaterialObj.MaterialCode = (sdr["MaterialCode"].ToString() != "" ? sdr["MaterialCode"].ToString() : _ReqObj.RawMaterialObj.MaterialCode);
                                        _ReqObj.ExtendedDescription = (sdr["MaterialDesc"].ToString() != "" ? sdr["MaterialDesc"].ToString() : _ReqObj.ExtendedDescription);
                                        _ReqObj.CurrStock = (sdr["CurrStock"].ToString() != "" ? sdr["CurrStock"].ToString() : _ReqObj.CurrStock);
                                        _ReqObj.RequestedQty = (sdr["RequestedQty"].ToString() != "" ? sdr["RequestedQty"].ToString() : _ReqObj.RequestedQty);
                                        _ReqObj.OrderedQty = (sdr["OrderedQty"].ToString() != "" ? sdr["OrderedQty"].ToString() : _ReqObj.OrderedQty);
                                        _ReqObj.POQty = (sdr["Qty"].ToString() != "" ? sdr["Qty"].ToString() : _ReqObj.POQty);
                                        _ReqObj.UnitCode = (sdr["UnitCode"].ToString() != "" ? sdr["UnitCode"].ToString() : _ReqObj.UnitCode);
                                        _ReqObj.AppxRate = (sdr["Rate"].ToString() != "" ? decimal.Parse(sdr["Rate"].ToString()) : _ReqObj.AppxRate);
                                    }
                                    Req_List.Add(_ReqObj);
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

            return Req_List;
        }

        public object ApproveSupplierOrder(Guid ID, DateTime FinalApprovedDate)
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
                        cmd.CommandText = "[Office].[ApproveSupplierOrder]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = ID;
                        cmd.Parameters.Add("@FinalApprovedDate", SqlDbType.DateTime).Value = FinalApprovedDate;
                        outputStatus = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outputStatus.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                    }
                }
                AppConst Cobj = new AppConst();
                object obj = new object();
                switch (outputStatus.Value.ToString())
                {
                    case "0":
                        throw new Exception(Cobj.UpdateFailure);
                    case "1":
                        return new
                        {
                            Status = outputStatus.Value.ToString(),
                            Message = Cobj.ApproveSuccess
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
                Status = outputStatus.Value.ToString()
            };
        }

        public object UpdateSupplierOrderMailStatus(SupplierOrder SPO)
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
                        cmd.CommandText = "[Office].[UpdateSupplierPOMailStatus]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = SPO.ID;
                        //cmd.Parameters.Add("@SentToEmails", SqlDbType.NVarChar, -1).Value = SPO.SentToEmails;
                        cmd.Parameters.Add("@EmailSentYN", SqlDbType.Bit).Value = SPO.EmailSentYN;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = SPO.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = SPO.commonObj.UpdatedDate;
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
            };
        }

        public object UpdateNotificationToCEO(SupplierOrder supObj)
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
                        cmd.CommandText = "[Office].[UpdateNotificationStatusToCEO]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = supObj.ID;
                        cmd.Parameters.Add("@IsNotificationSuccess", SqlDbType.Int).Value = 1;
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
            };
        }
    }
}