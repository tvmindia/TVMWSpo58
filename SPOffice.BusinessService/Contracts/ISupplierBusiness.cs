using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.BusinessService.Contracts
{
    public interface ISupplierBusiness
    {
        List<SupplierOrder> GetAllSupplierPOForMobile(string duration);

        List<Suppliers> GetAllSuppliers();
        Suppliers GetSupplierDetailsByID(Guid ID);

        List<SupplierOrder> GetAllSupplierPurchaseOrders();
        List<SupplierOrder> GetAllSupplierPurchaseOrdersList(SupplierOrder SupplierObj);
        List<SupplierOrder> GetAllPendingSupplierPurchaseOrders();
        SupplierOrder GetSupplierPurchaseOrderByID(Guid ID);
        object InsertPurchaseOrder(SupplierOrder SPO);
        object UpdatePurchaseOrder(SupplierOrder SPO);
        object UpdatePurchaseOrderDetailLink(SupplierOrder SPO);
        object DeletePurchaseOrder(Guid ID);
        object DeletePurchaseOrderDetail(Guid ID);
        object ApproveSupplierOrder(Guid ID, DateTime FinalApprovedDate);
        //detail
        List<SupplierPODetail> GetPurchaseOrderDetailTable(Guid ID);
        List<Requisition> GetAllRequisitionHeaderForSupplierPO();
        List<RequisitionDetail> GetRequisitionDetailsByIDs(string IDs, string SPOID);
        List<RequisitionDetail> EditPurchaseOrderDetail(string ID);

        SupplierOrder GetMailPreview(Guid ID);
        Task<bool> QuoteEmailPush(SupplierOrder SOObj);
        object UpdateSupplierOrderMailStatus(SupplierOrder SPO);
        void SendToFCMToCEO(string titleString, string descriptionString, Boolean isCommonForCEO, string click_action = "");
        object UpdateNotificationToCEO(SupplierOrder supObj);
    }
}
