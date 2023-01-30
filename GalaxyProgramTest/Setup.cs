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

namespace GalaxyProgramTest
{
    public partial class Setup : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        string description, code, tCode;
        int qty, SRID;
        float p_price, s_price;
        int SRNO;

        public string conString = "Data Source=DESKTOP-B8L8EEK\\SQLEXPRESS;Initial Catalog=ProgramTest;Integrated Security=True";

        public Setup()
        {
            InitializeComponent();
            ViewStock();
        }


        private void Setup_Load(object sender, EventArgs e)
        {
            StockGridView.DataSource = ViewStock();
        }
                       


        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Number Only");
            }

        }

        private void txtPPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Number Only");
            }
        }

        private void txtSPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Number Only");
            }
        }

        private void txtAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Number Only");
            }
        }   

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if(txtCode.Text.Length !=0 && txtDesc.Text.Length !=0 && txtQty.Text.Length !=0 && txtPPrice.Text.Length !=0 && txtSPrice.Text.Length != 0)
            {
                code = txtCode.Text;
                description = txtDesc.Text;
                p_price = float.Parse(txtPPrice.Text);
                s_price = float.Parse(txtSPrice.Text);
                qty = int.Parse(txtQty.Text);
                int sr = SortID();

                string query = "insert into stockcode (sr, code, description, qty,s_price, p_price) values(@sr, @code, @description, @qty, @s_price, @p_price)";

                con = new SqlConnection(conString);
                con.Open();


                if (con.State == System.Data.ConnectionState.Open)
                {
                    string checkQuery = "SELECT COUNT(*) FROM [StockCode] WHERE ([code] = @code)";
                    cmd = new SqlCommand(checkQuery, con);
                    cmd.Parameters.AddWithValue("@code", code);
                    int UserExist = (int)cmd.ExecuteScalar();

                    if (UserExist > 0)
                    {
                        //code exist
                        MessageBox.Show(
                        "This code is already exist",
                        "Alert",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                        );
                    }
                    else
                    {

                        //Code doesn't exist.
                        cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue("@sr", sr);
                        cmd.Parameters.AddWithValue("@code", code);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@qty", qty);
                        cmd.Parameters.AddWithValue("@s_price", s_price);
                        cmd.Parameters.AddWithValue("@p_price", p_price);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(
                            "Data Insert Successfully", 
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        StockGridView.DataSource = ViewStock();
                        BoxClear();
                    }

                    con.Close();
                }
            }
            else
            {
                MessageBox.Show(
                    "Please fill data",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                
            }           

        }
       
        private void StockGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F8)
            {
                DialogResult dialogResult = MessageBox.Show("Delete this Stock?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    RowDelete(code);
                    StockGridView.DataSource = ViewStock();
                }
                else if (dialogResult == DialogResult.No)
                {
                    StockGridView.DataSource = ViewStock();
                }
            }
        }

        private void StockGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow rows = this.StockGridView.Rows[e.RowIndex];
                code = SelectSR(e);
                SRNO = int.Parse(rows.Cells["sr"].Value.ToString());
            }
        }

        private void StockGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectCode(e);
                tCode = txtCode.Text; 
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {       
            UpdateStock();
            BoxClear();
            StockGridView.DataSource = ViewStock();
            SRNO = 0;
                
        }


        







        /*-------------------Methoa=da-------------------------*/
        public void BoxClear()
        {
            txtCode.Text = null;
            txtDesc.Text = null;
            txtQty.Text = null;
            txtPPrice.Text= null;
            txtSPrice.Text = null;
        }

        private DataTable ViewStock()
        {
            DataTable tblStock = new DataTable();
            con = new SqlConnection(conString);
            con.Open();
            string qry = "SELECT sr, code, description, qty, p_price, s_price FROM  StockCode";
            cmd = new SqlCommand(qry, con);
            SqlDataReader reader = cmd.ExecuteReader();
            tblStock.Load(reader);
            con.Close();
            return tblStock;
        }

        private int SortID()
        {
            string sql = "SELECT MAX(sr) FROM StockCode";
            con = new SqlConnection(conString);
            con.Open();
            cmd = new SqlCommand(sql, con);

            object result = cmd.ExecuteScalar();
            result = (result == DBNull.Value) ? null : result;
            SRID = Convert.ToInt32(result);

            if (SRID > 0)
            {
                SRID += 1;
            }
            else
            {
                SRID = 1;
            }

            con.Close();

            return SRID;


        }

        public void RowDelete(string code)
        {
            string sql = "DELETE FROM StockCode WHERE code='" + code +"'";
            con = new SqlConnection(conString);
            con.Open();
            cmd = new SqlCommand(sql, con);
            int ans = cmd.ExecuteNonQuery();

            if(ans == 1)
            {
                MessageBox.Show(
                    "Successfully Deleted!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );

                StockGridView.DataSource = ViewStock();
            }

            
        }

        private string SelectSR(DataGridViewCellEventArgs e)
        {
            DataGridViewRow rows = this.StockGridView.Rows[e.RowIndex];
            string cd = rows.Cells["Scode"].Value.ToString();

            return cd;
        }

        private void SelectCode(DataGridViewCellEventArgs e)
        {
            DataGridViewRow rows = this.StockGridView.Rows[e.RowIndex];
            txtCode.Text = rows.Cells["Scode"].Value.ToString();
            txtDesc.Text = rows.Cells["Sdescription"].Value.ToString();
            txtQty.Text = rows.Cells["Sqty"].Value.ToString();
            txtSPrice.Text = rows.Cells["Ss_price"].Value.ToString();
            txtPPrice.Text = rows.Cells["Sp_Price"].Value.ToString();
        }

        private void UpdateStock()
        {
            string sql = "UPDATE StockCode SET code='"+ txtCode.Text + "', description='" + txtDesc.Text + "', qty=" + txtQty.Text + ", s_price=" + txtSPrice.Text + ", p_price=" + txtPPrice.Text + " WHERE sr=" + SRNO + "";
            con = new SqlConnection(conString);
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Update Successfully!");
            con.Close();
        }
    }
}
