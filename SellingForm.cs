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

        private void SellingForm_Load(object sender, EventArgs e)
        {
            populate();
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
                Amtlbl.Text = "Ks " + Grdtotal;
            }
            
            
        }
    }
}
