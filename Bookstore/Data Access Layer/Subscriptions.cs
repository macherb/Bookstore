using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    class Subscriptions
    {
        #region Private variables

        private static string[] parameters = { "id", "name", "cost" };

        #endregion

        #region Public functions

        /// <summary>
        /// Returns a list of generic  type objects from the table
        /// </summary>
        public static List<Subscription> GetSubscriptions()
        {
            List<Subscription>    subscriptions =       new List<Subscription>();
            string          SQLStatement = SQLHelper.Select("Subscription", parameters[0], ", " + parameters[1] + ", " + parameters[2]);
            SqlCommand      objCommand;
            SqlDataReader   subscriptionReader;

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
                {
                    objConn.Open();
                    //Step #2: Code Logic to create appropriate SQL Server objects calls
                    //         Code Logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program
                    using (objCommand = new SqlCommand(SQLStatement, objConn))
                    {
                        //Step #3: Return the objtemp generic list variable  back to the calling UI 
                        using ((subscriptionReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (subscriptionReader.Read())
                            {
                                Subscription          objSubscription = new Subscription();
                                int             id;
                                float cost;
                                Int32.TryParse(             subscriptionReader[parameters[0]].ToString(), out id);
                                objSubscription.id =              id;
                                objSubscription.name =            subscriptionReader[parameters[1]].ToString();
                                float.TryParse(             subscriptionReader[parameters[2]].ToString(), out cost);
                                objSubscription.cost =      cost;
                                subscriptions.Add(objSubscription);
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
            return  subscriptions;
        }

        /// <summary>
        /// Returns a single record  from the table whose parameter matches a table field condition
        /// </summary>
        /// <param name="parameter">accepts a parameter to return a specific record</param>
        public static Subscription GetSubscription(int parameter)//(string parameter)
        {
            Subscription          objSubscription =        null;
            string          SQLStatement = SQLHelper.Select("Subscription", parameters[0], ", " + parameters[1] + ", " + parameters[2]) + " WHERE " + parameters[0] + " = @" + parameters[0];
            SqlCommand      objCommand;
            SqlDataReader   subscriptionReader;

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
                {
                    objConn.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Code logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program            
                    using (objCommand = new SqlCommand(SQLStatement, objConn))
                    {
                        objCommand.Parameters.AddWithValue('@' + parameters[0], parameter);
                        //Step #3: Return the objtemp variable back to the calling UI 
                        using ((subscriptionReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (subscriptionReader.Read())
                            {
                                objSubscription =         new Subscription();
                                int             id;
                                float cost;
                                Int32.TryParse(     subscriptionReader[parameters[0]].ToString(), out id);
                                objSubscription.id =      id;
                                objSubscription.name =    subscriptionReader[parameters[1]].ToString();
                                float.TryParse(subscriptionReader[parameters[2]].ToString(), out cost);
                                objSubscription.cost = cost;
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
            return  objSubscription;
        }

        /// <summary>
        /// Adds a record to the table with a Boolean returned status of True or False.
        /// </summary>
        /// <param name="subscription">accepts a custom object of that type as a parameter</param>
        public static bool AddSubscription(Subscription subscription)
        {
            string          SQLStatement1 = "SELECT MAX(id) AS max_subscription FROM Subscription",//Helper.Select("Subscription", "MAX(" + parameters[0] + ") AS max_subscription", ""),//"SELECT MAX(id) FROM Subscription",
                            SQLStatement2 = SQLHelper.Insert("Subscription", parameters[0], ", @" + parameters[1] + ", @" + parameters[2]);
            //TODO what if MAX is nothing?
            int             rowsAffected,
                            max;

            SqlCommand      objCommand1,
                            objCommand2;
            SqlDataReader   subscriptionReader;
            bool            result =        false;

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                using (SqlConnection objConn1 = AccessDataSQLServer.GetConnection())
                {
                    objConn1.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Cod Logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program
                    using (objCommand1 = new SqlCommand(SQLStatement1, objConn1))
                    {
                        using ((subscriptionReader = objCommand1.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            subscriptionReader.Read();
                            Int32.TryParse(subscriptionReader[0].ToString(), out max);
                        }
                    }
                    objConn1.Close();
                }
                using (SqlConnection objConn2 = AccessDataSQLServer.GetConnection())
                {
                    objConn2.Open();
                    using (objCommand2 = new SqlCommand(SQLStatement2, objConn2))
                    {
                        objCommand2.Parameters.AddWithValue('@' + parameters[0], max + 1);
                        objCommand2.Parameters.AddWithValue('@' + parameters[1], subscription.name);
                        objCommand2.Parameters.AddWithValue('@' + parameters[2], subscription.cost);
                        //Step #3: return false if record was not added successfully
                        //         return true if record was added successfully  
                        rowsAffected = objCommand2.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            result = true;   //Record was added successfully
                        }
                    }
                    objConn2.Close();
                }
            }
            catch (SqlException SQLex)
            {
                throw new Exception(SQLex.Message); //Record was not added successfully
            }
            catch (InvalidOperationException IOex)
            {
                throw new Exception(IOex.Message);  //Record was not added successfully
            }
            return result;
        }

        /// <summary>
        /// Updates a record in the table with a Boolean returned status of True or False
        /// </summary>
        /// <param name="subscription">accepts a custom object of that type as a parameter</param>
        public static bool UpdateSubscription(Subscription subscription)
        {
            string[]    first = { parameters[0] + " = @" + parameters[0] };
            string      SQLStatement = SQLHelper.Update("Subscription", first, ", " + parameters[1] + " = @" + parameters[1] + ", " + parameters[2] + " = @" + parameters[2]);
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
                {
                    objConn.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Code logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program
                    using (objCommand = new SqlCommand(SQLStatement, objConn))
                    {
                        objCommand.Parameters.AddWithValue('@' + parameters[0], subscription.id   );
                        objCommand.Parameters.AddWithValue('@' + parameters[1], subscription.name );
                        objCommand.Parameters.AddWithValue('@' + parameters[2], subscription.cost);
                        //Step #3: return false if record was not added successfully
                        //         return true if record was added successfully           
                        rowsAffected = objCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            result = true;   //Record was added successfully
                        }
                    }
                    objConn.Close();
                }
            }
            catch (SqlException SQLex)
            {
                throw new Exception(SQLex.Message); //Record was not added successfully
            }
            catch (InvalidOperationException IOex)
            {
                throw new Exception(IOex.Message);  //Record was not added successfully
            }
            return result;
        }

        /// <summary>
        /// Deletes a record from the database with a Boolean returned status of True or False
        /// </summary>
        /// <param name="subscription">accepts a custom object of that type as a parameter</param>
        public static bool DeleteSubscription(Subscription subscription)
        {
            string      SQLStatement = SQLHelper.Delete("Subscription", parameters[0], string.Empty);
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;

            //Step# 1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
                {
                    objConn.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Code logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program
                    using (objCommand = new SqlCommand(SQLStatement, objConn))
                    {
                        objCommand.Parameters.AddWithValue('@' + parameters[0], subscription.id);
                        //Step #3: return false if record was not added successfully
                        //         return true if record was added successfully
                        rowsAffected = objCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            result = true;   //Record was added successfully
                        }
                    }
                    objConn.Close();
                }
            }
            catch (SqlException SQLex)
            {
                throw new Exception(SQLex.Message); //Record was not added successfully
            }
            catch (InvalidOperationException IOex)
            {
                throw new Exception(IOex.Message);  //Record was not added successfully
            }
            return  result;
        }

        #endregion
    }
}
