using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.BusinessService.Contracts
{
   public interface IProductBusiness
    {
        List<Unit> GetAllUnits();
        List<Product> GetAllProducts();
        Product GetProductDetails(Guid? ID);
        object InsertProduct(Product product);
        object UpdateProduct(Product product);
        object DeleteProduct(Guid ID);
    }
}
