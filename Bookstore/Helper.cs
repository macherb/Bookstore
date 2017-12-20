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
        public static string Select(string tableName, string first, string second)
        {
            return  "SELECT " + first + second + " FROM " + tableName;
        }

        public static string Insert(string tableName, string first, string second)
        {
            return  "INSERT INTO " + tableName + " VALUES (@" + first + second + ")";
        }

        public static string Update(string tableName, string[] first, string second)
        {
            string  first1 =    first[0],
                    first2 =    first[0];

            for (int i = 1; i < first.Length; i++)
            {
                first1 +=       ", " + first[i];
                first2 +=       " AND " + first[i];
            }
            return  "UPDATE " + tableName + " SET " + first1 + second + " WHERE " + first2;
        }

        public static string Delete(string tableName, string first, string second)
        {
            return  "DELETE " + tableName + " WHERE " + first + " = @" + first + second;
        }
    }

    class MsgBoxHelper
    {
        public static string Select(string tableName, string first, string second)
        {
            return "SELECT " + first + second + " FROM " + tableName;
        }
        public static string Selected(string tableName)
        {
            return tableName + " not found in database.";
        }

        public static string Inserted(string tableName)
        {
            return tableName + " added to database.";
        }

        public static string Updated(string tableName)
        {
            return tableName + " updated in the database.";
        }

        public static string Deleted(string tableName)
        {
            return tableName + " deleted in the database.";
        }
    }
}
