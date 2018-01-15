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
    public partial class frmVendor : Form, Iuser_interface
    {
        public frmVendor()
        {
            InitializeComponent();
        }

        private void frmVendor_Load(object sender, EventArgs e)
        {
			try
			{
	            List<Vendor>                    vendorList = Vendors.GetVendors();
			    vendorDataGridView.DataSource =              vendorList;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(btnAdd, "Add Vendor Name");
            toolTip.SetToolTip(btnBrowse, "Browse by Vendor Number");
            toolTip.SetToolTip(btnUpdate, "Update Vendor Number and Name");
            toolTip.SetToolTip(btnDelete, "Delete Vendor Number");
            toolTip.SetToolTip(btnClear, "Clear all Vendor fields");

            toolTip.SetToolTip(txtID, "Unique vendor id");
            toolTip.SetToolTip(lblID, "Unique vendor id");

            toolTip.SetToolTip(txtName, "Movie vendor description");
            toolTip.SetToolTip(lblName, "Movie vendor description");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckAll())
            {
                Vendor objVendor = new Vendor();
                objVendor.name = txtName.Text.Trim();
                try
                {
                    bool status = Vendors.AddVendor(objVendor);
                    if (status)
                    {
                        MessageBox.Show(MsgBoxHelper.Inserted("Vendor"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmVendor_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show(MsgBoxHelper.Inserted("Vendor not"), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            int id;
            if (!Int32.TryParse(txtID.Text.Trim(), out id))
            {
                MessageBox.Show(lblID.Text + " must be an integer (" + short.MinValue + " - " + short.MaxValue + ").", "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtID.Focus();
            }
            else
            {
                if      (id > short.MaxValue)// 32767
                {
                    MessageBox.Show(lblID.Text + " must be less than or equal to " + short.MaxValue + ".", "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtID.Focus();
                }
                else if (id < short.MinValue)//-32768
                {
                    MessageBox.Show(lblID.Text + " must be greater than or equal to " + short.MinValue + ".", "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtID.Focus();
                }
                else
                {
                    Vendor                  objVendor;

                    try
                    {
                        objVendor =                     Vendors.GetVendor(id);
                        if (objVendor == null)
                        {
                            MessageBox.Show(MsgBoxHelper.Selected("Vendor " + lblID.Text + " " + id), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            txtName.Text =              objVendor.name;
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
            int id;
            if (!Int32.TryParse(txtID.Text.Trim(), out id))
            {
                MessageBox.Show(lblID.Text + " must be an integer (" + short.MinValue + " - " + short.MaxValue + ").", "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtID.Focus();
            }
            else
            {
                if      (id > short.MaxValue)// 32767
                {
                    MessageBox.Show(lblID.Text + " must be less than or equal to " + short.MaxValue + ".", "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtID.Focus();
                }
                else if (id < short.MinValue)//-32768
                {
                    MessageBox.Show(lblID.Text + " must be greater than or equal to " + short.MinValue + ".", "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtID.Focus();
                }
                else if (CheckAll())
                {
                    Vendor          objVendor = new Vendor();
                    objVendor.id =              id;
                    objVendor.name =            txtName.Text.Trim();
                    try
                    {
                        bool        status =    Vendors.UpdateVendor(objVendor);
                        if (status)
                        {
                            MessageBox.Show(MsgBoxHelper.Updated("Vendor"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmVendor_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show(MsgBoxHelper.Updated("Vendor not"), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            int id;
            if (!Int32.TryParse(txtID.Text.Trim(), out id))
            {
                MessageBox.Show(lblID.Text + " must be an integer (" + short.MinValue + " - " + short.MaxValue + ").", "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtID.Focus();
            }
            else
            {
                if      (id > short.MaxValue)// 32767
                {
                    MessageBox.Show(lblID.Text + " must be less than or equal to " + short.MaxValue + ".", "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtID.Focus();
                }
                else if (id < short.MinValue)//-32768
                {
                    MessageBox.Show(lblID.Text + " must be greater than or equal to " + short.MinValue + ".", "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtID.Focus();
                }
                else
                {
                    Vendor          objVendor = new Vendor();
                    objVendor.id =              id;
                    objVendor.name =            txtName.Text.Trim();
                    try
                    {
                        bool        status =    Vendors.DeleteVendor(objVendor);
                        if (status)
                        {
                            MessageBox.Show(MsgBoxHelper.Deleted("Vendor"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmVendor_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show(MsgBoxHelper.Deleted("Vendor not"), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtID.Text =    string.Empty;
            txtName.Text =  string.Empty;
        }

        public bool CheckAll()
        {
            if (txtName.Text.Trim() != string.Empty)
            {
                if (txtName.Text.Trim().Length > 30)
                {
                    MessageBox.Show(lblName.Text + " must be less than 30 characters.", "Invalid " + lblName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtName.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show(lblName.Text + " must not be blank.", "Invalid " + lblName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return false;
            }
            return  true;
        }
    }
}
