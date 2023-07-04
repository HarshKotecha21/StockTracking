using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.DAL.DAO
{
    public class SalesDAO : StockContext ,IDAO<SALE, SalesDetailDTO>

    {
        public bool Delete(SALE entity)
        {
            try
            {
                if(entity.ID != 0)
                {
                    SALE sales = db.SALES.First(x => x.ID == entity.ID);
                    sales.isDeleted = true;
                    sales.DeletedDate = DateTime.Today;
                    db.SaveChanges();
                }
                else if(entity.ProductID!= 0)
                {
                    List<SALE> list = db.SALES.Where(x=>x.ProductID == entity.ProductID).ToList();
                    foreach(var item in list)
                    {
                        item.isDeleted = true;
                        item.DeletedDate = DateTime.Today;  

                    }
                    db.SaveChanges();
                }
                else if(entity.CustomerID!= 0)
                {
                    List<SALE> list = db.SALES.Where(x => x.CustomerID == entity.CustomerID).ToList();
                    foreach (var item in list)
                    {
                        item.isDeleted = true;
                        item.DeletedDate = DateTime.Today;

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
                SALE sale = db.SALES.First(x => x.ID == ID);
                sale.isDeleted = false;
                sale.DeletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Insert(SALE entity)
        {
            try
            {
                db.SALES.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<SalesDetailDTO> select()
        {
            try
            {
                List<SalesDetailDTO> sales = new List<SalesDetailDTO>();
                var list = (from s in db.SALES.Where(x=>x.isDeleted == false)
                            join p in db.PRODUCTs on s.ProductID equals p.ID
                            join c in db.CUSTOMERs on s.CustomerID equals c.ID
                            join category in db.CATEGORies on s.CategoryID equals category.ID
                            select new
                            {
                                productname = p.ProductName,
                                customername = c.CustomerName,
                                categoryname = category.CategoryName,
                                productID = s.ProductID,
                                customerID = s.CustomerID,
                                salesID = s.ID,
                                categoryID = s.CategoryID,
                                salesprice = s.ProductSalesPrice,
                                salesamount = s.ProductSalesAmount,
                                salesdate = s.SalesDate
                            }).OrderBy(x=>x.salesdate).ToList();
                foreach(var item in list)
                {
                    SalesDetailDTO dto = new SalesDetailDTO();
                    dto.ProductName = item.productname;
                    dto.CustomerName = item.customername;
                    dto.CategoryName = item.categoryname;   
                    dto.ProductID = item.productID; 
                    dto.CustomerID = item.customerID;
                    dto.SalesID = item.salesID;
                    dto.CategoryID = item.categoryID;
                    dto.SalesAmount = item.salesamount;
                    dto.Price = item.salesprice;
                    dto.SalesDate = item.salesdate;
                    sales.Add(dto);
                }
                return sales;
            }
            catch (Exception ex)
            {

                throw ex;
            } 
        }

        public List<SalesDetailDTO> select(bool isDeleted)
        {
            try
            {
                List<SalesDetailDTO> sales = new List<SalesDetailDTO>();
                var list = (from s in db.SALES.Where(x => x.isDeleted == isDeleted)
                            join p in db.PRODUCTs on s.ProductID equals p.ID
                            join c in db.CUSTOMERs on s.CustomerID equals c.ID
                            join category in db.CATEGORies on s.CategoryID equals category.ID
                            select new
                            {
                                productname = p.ProductName,
                                customername = c.CustomerName,
                                categoryname = category.CategoryName,
                                productID = s.ProductID,
                                customerID = s.CustomerID,
                                salesID = s.ID,
                                categoryID = s.CategoryID,
                                salesprice = s.ProductSalesPrice,
                                salesamount = s.ProductSalesAmount,
                                salesdate = s.SalesDate,
                                categoryDeleted = category.isDeleted,
                                customerDeleted = c.isDeleted,
                                productDeleted = p.isDeleted
                            }).OrderBy(x => x.salesdate).ToList();
                foreach (var item in list)
                {
                    SalesDetailDTO dto = new SalesDetailDTO();
                    dto.ProductName = item.productname;
                    dto.CustomerName = item.customername;
                    dto.CategoryName = item.categoryname;
                    dto.ProductID = item.productID;
                    dto.CustomerID = item.customerID;
                    dto.SalesID = item.salesID;
                    dto.CategoryID = item.categoryID;
                    dto.SalesAmount = item.salesamount;
                    dto.Price = item.salesprice;
                    dto.SalesDate = item.salesdate;
                    dto.isCategoryDeleted = item.categoryDeleted;
                    dto.isCustomerDeleted = item.customerDeleted;
                    dto.isProductDeleted = item.productDeleted;
                    sales.Add(dto);
                }
                return sales;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Update(SALE entity)
        {
            try
            {
                SALE sales = db.SALES.First(x => x.ID == entity.ID);
                sales.ProductSalesAmount = entity.ProductSalesAmount;
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
