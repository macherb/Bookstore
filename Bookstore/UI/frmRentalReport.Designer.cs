namespace Bookstore
{
    partial class frmRentalReport
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
            this.RentalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.IS253_MACHERDataSet1 = new Bookstore.IS253_MACHERDataSet1();
            //this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();//TODO file not found
            this.RentalTableAdapter = new Bookstore.IS253_MACHERDataSet1TableAdapters.RentalTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.RentalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IS253_MACHERDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // RentalBindingSource
            // 
            this.RentalBindingSource.DataMember = "Rental";
            this.RentalBindingSource.DataSource = this.IS253_MACHERDataSet1;
            // 
            // IS253_MACHERDataSet1
            // 
            this.IS253_MACHERDataSet1.DataSetName = "IS253_MACHERDataSet1";
            this.IS253_MACHERDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.RentalBindingSource;
            //this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "Bookstore.UI.RentalReport.rdlc";
            //this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            //this.reportViewer1.Name = "reportViewer1";
            //this.reportViewer1.Size = new System.Drawing.Size(640, 365);
            //this.reportViewer1.TabIndex = 0;
            // 
            // RentalTableAdapter
            // 
            this.RentalTableAdapter.ClearBeforeFill = true;
            // 
            // frmRentalReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 366);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmRentalReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rental Report";
            this.Load += new System.EventHandler(this.frmRentalReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RentalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IS253_MACHERDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource RentalBindingSource;
        private IS253_MACHERDataSet1 IS253_MACHERDataSet1;
        private IS253_MACHERDataSet1TableAdapters.RentalTableAdapter RentalTableAdapter;
    }
}
