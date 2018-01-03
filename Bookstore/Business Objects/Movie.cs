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

        public int      movie_number        { get; set; }
        public string   movie_title         { get; set; }//TODO extra for another table
        public string   Description         { get; set; }
        public int      movie_year_made     { get; set; }
        public int      genre_id            { get; set; }
        public string   name                { get; set; }//TODO needs to be special parameter (extra)
        public string   movie_rating        { get; set; }
        public string   media_type          { get; set; }
        public float    movie_retail_cost   { get; set; }
        public int      copies_on_hand      { get; set; }
        public string   image               { get; set; }
        public string   trailer             { get; set; }

        #endregion

        #region Constructor

        public Movie()
        {
            ;
        }

        #endregion
    }
}
