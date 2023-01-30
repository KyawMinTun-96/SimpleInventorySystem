namespace GalaxyProgramTest
{
    partial class Report
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.Pur_StockBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DSS = new GalaxyProgramTest.DSS();
            this.Sale_StockBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.saleStockBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.RptPanel = new System.Windows.Forms.Panel();
            this.lblPReport = new System.Windows.Forms.Label();
            this.lblSReport = new System.Windows.Forms.Label();
            this.RptView = new System.Windows.Forms.Panel();
            this.RPT_ViewerS = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Sale_StockTableAdapter = new GalaxyProgramTest.DSSTableAdapters.Sale_StockTableAdapter();
            this.Pur_StockTableAdapter = new GalaxyProgramTest.DSSTableAdapters.Pur_StockTableAdapter();
            this.RPT_ViewerP = new Microsoft.Reporting.WinForms.ReportViewer();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Pur_StockBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DSS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sale_StockBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saleStockBindingSource)).BeginInit();
            this.RptPanel.SuspendLayout();
            this.RptView.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pur_StockBindingSource
            // 
            this.Pur_StockBindingSource.DataMember = "Pur_Stock";
            this.Pur_StockBindingSource.DataSource = this.DSS;
            // 
            // DSS
            // 
            this.DSS.DataSetName = "DSS";
            this.DSS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Sale_StockBindingSource
            // 
            this.Sale_StockBindingSource.DataMember = "Sale_Stock";
            this.Sale_StockBindingSource.DataSource = this.DSS;
            // 
            // RptPanel
            // 
            this.RptPanel.BackColor = System.Drawing.Color.AliceBlue;
            this.RptPanel.Controls.Add(this.lblTitle);
            this.RptPanel.Controls.Add(this.lblPReport);
            this.RptPanel.Controls.Add(this.lblSReport);
            this.RptPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.RptPanel.Location = new System.Drawing.Point(0, 0);
            this.RptPanel.Name = "RptPanel";
            this.RptPanel.Size = new System.Drawing.Size(349, 713);
            this.RptPanel.TabIndex = 0;
            // 
            // lblPReport
            // 
            this.lblPReport.AutoSize = true;
            this.lblPReport.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPReport.Location = new System.Drawing.Point(61, 230);
            this.lblPReport.Name = "lblPReport";
            this.lblPReport.Size = new System.Drawing.Size(174, 29);
            this.lblPReport.TabIndex = 2;
            this.lblPReport.Text = "Purchase Report";
            this.lblPReport.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblPReport_MouseClick);
            // 
            // lblSReport
            // 
            this.lblSReport.AutoSize = true;
            this.lblSReport.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSReport.Location = new System.Drawing.Point(61, 171);
            this.lblSReport.Name = "lblSReport";
            this.lblSReport.Size = new System.Drawing.Size(126, 29);
            this.lblSReport.TabIndex = 1;
            this.lblSReport.Text = "Sale Report";
            this.lblSReport.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblSReport_MouseClick);
            // 
            // RptView
            // 
            this.RptView.BackColor = System.Drawing.Color.LightGray;
            this.RptView.Controls.Add(this.RPT_ViewerP);
            this.RptView.Controls.Add(this.RPT_ViewerS);
            this.RptView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RptView.Location = new System.Drawing.Point(349, 0);
            this.RptView.Name = "RptView";
            this.RptView.Size = new System.Drawing.Size(1571, 713);
            this.RptView.TabIndex = 1;
            // 
            // RPT_ViewerS
            // 
            this.RPT_ViewerS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource3.Name = "Sale_Stock";
            reportDataSource3.Value = this.Sale_StockBindingSource;
            this.RPT_ViewerS.LocalReport.DataSources.Add(reportDataSource3);
            this.RPT_ViewerS.LocalReport.ReportEmbeddedResource = "GalaxyProgramTest.rpt_sreportrdlc.rdlc";
            this.RPT_ViewerS.Location = new System.Drawing.Point(209, 0);
            this.RPT_ViewerS.Name = "RPT_ViewerS";
            this.RPT_ViewerS.ServerReport.BearerToken = null;
            this.RPT_ViewerS.Size = new System.Drawing.Size(1113, 713);
            this.RPT_ViewerS.TabIndex = 0;
            this.RPT_ViewerS.Load += new System.EventHandler(this.RPT_Viewer_Load);
            // 
            // Sale_StockTableAdapter
            // 
            this.Sale_StockTableAdapter.ClearBeforeFill = true;
            // 
            // Pur_StockTableAdapter
            // 
            this.Pur_StockTableAdapter.ClearBeforeFill = true;
            // 
            // RPT_ViewerP
            // 
            this.RPT_ViewerP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource4.Name = "Pur_Stock";
            reportDataSource4.Value = this.Pur_StockBindingSource;
            this.RPT_ViewerP.LocalReport.DataSources.Add(reportDataSource4);
            this.RPT_ViewerP.LocalReport.ReportEmbeddedResource = "GalaxyProgramTest.rpt_preport.rdlc";
            this.RPT_ViewerP.Location = new System.Drawing.Point(209, 0);
            this.RPT_ViewerP.Name = "RPT_ViewerP";
            this.RPT_ViewerP.ServerReport.BearerToken = null;
            this.RPT_ViewerP.Size = new System.Drawing.Size(1113, 713);
            this.RPT_ViewerP.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Cambria", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(59, 57);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(133, 37);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Reports";
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1920, 713);
            this.Controls.Add(this.RptView);
            this.Controls.Add(this.RptPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Report";
            this.Text = "Report";
            this.Load += new System.EventHandler(this.Report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Pur_StockBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DSS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sale_StockBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saleStockBindingSource)).EndInit();
            this.RptPanel.ResumeLayout(false);
            this.RptPanel.PerformLayout();
            this.RptView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel RptPanel;
        private System.Windows.Forms.Panel RptView;
        private System.Windows.Forms.Label lblPReport;
        private System.Windows.Forms.Label lblSReport;
        private Microsoft.Reporting.WinForms.ReportViewer RPT_ViewerS;
        private System.Windows.Forms.BindingSource saleStockBindingSource;
        private System.Windows.Forms.BindingSource Sale_StockBindingSource;
        private DSS DSS;
        private DSSTableAdapters.Sale_StockTableAdapter Sale_StockTableAdapter;
        private System.Windows.Forms.BindingSource Pur_StockBindingSource;
        private DSSTableAdapters.Pur_StockTableAdapter Pur_StockTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer RPT_ViewerP;
        private System.Windows.Forms.Label lblTitle;
    }
}