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
        /// <param name="tableName1">The name of the table to SELECT from</param>
        /// <param name="primary">The primary field to SELECT</param>
        /// <param name="secondary">The secondary field(s) to SELECT</param>
        /// <returns>An SQL SELECT statement</returns>
        public static string Select(string tableName1a, string tableName1b, 
            
            string primary, 
            string secondary

            )
        {
            return  "SELECT " +
                    tableName1a + "." + primary + secondary + 
                    "" +
                    " FROM " + 
                    //"(" + 
                    tableName1b + 
                    "" +
                    ""
                    //+ ")"
                    ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName1a"></param>
        /// <param name="tableName1b"></param>
        /// <param name="tableName2"></param>
        /// <param name="primary"></param>
        /// <param name="secondary"></param>
        /// <param name="extra2"></param>
        /// <param name="joiner1"></param>
        /// <param name="joiner2"></param>
        /// <returns></returns>
        public static string Select(string tableName1a, string tableName1b, 
            string tableName2, 
            string primary, //TODO can combine with "table1a."
            string secondary, 
            string extra2, //TODO can combine with ", table2."
            string joiner1, string joiner2)
        {
            return  "SELECT " +
                    tableName1a + "." + primary + secondary +
                    ", " + tableName2 + "." + extra2 + //, 3.extra3
                    " FROM " + 
                    "(" + 
                    tableName1b + //(1 INNER JOIN 3 ON 1.joiner1_3 = 3.joiner1_3)
                    " INNER JOIN " + tableName2 + " ON " + 
                    tableName1a + "." + joiner1 + " = " + tableName2 + "." + joiner2
                    + ")"
                    ;
        }

        /// <summary>
        /// SQL statement that INSERT's primary and secondary fields into a table
        /// </summary>
        /// <param name="tableName">The name of the table to INSERT into</param>
        /// <param name="primary">The primary field to INSERT</param>
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
        /// <param name="primary">The primary field to UPDATE, and identify the row to UPDATE</param>
        /// <param name="secondary">The secondary field(s) to UPDATE</param>
        /// <returns>An SQL UPDATE statement</returns>
        public static string Update(string tableName, string[] primary, string secondary)
        {
            string  primary1 =  primary[0],
                    primary2 =  primary[0];

            for (int i = 1; i < primary.Length; i++)
            {
                primary1 +=     ", " + primary[i];
                primary2 +=     " AND " + primary[i];
            }
            return  "UPDATE " + tableName + " SET " + primary1 + secondary + " WHERE " + primary2;
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
