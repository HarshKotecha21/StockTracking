using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockTracking.BLL;
using StockTracking.DAL.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace StockTracking
{
    public partial class FrmDeleted : Form
    {
        public FrmDeleted()
        {
            InitializeComponent();
        }
        SalesBLL bll = new SalesBLL();
        SalesDTO dto = new SalesDTO();
        CustomerDetailDTO customerDetail = new CustomerDetailDTO();
        CategoryDetailDTO categoryDetail = new CategoryDetailDTO();
        ProductDetailDTO productDetail = new ProductDetailDTO();
        SalesDetailDTO salesDetail = new SalesDetailDTO();
        CategoryBLL categoryBLL = new CategoryBLL();
        CustomerBLL customerBLL = new CustomerBLL();
        ProductBLL productBLL = new ProductBLL();
        SalesBLL SalesBLL = new SalesBLL();
        private void FrmDeleted_Load(object sender, EventArgs e)
        {
            cmbDeletedData.Items.Add("Category");
            cmbDeletedData.Items.Add("Customer");
            cmbDeletedData.Items.Add("Product");
            cmbDeletedData.Items.Add("Sales");
            dto = bll.select(true);
            dataGridView1.DataSource = dto.Sales;
            dataGridView1.Columns[0].HeaderText = "Customer Name";
            dataGridView1.Columns[1].HeaderText = "Product Name";
            dataGridView1.Columns[2].HeaderText = "Category Name";
            dataGridView1.Columns[6].HeaderText = "Sales Amount";
            dataGridView1.Columns[7].HeaderText = "Price";
            dataGridView1.Columns[8].HeaderText = "Sales Date";
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGetBack_Click(object sender, EventArgs e)
        {
            if(cmbDeletedData.SelectedIndex == 0)
            {
                if(categoryBLL.GetBack(categoryDetail))
                {
                    MessageBox.Show("Category is added back");
                    dto = bll.select(true);
                    dataGridView1.DataSource = dto.Categories;
                }
            }
            else if (cmbDeletedData.SelectedIndex == 1)
            {
                if (customerBLL.GetBack(customerDetail))
                {
                    MessageBox.Show("Customer is added back");
                    dto = bll.select(true);
                    dataGridView1.DataSource = dto.Customers;
                }
            }
            else if (cmbDeletedData.SelectedIndex == 2)
            {
                if (productDetail.isCategoryDeleted)
                {
                    MessageBox.Show("Category was deleted first get back category");
                }
                else if (productBLL.GetBack(productDetail))
                {
                    MessageBox.Show("Product is added back");
                    dto = bll.select(true);
                    dataGridView1.DataSource = dto.Products;
                }
            }
            else if (cmbDeletedData.SelectedIndex == 3)
            {
                if (salesDetail.isCustomerDeleted || salesDetail.isCategoryDeleted || salesDetail.isProductDeleted)
                {
                    if (salesDetail.isCustomerDeleted)
                        MessageBox.Show("Customer was deleted first get back customer");
                    else if (salesDetail.isCategoryDeleted)
                        MessageBox.Show("Category was deleted first get back category");
                    else if (salesDetail.isProductDeleted)
                        MessageBox.Show("Product was deleted first get back product");
                }
                else if (SalesBLL.GetBack(salesDetail))
                {
                    MessageBox.Show("Sales is added back");
                    dto = bll.select(true);
                    dataGridView1.DataSource = dto.Sales;
                }
            }
        }

        private void cmbDeletedData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbDeletedData.SelectedIndex == 0)
            {
                dataGridView1.DataSource = dto.Categories;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Category Name";
            }
            else if (cmbDeletedData.SelectedIndex == 1)
            {
                dataGridView1.DataSource = dto.Customers;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "CustomerName";
            }
            else if (cmbDeletedData.SelectedIndex == 2)
            {
                dataGridView1.DataSource = dto.Products;
                dataGridView1.Columns[0].HeaderText = "Product Name";
                dataGridView1.Columns[1].HeaderText = "Category Name";
                dataGridView1.Columns[2].HeaderText = "Stock Amount";
                dataGridView1.Columns[3].HeaderText = "Price";
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
            }
            else if (cmbDeletedData.SelectedIndex == 3)
            {
                dataGridView1.DataSource = dto.Sales;
                dataGridView1.Columns[0].HeaderText = "Customer Name";
                dataGridView1.Columns[1].HeaderText = "Product Name";
                dataGridView1.Columns[2].HeaderText = "Category Name";
                dataGridView1.Columns[6].HeaderText = "Sales Amount";
                dataGridView1.Columns[7].HeaderText = "Price";
                dataGridView1.Columns[8].HeaderText = "Sales Date";
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[13].Visible = false;
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(cmbDeletedData.SelectedIndex == 0)
            {
                categoryDetail = new CategoryDetailDTO();
                categoryDetail.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                categoryDetail.CategoryName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

            }
            else if (cmbDeletedData.SelectedIndex == 1)
            {
                customerDetail.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                customerDetail.CutomerName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            else if (cmbDeletedData.SelectedIndex == 2)
            {
                productDetail = new ProductDetailDTO();
                productDetail.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                productDetail.CategoryID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                productDetail.ProductName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                productDetail.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                productDetail.isCategoryDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
            }
            else if (cmbDeletedData.SelectedIndex == 3)
            {
                salesDetail = new SalesDetailDTO();
                salesDetail.SalesID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
                salesDetail.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                salesDetail.CustomerID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                salesDetail.ProductName = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                salesDetail.CustomerName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                salesDetail.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                salesDetail.SalesAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                salesDetail.isCategoryDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
                salesDetail.isCustomerDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
                salesDetail.isProductDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[13].Value);
            }
        }
    }
}
