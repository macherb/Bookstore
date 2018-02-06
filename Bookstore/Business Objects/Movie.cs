using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    class Movie
    {
        #region Public variables

        public          int     movie_number        { get; set; }
        public          string  movie_title         { get; set; }//TODO extra for another table
        public          string  Description         { get; set; }
        public          int     movie_year_made     { get; set; }
        public          int     genre_id            { get; set; }
        public          string  name                { get; set; }//TODO needs to be special parameter (extra)
        public          string  movie_rating        { get; set; }
        public          string  media_type          { get; set; }
        public          float   movie_retail_cost   { get; set; }
        public          int     copies_on_hand      { get; set; }
        public          string  image               { get; set; }
        public          string  trailer             { get; set; }

        public  static  int     movie_titleLength =     100;
        public  static  int     DescriptionLength =     255;
        public  static  int     imageLength =           255;
        public  static  int     trailerLength =         255;

        public  static  string  movie_numberTip =       "movie unique identifier";
        public  static  string  movie_titleTip =        "movie title";
        public  static  string  DescriptionTip =        "movie descriptions";
        public  static  string  movie_year_madeTip =    "year the movie was made";
        public  static  string  genre_idTip =           "Type of movie genre";
        public  static  string  movie_ratingTip =       "rating of the movie";
        public  static  string  media_typeTip =         "What type of medium is the media";
        public  static  string  movie_retail_costTip =  "retail cost of the movie";
        public  static  string  copies_on_handTip =     "number of copies the stores has";
        public  static  string  imageTip =              "Movie image filename including file format extension";
        public  static  string  trailerTip =            "Web/Youtube url of the video trailer";

        #endregion

        #region Constructor

        public Movie()
        {
            ;
        }

        #endregion
    }
}
