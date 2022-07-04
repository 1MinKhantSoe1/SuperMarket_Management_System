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
    public partial class SellingForm : Form
    {
        public SellingForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\minkh\Documents\supermarketdb.mdf;Integrated Security=True;Connect Timeout=30");


        private void populate()
        {
            Con.Open();
            string query = "select ProductName,ProductPrice from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV1.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void populatebills()
        {
            Con.Open();
            string query = "select * from BillTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BillDGV.DataSource = ds.Tables[0];
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

        private void SellingForm_Load(object sender, EventArgs e)
        {
            fillcombo_for_catfilter();
            populate();
            populatebills();
            SellerNamelbl.Text = Form1.SellerName;
            
        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ProdDGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbox_ProdName.Text = ProdDGV1.SelectedRows[0].Cells[0].Value.ToString();
            tbox_ProdPrice.Text = ProdDGV1.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Datelbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }
        int Grdtotal = 0, n = 0;

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (tbox_BillID.Text == "")
            {
                MessageBox.Show("Missing Bill Id");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into BillTbl values(" + tbox_BillID.Text + ",'" + SellerNamelbl.Text + "', '" + Datelbl.Text + "','" + Amtlbl.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order Added  Successfully");
                    Con.Close();
                    tbox_BillID.Text = "";
                    tbox_ProdName.Text = "";
                    tbox_ProdPrice.Text = "";
                    tbox_ProdQuantity.Text = "";
                    populatebills();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("MINKHANTSOE_SUPERMARKET", new Font("Century Gothic",25,FontStyle.Bold), Brushes.Red, new Point(200));
            e.Graphics.DrawString("Bill ID: " + BillDGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100,70));
            e.Graphics.DrawString("Seller Name: " + BillDGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 100));
            e.Graphics.DrawString("Date: " + BillDGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 130));
            e.Graphics.DrawString("Total Amount: " + BillDGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 160));
            e.Graphics.DrawString("MIN KHANT SOE", new Font("Century Gothic", 25, FontStyle.Italic), Brushes.Red, new Point(270,230));
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            populate();
        }


        private void catfilter()
        {

            string query = "select ProductName,ProductPrice from ProductTbl where ProductCategory='" + cbo_catfilter.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV1.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void cbo_catfilter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }

        private void cbo_catfilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            catfilter();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void btn_AddProduct_Click(object sender, EventArgs e)
        {
        
            if (tbox_ProdName.Text == "" || tbox_ProdQuantity.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                int total_price = Convert.ToInt32(tbox_ProdPrice.Text) * Convert.ToInt32(tbox_ProdQuantity.Text);

                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(OrderDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = tbox_ProdName.Text;
                newRow.Cells[2].Value = tbox_ProdPrice.Text;
                newRow.Cells[3].Value = tbox_ProdQuantity.Text;
                newRow.Cells[4].Value = Convert.ToInt32(tbox_ProdPrice.Text) * Convert.ToInt32(tbox_ProdQuantity.Text);
                OrderDGV.Rows.Add(newRow);
                n++;
                Grdtotal = Grdtotal + total_price;
                Amtlbl.Text = "" + Grdtotal;
            }
            
            
        }
    }
}
