using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.RepositoryServices.Contracts
{
    public interface IProductRepository
    {
        List<Unit> GetAllUnits();
        List<Product> GetAllProducts();
        object InsertProduct(Product product);
        object InsertProductDetails(Product product);
        object UpdateProduct(Product product);
        object UpdateProductByCode(Product product);
        object DeleteProduct(Guid ID);
    }
}
