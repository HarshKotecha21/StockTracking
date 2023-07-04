using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockTracking.DAL.DAO
{
    public class ProductDAO : StockContext,IDAO<PRODUCT, ProductDetailDTO>
    {
        public bool Delete(PRODUCT entity)
        {
            try
            {
                if(entity.ID != 0)
                {
                    PRODUCT product = db.PRODUCTs.First(x => x.ID == entity.ID);
                    product.isDeleted = true;
                    product.DeletedDate = DateTime.Today;
                    db.SaveChanges();
                }
                if(entity.CategoryID != 0)
                {
                    List<PRODUCT> list = db.PRODUCTs.Where(x => x.CategoryID == entity.CategoryID).ToList();
                    foreach(var item in list) 
                    {
                        item.isDeleted = true;
                        item.DeletedDate = DateTime.Today;
                        List<SALE> sales = db.SALES.Where(x=>x.ProductID == item.ID).ToList();
                        foreach (var item1 in sales)
                        {
                            item1.isDeleted = true;
                            item1.DeletedDate = DateTime.Today;
                        }
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool GetBack(int ID)
        {
            try
            {
                PRODUCT product = db.PRODUCTs.First(x => x.ID == ID);
                product.isDeleted = false;
                product.DeletedDate = null;
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Insert(PRODUCT entity)
        {
            try
            {
                db.PRODUCTs.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ProductDetailDTO> select()
        {
            try
            {
                List<ProductDetailDTO> product = new List<ProductDetailDTO>();
                var list = (from p in db.PRODUCTs.Where(x=>x.isDeleted == false)
                            join c in db.CATEGORies on p.CategoryID equals c.ID
                            select new
                            {
                                productname = p.ProductName,
                                categoryname = c.CategoryName,
                                stockamount = p.StockAmount,
                                price = p.Price,
                                categoryID = c.ID,
                                productID = p.ID

                            }).OrderBy(x=>x.productname).ToList();
                foreach (var item in list)
                {
                    ProductDetailDTO dto = new ProductDetailDTO();
                    dto.ProductName = item.productname;
                    dto.CategoryName = item.categoryname;
                    dto.StockAmount = item.stockamount;
                    dto.Price = item.price;
                    dto.CategoryID = item.categoryID;
                    dto.ProductID = item.productID;
                    product.Add(dto);
                }
                return product;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ProductDetailDTO> select(bool isDeleted)
        {
            try
            {
                List<ProductDetailDTO> product = new List<ProductDetailDTO>();
                var list = (from p in db.PRODUCTs.Where(x => x.isDeleted == isDeleted)
                            join c in db.CATEGORies on p.CategoryID equals c.ID
                            select new
                            {
                                productname = p.ProductName,
                                categoryname = c.CategoryName,
                                stockamount = p.StockAmount,
                                price = p.Price,
                                categoryID = c.ID,
                                productID = p.ID,
                                categoryisDeleted=c.isDeleted

                            }).OrderBy(x => x.productname).ToList();
                foreach (var item in list)
                {
                    ProductDetailDTO dto = new ProductDetailDTO();
                    dto.ProductName = item.productname;
                    dto.CategoryName = item.categoryname;
                    dto.StockAmount = item.stockamount;
                    dto.Price = item.price;
                    dto.CategoryID = item.categoryID;
                    dto.ProductID = item.productID;
                    dto.isCategoryDeleted = item.categoryisDeleted;
                    product.Add(dto);
                }
                return product;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool Update(PRODUCT entity)
        {
            try
            {
                PRODUCT product = db.PRODUCTs.First(x=>x.ID == entity.ID);
                if(entity.CategoryID == 0)
                {
                    product.StockAmount = entity.StockAmount;
                }
                else
                {
                    product.ProductName = entity.ProductName;
                    product.CategoryID = entity.CategoryID;
                    product.Price = entity.Price;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
