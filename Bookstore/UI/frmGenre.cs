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
    public partial class frmGenre : Form, Iuser_interface
    {
        public frmGenre()
        {
            InitializeComponent();
        }

        private void frmGenre_Load(object sender, EventArgs e)
        {
			try
			{
	            List<Genre>                     genreList = Genres.GetGenres();
			    genreDataGridView.DataSource =              genreList;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			ToolTip                             toolTip =   new ToolTip();

            toolTip.SetToolTip(btnAdd, "Add Genre Name");
            toolTip.SetToolTip(btnBrowse, "Browse by Genre Number");
            toolTip.SetToolTip(btnUpdate, "Update Genre Number and Name");
            toolTip.SetToolTip(btnDelete, "Delete Genre Number");
            toolTip.SetToolTip(btnClear, "Clear all Genre fields");

            toolTip.SetToolTip(txtID, "Unique genre id");
            toolTip.SetToolTip(lblID, "Unique genre id");

            toolTip.SetToolTip(txtName, "Movie genre description");
            toolTip.SetToolTip(lblName, "Movie genre description");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckAll())
            {
                Genre           objGenre =  new Genre();

                objGenre.name =             txtName.Text.Trim();
                try
                {
                    bool        status =    Genres.AddGenre(objGenre);
                    if (status)
                    {
                        MessageBox.Show(MsgBoxHelper.Inserted("Genre"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmGenre_Load(sender, e);
                        txtID.Text =        objGenre.id.ToString();
                    }
                    else
                    {
                        MessageBox.Show(MsgBoxHelper.Inserted("Genre not"), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(MsgBoxHelper.MustBe(lblID.Text), "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtID.Focus();
            }
            else
            {
                if      (id > short.MaxValue)// 32767
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblID.Text), "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtID.Focus();
                }
                else if (id < short.MinValue)//-32768
                {
                    MessageBox.Show(MsgBoxHelper.GTETmin(lblID.Text), "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtID.Focus();
                }
                else
                {
                    Genre           objGenre;

                    try
                    {
                        objGenre =              Genres.GetGenre(id);
                        if (objGenre == null)
                        {
                            MessageBox.Show(MsgBoxHelper.Selected("Genre " + lblID.Text + " " + id), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            txtName.Text =      objGenre.name;
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
                MessageBox.Show(MsgBoxHelper.MustBe(lblID.Text), "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtID.Focus();
            }
            else
            {
                if      (id > short.MaxValue)// 32767
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblID.Text), "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtID.Focus();
                }
                else if (id < short.MinValue)//-32768
                {
                    MessageBox.Show(MsgBoxHelper.GTETmin(lblID.Text), "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtID.Focus();
                }
                else if (CheckAll())
                {
                    Genre           objGenre =  new Genre();
                    objGenre.id =               id;
                    objGenre.name =             txtName.Text.Trim();
                    try
                    {
                        bool        status =    Genres.UpdateGenre(objGenre);
                        if (status)
                        {
                            MessageBox.Show(MsgBoxHelper.Updated("Genre"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmGenre_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show(MsgBoxHelper.Updated("Genre not"), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(MsgBoxHelper.MustBe(lblID.Text), "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtID.Focus();
            }
            else
            {
                if      (id > short.MaxValue)// 32767
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblID.Text), "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtID.Focus();
                }
                else if (id < short.MinValue)//-32768
                {
                    MessageBox.Show(MsgBoxHelper.GTETmin(lblID.Text), "Invalid " + lblID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtID.Focus();
                }
                else
                {
                    Genre           objGenre =  new Genre();
                    objGenre.id =               id;
                    objGenre.name =             txtName.Text.Trim();
                    try
                    {
                        bool        status =    Genres.DeleteGenre(objGenre);
                        if (status)
                        {
                            MessageBox.Show(MsgBoxHelper.Deleted("Genre"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmGenre_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show(MsgBoxHelper.Deleted("Genre not"), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblName.Text, 30), "Invalid " + lblName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtName.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblName.Text), "Invalid " + lblName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return  false;
            }
            return  true;
        }
    }
}
