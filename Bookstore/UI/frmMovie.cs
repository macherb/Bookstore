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
    public partial class frmMovie : Form, Iuser_interface
    {
		List<Movie>	movieList;

		public frmMovie()
        {
            InitializeComponent();
        }

        private void frmMovie_Load(object sender, EventArgs e)
        {
			try
			{
	            movieList =                                 Movies.GetMovies();
		        movieDataGridView.DataSource =              movieList;

			    List<Genre>                     genreList = Genres.GetGenres();
				cmbGenreID.DataSource =                     genreList;
				cmbGenreID.DisplayMember =                  Genres.extra;
			    cmbGenreID.ValueMember =                    Genres.key;
		        cmbGenreID.SelectedIndex =                  -1;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			ToolTip                             toolTip =   new ToolTip();

            toolTip.SetToolTip(btnAdd, "Add all Movie fields");
            toolTip.SetToolTip(btnFind, "Browse by Movie Number");
            toolTip.SetToolTip(btnUpdate, "Update all Movie fields");
            toolTip.SetToolTip(btnDelete, "Delete Movie Number");
            toolTip.SetToolTip(btnClear, "Clear all Movie fields");

            toolTip.SetToolTip(txtMovieNumber, "movie unique identifier");
            toolTip.SetToolTip(lblMovieNumber, "movie unique identifier");

            toolTip.SetToolTip(txtMovieTitle, "movie title");
            toolTip.SetToolTip(lblMovieTitle, "movie title");

            toolTip.SetToolTip(txtDescription, "movie descriptions");
            toolTip.SetToolTip(lblDescription, "movie descriptions");

            toolTip.SetToolTip(txtMovieYearMade, "year the movie was made");
            toolTip.SetToolTip(lblMovieYearMade, "year the movie was made");

            toolTip.SetToolTip(cmbGenreID, "Type of movie genre");
            toolTip.SetToolTip(lblGenreID, "Type of movie genre");

            toolTip.SetToolTip(cmbMovieRating, "rating of the movie");
            toolTip.SetToolTip(lblMovieRating, "rating of the movie");

            toolTip.SetToolTip(cmbMediaType, "What type of medium is the media");
            toolTip.SetToolTip(lblMediaType, "What type of medium is the media");

            toolTip.SetToolTip(txtMovieRetailCost, "retail cost of the movie");
            toolTip.SetToolTip(lblMovieRetailCost, "retail cost of the movie");

            toolTip.SetToolTip(txtCopiesOnHand, "number of copies the stores has");
            toolTip.SetToolTip(lblCopiesOnHand, "number of copies the stores has");

            toolTip.SetToolTip(txtImage, "Movie image filename including file format extension");
            toolTip.SetToolTip(lblImage, "Movie image filename including file format extension");

            toolTip.SetToolTip(txtTrailer, "Web/Youtube url of the video trailer");
            toolTip.SetToolTip(lblTrailer, "Web/Youtube url of the video trailer");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckAll())
            {
                Movie   objMovie =              new Movie();

                int                             movie_year_made,
                                                copies_on_hand;
                float                           movie_retail_cost;
                objMovie.movie_title =          txtMovieTitle.Text.Trim();
                objMovie.Description =          txtDescription.Text.Trim();
                Int32.TryParse(txtMovieYearMade.Text.Trim(), out movie_year_made);
                objMovie.movie_year_made =      movie_year_made;
                objMovie.genre_id =             (int)cmbGenreID.SelectedValue;
                objMovie.movie_rating =         cmbMovieRating.Text;
                objMovie.media_type =           cmbMediaType.Text;
                float.TryParse(txtMovieRetailCost.Text.Trim(), out movie_retail_cost);
                objMovie.movie_retail_cost =    movie_retail_cost;
                Int32.TryParse(txtCopiesOnHand.Text.Trim(), out copies_on_hand);
                objMovie.copies_on_hand =       copies_on_hand;
                objMovie.image =                txtImage.Text.Trim();
                picImage.ImageLocation =        objMovie.image;
                objMovie.trailer =              txtTrailer.Text.Trim();
                try
                {
                    bool                    status =    Movies.AddMovie(objMovie);
                    if (status)
                    {
                        MessageBox.Show(MsgBoxHelper.Inserted("Movie"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        movieList =                     Movies.GetMovies();
                        movieDataGridView.DataSource =  movieList;//TODO genre not -1?
                        txtMovieNumber.Text =           objMovie.movie_number.ToString();
                    }
                    else
                    {
                        MessageBox.Show(MsgBoxHelper.Inserted("Movie not"), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            int movie_number;
            if (!Int32.TryParse(txtMovieNumber.Text.Trim(), out movie_number))
            {
                MessageBox.Show(MsgBoxHelper.MustBe(lblMovieNumber.Text), "Invalid " + lblMovieNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMovieNumber.Focus();
            }
            else
            {
                if      (movie_number > short.MaxValue)// 32767
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblMovieNumber.Text), "Invalid " + lblMovieNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMovieNumber.Focus();
                }
                else if (movie_number < short.MinValue)//-32768
                {
                    MessageBox.Show(MsgBoxHelper.GTETmin(lblMovieNumber.Text), "Invalid " + lblMovieNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMovieNumber.Focus();
                }
                else
                {
                    Movie                               objMovie;

                    try
                    {
                        objMovie =                                  Movies.GetMovie(movie_number);
                        if (objMovie == null)
                        {
                            MessageBox.Show(MsgBoxHelper.Selected("Movie " + lblMovieNumber.Text + " " + movie_number), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            txtMovieTitle.Text =                    objMovie.movie_title;
                            txtDescription.Text =                   objMovie.Description;
                            txtMovieYearMade.Text =                 objMovie.movie_year_made.ToString();
                            cmbGenreID.SelectedValue =              objMovie.genre_id;
                            cmbMovieRating.Text =                   objMovie.movie_rating.Trim();
                            cmbMediaType.Text =                     objMovie.media_type;
                            txtMovieRetailCost.Text =               objMovie.movie_retail_cost.ToString();
                            txtCopiesOnHand.Text =                  objMovie.copies_on_hand.ToString();
                            txtImage.Text =                         objMovie.image;
                            picImage.ImageLocation =                objMovie.image;
                            txtTrailer.Text =                       objMovie.trailer;
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
            int movie_number;
            if (!Int32.TryParse(txtMovieNumber.Text.Trim(), out movie_number))
            {
                MessageBox.Show(MsgBoxHelper.MustBe(lblMovieNumber.Text), "Invalid " + lblMovieNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMovieNumber.Focus();
            }
            else
            {
                if      (movie_number > short.MaxValue)// 32767
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblMovieNumber.Text), "Invalid " + lblMovieNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMovieNumber.Focus();
                }
                else if (movie_number < short.MinValue)//-32768
                {
                    MessageBox.Show(MsgBoxHelper.GTETmin(lblMovieNumber.Text), "Invalid " + lblMovieNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMovieNumber.Focus();
                }
                else if (CheckAll())
                {
                    Movie                       objMovie =          new Movie();
                    int                         movie_year_made,
                                                copies_on_hand;
                    float                       movie_retail_cost;
                    objMovie.movie_number =                         movie_number;
                    objMovie.movie_title =                          txtMovieTitle.Text.Trim();
                    objMovie.Description =                          txtDescription.Text.Trim();
                    Int32.TryParse(txtMovieYearMade.Text.Trim(), out movie_year_made);
                    objMovie.movie_year_made =                      movie_year_made;
                    objMovie.genre_id =                             (int)cmbGenreID.SelectedValue;
                    objMovie.movie_rating =                         cmbMovieRating.Text;
                    objMovie.media_type =                           cmbMediaType.Text;
                    float.TryParse(txtMovieRetailCost.Text.Trim(), out movie_retail_cost);
                    objMovie.movie_retail_cost =                    movie_retail_cost;
                    Int32.TryParse(txtCopiesOnHand.Text.Trim(), out copies_on_hand);
                    objMovie.copies_on_hand =                       copies_on_hand;
                    objMovie.image =                                txtImage.Text.Trim();
                    picImage.ImageLocation =                        objMovie.image;
                    objMovie.trailer =                              txtTrailer.Text.Trim();

                    try
                    {
                        bool                    status =            Movies.UpdateMovie(objMovie);
                        if (status)
                        {
                            MessageBox.Show(MsgBoxHelper.Updated("Movie"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            movieList =                             Movies.GetMovies();
                            movieDataGridView.DataSource =          movieList;//TODO genre not -1?
                        }
                        else
                        {
                            MessageBox.Show(MsgBoxHelper.Updated("Movie not"), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            int     movie_number;
            if (!Int32.TryParse(txtMovieNumber.Text.Trim(), out movie_number))
            {
                MessageBox.Show(MsgBoxHelper.MustBe(lblMovieNumber.Text), "Invalid " + lblMovieNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMovieNumber.Focus();
            }
            else
            {
                if      (movie_number > short.MaxValue)// 32767
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblMovieNumber.Text), "Invalid " + lblMovieNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMovieNumber.Focus();
                }
                else if (movie_number < short.MinValue)//-32768
                {
                    MessageBox.Show(MsgBoxHelper.GTETmin(lblMovieNumber.Text), "Invalid " + lblMovieNumber.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMovieNumber.Focus();
                }
                else
                {
                    Movie                                   objMovie =  new Movie();
                    objMovie.movie_number =                             movie_number;

                    try
                    {
                        bool                                status =    Movies.DeleteMovie(objMovie);
                        if (status)
                        {
                            MessageBox.Show(MsgBoxHelper.Deleted("Movie"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            movieList =                                 Movies.GetMovies();
                            movieDataGridView.DataSource =              movieList;//TODO genre not -1?
                        }
                        else
                        {
                            MessageBox.Show(MsgBoxHelper.Deleted("Movie not"), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtMovieNumber.Text =           string.Empty;
            txtMovieTitle.Text =            string.Empty;
            txtDescription.Text =           string.Empty;
            txtMovieYearMade.Text =         string.Empty;
            cmbGenreID.SelectedIndex =      -1;
            cmbMovieRating.SelectedIndex =  -1;
            cmbMediaType.SelectedIndex =    -1;
            txtMovieRetailCost.Text =       string.Empty;
            txtCopiesOnHand.Text =          string.Empty;
            txtImage.Text =                 string.Empty;
            picImage.ImageLocation =        string.Empty;
            txtTrailer.Text =               string.Empty;
        }

        public bool CheckAll()
        {
            if (txtMovieTitle.Text.Trim() != string.Empty)
            {
                if (txtMovieTitle.Text.Trim().Length > 100)
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblMovieTitle.Text, 100), "Invalid " + lblMovieTitle.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMovieTitle.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblMovieTitle.Text), "Invalid " + lblMovieTitle.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMovieTitle.Focus();
                return  false;
            }

            if (txtDescription.Text.Trim() != string.Empty)
            {
                if (txtDescription.Text.Trim().Length > 255)
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblDescription.Text, 255), "Invalid " + lblDescription.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDescription.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblDescription.Text), "Invalid " + lblDescription.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescription.Focus();
                return  false;
            }

            int movie_year_made;
            if (Int32.TryParse(txtMovieYearMade.Text.Trim(), out movie_year_made))
            {
                if      (movie_year_made > short.MaxValue)// 32767
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblMovieYearMade.Text), "Invalid " + lblMovieYearMade.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMovieYearMade.Focus();
                    return  false;
                }
                else if (movie_year_made < short.MinValue)//-32768
                {
                    MessageBox.Show(MsgBoxHelper.GTETmin(lblMovieYearMade.Text), "Invalid " + lblMovieYearMade.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMovieYearMade.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(MsgBoxHelper.MustBe(lblMovieYearMade.Text), "Invalid " + lblMovieYearMade.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMovieYearMade.Focus();
                return  false;
            }

            if (cmbGenreID.SelectedIndex == -1)
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblGenreID.Text), "Invalid " + lblGenreID.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbGenreID.Focus();
                return  false;
            }

            if (cmbMovieRating.SelectedIndex == -1)
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblMovieRating.Text), "Invalid " + lblMovieRating.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbMovieRating.Focus();
                return  false;
            }
            
            if (cmbMediaType.SelectedIndex == -1)
            {
                MessageBox.Show(MsgBoxHelper.NotBlank(lblMediaType.Text), "Invalid " + lblMediaType.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbMediaType.Focus();
                return  false;
            }

            float movie_retail_cost;
            if (float.TryParse(txtMovieRetailCost.Text.Trim(), out movie_retail_cost))
            {
                if      (movie_retail_cost > float.MaxValue)//3.40282347E+38
                {
                    MessageBox.Show(lblMovieRetailCost.Text + " must be less than or equal to " + float.MaxValue + ".", "Invalid " + lblMovieRetailCost.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMovieRetailCost.Focus();
                    return  false;
                }
                else if (movie_retail_cost < 0)
                {
                    MessageBox.Show(lblMovieRetailCost.Text + " must be greater than or equal to 0.00.", "Invalid " + lblMovieRetailCost.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMovieRetailCost.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(lblMovieRetailCost.Text + " must be a monetary value (0.00 - " + float.MaxValue + ").", "Invalid " + lblMovieRetailCost.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMovieRetailCost.Focus();
                return  false;
            }

            int copies_on_hand;
            if (Int32.TryParse(txtCopiesOnHand.Text.Trim(), out copies_on_hand))
            {
                if      (copies_on_hand > short.MaxValue)// 32767
                {
                    MessageBox.Show(MsgBoxHelper.LTETmax(lblCopiesOnHand.Text), "Invalid " + lblCopiesOnHand.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCopiesOnHand.Focus();
                    return  false;
                }
                else if (copies_on_hand < short.MinValue)//-32768
                {
                    MessageBox.Show(MsgBoxHelper.GTETmin(lblCopiesOnHand.Text), "Invalid " + lblCopiesOnHand.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCopiesOnHand.Focus();
                    return  false;
                }
            }
            else
            {
                MessageBox.Show(lblCopiesOnHand.Text + " must be an integer (0 - " + short.MaxValue + ").", "Invalid " + lblCopiesOnHand.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCopiesOnHand.Focus();
                return  false;
            }

            if (txtImage.Text.Trim().Length > 255)
            {
                MessageBox.Show(MsgBoxHelper.LTETmax(lblImage.Text, 255), "Invalid " + lblImage.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtImage.Focus();
                return  false;
            }

            if (txtTrailer.Text.Trim().Length > 255)
            {
                MessageBox.Show(MsgBoxHelper.LTETmax(lblTrailer.Text, 255), "Invalid " + lblTrailer.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTrailer.Focus();
                return  false;
            }

            return  true;
        }
    }
}
