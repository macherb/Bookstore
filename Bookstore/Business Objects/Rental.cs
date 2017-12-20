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

        public int      movie_number        { get; set; }
        public int      member_number       { get; set; }
        public DateTime media_checkout_date { get; set; }
        public DateTime media_return_date   { get; set; }

        #endregion

        #region Constructor

        public Rental()
        {
            ;
        }

        #endregion
    }
}
