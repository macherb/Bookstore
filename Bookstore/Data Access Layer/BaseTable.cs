using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    class BaseTable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetMax(string tableName, string key)
        {
            string          SQLStatement;
            int             max;
            SqlCommand      objCommand;
            SqlDataReader   reader;

            SQLStatement =                  SQLHelper.Select(   "MAX(" + tableName,
                                                                " FROM " + tableName,
                                                                key,
                                                                ")");

            using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
            {
                objConn.Open();
                using (objCommand = new SqlCommand(SQLStatement, objConn))
                {
                    using ((reader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                    {
                        reader.Read();
                        Int32.TryParse(reader[0].ToString(), out max);
                    }
                }
                objConn.Close();
            }

            return  max;
        }
    }
}
