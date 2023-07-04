using StockTracking.DAL;
using StockTracking.DAL.DAO;
using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StockTracking.BLL
{
    public class ProductBLL : IBLL<ProductDetailDTO, ProductDTO>
    {
        CategoryDAO categorydao = new CategoryDAO();
        ProductDAO dao = new ProductDAO();
        SalesDAO Salesdao = new SalesDAO();
        public bool Delete(ProductDetailDTO entity)
        {
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductID;
            dao.Delete(product);
            SALE sales = new SALE();
            sales.ProductID = entity.ProductID;
            Salesdao.Delete(sales);
            return true;
        }

        public bool GetBack(ProductDetailDTO entity)
        {
            return dao.GetBack(entity.ProductID);
        }
        
        public bool Insert(ProductDetailDTO entity)
        {
            PRODUCT product = new PRODUCT();
            product.ProductName = entity.ProductName;
            product.CategoryID = entity.CategoryID;
            product.Price = entity.Price;
            return dao.Insert(product);

        }

        public ProductDTO select()
        {
            ProductDTO dto = new ProductDTO();
            dto.Categories = categorydao.select();
            dto.Products = dao.select();
            return dto;
        }

        public bool Update(ProductDetailDTO entity)
        {
            PRODUCT product = new PRODUCT();
            product.ProductName = entity.ProductName;
            product.CategoryID = entity.CategoryID;
            product.Price = entity.Price;
            product.ID = entity.ProductID;
            product.StockAmount = entity.StockAmount;
            return dao.Update(product);
        }
    }
}
