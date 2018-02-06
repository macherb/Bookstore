using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    class Vendor
    {
        #region Public variables

        public          int     id      { get; set; }
        public          string  name    { get; set; }

        public  static  int     nameLength =    30;

        public static   string  idTip =         "Unique vendor id";
        public static   string  nameTip =       "Movie vendor description";

        #endregion

        #region Constructor

        public Vendor()
        {
            ;
        }

        #endregion
    }
}
