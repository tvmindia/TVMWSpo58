using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.RepositoryServices.Contracts
{
    public interface ISupplierRepository
    {
        List<Supplier> GetAllSupplierPOForMobile(string duration);

        List<Suppliers> GetAllSuppliers();

        List<SupplierOrder> GetAllSupplierPurchaseOrders();
        SupplierOrder GetSupplierPurchaseOrderByID(Guid ID);
        object InsertPurchaseOrder(SupplierOrder SPO);
        object UpdatePurchaseOrder(SupplierOrder SPO);
        object DeletePurchaseOrder(Guid ID);

    }
}
