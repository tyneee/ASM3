using ProductLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductStore
{
    public partial class frmMainProduct : Form
    {
       
        // khai bao di tg de thtac du lieu bang Product   
        // khai bao doi tg Datatable de luu du lieu
        private ProductDB db = new ProductDB();
        private List<Product> products;

        public frmMainProduct()
        {
            InitializeComponent();
        }

        private void frmMainProduct_Load(object sender, EventArgs e)
        {
            LoadProduct();
        }
        private void LoadProduct()
        {
            //laydu lieu
            products = db.GetProductList();
            //Clear
            txtProductID.DataBindings.Clear();
            txtProductName.DataBindings.Clear();
            txtPrice.DataBindings.Clear();
            txtQuantity.DataBindings.Clear();
            //Set value
            txtProductID.DataBindings.Add("Text", products, "ProductID");
            txtProductName.DataBindings.Add("Text", products, "ProductName");
            txtPrice.DataBindings.Add("Text", products, "UnitPrice");
            txtQuantity.DataBindings.Add("Text", products, "Quantity");
            //rang buoc du lieu
            
            dgvListProduct.DataSource = products;
        }

        // try-catch loi
        private string validData()
        {
            string mes = "";
            //ID                  
            try
            {
                int Id = int.Parse(txtProductID.Text);
                db.GetProductList().ForEach(delegate (Product tp)
                {
                    if (tp.ProductID == Id)
                    {
                        mes += "ID is existed!\n";
                    }
                });
            }
            catch (Exception e)
            {
                  mes += "ID must be a integer number!\n";
            }
            //Name
            if (txtProductName.Text.Trim().Length == 0)
            {
                mes += "Name cannot be empty!\n";
            }
            //UnitPrice
            try
            {
                float price = float.Parse(txtPrice.Text);
                if (price < 0)
                {
                    mes += "UnitPrice must be greater or equal than 0!\n";
                }
            }
            catch (Exception e)
            {
                mes += "UnitPrice must be a number!\n";
            }
            //Quantity
            try
            {
                int quantity = int.Parse(txtQuantity.Text);
                if (quantity < 0)
                {
                    mes += "Quantity must be greater or equal than 0!\n";
                }
            }
            catch (Exception e)
            {
                mes += "Quantity must be a integer number!\n";
            }
            return mes;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            string mes = validData();
            if (mes.Length != 0)
            {
                MessageBox.Show(mes);
                return;
            }
            Product p = new Product()
            {
                ProductID = int.Parse(txtProductID.Text),
                ProductName = txtProductName.Text,
                UnitPrice = float.Parse(txtPrice.Text),
                Quantity = int.Parse(txtQuantity.Text)
            };
            if (db.AddProduct(p))
            {
                MessageBox.Show("Insert sucessfull.");
            }
            else
            {
                MessageBox.Show("fail...");
            }
            LoadProduct();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string mes = validData();
            if (mes.Length != 0)
            {
                MessageBox.Show(mes);
                return;
            }
            Product p = new Product()
            {
                ProductID = int.Parse(txtProductID.Text),
                ProductName = txtProductName.Text,
                UnitPrice = float.Parse(txtPrice.Text),
                Quantity = int.Parse(txtQuantity.Text)
            };
            if (db.UpdateProduct(p))
            {
                MessageBox.Show("Update sucessfull.");
                LoadProduct();
            }
            else
            {
                MessageBox.Show("fail....");
            }
            LoadProduct();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Product p = new Product() { ProductID = int.Parse(txtProductID.Text) };
            if (db.DeleteProduct(p))
            {
                MessageBox.Show("Delete sucessfull.");
                LoadProduct();
            }
            else
            {
                MessageBox.Show("fail...");
            }
            LoadProduct();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmSearch frm = new frmSearch();
            frm.ShowDialog();
        }

        private void dgvListProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
