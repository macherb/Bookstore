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
        List<Member>    memberList;
        string          imageLocation;

        public frmMember()
        {
            imageLocation = string.Empty;

            InitializeComponent();
        }

        private void frmMember_Load(object sender, EventArgs e)
        {
			try
			{
	            memberList =                                        Members.GetMembers();
		        memberDataGridView.DataSource =                     memberList;

			    List<Subscription>              subscriptionList =  Subscriptions.GetSubscriptions();
				cmbSubscriptionID.DataSource =                      subscriptionList;
				cmbSubscriptionID.DisplayMember =                   Subscriptions.extra;
			    cmbSubscriptionID.ValueMember =                     Subscriptions.key;
		        cmbSubscriptionID.SelectedIndex =                   -1;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			ToolTip                             toolTip =           new ToolTip();

            toolTip.SetToolTip(btnAdd, "Add all Member fields");
            toolTip.SetToolTip(btnBrowse, "Browse by Member Number");
            toolTip.SetToolTip(btnUpdate, "Update all Member fields");
            toolTip.SetToolTip(btnDelete, "Delete Member Number");
            toolTip.SetToolTip(btnClear, "Clear all Member fields");

            toolTip.SetToolTip(txtNumber, Member.numberTip);
            toolTip.SetToolTip(lblNumber, Member.numberTip);

            toolTip.SetToolTip(dtpJoinDate, Member.joindateTip);
            toolTip.SetToolTip(lblJoinDate, Member.joindateTip);

            toolTip.SetToolTip(txtFirstName, Member.firstnameTip);
            toolTip.SetToolTip(lblFirstName, Member.firstnameTip);

            toolTip.SetToolTip(txtLastName, Member.lastnameTip);
            toolTip.SetToolTip(lblLastName, Member.lastnameTip);

            toolTip.SetToolTip(txtAddress, Member.addressTip);
            toolTip.SetToolTip(lblAddress, Member.addressTip);

            toolTip.SetToolTip(txtCity, Member.cityTip);
            toolTip.SetToolTip(lblCity, Member.cityTip);

            toolTip.SetToolTip(txtState, Member.stateTip);
            toolTip.SetToolTip(lblState, Member.stateTip);

            toolTip.SetToolTip(txtZipCode, Member.zipcodeTip);
            toolTip.SetToolTip(lblZipCode, Member.zipcodeTip);

            toolTip.SetToolTip(txtPhone, Member.phoneTip);
            toolTip.SetToolTip(lblPhone, Member.phoneTip);


            toolTip.SetToolTip(txtLoginName, Member.login_nameTip);
            toolTip.SetToolTip(lblLoginName, Member.login_nameTip);

            toolTip.SetToolTip(txtPassword, Member.passwordTip);
            toolTip.SetToolTip(lblPassword, Member.passwordTip);

            toolTip.SetToolTip(txtEmail, Member.emailTip);
            toolTip.SetToolTip(lblEmail, Member.emailTip);

            toolTip.SetToolTip(grpContactMethod, Member.contact_methodTip);

            toolTip.SetToolTip(cmbSubscriptionID, Member.subscription_idTip);
            toolTip.SetToolTip(lblSubscriptionID, Member.subscription_idTip);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckAll())
            {
                Member                      objMember = new Member();

                objMember.joindate =                    dtpJoinDate.Value;
                objMember.firstname =                   txtFirstName.Text.Trim();
                objMember.lastname =                    txtLastName.Text.Trim();
                objMember.address =                     txtAddress.Text.Trim();
                objMember.city =                        txtCity.Text.Trim();
                objMember.state =                       txtState.Text.Trim();
                objMember.zipcode =                     txtZipCode.Text.Trim();
                objMember.phone =                       txtPhone.Text.Trim();
                objMember.member_status =               rdoActive.Checked ? "A" : "I";
                objMember.login_name =                  txtLoginName.Text.Trim();
                objMember.password =                    txtPassword.Text.Trim();
                objMember.email =                       txtEmail.Text.Trim();
                if      (rdoEmail.Checked       )
                    objMember.contact_method =          1;
                else if (rdoFacebook.Checked    )
                    objMember.contact_method =          2;
                else if (rdoPhoneText.Checked   )
                    objMember.contact_method =          3;
                else if (rdoTwitter.Checked     )
                    objMember.contact_method =          4;
                else
                    objMember.contact_method =          0;

                objMember.subscription_id =             (int)cmbSubscriptionID.SelectedValue;
                objMember.photo =                       imageLocation;
                try
                {
                    bool                    status =    Members.AddMember(objMember);
                    if (status)
                    {
                        MessageBox.Show(MsgBoxHelper.Inserted("Member"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        memberList =                    Members.GetMembers();
		                memberDataGridView.DataSource = memberList;
                        txtNumber.Text =                objMember.number.ToString();
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
                MessageBox.Show(MsgBoxHelper.MustBe(lblNumber.Text), "Invalid " + lblNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNumber.Focus();
            }
            else
            {
                if      (number > short.MaxValue)// 32767
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblNumber.Text), "Invalid " + lblNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNumber.Focus();
                }
                else if (number < short.MinValue)//-32768
                {
                    MessageBox.Show(MsgBoxHelper.GTETmin(lblNumber.Text), "Invalid " + lblNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNumber.Focus();
                }
                else
                {
                    Member                          objMember;

                    try
                    {
                        objMember =                             Members.GetMember(number);
                        if (objMember == null)
                        {
                            MessageBox.Show(MsgBoxHelper.Selected("Member " + lblNumber.Text + " " + number), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            dtpJoinDate.Value =                 objMember.joindate;
                            txtFirstName.Text =                 objMember.firstname.Trim();
                            txtLastName.Text =                  objMember.lastname.Trim();
                            txtAddress.Text =                   objMember.address.Trim();
                            txtCity.Text =                      objMember.city.Trim();
                            txtState.Text =                     objMember.state;
                            txtZipCode.Text =                   objMember.zipcode;
                            txtPhone.Text =                     objMember.phone;
                            if (objMember.member_status == "A")
                                rdoActive.Checked =             true;
                            else
                                rdoInactive.Checked =           true;
                            txtLoginName.Text =                 objMember.login_name;
                            txtPassword.Text =                  objMember.password;
                            txtEmail.Text =                     objMember.email;

                            if      (objMember.contact_method == 1)
                                rdoEmail.Checked =              true;
                            else if (objMember.contact_method == 2)
                                rdoFacebook.Checked =           true;
                            else if (objMember.contact_method == 3)
                                rdoPhoneText.Checked =          true;
                            else if (objMember.contact_method == 4)
                                rdoTwitter.Checked =            true;
                            else
                                objMember.contact_method =      0;

                            cmbSubscriptionID.SelectedValue =   objMember.subscription_id;
                            imageLocation =                     objMember.photo;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int number;
            if (!Int32.TryParse(txtNumber.Text.Trim(), out number))
            {
                MessageBox.Show(MsgBoxHelper.MustBe(lblNumber.Text), "Invalid " + lblNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNumber.Focus();
            }
            else
            {
                if      (number > short.MaxValue)// 32767
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblNumber.Text), "Invalid " + lblNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNumber.Focus();
                }
                else if (number < short.MinValue)//-32768
                {
                    MessageBox.Show(MsgBoxHelper.GTETmin(lblNumber.Text), "Invalid " + lblNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNumber.Focus();
                }
                else if (CheckAll())
                {
                    Member                      objMember = new Member();

                    Int32.TryParse(txtNumber.Text.Trim(), out number);
                    objMember.number =                      number;
                    objMember.joindate =                    dtpJoinDate.Value;
                    objMember.firstname =                   txtFirstName.Text.Trim();
                    objMember.lastname =                    txtLastName.Text.Trim();
                    objMember.address =                     txtAddress.Text.Trim();
                    objMember.city =                        txtCity.Text.Trim();
                    objMember.state =                       txtState.Text.Trim();
                    objMember.zipcode =                     txtZipCode.Text.Trim();
                    objMember.phone =                       txtPhone.Text.Trim();
                    objMember.member_status =               rdoActive.Checked ? "A" : "I";
                    objMember.login_name =                  txtLoginName.Text.Trim();
                    objMember.password =                    txtPassword.Text.Trim();
                    objMember.email =                       txtEmail.Text.Trim();

                    if      (rdoEmail.Checked       )
                        objMember.contact_method =          1;
                    else if (rdoFacebook.Checked    )
                        objMember.contact_method =          2;
                    else if (rdoPhoneText.Checked   )
                        objMember.contact_method =          3;
                    else if (rdoTwitter.Checked     )
                        objMember.contact_method =          4;
                    else
                        objMember.contact_method =          0;

                    objMember.subscription_id =             (int)cmbSubscriptionID.SelectedValue;
                    objMember.photo =                       imageLocation;
                    try
                    {
                        bool                    status =    Members.UpdateMember(objMember);
                        if (status)
                        {
                            MessageBox.Show(MsgBoxHelper.Updated("Member"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            memberList =                    Members.GetMembers();
                            memberDataGridView.DataSource = memberList;
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
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int number;
            if (!Int32.TryParse(txtNumber.Text.Trim(), out number))
            {
                MessageBox.Show(MsgBoxHelper.MustBe(lblNumber.Text), "Invalid " + lblNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNumber.Focus();
            }
            else
            {
                if      (number > short.MaxValue)// 32767
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblNumber.Text), "Invalid " + lblNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNumber.Focus();
                }
                else if (number < short.MinValue)//-32768
                {
                    MessageBox.Show(MsgBoxHelper.GTETmin(lblNumber.Text), "Invalid " + lblNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNumber.Focus();
                }
                else
                {
                    Member                                  objMember = new Member();
                    objMember.number =                                  number;
                    try
                    {
                        bool                                status =    Members.DeleteMember(objMember);
                        if (status)
                        {
                            MessageBox.Show(MsgBoxHelper.Deleted("Member"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            memberList =                                Members.GetMembers();
                            memberDataGridView.DataSource =             memberList;
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
            if      (dtpJoinDate.Value > DateTime.MaxValue)//
            {
                MessageBox.Show(lblJoinDate.Text + " must be less than or equal to " + DateTime.MaxValue + ".", "Invalid " + lblJoinDate.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpJoinDate.Focus();
                return  false;
            }
            else if (dtpJoinDate.Value <= new DateTime(1753, 1, 1, 0, 0, 0))//
            {
                MessageBox.Show(lblJoinDate.Text + " must be greater than " + new DateTime(1753, 1, 1, 0, 0, 0) + ".", "Invalid " + lblJoinDate.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpJoinDate.Focus();
                return  false;
            }

            if (txtFirstName.Text.Trim() != string.Empty)
            {
                if (txtFirstName.Text.Trim().Length > Member.firstnameLength)
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblFirstName.Text, Member.firstnameLength), "Invalid " + lblFirstName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFirstName.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblFirstName.Text), "Invalid " + lblFirstName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Focus();
                return  false;
            }

            if (txtLastName.Text.Trim() != string.Empty)
            {
                if (txtLastName.Text.Trim().Length > Member.lastnameLength)
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblLastName.Text, Member.lastnameLength), "Invalid " + lblLastName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtLastName.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblLastName.Text), "Invalid " + lblLastName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLastName.Focus();
                return  false;
            }

            if (txtAddress.Text.Trim() != string.Empty)
            {
                if (txtAddress.Text.Trim().Length > Member.addressLength)
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblAddress.Text, Member.addressLength), "Invalid " + lblAddress.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAddress.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblAddress.Text), "Invalid " + lblAddress.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return  false;
            }

            if (txtCity.Text.Trim() != string.Empty)
            {
                if (txtCity.Text.Trim().Length > Member.cityLength)
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblCity.Text, Member.cityLength), "Invalid " + lblCity.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCity.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblCity.Text), "Invalid " + lblCity.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCity.Focus();
                return  false;
            }

            if (txtState.Text.Trim() != string.Empty)
            {
                if (txtState.Text.Trim().Length > Member.stateLength)
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblState.Text, Member.stateLength), "Invalid " + lblState.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtState.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblState.Text), "Invalid " + lblState.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtState.Focus();
                return  false;
            }

            if (txtZipCode.Text.Trim() != string.Empty)
            {
                if (txtZipCode.Text.Trim().Length > Member.zipcodeLength)
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblZipCode.Text, Member.zipcodeLength), "Invalid " + lblZipCode.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtZipCode.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblZipCode.Text), "Invalid " + lblZipCode.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtZipCode.Focus();
                return  false;
            }

            if (txtPhone.Text.Trim() != string.Empty)
            {
                if (txtPhone.Text.Trim().Length > Member.phoneLength)
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblPhone.Text, Member.phoneLength), "Invalid " + lblPhone.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhone.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblPhone.Text), "Invalid " + lblPhone.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (txtLoginName.Text.Trim().Length > Member.login_nameLength)
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblLoginName.Text, Member.login_nameLength), "Invalid " + lblLoginName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtLoginName.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblLoginName.Text), "Invalid " + lblLoginName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLoginName.Focus();
                return  false;
            }

            if (txtPassword.Text.Trim() != string.Empty)
            {
                if (txtPassword.Text.Trim().Length > Member.passwordLength)
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblPassword.Text, Member.passwordLength), "Invalid " + lblPassword.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblPassword.Text), "Invalid " + lblPassword.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return  false;
            }

            if (txtEmail.Text.Trim() != string.Empty)
            {
                if (txtEmail.Text.Trim().Length > Member.emailLength)
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblEmail.Text, Member.emailLength), "Invalid " + lblEmail.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblEmail.Text), "Invalid " + lblEmail.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return  false;
            }

            if (cmbSubscriptionID.SelectedIndex == -1)
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblSubscriptionID.Text), "Invalid " + lblSubscriptionID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSubscriptionID.Focus();
                return  false;
            }

            if (!rdoEmail.Checked && !rdoFacebook.Checked && !rdoPhoneText.Checked && !rdoTwitter.Checked)
            {
                MessageBox.Show(grpContactMethod.Text + " must be " + rdoEmail.Text + " or " + rdoFacebook.Text + " or " + rdoPhoneText.Text + " or " + rdoTwitter.Text + ".",
                                "Invalid " + grpContactMethod.Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                               );
                return  false;
            }

            return  true;
        }
    }
}
