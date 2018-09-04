using SPOffice.BusinessService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;

namespace SPOffice.BusinessService.Services
{
    public class ProductBusiness : IProductBusiness
    {
        IProductRepository _productRepository;
        public ProductBusiness(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public object DeleteProduct(Guid ID)
        {
            return _productRepository.DeleteProduct(ID);
        }

        

        public List<Product> GetAllProducts(string Category)
        {
            List<Product> ProductList = null;
            try
            {
                ProductList = _productRepository.GetAllProducts(Category);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ProductList;
        }

        public List<Unit> GetAllUnits()
        {
            List<Unit> unitList = null;
            try
            {
                unitList = _productRepository.GetAllUnits();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return unitList;
        }
        public List<Product> GetAllCategory()
        {
            List<Product> categoryList = null;
            try
            {
               categoryList = _productRepository.GetAllCategory();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return categoryList;
        }
        public Product GetProductDetails(Guid? ID)
        {
            List<Product> ProductList = null;
            Product product = null;
            try
            {
                ProductList = GetAllProducts(null);
                product = ProductList != null ? ProductList.Where(D => D.ID == ID).SingleOrDefault() : null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return product;
        }

        

        public object InsertProduct(Product product)
        {

         return _productRepository.InsertProduct(product);
        }

        public object InsertProductDetails(Product product)
        {

            return _productRepository.InsertProductDetails(product);
        }


        public object UpdateProduct(Product product)
        {
           return  _productRepository.UpdateProduct(product);
        }

        public object UpdateProductByCode(Product product)
        {
            return _productRepository.UpdateProductByCode(product);
        }
        
    }
}