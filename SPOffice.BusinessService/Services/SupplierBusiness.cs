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
    }
}