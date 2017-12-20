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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(btnSignIn, "Sign In");
            toolTip.SetToolTip(btnSignUp, "Sign Up");

            toolTip.SetToolTip(txtLoginName, "member login credentials");
            toolTip.SetToolTip(lblLoginName, "member login credentials");

            toolTip.SetToolTip(txtPassword, "member login password");
            toolTip.SetToolTip(lblPassword, "member login password");
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (txtLoginName.Text.Trim() != string.Empty)
            {
                if (txtLoginName.Text.Trim().Length > 20)
                {
                    MessageBox.Show(lblLoginName.Text + " must be less than 20 characters.", "Invalid " + lblLoginName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtLoginName.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show(lblLoginName.Text + " must not be blank.", "Invalid " + lblLoginName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLoginName.Focus();
                return;
            }

            if (txtPassword.Text.Trim() != string.Empty)
            {
                if (txtPassword.Text.Trim().Length > 20)
                {
                    MessageBox.Show(lblPassword.Text + " must be less than 20 characters.", "Invalid " + lblPassword.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show(lblPassword.Text + " must not be blank.", "Invalid " + lblPassword.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }

            Login objMember =   new Login();

            try
            {
                objMember.Credentials = txtLoginName.Text;
                objMember.Password =    txtPassword.Text;
                bool status = objMember.IsValid();
                if (status)
                {
                    MessageBox.Show("Welcome " + txtLoginName.Text, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(MsgBoxHelper.Selected("Member " + lblLoginName.Text + " " + txtLoginName.Text), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            frmMember   myMember =  new frmMember();
            myMember.Show();
        }
    }
}
