using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            DetailsXMl(SPO);
            return _supplierRepository.InsertPurchaseOrder(SPO);

        }

        public object UpdatePurchaseOrder(SupplierOrder SPO)
        {
            DetailsXMl(SPO);
            return _supplierRepository.UpdatePurchaseOrder(SPO);

        }

        public void DetailsXMl(SupplierOrder SPO)
        {
            string result = "<Details>";
            int totalRows = 0;
            foreach (object some_object in SPO.reqDetailObj)
            {
                XML(some_object, ref result, ref totalRows);
            }
            result = result + "</Details>";

            SPO.reqDetailObjXML = result;

            //reqDetailLink
            result = "<Details>";
            totalRows = 0;
            foreach (object some_object in SPO.reqDetailLinkObj)
            {
                XML(some_object, ref result, ref totalRows);
            }
            result = result + "</Details>";

            SPO.reqDetailLinkObjXML = result;

        }

        private void XML(object some_object, ref string result, ref int totalRows)
        {
            var properties = GetProperties(some_object);
            result = result + "<item ";
            foreach (var p in properties)
            {
                string name = p.Name;
                var value = p.GetValue(some_object, null);
                result = result + " " + name + @"=""" + value + @""" ";
            }
            result = result + "></item>";
            totalRows = totalRows + 1;
        }

        private static PropertyInfo[] GetProperties(object obj)
        {
            return obj.GetType().GetProperties();
        }

        public object DeletePurchaseOrder(Guid ID)
        {
            return _supplierRepository.DeletePurchaseOrder(ID);

        }
        
        //------------------------------------------------------------------------//

        public List<SupplierPODetail> GetPurchaseOrderDetailTable(Guid ID)
        {
            return _supplierRepository.GetPurchaseOrderDetailTable(ID);
        }

        public List<Requisition> GetAllRequisitionHeaderForSupplierPO()
        {
            return _supplierRepository.GetAllRequisitionHeaderForSupplierPO();
        }

        public List<RequisitionDetail> GetRequisitionDetailsByIDs(string IDs)
        {
            return _supplierRepository.GetRequisitionDetailsByIDs(IDs);
        }
    }
}