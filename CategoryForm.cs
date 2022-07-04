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
    public partial class CategoryForm : Form
    {
        public CategoryForm()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //Local MSSQL Location
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\minkh\Documents\supermarketdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open(); 
                string query = "insert into CategoryTbl values(" + tbox_catID.Text + ",'" + tbox_catname.Text + "', '" + tbox_catdescri.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("category Added  Successfully");
                Con.Close();
                tbox_catID.Text = "";
                tbox_catname.Text = "";
                tbox_catdescri.Text = "";
                populate();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from CategoryTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CatDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void CategoryForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void CatDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbox_catID.Text = CatDGV.SelectedRows[0].Cells[0].Value.ToString();
            tbox_catname.Text = CatDGV.SelectedRows[0].Cells[1].Value.ToString();
            tbox_catdescri.Text = CatDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if(tbox_catID.Text == "")
                {
                    MessageBox.Show("Select The Category to Delete");
                }
                else
                {
                    Con.Open();
                    string query = "delete from CategoryTbl where CategoryID=" + tbox_catID.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Deleted Successfully");
                    Con.Close();
                    tbox_catID.Text = "";
                    tbox_catname.Text = "";
                    tbox_catdescri.Text = "";
                    populate();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                if(tbox_catID.Text == "" || tbox_catname.Text == "" || tbox_catdescri.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    string query = "update CategoryTbl set CategoryName='" + tbox_catname.Text + "',CategoryDescription='" + tbox_catdescri.Text + "',CategoryID='" + tbox_catID.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Successfully Updated");
                    Con.Close();
                    tbox_catID.Text = "";
                    tbox_catname.Text = "";
                    tbox_catdescri.Text = "";
                    populate();
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_products_Click(object sender, EventArgs e)
        {
            ProductForm prod = new ProductForm();
            prod.Show();
            this.Hide();
        }

        private void btn_Sellers_Click(object sender, EventArgs e)
        {
            SellerForm sellerfm = new SellerForm();
            sellerfm.Show();
            this.Hide();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }
    }
}
