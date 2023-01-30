using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace GalaxyProgramTest
{
    public partial class Report : Form
    {
        String configString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        public Report()
        {
            InitializeComponent();
        }

        /*-------------------My Method---------------------*/
        private void RPT_Viewer_Load(object sender, EventArgs e)
        {
            
            //this.RPT_ViewerS.RefreshReport();
            //this.RPT_ViewerP.RefreshReport();
        }

        private void Report_Load(object sender, EventArgs e)
        {

            this.Sale_StockTableAdapter.Fill(this.DSS.Sale_Stock);
            this.Pur_StockTableAdapter.Fill(this.DSS.Pur_Stock);

        }


        private void lblSReport_MouseClick(object sender, MouseEventArgs e)
        {
            RPT_ViewerP.Visible = false;
            lblSReport.ForeColor = Color.IndianRed;
            this.RPT_ViewerS.RefreshReport();
            RPT_ViewerS.Visible = true; 

        }

        private void lblPReport_MouseClick(object sender, MouseEventArgs e)
        {

            RPT_ViewerS.Visible = false;
            lblPReport.ForeColor = Color.IndianRed;
            this.RPT_ViewerP.RefreshReport();
            RPT_ViewerP.Visible = true;

        }
    }
}
