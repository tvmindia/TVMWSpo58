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
        public List<Supplier> GetAllSuppliersForMobile()
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
                                        _supplierObj.SupplierID = (sdr["SupplierID"].ToString() != "" ? Guid.Parse(sdr["SupplierID"].ToString()) : _supplierObj.SupplierID);
                                        _supplierObj.PONo = (sdr["PONo"].ToString() != "" ? sdr["PONo"].ToString() : _supplierObj.PONo);
                                        _supplierObj.POIssuedDate = (sdr["POIssuedDate"].ToString() != "" ? DateTime.Parse(sdr["POIssuedDate"].ToString()) : _supplierObj.POIssuedDate);
                                        _supplierObj.PODate = (sdr["PODate"].ToString() != "" ? DateTime.Parse(sdr["PODate"].ToString()) : _supplierObj.PODate);
                                        _supplierObj.POFromCompCode = (sdr["POFromCompCode"].ToString() != "" ? sdr["POFromCompCode"].ToString() : _supplierObj.POFromCompCode);
                                        _supplierObj.SupplierMailingAddress = (sdr["SupplierMailingAddress"].ToString() != "" ? sdr["SupplierMailingAddress"].ToString() : _supplierObj.SupplierMailingAddress);
                                        _supplierObj.ShipToAddress = (sdr["ShipToAddress"].ToString() != "" ? sdr["ShipToAddress"].ToString() : _supplierObj.ShipToAddress);
                                        _supplierObj.GeneralNotes = (sdr["GeneralNotes"].ToString() != "" ? sdr["GeneralNotes"].ToString() : _supplierObj.GeneralNotes);
                                        _supplierObj.BodyHeader = (sdr["BodyHeader"].ToString() != "" ? sdr["BodyHeader"].ToString() : _supplierObj.BodyHeader);
                                        _supplierObj.BodyFooter = (sdr["BodyFooter"].ToString() != "" ? sdr["BodyFooter"].ToString() : _supplierObj.BodyFooter);
                                        _supplierObj.EmailSentYN = (sdr["EmailSentYN"].ToString() != "" ? sdr["EmailSentYN"].ToString() : _supplierObj.EmailSentYN);
                                        
                                        _supplierObj.TaxAmount = (sdr["TaxAmount"].ToString() != "" ? decimal.Parse(sdr["TaxAmount"].ToString()) : _supplierObj.TaxAmount);
                                        _supplierObj.Discount = (sdr["Discount"].ToString() != "" ? decimal.Parse(sdr["Discount"].ToString()) : _supplierObj.Discount);
                                        _supplierObj.TaxPercApplied = (sdr["TaxPercApplied"].ToString() != "" ? decimal.Parse(sdr["TaxPercApplied"].ToString()) : _supplierObj.TaxPercApplied);
                                        _supplierObj.GrantTotal = (sdr["GrantTotal"].ToString() != "" ? decimal.Parse(sdr["GrantTotal"].ToString()) : _supplierObj.GrantTotal);
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
        #endregion GetAllCustomerMobile
    }
}