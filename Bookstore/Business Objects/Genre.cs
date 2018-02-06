using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    class Genre
    {
        #region Public variables

        public          int     id      { get; set; }
        public          string  name    { get; set; }

        public  static  int     nameLength =    30;

        public static   string  idTip =         "Unique genre id";
        public static   string  nameTip =       "Movie genre description";

        #endregion

        #region Constructor

        public Genre()
        {
            ;
        }

        #endregion
    }
}
