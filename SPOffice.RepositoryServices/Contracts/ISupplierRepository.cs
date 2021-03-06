﻿using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.RepositoryServices.Contracts
{
    public interface ISupplierRepository
    {
        List<SupplierOrder> GetAllSupplierPOForMobile(string duration);

        List<Suppliers> GetAllSuppliers();

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
        List<SupplierPODetail> GetPurchaseOrderDetailTable(Guid ID);
        List<Requisition> GetAllRequisitionHeaderForSupplierPO();
        List<RequisitionDetail> GetRequisitionDetailsByIDs(string IDs, string SPOID);
        List<RequisitionDetail> EditPurchaseOrderDetail(string ID);
        object UpdateSupplierOrderMailStatus(SupplierOrder SPO);
        object UpdateNotificationToCEO(SupplierOrder supObj);

    }
}
