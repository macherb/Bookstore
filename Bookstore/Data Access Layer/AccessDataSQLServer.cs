using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    /// <summary>
    /// This public static class will be used to return a database connection object.
    /// </summary>
    class AccessDataSQLServer
    {
        #region Private variables

        private string connectionString { get; set; }

        #endregion

        #region Public functions

        /// <summary>
        /// Returns a database connection from the SQLConnection class.
        /// </summary>
        /// <returns>Used to initialize a SqlConnection</returns>
        public static SqlConnection GetConnection()
        {
            //You must instantiate the connection object with a supplied/valid connection string.
            SqlConnection sqlConnection = new SqlConnection("");
            return  sqlConnection;
        }
        
        #endregion
    }
}
