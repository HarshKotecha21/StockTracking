using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracking.DAL;
using StockTracking.DAL.DAO;

namespace StockTracking.BLL
{
    public class SalesBLL : IBLL<SalesDetailDTO, SalesDTO>
    {
        ProductDAO productdao = new ProductDAO();
        CustomerDAO customerdao = new CustomerDAO();
        CategoryDAO categorydao = new CategoryDAO();
        SalesDAO salesdao = new SalesDAO();
        public bool Delete(SalesDetailDTO entity)
        {
            SALE sale = new SALE();
            sale.ID = entity.SalesID;
            salesdao.Delete(sale);
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductID;  
            product.StockAmount = entity.StockAmount + entity.SalesAmount;
            productdao.Update(product);
            return true;
        }

        public bool GetBack(SalesDetailDTO entity)
        {
            salesdao.GetBack(entity.SalesID);
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductID;
            int temp = entity.StockAmount - entity.SalesAmount;
            product.StockAmount = temp;
            productdao.Update(product);
            return true;
        }

        public bool Insert(SalesDetailDTO entity)
        {
            SALE sales = new SALE();    
            sales.CategoryID = entity.CategoryID;
            sales.ProductID = entity.ProductID;
            sales.CustomerID = entity.CustomerID;
            sales.ProductSalesPrice = entity.Price;
            sales.ProductSalesAmount = entity.SalesAmount;
            sales.SalesDate = entity.SalesDate;
            salesdao.Insert(sales);
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductID;
            int temp = entity.StockAmount - entity.SalesAmount;
            product.StockAmount = temp;
            productdao.Update(product);
            return true;    

        }

        public SalesDTO select()
        {
            SalesDTO dto = new SalesDTO();
            dto.Products = productdao.select();
            dto.Categories = categorydao.select();
            dto.Customers = customerdao.select();
            dto.Sales = salesdao.select();
            return dto;
        }

        public SalesDTO select(bool isDeleted)
        {
            SalesDTO dto = new SalesDTO();
            dto.Products = productdao.select(isDeleted);
            dto.Categories = categorydao.select(isDeleted);
            dto.Customers = customerdao.select(isDeleted);
            dto.Sales = salesdao.select(isDeleted);
            return dto;
        }

        public bool Update(SalesDetailDTO entity)
        {
            SALE sales = new SALE();
            sales.ID = entity.SalesID;
            sales.ProductSalesAmount = entity.SalesAmount;
            salesdao.Update(sales);
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductID;
            product.StockAmount = entity.StockAmount;
            productdao.Update(product);
            return true;
        }
    }
}
