using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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
                        MessageBox.Show("Your In The Seller Section");
                    }
                }
                else
                {
                    MessageBox.Show("Select A Role");
                }
            }
        }
    }
}
