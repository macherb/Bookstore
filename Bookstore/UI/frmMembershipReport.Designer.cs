namespace Bookstore
{
    partial class frmMembershipReport
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.MemberBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.IS253_MACHERDataSet = new Bookstore.IS253_MACHERDataSet();
            //this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.MemberTableAdapter = new Bookstore.IS253_MACHERDataSetTableAdapters.MemberTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.MemberBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IS253_MACHERDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // MemberBindingSource
            // 
            this.MemberBindingSource.DataMember = "Member";
            this.MemberBindingSource.DataSource = this.IS253_MACHERDataSet;
            // 
            // IS253_MACHERDataSet
            // 
            this.IS253_MACHERDataSet.DataSetName = "IS253_MACHERDataSet";
            this.IS253_MACHERDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.MemberBindingSource;
            //this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "Bookstore.UI.MembershipReport.rdlc";
            //this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            //this.reportViewer1.Name = "reportViewer1";
            //this.reportViewer1.Size = new System.Drawing.Size(1200, 374);
            //this.reportViewer1.TabIndex = 0;
            // 
            // MemberTableAdapter
            // 
            this.MemberTableAdapter.ClearBeforeFill = true;
            // 
            // frmMembershipReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1201, 375);
            //this.Controls.Add(this.reportViewer1);
            this.Name = "frmMembershipReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Membership Report";
            this.Load += new System.EventHandler(this.frmMembershipReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MemberBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IS253_MACHERDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource MemberBindingSource;
        private IS253_MACHERDataSet IS253_MACHERDataSet;
        private IS253_MACHERDataSetTableAdapters.MemberTableAdapter MemberTableAdapter;
    }
}
