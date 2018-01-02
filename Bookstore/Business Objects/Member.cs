using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    class Member
    {
        #region Public variables

        public int      number          { get; set; }
        public DateTime joindate        { get; set; }
        public string   firstname       { get; set; }
        public string   lastname        { get; set; }
        public string   address         { get; set; }
        public string   city            { get; set; }
        public string   state           { get; set; }
        public string   zipcode         { get; set; }
        public string   phone           { get; set; }
        public string   member_status   { get; set; }
        public string   login_name      { get; set; }
        public string   password        { get; set; }
        public string   email           { get; set; }
        public int      contact_method  { get; set; }
        public int      subscription_id { get; set; }
        public string   name            { get; set; }//TODO needs to be special parameter
        public string   photo           { get; set; }

        #endregion

        #region Constructor

        public Member()
        {
            ;
        }
        #endregion
    }
}
