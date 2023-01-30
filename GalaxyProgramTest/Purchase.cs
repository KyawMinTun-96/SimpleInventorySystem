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
    public partial class Purchase : Form
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        string SCode, SDesc, invoice, STotalQty, STotalAmount, SInv, SDate;
        int SQty;
        float SPrice, SAmt;
        int SRID, SRNO;
        int Sstatus = 0;

        String conString = "Data Source=DESKTOP-B8L8EEK\\SQLEXPRESS;Initial Catalog=ProgramTest;Integrated Security=True";
        String configString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;

        public Purchase()
        {
            InitializeComponent();
            InvoiceNo();
            Total();
            //SelectData();

        }

        private void Purchase_Load(object sender, EventArgs e)
        {
            NewPanel.Visible = false;
            PanelAddDetail.Visible = false;
            tblPurchase.EnableHeadersVisualStyles = false;
            btnUpdate.Visible = false;
            lblUpdate.Visible = false;
            tblPurchase.DataSource = GetPurchaseList();
            tblPurchase.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            TotalInvoice();
        }








        /*-------------------Events---------------------*/

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();
                string searchChar = "SELECT code, description, qty, p_price FROM StockCode WHERE code=@code";
                cmd = new SqlCommand(searchChar, con);
                cmd.Parameters.AddWithValue("@code", txtCode.Text);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtDescription.Text = reader["description"].ToString();
                    //txtAddQty.Text = reader["qty"].ToString();
                    txtAddQty.Text = 1.ToString();
                    txtPrice.Text = reader["p_price"].ToString();
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
                //SPrice = float.Parse(txtPrice.Text);
                SAmt = SQty * SPrice;
                txtPTAmount.Text = SAmt.ToString();
            }
            catch
            {
            }
        }

        private void btnAddPur_Click(object sender, EventArgs e)
        {
            Sstatus = 1;
            NewPanel.Visible = true;
            tblPStockCode.EnableHeadersVisualStyles = false;
            tblPStockCode.DataSource = GetStockList();
            tblPStockCode.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            txtTotalAmount.Text = null;
            txtTotalQty.Text = null;
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            PanelAddDetail.Visible = true;
            btnPUpdate.Visible = false;
            btnAdd.Visible = true;
            txtAddDate.Text = txtDate.Text;
            txtInvoiceNo.Text = txtInv.Text;
        }

        private void NewPanel_Click(object sender, EventArgs e)
        {
            PanelAddDetail.Visible = false;
            ClearBox();
        }

        private void btnSavePur_Click(object sender, EventArgs e)
        {
            PanelAddDetail.Visible = false;
            VouSave();
            txtTotalAmount.Text = null;
            txtTotalQty.Text = null;
            ClearTable();
            InvoiceNo();
            tblPStockCode.DataSource = GetStockList();
            ClearBox(); ClearBox();
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
                    tblPStockCode.DataSource = GetStockList();
                    Total();
                    ClearBox();
                }
                else if (Sstatus == 2)
                {
                    InsertTemp();
                    Total();
                    tblPStockCode.DataSource = GetStockList();
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
            if (Sstatus == 1)
            {
                string sql = "SELECT COUNT(*) FROM PurTemp";
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
                        NewPanel.Visible = false;
                        tblPurchase.DataSource = GetPurchaseList();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        ClearTable();
                        NewPanel.Visible = false;
                        tblPurchase.DataSource = GetPurchaseList();
                    }
                }
                else
                {
                    ClearTable(); 
                    NewPanel.Visible = false;
                    ClearBox();
                    tblPurchase.DataSource = GetPurchaseList();
                    PanelAddDetail.Visible = false;
                }
            }
            else if (Sstatus == 2)
            {
                con = new SqlConnection(conString);
                con.Open();
                string tmp = "SELECT COUNT(*) FROM Pur_Stock where invoice_no='" + invoice + "'";
                string sql = "SELECT COUNT(*) FROM PurTemp";

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
                        NewPanel.Visible = false;
                        tblPurchase.DataSource = GetPurchaseList();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        ClearTable();
                        NewPanel.Visible = false;
                        tblPurchase.DataSource = GetPurchaseList();
                    }
                }
                else if (tmpCount == DetCount)
                {
                    ClearTable();
                    NewPanel.Visible = false;
                    tblPurchase.DataSource = GetPurchaseList();
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Save Change?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        VouSave();
                        txtTotalAmount.Text = null;
                        txtTotalQty.Text = null;
                        ClearTable();
                        PanelAddDetail.Visible = false;
                        tblPurchase.DataSource = GetPurchaseList();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        ClearTable();
                        PanelAddDetail.Visible = false;
                        tblPurchase.DataSource = GetPurchaseList();
                    }
                }

            }
            con.Close();
        }

        private void tblPurchase_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectRow(e);
            GetVouData();
        }

        private void tblPurchase_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectRow(e);
            }
        }

        private void btnPurEdit_Click(object sender, EventArgs e)
        {
            GetVouData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            VouSave();
            ClearTable();
            NewPanel.Visible = false;
            tblPurchase.DataSource = GetPurchaseList();
            TotalInvoice();

        }
               
        private void tblPStockCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {

                DialogResult dialogResult = MessageBox.Show("Delete this item?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    DeleteItem();
                    tblPStockCode.DataSource = GetStockList();
                    Total();
                }
                else if (dialogResult == DialogResult.No)
                {
                    tblPStockCode.DataSource = GetStockList();
                }
            }
        }

        private void tblPStockCode_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PanelAddDetail.Visible = true;
            btnPUpdate.Visible = true;
            btnAdd.Visible = false;
            txtAddDate.Text = txtDate.Text;
            txtInvoiceNo.Text = txtInv.Text;
            SelectCode(e);
        }

        private void tblPStockCode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SRNO = SelectSR(e);
        }

        private void btnPUpdate_Click(object sender, EventArgs e)
        {
            UpdateStock();
            tblPStockCode.DataSource = GetStockList();
            Total();
            ClearBox();
            PanelAddDetail.Visible = false;
        }






        /*-------------------Method---------------------*/

        private void SelectRow(DataGridViewCellEventArgs e)
        {
            DataGridViewRow rows = this.tblPurchase.Rows[e.RowIndex];
            invoice = rows.Cells["invoice_no"].Value.ToString();
            STotalQty = string.Format("{0:#,#}", rows.Cells["total_qty"].Value);
            STotalAmount = string.Format("{0:#,#}", rows.Cells["invoice_total"].Value);
            SInv = rows.Cells["invoice_no"].Value.ToString();
            SDate = rows.Cells["date"].Value.ToString();
        }

        private void GetVouData()
        {
            Sstatus = 2;
            NewPanel.Visible = true;
            tblPStockCode.DataSource = SelectData();
            txtTotalQty.Text = STotalQty;
            txtTotalAmount.Text = STotalAmount;
            txtInv.Text = SInv;
            txtDate.Text = SDate;
            btnSavePur.Visible = false;
            lblsave.Visible = false;
            lblsave.Visible = false;
            btnSavePur.Visible = false;
            btnUpdate.Visible = true;
            lblUpdate.Visible = true;
            tblPurchase.DataSource = GetPurchaseList();
        }

        private DataTable GetPurchaseList()
        {

            DataTable tblPur = new DataTable();


            using (con = new SqlConnection(configString))
            {

                using (cmd = new SqlCommand("SELECT date, invoice_no, total_qty, invoice_total FROM purchase", con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    tblPur.Load(reader);
                }

            }

            con.Close();
            return tblPur;

        }

        public DataTable GetStockList()
        {

            DataTable tblStock = new DataTable();
            using (SqlConnection con = new SqlConnection(configString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT sr, code, description, qty, price, amount FROM PurTemp", con))
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
            string SQuery = "DELETE FROM PurTemp";

            con = new SqlConnection(conString);
            con.Open();
            cmd = new SqlCommand(SQuery, con);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        private void SelectCode(DataGridViewCellEventArgs e)
        {
            DataGridViewRow rows = this.tblPStockCode.Rows[e.RowIndex];
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
                    string del_Sql = "DELETE FROM Pur_Stock where invoice_no='" + txtInv.Text + "';" +
                                     "DELETE FROM Purchase WHERE invoice_no='" + txtInv.Text + "'";
                    con = new SqlConnection(conString);
                    con.Open();
                    cmd = new SqlCommand(del_Sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                string sql = "INSERT INTO Pur_Stock(sr, code, invoice_no, description, qty, price, amount) SELECT sr, code,'" + txtInv.Text + "',description, qty, price, amount FROM PurTemp";
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
                    string Sql = "INSERT INTO Purchase(invoice_no, total_qty, invoice_total, date) SELECT max(invoice_no), sum(qty), sum(amount), max(date) FROM Pur_Stock WHERE invoice_no ='" + txtInv.Text + "'";
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
            string total = "SELECT sum(qty) as 'Tqty', sum(amount) as 'TAmt' FROM PurTemp";
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
            string sql = "SELECT MAX(sr) FROM PurTemp";
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
                string sql = "SELECT MAX(invoice_no) FROM Pur_Stock";
                con = new SqlConnection(conString);
                con.Open();
                cmd = new SqlCommand(sql, con);
                string maxId = cmd.ExecuteScalar() as string;

                if (maxId == null)
                {
                    txtInv.Text = "P00001";
                }
                else
                {
                    int invNo = int.Parse(maxId.Substring(1, 5));
                    invNo++;
                    txtInv.Text = string.Format("P{0:00000}", invNo);
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
                string SQuery = "INSERT INTO PurTemp(sr, code, description, qty, price, amount) " +
                                "SELECT sr, code, description, qty, price, amount FROM Pur_Stock where invoice_no='" + invoice + "'";
                con.Open();
                cmd = new SqlCommand(SQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();

                using (SqlCommand cmd = new SqlCommand("SELECT sr, code, description, qty, price, amount FROM PurTemp", con))
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

            string SQuery = "INSERT INTO PurTemp(sr, code, description, qty, price, amount) VALUES(@Sr, @Scode, @Sdescription, @Sqty, @Sprice, @Samount)";
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
            string total = "SELECT sum(total_qty) as 'Tqty', sum(invoice_total) as 'TAmt' FROM Purchase";
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
            string sql = "UPDATE PurTemp SET qty=" + txtAddQty.Text + ", price=" + txtPrice.Text + ", amount=" + txtPTAmount.Text + " WHERE code='" + txtCode.Text + "'";
            con = new SqlConnection(conString);
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void DeleteItem()
        {
            string sql = "DELETE FROM PurTemp WHERE sr=" + SRNO + "";
            con = new SqlConnection(conString);
            con.Open();
            cmd = new SqlCommand(sql, con);
            int ans = cmd.ExecuteNonQuery();

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
            DataGridViewRow rows = this.tblPStockCode.Rows[e.RowIndex];
            string sr = rows.Cells["sr"].Value.ToString();

            return int.Parse(sr);
        }
    }
}


