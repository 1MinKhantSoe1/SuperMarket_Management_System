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
    public partial class SellerForm : Form
    {
        public SellerForm()
        {
            InitializeComponent();
        }
        //Local MSSQL Location
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\minkh\Documents\supermarketdb.mdf;Integrated Security=True;Connect Timeout=30");


        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from SellerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SellerDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into SellerTbl values(" + tbox_SellerID.Text + ",'" + tbox_SellerName.Text + "', '" + tbox_SellerAge.Text + "', '"+tbox_SellerPhone.Text+"', '"+tbox_SellerPassword.Text+"')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Seller Added  Successfully");
                Con.Close();
                tbox_SellerID.Text = "";
                tbox_SellerName.Text = "";
                tbox_SellerAge.Text = "";
                tbox_SellerPhone.Text = "";
                tbox_SellerPassword.Text = "";
                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SellerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbox_SellerID.Text = SellerDGV.SelectedRows[0].Cells[0].Value.ToString();
            tbox_SellerName.Text = SellerDGV.SelectedRows[0].Cells[1].Value.ToString();
            tbox_SellerAge.Text = SellerDGV.SelectedRows[0].Cells[2].Value.ToString();
            tbox_SellerPhone.Text = SellerDGV.SelectedRows[0].Cells[3].Value.ToString();
            tbox_SellerPassword.Text = SellerDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbox_SellerID.Text == "" || tbox_SellerName.Text == "" || tbox_SellerAge.Text == "" || tbox_SellerPhone.Text == "" || tbox_SellerPassword.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    string query = "update SellerTbl set SellerName='" + tbox_SellerName.Text + "',SellerAge='" + tbox_SellerAge.Text + "',SellerPhone='" + tbox_SellerPhone.Text + "',SellerPass='"+tbox_SellerPassword.Text+"'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Successfully Updated");
                    Con.Close();
                    tbox_SellerID.Text = "";
                    tbox_SellerName.Text = "";
                    tbox_SellerAge.Text = "";
                    tbox_SellerPhone.Text = "";
                    tbox_SellerPassword.Text = "";
                    populate();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbox_SellerID.Text == "")
                {
                    MessageBox.Show("Select The Seller to Delete");
                }
                else
                {
                    Con.Open();
                    string query = "delete from SellerTbl where SellerID=" + tbox_SellerID.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Deleted Successfully");
                    Con.Close();
                    tbox_SellerID.Text = "";
                    tbox_SellerName.Text = "";
                    tbox_SellerAge.Text = "";
                    tbox_SellerPhone.Text = "";
                    tbox_SellerPassword.Text = "";
                    populate();
                }
            }
            catch (Exception ex)
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

        private void btn_categories_Click(object sender, EventArgs e)
        {
            CategoryForm catform = new CategoryForm();
            catform.Show();
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
