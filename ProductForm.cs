using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SuperMarket
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }
        //Local MSSQL Location
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\minkh\Documents\supermarketdb.mdf;Integrated Security=True;Connect Timeout=30");


        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fillcombo()
        {
            // This Method will bind the Combox with Database

            Con.Open();
            string query = "select CategoryName from CategoryTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CategoryName", typeof(string));
            dt.Load(rdr);
            cbo_select_category.ValueMember = "CategoryName";
            cbo_select_category.DataSource = dt;
            Con.Close();

        }

        private void fillcombo_for_catfilter()
        {
            Con.Open();
            string query = "select CategoryName from CategoryTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CategoryName", typeof(string));
            dt.Load(rdr);
            cbo_catfilter.ValueMember = "CategoryName";
            cbo_catfilter.DataSource = dt;
            Con.Close();
        }
        private void populate()
        {
            Con.Open();
            string query = "select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        // Category filter by Product Category
        private void catfilter()
        {
            
            string query = "select * from ProductTbl where ProductCategory='" + cbo_catfilter.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ProductForm_Load(object sender, EventArgs e)
        {
            fillcombo_for_catfilter();
            fillcombo();
            populate();
            
        }

        private void btn_categories_Click(object sender, EventArgs e)
        {
            CategoryForm cat = new CategoryForm();
            cat.Show();
            this.Hide();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into ProductTbl values(" + tbox_ProdID.Text + ",'" + tbox_ProdName.Text + "', '" + tbox_ProdQuantity.Text + "','"+tbox_ProdPrice.Text+"', '"+cbo_select_category.SelectedValue.ToString()+"')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Added  Successfully");
                Con.Close();
                tbox_ProdID.Text = "";
                tbox_ProdName.Text = "";
                tbox_ProdPrice.Text = "";
                tbox_ProdQuantity.Text = "";
                cbo_select_category.Text = "";
                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProdDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbox_ProdID.Text = ProdDGV.SelectedRows[0].Cells[0].Value.ToString();
            tbox_ProdName.Text = ProdDGV.SelectedRows[0].Cells[1].Value.ToString();
            tbox_ProdPrice.Text = ProdDGV.SelectedRows[0].Cells[3].Value.ToString();
            tbox_ProdQuantity.Text = ProdDGV.SelectedRows[0].Cells[2].Value.ToString();
            cbo_select_category.Text = ProdDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbox_ProdID.Text == "")
                {
                    MessageBox.Show("Select The Product to Delete");
                }
                else
                {
                    Con.Open();
                    string query = "delete from ProductTbl where Productid=" + tbox_ProdID.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted Successfully");
                    Con.Close();
                    tbox_ProdID.Text = "";
                    tbox_ProdName.Text = "";
                    tbox_ProdPrice.Text = "";
                    tbox_ProdQuantity.Text = "";
                    cbo_select_category.Text = "";
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbox_ProdID.Text == "" || tbox_ProdName.Text == "" || tbox_ProdPrice.Text == "" || tbox_ProdQuantity.Text == "" || cbo_select_category.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    string query = "update ProductTbl set ProductName='" + tbox_ProdName.Text + "',ProductQty='" + tbox_ProdQuantity.Text + "',ProductPrice='" + tbox_ProdPrice.Text + "', ProductCategory='"+cbo_select_category.SelectedValue.ToString()+"'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Successfully Updated");
                    Con.Close();
                    tbox_ProdID.Text = "";
                    tbox_ProdName.Text = "";
                    tbox_ProdPrice.Text = "";
                    tbox_ProdQuantity.Text = "";
                    cbo_select_category.Text = "";
                    populate();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void cbo_catfilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            catfilter();
        }

        private void cbo_select_category_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void btn_seller_Click(object sender, EventArgs e)
        {
            SellerForm sellerfm = new SellerForm();
            sellerfm.Show();
            this.Hide();
        }
    }
}
