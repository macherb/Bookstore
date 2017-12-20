using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    /// <summary>
    /// This public static class will be used to facilitate the validation of a user trying to log into the application with valid or non-valid credentials.
    /// </summary>
    class Login
    {
        #region Public variables

        public  string  Credentials { get; set; }
        public  string  Password    { get; set; }

        #endregion

        #region Public functions

        /// <summary>
        /// This method should determine if the user logging into the database is valid or not. The method should return a Boolean (True or False).
        /// </summary>
        public bool IsValid()
        {
            string SQLStatement = SQLHelper.Select("Member", "password", "") + " WHERE login_name = @Credentials";
            SqlCommand objCommand;
            SqlDataReader memberReader;
            bool    result =    false;
            //The IsValid method will be using the SqlConnection, SqlCommand and SqlData Reader objects.
            //Note: **The IsValid() method is reading the member table to see if the credentials that are passed in are valid.
            try
            {
                using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
                {
                    objConn.Open();
                    using (objCommand = new SqlCommand(SQLStatement, objConn))
                    {
                        objCommand.Parameters.AddWithValue("@Credentials", Credentials);
                        using ((memberReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (memberReader.Read())
                            {
                                string  password;
                                password = memberReader["password"].ToString();
                                if (password.Equals(Password))
                                    result =    true;   //represents the credentials are valid and you should enable the Menu Items.
                                else
                                    result =    false;  //represents the credentials are invalid and should not enabled the Menu Items.
                            }
                        }
                    }
                    objConn.Close();
                }
            }
            catch (SqlException SQLex)
            {
                throw new Exception(SQLex.Message);
            }
            catch (InvalidOperationException IOex)
            {
                throw new Exception(IOex.Message);
            }
            return  result;
        }

        #endregion
    }
}
