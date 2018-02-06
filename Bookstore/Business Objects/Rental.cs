using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    class Rental
    {
        #region Public variables

        public          int         movie_number        { get; set; }//TODO foreign
        public          string      movie_title         { get; set; }//TODO needs to be special parameter (extra)
        public          int         member_number       { get; set; }//TODO foreign
        public          string      login_name          { get; set; }//TODO needs to be special parameter (extra)
        public          DateTime    joindate            { get; set; }//TODO needs to be special parameter (extra)
        public          DateTime    media_checkout_date { get; set; }
        public          DateTime    media_return_date   { get; set; }

        public static   string      movie_numberTip =       "movie unique identifier";
        public static   string      member_numberTip =      "member unique number";
        public static   string      media_checkout_dateTip ="date the media (dvd) was checked out";
        public static   string      media_return_dateTip =  "date the media (dvd) was returned to store";

        #endregion

        #region Constructor

        public Rental()
        {
            ;
        }

        #endregion
    }
}
