using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    interface Iuser_interface
    {
        bool CheckAll();
    }

    class SQLHelper
    {
        /// <summary>
        /// SQL statement that SELECT's primary and secondary fields from a table
        /// </summary>
        /// <param name="tableName1a">The name of the table to SELECT from</param>
        /// <param name="tableName1b">The name of the table to SELECT from with any JOIN's</param>
        /// <param name="primary">The primary field(s) to SELECT</param>
        /// <param name="secondary">The secondary field(s) to SELECT</param>
        /// <returns>An SQL SELECT statement</returns>
        public static string Select(string tableName1a, string tableName1b, 
            string primary, 
            string secondary
            //TODO if adding for where, need first as second, don't select primary
            )
        {
            return  "SELECT " + tableName1a + "." + primary + secondary + 
                    tableName1b;//extra2 + " FROM (tableName1b + " INNER JOIN 2 ON 1.joiner1_2 = 2.joiner1_2)
            //TODO reset parenthesis counter
        }

        /// <summary>
        /// Everything necessary to JOIN two tables, and get the field(s) you want
        /// </summary>
        /// <param name="tableName1a">The name of the table to JOIN</param>
        /// <param name="tableName1b">The extra(s) from other JOIN's with the name of the table to JOIN and any additional JOIN's</param>
        /// <param name="tableName2">The name of the table to be JOIN'ed</param>
        /// <param name="extra2">Extra field(s) to SELECT from the second table</param>
        /// <param name="joiner1">The field from the first table to JOIN</param>
        /// <param name="joiner2">The field from the second table to JOIN</param>
        /// <returns>...and the extra field(s) from the second table FROM the first table JOIN the second table ON first table's joiner equals second table's joiner</returns>
        public static string Join(
            string tableName1b, 
            string tableName2, 
            string extra2, 
            string joiner1, string joiner2)
        {
            return  //", " + tableName2 + "." + 
                    extra2 + 
                    tableName1b + //extra3 + " FROM ((tableName1b + " INNER JOIN 3 ON 1.joiner1_3 = 3.joiner1_3)
                    " INNER JOIN " + tableName2 + " ON " + 
                    joiner1 + " = " + tableName2 + "." + joiner2
                    + ")";
            //TODO increment parenthesis counter
        }

        /// <summary>
        /// SQL statement that INSERT's primary and secondary fields into a table
        /// </summary>
        /// <param name="tableName">The name of the table to INSERT into</param>
        /// <param name="primary">The primary field(s) to INSERT</param>
        /// <param name="secondary">The secondary field(s) to INSERT</param>
        /// <returns>An SQL INSERT statement</returns>
        public static string Insert(string tableName, string primary, string secondary)
        {
            return  "INSERT INTO " + tableName + " VALUES (@" + primary + secondary + ")";
        }

        /// <summary>
        /// SQL statement that UPDATE's primary and secondary fields in a table
        /// </summary>
        /// <param name="tableName">The name of the table to UPDATE in</param>
        /// <param name="primary">The primary field(s) to identify the row to UPDATE</param>
        /// <param name="secondary">The secondary field(s) to UPDATE</param>
        /// <returns>An SQL UPDATE statement</returns>
        public static string Update(string tableName, string primary, string secondary)
        {
            return  "UPDATE " + 
                    tableName + 
                    " SET " + 
                    secondary + 
                    " WHERE " + 
                    primary;
        }

        /// <summary>
        /// SQL statement that DELETE's a row from a table
        /// </summary>
        /// <param name="tableName">The name of the table to DELETE from</param>
        /// <param name="first">The primary field to identify the row to DELETE</param>
        /// <param name="secondary">The secondary field(s) to identify the row to DELETE</param>
        /// <returns>An SQL DELETE statement</returns>
        public static string Delete(string tableName, string primary, string secondary)
        {
            return  "DELETE " + tableName + " WHERE " + primary + " = @" + primary + secondary;
        }
    }

    class MsgBoxHelper
    {
        /// <summary>
        /// A message to display when a SELECT fails
        /// </summary>
        /// <param name="tableName">The name of the table that was SELECT'ed from</param>
        /// <returns>A message to display in the MessageBox</returns>
        public static string Selected(string tableName)
        {
            return tableName + " not found in database.";
        }

        /// <summary>
        /// A message to display after an INSERT is attempted
        /// </summary>
        /// <param name="tableName">The name of the table that was INSERT'ed into</param>
        /// <returns>A message to display in the MessageBox</returns>
        public static string Inserted(string tableName)
        {
            return tableName + " added to database.";
        }

        /// <summary>
        /// A message to display after an UPDATE is attempted
        /// </summary>
        /// <param name="tableName">The name of the table that was UPDATE'ed</param>
        /// <returns>A message to display in the MessageBox</returns>
        public static string Updated(string tableName)
        {
            return tableName + " updated in the database.";
        }

        /// <summary>
        /// A message to display after a DELETE is attempted
        /// </summary>
        /// <param name="tableName">The name of the table that was DELETE'd from</param>
        /// <returns>A message to display in the MessageBox</returns>
        public static string Deleted(string tableName)
        {
            return tableName + " deleted in the database.";
        }
    }
}
