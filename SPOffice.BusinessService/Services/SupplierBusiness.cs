using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class SupplierBusiness : ISupplierBusiness
    {
        private ISupplierRepository _supplierRepository;
        IMailBusiness _mailBusiness;

        public SupplierBusiness(ISupplierRepository supplierRepository, IMailBusiness mailBusiness)
        {
            _supplierRepository = supplierRepository;
            _mailBusiness = mailBusiness;
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
            SupplierOrder SPO = null;
            try
            {
                SPO = _supplierRepository.GetSupplierPurchaseOrderByID(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SPO;
        }
        public object ApproveSupplierOrder(Guid ID, DateTime FinalApprovedDate)
        {
            return _supplierRepository.ApproveSupplierOrder(ID, FinalApprovedDate);
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
        public object UpdatePurchaseOrderDetailLink(SupplierOrder SPO)
        {
            DetailsXMl(SPO);
            return _supplierRepository.UpdatePurchaseOrderDetailLink(SPO);

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
        public object DeletePurchaseOrderDetail(Guid ID)
        {
            return _supplierRepository.DeletePurchaseOrderDetail(ID);

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

        public List<RequisitionDetail> GetRequisitionDetailsByIDs(string IDs, string SPOID)
        {
            return _supplierRepository.GetRequisitionDetailsByIDs(IDs,SPOID);
        }
        public List<RequisitionDetail> EditPurchaseOrderDetail(string ID)
        {
            return _supplierRepository.EditPurchaseOrderDetail(ID);
        }

        public SupplierOrder GetMailPreview(Guid ID)
        {
            SupplierOrder SOObj = null;
            try
            {
                SOObj = GetSupplierPurchaseOrderByID(ID);
                if (SOObj != null)
                {
                    if ((SOObj.ID != Guid.Empty) && (SOObj.ID != null))
                    {
                        SOObj.SPODObj = new SupplierPODetail();
                        SOObj.SPODObj.SupplierPODetailList = GetPurchaseOrderDetailTable(ID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SOObj;
        }

        public async Task<bool> QuoteEmailPush(SupplierOrder SOObj)
        {
             
            bool sendsuccess = false;
            SupplierOrder pH = null;
            try
            {
                pH = GetSupplierPurchaseOrderByID((Guid)SOObj.ID);

                if (!string.IsNullOrEmpty(SOObj.mailPreviewVMObj.SentToEmails))
                {
                    string[] EmailList = SOObj.mailPreviewVMObj.SentToEmails.Split(',');
                    foreach (string email in EmailList)
                    {
                        Mail _mail = new Mail();
                        _mail.Body = SOObj.mailPreviewVMObj.MailBody;
                        _mail.Subject = "Supplier Purchase Order";
                        _mail.To = email;
                        sendsuccess = await _mailBusiness.MailSendAsync(_mail);
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sendsuccess;
        }

        public object UpdateSupplierOrderMailStatus(SupplierOrder SPO)
        {
            Object result = null;
            try
            {

                result = _supplierRepository.UpdateSupplierOrderMailStatus(SPO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}