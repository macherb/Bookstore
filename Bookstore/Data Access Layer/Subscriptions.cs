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
    /// 
    /// </summary>
    class Subscriptions : BaseTable
    {
        #region Private variables

        private static string[] parameters =        { "id", "name", "cost" };


        private static int      lowestSecondary =   1;

        #endregion

        #region Public variables

        public static string    key =               parameters[ 0];
        public static string    extra =             parameters[ 1];


        public static string    SQLGetList =        SQLHelper.Select(
                                                                    "Subscription",


                                                                                                    " FROM " + "Subscription"









                                                                                   ,
                                                                    Primary(key, ", Subscription."),
                                                                    Secondary(", Subscription." + parameters[lowestSecondary], ", Subscription.")
                                                                    );
            
        #endregion

        #region Private functions

        /// <summary>
        /// Sets all the non-primary key(s) in a <see cref="Bookstore.Subscription"/>
        /// </summary>
        /// <param name="subscriptionReader">The <see cref="Bookstore.Subscription"/> that was read from</param>
        /// <param name="objSubscription">The <see cref="Bookstore.Subscription"/> that will be written to</param>
        private static void SetSecondary(SqlDataReader subscriptionReader, Subscription objSubscription)
        {
            float                   cost;

            objSubscription.name =          subscriptionReader[parameters[ 1]].ToString();
            float.TryParse(                 subscriptionReader[parameters[ 2]].ToString(),  out cost);
            objSubscription.cost =          cost;
        }

        /// <summary>
        /// Writes a <see cref="Bookstore.Subscription"/> to the database's table
        /// </summary>
        /// <param name="SQLStatement">The command to write a <see cref="Bookstore.Subscription"/></param>
        /// <param name="subscription">The <see cref="Bookstore.Subscription"/> that will have its data written to the table</param>
        /// <returns>Whether or not the <see cref="Bookstore.Subscription"/> was successfully written</returns>
        private static bool WriteSubscription(string SQLStatement, Subscription subscription)
        {
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;

            using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
            {
                objConn.Open();
                using (objCommand = new SqlCommand(SQLStatement, objConn))
                {
                    objCommand.Parameters.AddWithValue('@' + parameters[ 0],    subscription.id     );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 1],    subscription.name   );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 2],    subscription.cost   );
                    rowsAffected =  objCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        result =    true;   //Record was added successfully
                    }
                }
                objConn.Close();
            }
            return  result;
        }

        /// <summary>
        /// Sets the list of field(s) that will be SELECT'ed
        /// </summary>
        /// <param name="primary">The list of primary key(s)</param>
        /// <param name="preprimary">What will be placed before every primary key</param>
        /// <param name="secondary">The list of non-primary key(s)</param>
        /// <param name="presecondary">What will be placed before every non-primary key</param>
        /// <returns></returns>
        private static string Primary(string primary, string preprimary)
        {
            for (int i = 1                  ; i < lowestSecondary  ; i++)
                primary +=      preprimary + parameters[i];
            return  primary;
        }
        private static string Secondary(string secondary, string presecondary)
        {
            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=    presecondary + parameters[i];
            return  secondary;
        }










        #endregion

        #region Public functions

        /// <summary>
        /// Returns a list of generic  type objects from the table
        /// </summary>
        /// <returns>All fields of all <see cref="Bookstore.Subscription"/>'s</returns>
        /// <exception cref="System.Exception" />
        public static List<Subscription> GetSubscriptions()
        {
            List<Subscription>  subscriptions =     new List<Subscription>();
            SqlCommand          objCommand;
            SqlDataReader       subscriptionReader;
            
            

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
                    using (objCommand = new SqlCommand(SQLGetList, objConn))
                    {
                        //Step #3: Return the objtemp generic list variable  back to the calling UI 
                        using ((subscriptionReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (subscriptionReader.Read())
                            {
                                Subscription        objSubscription =   new Subscription();
                                int                 id;


                                Int32.TryParse(                         subscriptionReader[parameters[ 0]].ToString(),  out id  );
                                objSubscription.id =                    id;

                                SetSecondary(subscriptionReader, objSubscription);




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
        /// <returns>All the fields (except the primary key) of a <see cref="Bookstore.Subscription"/></returns>
        /// <exception cref="System.Exception" />
        public static Subscription GetSubscription(int parameter)//(string parameter)
        {
            Subscription    objSubscription =   null;
            string          //primary =           string.Empty,
                            //secondary =         string.Empty,
                            SQLStatement;
            SqlCommand      objCommand;
            SqlDataReader   subscriptionReader;

            //secondary +=                        parameters[lowestSecondary];
            //PrimarySecondary(ref primary, ", Subscription.", ref secondary, ", Subscription.");
            SQLStatement =                      SQLHelper.Select("Subscription", 
                                                                " FROM " + "Subscription",
                                                                Primary(string.Empty,   ", Subscription."), 
                                                                Secondary(              parameters[lowestSecondary], ", Subscription.")
                                                                ) + " WHERE ";


            SQLStatement +=                     "Subscription." + parameters[ 0] + " = @" + parameters[ 0];

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
                        objCommand.Parameters.AddWithValue('@' + parameters[ 0],    parameter   );
                        //Step #3: Return the objtemp variable back to the calling UI 
                        using ((subscriptionReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (subscriptionReader.Read())
                            {
                                objSubscription =       new Subscription();

                                objSubscription.id =    parameter;

                                SetSecondary(subscriptionReader, objSubscription);

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
        /// <returns>Whether or not the <see cref="Bookstore.Subscription"/> was successfully written</returns>
        /// <exception cref="System.Exception" />
        public static bool AddSubscription(Subscription subscription)
        {
            string          //primary,
                            //secondary,
                            SQLStatement;
            //TODO what if MAX is 32767            
            bool            result;

            //primary =                       key;
            //secondary =                     ", @" + parameters[lowestSecondary];
            //PrimarySecondary(ref primary, ", @", ref secondary, ", @");
            SQLStatement =                  SQLHelper.Insert(   "Subscription",
                                                                Primary(key, ", @"),
                                                                Secondary(", @" + parameters[lowestSecondary], ", @")
                                                            );

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {                
                subscription.id =           GetMax("Subscription", key) + 1;
                



                








                
                
                
                //
                result =    WriteSubscription(SQLStatement, subscription);
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

        /// <summary>
        /// Updates a record in the table with a Boolean returned status of True or False
        /// </summary>
        /// <param name="subscription">accepts a custom object of that type as a parameter</param>
        /// <returns>Whether or not the <see cref="Bookstore.Subscription"/> was successfully written</returns>
        /// <exception cref="System.Exception" />
        public static bool UpdateSubscription(Subscription subscription)
        {

            string          primary,
                            secondary,                            
                            SQLStatement;            
            bool            result;

            primary =                       key                         + " = @" + key                        ;
            secondary =                     parameters[lowestSecondary] + " = @" + parameters[lowestSecondary];


            
            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=                ", " + parameters[i] + " = @" + parameters[i];
            SQLStatement =                  SQLHelper.Update(   "Subscription",
                                                                primary,
                                                                secondary
                                                            );

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {














































                result =    WriteSubscription(SQLStatement, subscription);
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

        /// <summary>
        /// Deletes a record from the database with a Boolean returned status of True or False
        /// </summary>
        /// <param name="subscription">accepts a custom object of that type as a parameter</param>
        /// <returns>Whether or not the <see cref="Bookstore.Subscription"/> was successfully deleted</returns>
        /// <exception cref="System.Exception" />
        public static bool DeleteSubscription(Subscription subscription)
        {
            string      primary =       string.Empty,
                        SQLStatement;
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;



            SQLStatement =              SQLHelper.Delete("Subscription",
                                                        key,
                                                        primary
                                                        );

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
                        objCommand.Parameters.AddWithValue('@' + parameters[ 0],    subscription.id );
                        //Step #3: return false if record was not added successfully
                        //         return true if record was added successfully
                        rowsAffected =  objCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            result =    true;   //Record was added successfully
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
