using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    class Subscriptions : BaseTable
    {
        #region Private variables

        private static string[] parameters =        { "id", "name", "cost" };


        private static int      lowestSecondary =   1;

        #endregion

        #region Public variables

        public static string    key =               parameters[ 0];
        public static string    extra =             parameters[ 1];


        #endregion

        #region Private functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscriptionReader"></param>
        /// <param name="objSubscription"></param>
        private static void SetSecondary(SqlDataReader subscriptionReader, Subscription objSubscription)
        {
            float                   cost;

            objSubscription.name =          subscriptionReader[parameters[ 1]].ToString();
            float.TryParse(                 subscriptionReader[parameters[ 2]].ToString(),  out cost);
            objSubscription.cost =          cost;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQLStatement"></param>
        /// <param name="subscription"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="primary"></param>
        /// <param name="preprimary"></param>
        /// <param name="secondary"></param>
        /// <param name="presecondary"></param>
        private static void PrimarySecondary(ref string primary, string preprimary, ref string secondary, string presecondary)
        {
            for (int i = 1                  ; i < lowestSecondary  ; i++)
                primary +=      preprimary + parameters[i];
            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=    presecondary + parameters[i];
        }










        #endregion

        #region Public functions

        /// <summary>
        /// Returns a list of generic  type objects from the table
        /// </summary>
        public static List<Subscription> GetSubscriptions()
        {
            List<Subscription>  subscriptions =     new List<Subscription>();
            string              primary,
                                secondary,
                                SQLStatement;
            SqlCommand          objCommand;
            SqlDataReader       subscriptionReader;
            
            primary =                               key;
            secondary =                             ", Subscription." + parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", Subscription.", ref secondary, ", Subscription.");
            SQLStatement =                          SQLHelper.Select(
                                                                    "Subscription",


                                                                                                    " FROM " + "Subscription"









                                                                                   ,
                                                                    primary,
                                                                    secondary
                                                                    );

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
        public static Subscription GetSubscription(int parameter)//(string parameter)
        {
            Subscription    objSubscription =   null;
            string          primary =           string.Empty,
                            secondary =         string.Empty,
                            SQLStatement;
            SqlCommand      objCommand;
            SqlDataReader   subscriptionReader;

            secondary +=                        parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", Subscription.", ref secondary, ", Subscription.");
            SQLStatement =                      SQLHelper.Select("Subscription", 
                                                                " FROM " + "Subscription",
                                                                primary, 
                                                                secondary
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
        public static bool AddSubscription(Subscription subscription)
        {
            string          primary,
                            secondary,
                            SQLStatement;
            //TODO what if MAX is 32767            
            bool            result;

            primary =                       key;
            secondary =                     ", @" + parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", @", ref secondary, ", @");
            SQLStatement =                  SQLHelper.Insert(   "Subscription",
                                                                primary,
                                                                secondary
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
