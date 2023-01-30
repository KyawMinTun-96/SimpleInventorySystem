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
using GalaxyProgramTest;


namespace GalaxyProgramTest
{
    public partial class Form1 : Form
    {

        
        public Form1()
        {
            InitializeComponent();
        }

        GalaxyProgramTest.Sale Sl = new Sale();
        GalaxyProgramTest.Purchase Pc = new Purchase();



        public void formLoad(object Form)
        {            
            if (this.mainPanel.Controls.Count > 0)
                this.mainPanel.Controls.RemoveAt(0);

            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainPanel.Controls.Add(f);
            this.mainPanel.Tag = f;
            f.Show();
        }



        /*--------------------------Form Load Event----------------------------*/
        private void saleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formLoad(new Sale());
            lbTitle.Text = "Sale";
            Sl.ClearTable();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
           formLoad(new Purchase());
            lbTitle.Text = "Purchase";
            Pc.ClearTable();
        }

        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formLoad(new Setup());
            lbTitle.Text = "Setup";
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formLoad(new Report());
            lbTitle.Text = "Report";
        }




        /*--------------------------Btn Click----------------------------*/
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Minimized;
            }
            else if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            //if(WindowState == FormWindowState.Maximized)
            //{
            //    //WindowState = FormWindowState.Normal;
            //}
            //else if(WindowState == FormWindowState.Normal)
            //{
            //    //WindowState = FormWindowState.Maximized;
            //}
        }
    }
}
