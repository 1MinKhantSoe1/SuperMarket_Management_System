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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string SellerName = "";

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\minkh\Documents\supermarketdb.mdf;Integrated Security=True;Connect Timeout=30");


        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            tbox_username.Text = "";
            tbox_password.Text = "";
        }

        private void label1_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (tbox_username.Text == "" || tbox_password.Text == "")
            {
                MessageBox.Show("Enter The Username And Password");
            }
            else
            {
                if(cbo_select_role.SelectedIndex > -1)
                {
                    if(cbo_select_role.SelectedItem.ToString() == "Admin")
                    {
                        if (tbox_username.Text == "Admin" && tbox_password.Text == "Admin")
                        {
                            ProductForm prod = new ProductForm();
                            prod.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("If You Are The Admin, Please Enter Correct Username And Password");
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Your In The Seller Section");

                        Con.Open();
                        string query = ("Select count(8) from SellerTbl where SellerName='" + tbox_username.Text + "' and SellerPass='" + tbox_password.Text + "'");
                        SqlDataAdapter sda = new SqlDataAdapter(query, Con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            SellerName = tbox_username.Text;
                            SellingForm sell = new SellingForm();
                            sell.Show();
                            this.Hide();
                            Con.Close();
                        }
                        else
                        {
                            MessageBox.Show("Wrong UserName or Password");
                        }
                        Con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Select A Role");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
