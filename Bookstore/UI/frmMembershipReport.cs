using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookstore
{
    public partial class frmMembershipReport : Form
    {
        public frmMembershipReport()
        {
            InitializeComponent();
        }

        private void frmMembershipReport_Load(object sender, EventArgs e)
        {
			try
			{
				// TODO: This line of code loads data into the 'IS253_MACHERDataSet.Member' table. You can move, or remove it, as needed.
				this.MemberTableAdapter.Fill(this.IS253_MACHERDataSet.Member);

				//this.reportViewer1.RefreshReport();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
    }
}
