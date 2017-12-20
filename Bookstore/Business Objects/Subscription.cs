using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    class Subscription
    {
        #region Public variables

        public int      id      { get; set; }
        public string   name    { get; set; }
        public float    cost    { get; set; }

        #endregion

        #region Constructor

        public Subscription()
        {
            ;
        }

        #endregion
    }
}
