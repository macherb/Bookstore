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
    public partial class frmMember : Form, Iuser_interface
    {
        //Members memberList;
        string  imageLocation;

        public frmMember()
        {
            //memberList =    new Members();
            imageLocation = string.Empty;

            InitializeComponent();
        }

        private void frmMember_Load(object sender, EventArgs e)
        {
			try
			{
	            List<Member>                    memberList =    Members.GetMembers();
		        memberDataGridView.DataSource =                 memberList;

			    List<Subscription>              subscriptionList =  Subscriptions.GetSubscriptions();
				cmbSubscriptionID.DataSource =                      subscriptionList;
				cmbSubscriptionID.DisplayMember =                   "name";
			    cmbSubscriptionID.ValueMember =                     "id";
		        cmbSubscriptionID.SelectedIndex =                   -1;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(btnAdd, "Add all Member fields");
            toolTip.SetToolTip(btnBrowse, "Browse by Member Number");
            toolTip.SetToolTip(btnUpdate, "Update all Member fields");
            toolTip.SetToolTip(btnDelete, "Delete Member Number");
            toolTip.SetToolTip(btnClear, "Clear all Member fields");

            toolTip.SetToolTip(txtNumber, "customer or member unique number");
            toolTip.SetToolTip(lblNumber, "customer or member unique number");

            toolTip.SetToolTip(dtpJoinDate, "date in which the member join club");
            toolTip.SetToolTip(lblJoinDate, "date in which the member join club");

            toolTip.SetToolTip(txtFirstName, "first name of the member");
            toolTip.SetToolTip(lblFirstName, "first name of the member");

            toolTip.SetToolTip(txtLastName, "last name of the member");
            toolTip.SetToolTip(lblLastName, "last name of the member");

            toolTip.SetToolTip(txtAddress, "address of the member");
            toolTip.SetToolTip(lblAddress, "address of the member");

            toolTip.SetToolTip(txtCity, "city where the member resides");
            toolTip.SetToolTip(lblCity, "city where the member resides");

            toolTip.SetToolTip(txtState, "state where the member resides");
            toolTip.SetToolTip(lblState, "state where the member resides");

            toolTip.SetToolTip(txtZipCode, "zipcode of the member");
            toolTip.SetToolTip(lblZipCode, "zipcode of the member");

            toolTip.SetToolTip(txtPhone, "daytime phone number of the member");
            toolTip.SetToolTip(lblPhone, "daytime phone number of the member");


            toolTip.SetToolTip(txtLoginName, "member login credentials");
            toolTip.SetToolTip(lblLoginName, "member login credentials");

            toolTip.SetToolTip(txtPassword, "member login password");
            toolTip.SetToolTip(lblPassword, "member login password");

            toolTip.SetToolTip(txtEmail, "member email address");
            toolTip.SetToolTip(lblEmail, "member email address");

            toolTip.SetToolTip(grpContactMethod, "How does the member preferred to be contacted");

            toolTip.SetToolTip(cmbSubscriptionID, "Member subscription type");
            toolTip.SetToolTip(lblSubscriptionID, "Member subscription type");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckAll())
            {
                Member              objMember = new Member();

                int                 number;
                Int32.TryParse(txtNumber.Text.Trim(), out number);
                objMember.number =              number;
                objMember.joindate =            dtpJoinDate.Value;
                objMember.firstname =           txtFirstName.Text.Trim();
                objMember.lastname =            txtLastName.Text.Trim();
                objMember.address =             txtAddress.Text.Trim();
                objMember.city =                txtCity.Text.Trim();
                objMember.state =               txtState.Text.Trim();
                objMember.zipcode =             txtZipCode.Text.Trim();
                objMember.phone =               txtPhone.Text.Trim();
                objMember.member_status =       rdoActive.Checked ? "A" : "I";
                objMember.login_name =          txtLoginName.Text.Trim();
                objMember.password =            txtPassword.Text.Trim();
                objMember.email =               txtEmail.Text.Trim();
                if      (rdoEmail.Checked       )
                    objMember.contact_method =  1;
                else if (rdoFacebook.Checked    )
                    objMember.contact_method =  2;
                else if (rdoPhoneText.Checked   )
                    objMember.contact_method =  3;
                else if (rdoTwitter.Checked     )
                    objMember.contact_method =  4;
                else
                    objMember.contact_method =  0;

                objMember.subscription_id =     (int)cmbSubscriptionID.SelectedValue;
                objMember.photo =               imageLocation;
                try
                {
                    bool            status =    Members.AddMember(objMember);
                    if (status)
                    {
                        MessageBox.Show(MsgBoxHelper.Inserted("Member"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmMember_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show(MsgBoxHelper.Inserted("Member not"), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            int number;
            if (!Int32.TryParse(txtNumber.Text.Trim(), out number))
            {
                MessageBox.Show(lblNumber.Text + " must be an integer (" + short.MinValue + " - " + short.MaxValue + ").", "Invalid " + lblNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNumber.Focus();
            }
            else
            {
                Member                  objMember;

                try
                {
                    objMember =                     Members.GetMember(number);
                    if (objMember == null)
                    {
                        MessageBox.Show(MsgBoxHelper.Selected("Member " + lblNumber.Text + " " + number), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dtpJoinDate.Value =         objMember.joindate;
                        txtFirstName.Text =         objMember.firstname.Trim();
                        txtLastName.Text =          objMember.lastname.Trim();
                        txtAddress.Text =           objMember.address.Trim();
                        txtCity.Text =              objMember.city.Trim();
                        txtState.Text =             objMember.state;
                        txtZipCode.Text =           objMember.zipcode;
                        txtPhone.Text =             objMember.phone;
                        if (objMember.member_status == "A")
                            rdoActive.Checked =     true;
                        else
                            rdoInactive.Checked =   true;
                        txtLoginName.Text =         objMember.login_name;
                        txtPassword.Text =          objMember.password;
                        txtEmail.Text =             objMember.email;

                        if      (objMember.contact_method == 1)
                            rdoEmail.Checked =      true;
                        else if (objMember.contact_method == 2)
                            rdoFacebook.Checked =   true;
                        else if (objMember.contact_method == 3)
                            rdoPhoneText.Checked =  true;
                        else if (objMember.contact_method == 4)
                            rdoTwitter.Checked =    true;
                        else
                            objMember.contact_method =  0;

                        cmbSubscriptionID.SelectedValue = objMember.subscription_id;
                        imageLocation =             objMember.photo;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (CheckAll())
            {
                Member                  objMember = new Member();

                int number;
                Int32.TryParse(txtNumber.Text.Trim(), out number);
                objMember.number =                  number;
                objMember.joindate =                dtpJoinDate.Value;
                objMember.firstname =               txtFirstName.Text.Trim();
                objMember.lastname =                txtLastName.Text.Trim();
                objMember.address =                 txtAddress.Text.Trim();
                objMember.city =                    txtCity.Text.Trim();
                objMember.state =                   txtState.Text.Trim();
                objMember.zipcode =                 txtZipCode.Text.Trim();
                objMember.phone =                   txtPhone.Text.Trim();
                objMember.member_status =           rdoActive.Checked ? "A" : "I";
                objMember.login_name =              txtLoginName.Text.Trim();
                objMember.password =                txtPassword.Text.Trim();
                objMember.email =                   txtEmail.Text.Trim();

                if      (rdoEmail.Checked       )
                    objMember.contact_method =      1;
                else if (rdoFacebook.Checked    )
                    objMember.contact_method =      2;
                else if (rdoPhoneText.Checked   )
                    objMember.contact_method =      3;
                else if (rdoTwitter.Checked     )
                    objMember.contact_method =      4;
                else
                    objMember.contact_method =      0;

                objMember.subscription_id =         (int)cmbSubscriptionID.SelectedValue;
                objMember.photo =                   imageLocation;
                try
                {
                    bool                status =    Members.UpdateMember(objMember);
                    if (status)
                    {
                        MessageBox.Show(MsgBoxHelper.Updated("Member"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmMember_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show(MsgBoxHelper.Updated("Member not"), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int number;
            if (!Int32.TryParse(txtNumber.Text.Trim(), out number))
            {
                MessageBox.Show(lblNumber.Text + " must be an integer (" + short.MinValue + " - " + short.MaxValue + ").", "Invalid " + lblNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNumber.Focus();
            }
            else
            {
                Member                  objMember = new Member();
                objMember.number =                  number;
                try
                {
                    bool                status =    Members.DeleteMember(objMember);
                    if (status)
                    {
                        MessageBox.Show(MsgBoxHelper.Deleted("Member"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmMember_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show(MsgBoxHelper.Deleted("Member not"), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtNumber.Text =        string.Empty;
            dtpJoinDate.Value =     new DateTime(1753, 1, 1, 0, 0, 0);
            txtFirstName.Text =     string.Empty;
            txtLastName.Text =      string.Empty;
            txtAddress.Text =       string.Empty;
            txtCity.Text =          string.Empty;
            txtState.Text =         string.Empty;
            txtZipCode.Text =       string.Empty;
            txtPhone.Text =         string.Empty;
            rdoActive.Checked =     false;
            rdoInactive.Checked =   false;
            txtLoginName.Text =     string.Empty;
            txtPassword.Text =      string.Empty;
            txtEmail.Text =         string.Empty;
            cmbSubscriptionID.SelectedIndex = -1;
            rdoEmail.Checked =      false;
            rdoFacebook.Checked =   false;
            rdoPhoneText.Checked =  false;
            rdoTwitter.Checked =    false;
            picPhoto.ImageLocation =    string.Empty;//"https://avatars2.githubusercontent.com/u/10159754?s=460&v=4";
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Functionality not implemented.", "Functionality not implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //imageLocation = ;
        }

        public bool CheckAll()
        {
            int number;
            if (Int32.TryParse(txtNumber.Text.Trim(), out number))
            {
                if      (number > short.MaxValue)// 32767
                {
                    MessageBox.Show(lblNumber.Text + " must be less than or equal to " + short.MaxValue + ".", "Invalid " + lblNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNumber.Focus();
                    return  false;
                }
                else if (number < short.MinValue)//-32768
                {
                    MessageBox.Show(lblNumber.Text + " must be greater than or equal to " + short.MinValue + ".", "Invalid " + lblNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNumber.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(lblNumber.Text + " must be an integer (" + short.MinValue + " - " + short.MaxValue + ").", "Invalid " + lblNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNumber.Focus();
                return  false;
            }

            if      (dtpJoinDate.Value >= DateTime.MaxValue)//
            {
                MessageBox.Show(lblJoinDate.Text + " must be less than or equal to " + DateTime.MaxValue + ".", "Invalid " + lblJoinDate.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpJoinDate.Focus();
                return  false;
            }
            else if (dtpJoinDate.Value <= DateTime.MinValue)//
            {
                MessageBox.Show(lblJoinDate.Text + " must be greater than or equal to " + DateTime.MinValue + ".", "Invalid " + lblJoinDate.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpJoinDate.Focus();
                return  false;
            }

            if (txtFirstName.Text.Trim() != string.Empty)
            {
                if (txtFirstName.Text.Trim().Length > 15)
                {
                    MessageBox.Show(lblFirstName.Text + " must be less than 15 characters.", "Invalid " + lblFirstName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFirstName.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(lblFirstName.Text + " must not be blank.", "Invalid " + lblFirstName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Focus();
                return  false;
            }

            if (txtLastName.Text.Trim() != string.Empty)
            {
                if (txtLastName.Text.Trim().Length > 25)
                {
                    MessageBox.Show(lblLastName.Text + " must be less than 25 characters.", "Invalid " + lblLastName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtLastName.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(lblLastName.Text + " must not be blank.", "Invalid " + lblLastName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLastName.Focus();
                return  false;
            }

            if (txtAddress.Text.Trim() != string.Empty)
            {
                if (txtAddress.Text.Trim().Length > 30)
                {
                    MessageBox.Show(lblAddress.Text + " must be less than 30 characters.", "Invalid " + lblAddress.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAddress.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(lblAddress.Text + " must not be blank.", "Invalid " + lblAddress.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return  false;
            }

            if (txtCity.Text.Trim() != string.Empty)
            {
                if (txtCity.Text.Trim().Length > 20)
                {
                    MessageBox.Show(lblCity.Text + " must be less than 20 characters.", "Invalid " + lblCity.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCity.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(lblCity.Text + " must not be blank.", "Invalid " + lblCity.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCity.Focus();
                return  false;
            }

            if (txtState.Text.Trim() != string.Empty)
            {
                if (txtState.Text.Trim().Length > 2)
                {
                    MessageBox.Show(lblState.Text + " must be less than 2 characters.", "Invalid " + lblState.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtState.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(lblState.Text + " must not be blank.", "Invalid " + lblState.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtState.Focus();
                return  false;
            }

            if (txtZipCode.Text.Trim() != string.Empty)
            {
                if (txtZipCode.Text.Trim().Length > 5)
                {
                    MessageBox.Show(lblZipCode.Text + " must be less than 5 characters.", "Invalid " + lblZipCode.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtZipCode.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(lblZipCode.Text + " must not be blank.", "Invalid " + lblZipCode.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtZipCode.Focus();
                return  false;
            }

            if (txtPhone.Text.Trim() != string.Empty)
            {
                if (txtPhone.Text.Trim().Length > 10)
                {
                    MessageBox.Show(lblPhone.Text + " must be less than 10 characters.", "Invalid " + lblPhone.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhone.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(lblPhone.Text + " must not be blank.", "Invalid " + lblPhone.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhone.Focus();
                return  false;
            }

            if (!rdoActive.Checked && !rdoInactive.Checked)
            {
                MessageBox.Show(grpMemberStatus.Text + " must be " + rdoActive.Text + " or " + rdoInactive.Text + ".",
                                "Invalid " + grpMemberStatus.Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                               );
                return  false;
            }

            if (txtLoginName.Text.Trim() != string.Empty)
            {
                if (txtLoginName.Text.Trim().Length > 20)
                {
                    MessageBox.Show(lblLoginName.Text + " must be less than 20 characters.", "Invalid " + lblLoginName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtLoginName.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(lblLoginName.Text + " must not be blank.", "Invalid " + lblLoginName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLoginName.Focus();
                return  false;
            }

            if (txtPassword.Text.Trim() != string.Empty)
            {
                if (txtPassword.Text.Trim().Length > 20)
                {
                    MessageBox.Show(lblPassword.Text + " must be less than 20 characters.", "Invalid " + lblPassword.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(lblPassword.Text + " must not be blank.", "Invalid " + lblPassword.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return  false;
            }

            if (txtEmail.Text.Trim() != string.Empty)
            {
                if (txtEmail.Text.Trim().Length > 20)
                {
                    MessageBox.Show(lblEmail.Text + " must be less than 20 characters.", "Invalid " + lblEmail.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(lblEmail.Text + " must not be blank.", "Invalid " + lblEmail.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return  false;
            }

            if (cmbSubscriptionID.SelectedIndex == -1)
            {
                MessageBox.Show(lblSubscriptionID.Text + " must not be blank.", "Invalid " + lblSubscriptionID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSubscriptionID.Focus();
                return false;
            }

            if (!rdoEmail.Checked && !rdoFacebook.Checked && !rdoPhoneText.Checked && !rdoTwitter.Checked)
            {
                MessageBox.Show(grpContactMethod.Text + " must be " + rdoEmail.Text + " or " + rdoFacebook.Text + " or " + rdoPhoneText.Text + " or " + rdoTwitter.Text + ".",
                                "Invalid " + grpContactMethod.Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                               );
                return false;
            }

            return  true;
        }
    }
}
