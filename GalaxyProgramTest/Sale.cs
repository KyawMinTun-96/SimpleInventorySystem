using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace GalaxyProgramTest
{
    public partial class Sale : Form
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        string SCode, SDesc, invoice, STotalQty, STotalAmount, SInv, SDate;
        int SQty, SRNO;
        float SPrice, SAmt;
        int SRID;
        int Sstatus = 0;

        String conString = "Data Source=DESKTOP-B8L8EEK\\SQLEXPRESS;Initial Catalog=ProgramTest;Integrated Security=True";
        String configString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;

        public Sale()
        {
            InitializeComponent();
            InvoiceNo();
            Total();
            //SelectData();

        }

        private void Sale_Load(object sender, EventArgs e)
        {
            SNewPanel.Visible = false;
            PanelAddDetail.Visible = false;
            tblSale.EnableHeadersVisualStyles = false;
            btnUpdate.Visible = false;
            lblUpdate.Visible = false;
            btnSUpdate.Visible = false;
            tblSale.DataSource = GetSaleList();
            tblSale.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            TotalInvoice();
        }








        /*-------------------Events---------------------*/

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();
                string searchChar = "SELECT code, description, qty, s_price FROM StockCode WHERE code=@code";
                cmd = new SqlCommand(searchChar, con);
                cmd.Parameters.AddWithValue("@code", txtCode.Text);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtDescription.Text = reader["description"].ToString();
                    //txtAddQty.Text = reader["qty"].ToString();
                    txtAddQty.Text = 1.ToString();
                    txtPrice.Text = reader["s_price"].ToString();
                    SQty = int.Parse(txtAddQty.Text);
                    SPrice = float.Parse(txtPrice.Text);
                    SAmt = SQty * SPrice;
                    txtPTAmount.Text = SAmt.ToString();

                }

                con.Close();
            }
            catch
            {

            }

        }

        private void txtAddQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SQty = int.Parse(txtAddQty.Text);
                SPrice = float.Parse(txtPrice.Text);
                SAmt = SQty * SPrice;
                txtPTAmount.Text = SAmt.ToString();
            }
            catch
            {
            }
        }

        private void btnAddSave_Click(object sender, EventArgs e)
        {
            Sstatus = 1;
            SNewPanel.Visible = true;
            tblSStockCode.EnableHeadersVisualStyles = false;
            tblSStockCode.DataSource = GetStockList();
            tblSStockCode.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            txtTotalAmount.Text = null;
            txtTotalQty.Text = null;
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            PanelAddDetail.Visible = true;
            btnSUpdate.Visible = false;
            btnAdd.Visible = true;
            txtAddDate.Text = txtDate.Text;
            txtInvoiceNo.Text = txtInv.Text;
        }

        private void NewPanel_Click(object sender, EventArgs e)
        {
            PanelAddDetail.Visible = false;
            ClearBox();
        }

        private void btnSaveSale_Click(object sender, EventArgs e)
        {

            PanelAddDetail.Visible = false;
            VouSave();
            txtTotalAmount.Text = null;
            txtTotalQty.Text = null;
            ClearTable();
            InvoiceNo();
            tblSStockCode.DataSource = GetStockList();
            ClearBox();
            TotalInvoice();
            SRID = 0;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtAddQty.TextLength != 0 && txtAddDate.TextLength != 0 && txtCode.TextLength != 0 && txtDescription.TextLength != 0 && txtAddQty.TextLength != 0 && txtPrice.TextLength != 0 && txtPTAmount.TextLength != 0)
            {
                if (Sstatus == 1)
                {
                    InsertTemp();
                    tblSStockCode.DataSource = GetStockList();
                    Total();
                    ClearBox();
                }
                else if (Sstatus == 2)
                {
                    InsertTemp();
                    Total();
                    tblSStockCode.DataSource = GetStockList();
                    ClearBox();

                }
            }
            else
            {
                MessageBox.Show(
                    "Please Fill Data First!",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                ClearBox();
            }



        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if(Sstatus == 1)
            {
                string sql = "SELECT COUNT(*) FROM SaleTemp";
                con = new SqlConnection(conString);
                con.Open();
                cmd = new SqlCommand(sql, con);
                int res = (int)cmd.ExecuteScalar();

                if (res >= 1)
                {
                    DialogResult dialogResult = MessageBox.Show("Save Change?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        VouSave();
                        txtTotalAmount.Text = null;
                        txtTotalQty.Text = null;
                        ClearTable();
                        SNewPanel.Visible = false;
                        tblSale.DataSource = GetSaleList();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        ClearTable();
                        SNewPanel.Visible = false;
                        tblSale.DataSource = GetSaleList();
                    }
                }
                else
                {
                    ClearTable();
                    SNewPanel.Visible = false;
                    ClearBox();
                    tblSale.DataSource = GetSaleList();
                    PanelAddDetail.Visible = false;
                }
            }
            else if(Sstatus == 2)
            {
                con = new SqlConnection(conString);
                con.Open();
                string tmp = "SELECT COUNT(*) FROM Sale_Stock where invoice_no='" + invoice + "'";
                string sql = "SELECT COUNT(*) FROM SaleTemp";

                cmd = new SqlCommand(tmp, con);
                int DetCount = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand(sql, con);
                int tmpCount = (int)cmd.ExecuteScalar();

                if (tmpCount > DetCount)
                {
                    DialogResult dialogResult = MessageBox.Show("Save Change?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        VouSave();
                        txtTotalAmount.Text = null;
                        txtTotalQty.Text = null;
                        ClearTable();
                        SNewPanel.Visible = false;
                        tblSale.DataSource = GetSaleList();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        ClearTable();
                        SNewPanel.Visible = false;
                        tblSale.DataSource = GetSaleList();
                    }
                }
                else if(tmpCount == DetCount)
                {
                    ClearTable();
                    SNewPanel.Visible = false;
                    tblSale.DataSource = GetSaleList();
                }else
                {
                    DialogResult dialogResult = MessageBox.Show("Save Change?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        VouSave();
                        txtTotalAmount.Text = null;
                        txtTotalQty.Text = null;
                        ClearTable();
                        SNewPanel.Visible = false;
                        tblSale.DataSource = GetSaleList();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        ClearTable();
                        SNewPanel.Visible = false;
                        tblSale.DataSource = GetSaleList();
                    }
                }

            }
            con.Close();
        }

        private void tblSale_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {         
            SelectRow(e);
            GetVouData();
        }

        private void tblSale_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectRow(e);
            }
        }

        private void btnSaleEdit_Click(object sender, EventArgs e)
        {
            GetVouData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            VouSave();
            ClearTable();
            SNewPanel.Visible = false;
            tblSale.DataSource = GetSaleList();
            TotalInvoice();

        }

        private void tblSStockCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {

                DialogResult dialogResult = MessageBox.Show("Delete this item?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    DeleteItem();
                    tblSStockCode.DataSource = GetStockList();
                    Total();
                }
                else if (dialogResult == DialogResult.No)
                {
                    tblSStockCode.DataSource = GetStockList();
                }
            }
        }

        private void tblSStockCode_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PanelAddDetail.Visible = true;
            btnSUpdate.Visible = true;
            btnAdd.Visible = false;
            txtAddDate.Text = txtDate.Text;
            txtInvoiceNo.Text = txtInv.Text;
            SelectCode(e);
        }

        private void tblSStockCode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SRNO = SelectSR(e);
        }

        private void btnSUpdate_Click(object sender, EventArgs e)
        {
            UpdateStock();
            tblSStockCode.DataSource = GetStockList();
            Total();
            ClearBox();
            PanelAddDetail.Visible = false;
        }



        /*-------------------Method---------------------*/

        private void SelectRow(DataGridViewCellEventArgs e)
        {
            DataGridViewRow rows = this.tblSale.Rows[e.RowIndex];
            invoice = rows.Cells["invoice_no"].Value.ToString();
            STotalQty = string.Format("{0:#,#}", rows.Cells["total_qty"].Value);
            STotalAmount = string.Format("{0:#,#}", rows.Cells["invoice_total"].Value);
            SInv = rows.Cells["invoice_no"].Value.ToString();
            SDate = rows.Cells["date"].Value.ToString();
        }

        private void GetVouData()
        {
            Sstatus = 2;
            SNewPanel.Visible = true;
            tblSStockCode.DataSource = SelectData();
            txtTotalQty.Text = STotalQty;
            txtTotalAmount.Text = STotalAmount;
            txtInv.Text = SInv;
            txtDate.Text = SDate;
            btnSaveSale.Visible = false;
            lblsave.Visible = false;
            lblsave.Visible = false;
            btnSaveSale.Visible = false;
            btnUpdate.Visible = true;
            lblUpdate.Visible = true;
            tblSale.DataSource = GetSaleList();
        }

        private DataTable GetSaleList()
        {

            DataTable tblSale = new DataTable();


            using (con = new SqlConnection(configString))
            {

                using (cmd = new SqlCommand("SELECT date, invoice_no, total_qty, invoice_total FROM sale", con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    tblSale.Load(reader);
                }

            }

            con.Close();
            return tblSale;

        }

        public DataTable GetStockList()
        {

            DataTable tblStock = new DataTable();
            using (SqlConnection con = new SqlConnection(configString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT sr, code, description, qty, price, amount FROM SaleTemp", con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    tblStock.Load(reader);
                }
            }

            con.Close();
            return tblStock;
        }

        private void ClearBox()
        {
            txtCode.Text = null;
            txtAddQty.Text = null;
            txtDescription.Text = null;
            txtPrice.Text = null;
            txtPTAmount.Text = null;

        }

        public void ClearTable()
        {
            string SQuery = "DELETE FROM SaleTemp";

            con = new SqlConnection(conString);
            con.Open();
            cmd = new SqlCommand(SQuery, con);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        private void SelectCode(DataGridViewCellEventArgs e)
        {
            DataGridViewRow rows = this.tblSStockCode.Rows[e.RowIndex];
            txtCode.Text = rows.Cells["code"].Value.ToString();
            txtDescription.Text = rows.Cells["description"].Value.ToString();
            txtAddQty.Text = rows.Cells["balance"].Value.ToString();
            txtPrice.Text = rows.Cells["price"].Value.ToString();
            txtPTAmount.Text = rows.Cells["amounts"].Value.ToString();
        }

        public void VouSave()
        {
            try
            {
                if (Sstatus == 2)
                {
                    string del_Sql = "DELETE FROM Sale_Stock where invoice_no='" + txtInv.Text + "';" +
                                     "DELETE FROM Sale WHERE invoice_no='" + txtInv.Text + "'";
                    con = new SqlConnection(conString);
                    con.Open();
                    cmd = new SqlCommand(del_Sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                string sql = "INSERT INTO Sale_Stock(sr, code, invoice_no, description, qty, price, amount) SELECT sr, code,'" + txtInv.Text + "',description, qty, price, amount FROM SaleTemp";
                con = new SqlConnection(conString);
                con.Open();

                cmd = new SqlCommand(sql, con);
                int res = cmd.ExecuteNonQuery();

                if (res == 0)
                {
                    MessageBox.Show(
                        "Insert Failed!",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                }
                else
                {
                    string Sql = "INSERT INTO Sale(invoice_no, total_qty, invoice_total, date) SELECT max(invoice_no), sum(qty), sum(amount), max(date) FROM Sale_Stock WHERE invoice_no ='" + txtInv.Text + "'";
                    cmd = new SqlCommand(Sql, con);
                    int ans = cmd.ExecuteNonQuery();

                    if (ans == 0)
                    {
                        MessageBox.Show("Connot Save Data");
                    }
                    else
                    {
                        MessageBox.Show(
                            "Successfully Saved",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                    }

                }

                con.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void Total()
        {

            con = new SqlConnection(conString);
            con.Open();
            string total = "SELECT sum(qty) as 'Tqty', sum(amount) as 'TAmt' FROM SaleTemp";
            cmd = new SqlCommand(total, con);

            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtTotalQty.Text = reader["Tqty"].ToString();
                txtTotalAmount.Text = string.Format("{0:#,#}", reader["TAmt"]);
            }

            con.Close();
        }

        private int SortID()
        {
            string sql = "SELECT MAX(sr) FROM SaleTemp";
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

        private void InvoiceNo()
        {
            try
            {
                string sql = "SELECT MAX(invoice_no) FROM Sale_Stock";
                con = new SqlConnection(conString);
                con.Open();
                cmd = new SqlCommand(sql, con);
                string maxId = cmd.ExecuteScalar() as string;

                if (maxId == null)
                {
                    txtInv.Text = "S00001";
                }
                else
                {
                    int invNo = int.Parse(maxId.Substring(1, 5));
                    invNo++;
                    txtInv.Text = string.Format("S{0:00000}", invNo);
                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private DataTable SelectData()
        {
            DataTable rowStock = new DataTable();
            using (SqlConnection con = new SqlConnection(configString))
            {
                string SQuery = "INSERT INTO SaleTemp(sr, code, description, qty, price, amount) " +
                                "SELECT sr, code, description, qty, price, amount FROM Sale_Stock where invoice_no='" + invoice + "'";
                con.Open();
                cmd = new SqlCommand(SQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();

                using (SqlCommand cmd = new SqlCommand("SELECT sr, code, description, qty, price, amount FROM SaleTemp", con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    rowStock.Load(reader);
                    con.Close();
                }
            }

            return rowStock;
        }

        private void InsertTemp()
        {
            SRID = SortID();
            SCode = txtCode.Text;
            SQty = int.Parse(txtAddQty.Text);
            SDesc = txtDescription.Text;
            SPrice = float.Parse(txtPrice.Text);

            string SQuery = "INSERT INTO SaleTemp(sr, code, description, qty, price, amount) VALUES(@Sr, @Scode, @Sdescription, @Sqty, @Sprice, @Samount)";
            con = new SqlConnection(conString);
            con.Open();

            cmd = new SqlCommand(SQuery, con);
            cmd.Parameters.AddWithValue("@Sr", SRID);
            cmd.Parameters.AddWithValue("@Scode", SCode);
            cmd.Parameters.AddWithValue("@Sdescription", SDesc);
            cmd.Parameters.AddWithValue("@Sqty", SQty);
            cmd.Parameters.AddWithValue("@Sprice", SPrice);
            cmd.Parameters.AddWithValue("@Samount", SAmt);

            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void TotalInvoice()
        {
            con = new SqlConnection(conString);
            con.Open();
            string total = "SELECT sum(total_qty) as 'Tqty', sum(invoice_total) as 'TAmt' FROM Sale";
            cmd = new SqlCommand(total, con);

            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtTQty.Text = reader["Tqty"].ToString();
                txtTAmt.Text = string.Format("{0:#,#}", reader["TAmt"]);
            }

            con.Close();
        }

        private void UpdateStock()
        {
            string sql = "UPDATE SaleTemp SET qty=" + txtAddQty.Text + ", price=" + txtPrice.Text + ", amount=" + txtPTAmount.Text + " WHERE code='"+ txtCode.Text +"'";
            con =new SqlConnection(conString);
            con.Open();
            cmd =new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void DeleteItem()
        {
            string sql = "DELETE FROM SaleTemp WHERE sr=" + SRNO +"";
            con = new SqlConnection(conString);
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            MessageBox.Show(
                "Deleted Successfully!",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
            con.Close();
        }

        private int SelectSR(DataGridViewCellEventArgs e)
        {
            DataGridViewRow rows = this.tblSStockCode.Rows[e.RowIndex];
            string sr = rows.Cells["sr"].Value.ToString();

            return int.Parse(sr);
        }
    }
}


