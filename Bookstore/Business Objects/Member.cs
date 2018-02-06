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

        public          int         number          { get; set; }
        public          DateTime    joindate        { get; set; }
        public          string      firstname       { get; set; }
        public          string      lastname        { get; set; }
        public          string      address         { get; set; }
        public          string      city            { get; set; }
        public          string      state           { get; set; }
        public          string      zipcode         { get; set; }
        public          string      phone           { get; set; }
        public          string      member_status   { get; set; }
        public          string      login_name      { get; set; }
        public          string      password        { get; set; }
        public          string      email           { get; set; }
        public          int         contact_method  { get; set; }
        public          int         subscription_id { get; set; }
        public          string      name            { get; set; }//TODO needs to be special parameter
        public          string      photo           { get; set; }

        public  static  int         firstnameLength =   15;
        public  static  int         lastnameLength =    25;
        public  static  int         addressLength =     30;
        public  static  int         cityLength =        20;
        public  static  int         stateLength =        2;
        public  static  int         zipcodeLength =      5;
        public  static  int         phoneLength =       10;
        public  static  int         login_nameLength =  20;
        public  static  int         passwordLength =    20;
        public  static  int         emailLength =       20;

        public  static  string      numberTip =         "customer or member unique number";
        public  static  string      joindateTip =       "date in which the member join club";
        public  static  string      firstnameTip =      "first name of the member";
        public  static  string      lastnameTip =       "last name of the member";
        public  static  string      addressTip =        "address of the member";
        public  static  string      cityTip =           "city where the member resides";
        public  static  string      stateTip =          "state where the member resides";
        public  static  string      zipcodeTip =        "zipcode of the member";
        public  static  string      phoneTip =          "daytime phone number of the member";
        public  static  string      login_nameTip =     "member login credentials";
        public  static  string      passwordTip =       "member login password";
        public  static  string      emailTip =          "member email address";
        public  static  string      contact_methodTip = "How does the member preferred to be contacted";
        public  static  string      subscription_idTip ="Member subscription type";

        #endregion

        #region Constructor

        public Member()
        {
            ;
        }
        #endregion
    }
}
