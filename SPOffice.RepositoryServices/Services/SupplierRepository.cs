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

    }
}