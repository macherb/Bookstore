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
    public partial class frmRental : Form, Iuser_interface
    {
        List<Rental>	rentalList;

        public frmRental()
        {
            InitializeComponent();
        }

        private void frmRental_Load(object sender, EventArgs e)
        {
			try
			{
	            rentalList =                                    Rentals.GetRentals();
		        rentalDataGridView.DataSource =                 rentalList;

			    List<Movie>                     movieList =     Movies.GetMovies();
				cmbMovieNumber.DataSource =                     movieList;
				cmbMovieNumber.DisplayMember =                  Movies.extra;
				cmbMovieNumber.ValueMember =                    Movies.key;
				cmbMovieNumber.SelectedIndex =                  -1;

				List<Member>                    memberList =    Members.GetMembers();
				cmbMemberNumber.DataSource =                    memberList;
				cmbMemberNumber.DisplayMember =                 Members.extra;
				cmbMemberNumber.ValueMember =                   Members.key;
				cmbMemberNumber.SelectedIndex =                 -1;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			ToolTip                             toolTip =       new ToolTip();

            toolTip.SetToolTip(btnAdd, "Add Movie, Member, Checkout and Return");
            toolTip.SetToolTip(btnBrowse, "Browse by Movie, Member and Checkout");
            toolTip.SetToolTip(btnUpdate, "Update Movie, Member, Checkout and Return");
            toolTip.SetToolTip(btnDelete, "Delete Movie, Member and Checkout");
            toolTip.SetToolTip(btnClear, "Clear all Rental fields");

            toolTip.SetToolTip(cmbMovieNumber, "movie unique identifier");
            toolTip.SetToolTip(lblMovieNumber, "movie unique identifier");

            toolTip.SetToolTip(cmbMemberNumber, "member unique number");
            toolTip.SetToolTip(lblMemberNumber, "member unique number");

            toolTip.SetToolTip(dtpMediaCheckoutDate, "date the media (dvd) was checked out");
            toolTip.SetToolTip(lblMediaCheckoutDate, "date the media (dvd) was checked out");

            toolTip.SetToolTip(dtpMediaReturnDate, "date the media (dvd) was returned to store");
            toolTip.SetToolTip(lblMediaReturnDate, "date the media (dvd) was returned to store");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckAll())
            {
				Rental                          objRental;

				try
				{
					objRental = Rentals.GetRental((int)cmbMovieNumber.SelectedValue, (int)cmbMemberNumber.SelectedValue, dtpMediaCheckoutDate.Value);//TODO
					if (objRental != null)
					{
						MessageBox.Show(lblMovieNumber.Text + " and " + lblMemberNumber.Text + " and " + lblMediaCheckoutDate.Text + " already exists.",
										"Invalid " + lblMovieNumber.Text + " and/or " + lblMemberNumber.Text + " and/or " + lblMediaCheckoutDate.Text,
										MessageBoxButtons.OK,
										MessageBoxIcon.Error);
						return;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				//TODO make sure Rental doesn't exist
				objRental =									new Rental();
                objRental.movie_number =                    (int)cmbMovieNumber.SelectedValue;
                objRental.member_number =                   (int)cmbMemberNumber.SelectedValue;
                objRental.media_checkout_date =             dtpMediaCheckoutDate.Value;
                objRental.media_return_date =               dtpMediaReturnDate.Value;//TODO only add if not blank
                try
                {
                    bool                        status =    Rentals.AddRental(objRental);
                    if (status)
                    {
                        MessageBox.Show(MsgBoxHelper.Inserted("Rental"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rentalList =                        Rentals.GetRentals();
                        rentalDataGridView.DataSource =     rentalList;//TODO movie and member not -1?
                    }
                    else
                    {
                        MessageBox.Show(MsgBoxHelper.Inserted("Rental not"), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if ((cmbMovieNumber.SelectedIndex == -1) && (cmbMemberNumber.SelectedIndex == -1) && dtpMediaCheckoutDate.Value == new DateTime(1753, 1, 1, 0, 0, 0))
            {
                MessageBox.Show(lblMovieNumber.Text + " and " + lblMemberNumber.Text + " and " + lblMediaCheckoutDate.Text + " must not all be blank.",
                                "Invalid " + lblMovieNumber.Text + " and/or " + lblMemberNumber.Text + " and/or " + lblMediaCheckoutDate.Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                int                                     first,
                                                        second;

                if (cmbMovieNumber.SelectedIndex == -1)
                    first =                             -1;
                else
                    first =                             (int)cmbMovieNumber.SelectedValue;
                if (cmbMemberNumber.SelectedIndex == -1)
                    second =                            -1;
                else
                    second =                            (int)cmbMemberNumber.SelectedValue;

                Rental                                  objRental;

                try
                {
                    objRental =                         Rentals.GetRental(first, second, dtpMediaCheckoutDate.Value);//TODO
                    if (objRental == null)
                    {
                        MessageBox.Show(MsgBoxHelper.Selected("Rental " + 
                            lblMovieNumber.Text + " " + cmbMovieNumber.Text + 
                            ", " + 
                            lblMemberNumber.Text + " " + cmbMemberNumber.Text +
                            ", " +
                            lblMediaCheckoutDate.Text + " " + dtpMediaCheckoutDate.Text
                            ), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);//TODO if not -1
                    }
                    else
                    {
                        cmbMovieNumber.SelectedValue =  objRental.movie_number;
                        cmbMemberNumber.SelectedValue = objRental.member_number;
                        dtpMediaCheckoutDate.Value =    objRental.media_checkout_date;
                        dtpMediaReturnDate.Value =      objRental.media_return_date;
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
                Rental                          objRental = new Rental();
                objRental.movie_number =                    (int)cmbMovieNumber.SelectedValue;
                objRental.member_number =                   (int)cmbMemberNumber.SelectedValue;
                objRental.media_checkout_date =             dtpMediaCheckoutDate.Value;
                objRental.media_return_date =               dtpMediaReturnDate.Value;//TODO only add if not blank

                try
                {
                    bool                        status =    Rentals.UpdateRental(objRental);
                    if (status)
                    {
                        MessageBox.Show(MsgBoxHelper.Updated("Rental"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rentalList =                        Rentals.GetRentals();
                        rentalDataGridView.DataSource =     rentalList;//TODO movie and member not -1?
                    }
                    else
                    {
                        MessageBox.Show(MsgBoxHelper.Updated("Rental not"), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if      (cmbMovieNumber.SelectedIndex == -1)
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblMovieNumber.Text), "Invalid " + lblMovieNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbMovieNumber.Focus();
            }
            else if (cmbMemberNumber.SelectedIndex == -1)
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblMemberNumber.Text), "Invalid " + lblMemberNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbMemberNumber.Focus();
            }
            //TODO add m c d
            else
            {
                Rental                          objRental = new Rental();
                objRental.movie_number =                    (int)cmbMovieNumber.SelectedValue;//TODO
                objRental.member_number =                   (int)cmbMemberNumber.SelectedValue;
                objRental.media_checkout_date =             dtpMediaCheckoutDate.Value;
                try
                {
                    bool                        status =    Rentals.DeleteRental(objRental);
                    if (status)
                    {
                        MessageBox.Show(MsgBoxHelper.Deleted("Rental"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rentalList =                        Rentals.GetRentals();
                        rentalDataGridView.DataSource =     rentalList;//movie and member not -1?
                    }
                    else
                    {
                        MessageBox.Show(MsgBoxHelper.Deleted("Rental not"), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            cmbMovieNumber.SelectedIndex =  -1;
            cmbMemberNumber.SelectedIndex = -1;
            dtpMediaCheckoutDate.Value =    new DateTime(1753, 1, 1, 0, 0, 0);
            dtpMediaReturnDate.Value =      new DateTime(1753, 1, 1, 0, 0, 0);
        }

        public bool CheckAll()
        {
            if (cmbMovieNumber.SelectedIndex == -1)
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblMovieNumber.Text), "Invalid " + lblMovieNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbMovieNumber.Focus();
                return  false;
            }

            if (cmbMemberNumber.SelectedIndex == -1)
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblMemberNumber.Text), "Invalid " + lblMemberNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbMemberNumber.Focus();
                return  false;
            }

            if      (dtpMediaCheckoutDate.Value >= DateTime.MaxValue)//
            {
                MessageBox.Show(lblMediaCheckoutDate.Text + " must be less than or equal to " + DateTime.MaxValue + ".", "Invalid " + lblMediaCheckoutDate.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpMediaCheckoutDate.Focus();
                return  false;
            }
            else if (dtpMediaCheckoutDate.Value <= DateTime.MinValue)//
            {
                MessageBox.Show(lblMediaCheckoutDate.Text + " must be greater than or equal to " + DateTime.MinValue + ".", "Invalid " + lblMediaCheckoutDate.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpMediaCheckoutDate.Focus();
                return  false;
            }
            return  true;
        }
    }
}
