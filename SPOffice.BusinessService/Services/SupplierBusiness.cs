using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class SupplierBusiness : ISupplierBusiness
    {
        private ISupplierRepository _supplierRepository;

        public SupplierBusiness(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

      

        public List<Supplier> GetAllSupplierPOForMobile(string duration)
        {
            return _supplierRepository.GetAllSupplierPOForMobile(duration);
        }

        public List<Suppliers> GetAllSuppliers()
        {
            try
            {
                return _supplierRepository.GetAllSuppliers();
            }
            catch (Exception)
            {

                throw;
            }
        }


        //------------------------------------------------------------------------//
        public List<SupplierOrder> GetAllSupplierPurchaseOrders()
        {
            List<SupplierOrder> SPOList = null;
            try
            {
                SPOList = _supplierRepository.GetAllSupplierPurchaseOrders();
              //  SPOList = SPOList != null ? SPOList.Select(Q => { Q.NetTaxableAmount = Q.GrossAmount - Q.Discount; Q.TotalAmount = Q.NetTaxableAmount + Q.TaxAmount; return Q; }).ToList() : new List<SupplierOrder>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SPOList;
        }

        public SupplierOrder GetSupplierPurchaseOrderByID(Guid ID)
        {
            SupplierOrder SPOList = null;
            try
            {
                SPOList = _supplierRepository.GetSupplierPurchaseOrderByID(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SPOList;
        }

        public object InsertPurchaseOrder(SupplierOrder SPO)
        {
            return _supplierRepository.InsertPurchaseOrder(SPO);

        }

        public object UpdatePurchaseOrder(SupplierOrder SPO)
        {
            return _supplierRepository.UpdatePurchaseOrder(SPO);

        }

        public object DeletePurchaseOrder(Guid ID)
        {
            throw new NotImplementedException();
        }
        //------------------------------------------------------------------------//

    }
}