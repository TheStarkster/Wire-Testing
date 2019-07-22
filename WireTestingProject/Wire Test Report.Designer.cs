namespace WireTestingProject
{
    partial class Wire_Test_Report
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.caliberReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.electricalReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.chemicalReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mechanicalReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.thermalReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.caliberReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.electricalReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chemicalReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mechanicalReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thermalReportBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.AutoSize = true;
            reportDataSource1.Name = "CaliberDataSet";
            reportDataSource1.Value = this.caliberReportBindingSource;
            reportDataSource2.Name = "ElectricalDataSet";
            reportDataSource2.Value = this.electricalReportBindingSource;
            reportDataSource3.Name = "ChemicalDataSet";
            reportDataSource3.Value = this.chemicalReportBindingSource;
            reportDataSource4.Name = "MechanicalDataSet";
            reportDataSource4.Value = this.mechanicalReportBindingSource;
            reportDataSource5.Name = "ThermalDataSet";
            reportDataSource5.Value = this.thermalReportBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "WireTestingProject.ReportTest.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(15, 11);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1393, 874);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // Wire_Test_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1423, 899);
            this.Controls.Add(this.reportViewer1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Wire_Test_Report";
            this.Text = "Wire_Test_Report";
            this.Load += new System.EventHandler(this.Wire_Test_Report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.caliberReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.electricalReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chemicalReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mechanicalReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thermalReportBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource caliberReportBindingSource;
        private System.Windows.Forms.BindingSource electricalReportBindingSource;
        private System.Windows.Forms.BindingSource chemicalReportBindingSource;
        private System.Windows.Forms.BindingSource mechanicalReportBindingSource;
        private System.Windows.Forms.BindingSource thermalReportBindingSource;
    }
}