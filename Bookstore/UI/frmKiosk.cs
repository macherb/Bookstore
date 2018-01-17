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
    public partial class frmKiosk : Form
    {
        public frmKiosk()
        {
            InitializeComponent();
        }

        private void frmKiosk_Load(object sender, EventArgs e)
        {
            frmLogin    myLogin =   new frmLogin();
            if (myLogin.ShowDialog() > DialogResult.None)
                this.WindowState =  System.Windows.Forms.FormWindowState.Normal;
        }

        private void movieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMovie    myMovie =   new frmMovie();
            myMovie.Show();
        }

        private void memberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMember   myMember =  new frmMember();
            myMember.Show();
        }

        private void rentalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRental   myRental =  new frmRental();
            myRental.Show();
        }

        private void genreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGenre    myGenre =   new frmGenre();
            myGenre.Show();
        }

        private void vendorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVendor   myVendor =  new frmVendor();
            myVendor.Show();
        }

        private void reportMembershipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMembershipReport myReport =  new frmMembershipReport();
            myReport.Show();
        }

        private void reportRentalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRentalReport     myReport =  new frmRentalReport();
            myReport.Show();
        }
    }
}
