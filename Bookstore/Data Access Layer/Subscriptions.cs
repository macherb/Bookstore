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

        private static string[] parameters =        { "id", "name", "cost" };
        private static int      lowestSecondary =   1;

        #endregion

        #region Public variables

        public static string    key =               parameters[0];
        public static string    extra =             parameters[1];

        #endregion

        #region Private functions

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
                                Subscription            objSubscription =   new Subscription();
                                int                     id;
                                float                   cost;
                                Int32.TryParse(                             subscriptionReader[parameters[0]].ToString(), out id    );
                                objSubscription.id =                        id;
                                objSubscription.name =                      subscriptionReader[parameters[1]].ToString();
                                float.TryParse(                             subscriptionReader[parameters[2]].ToString(), out cost  );
                                objSubscription.cost =                      cost;
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


            SQLStatement +=                     "Subscription." + parameters[0] + " = @" + parameters[0];

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
                                objSubscription =       new Subscription();
                                float                   cost;
                                objSubscription.id =    parameter;
                                objSubscription.name =  subscriptionReader[parameters[1]].ToString();
                                float.TryParse(         subscriptionReader[parameters[2]].ToString(), out cost  );
                                objSubscription.cost =  cost;
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
            string
                            primaryInsertSubscription,

                            secondaryInsertSubscription,

                            SQLMax,
                            SQLInsertSubscription;
            //TODO what if MAX is 32767
            int
                            rowsAffectedInsertSubscription,
                            max;
            SqlCommand      objCommandMax,
                            objCommandInsertSubscription;
            SqlDataReader   subscriptionReader;
            bool            result =        false;












            SQLMax =                        SQLHelper.Select(   "MAX(Subscription",
                                                                " FROM " + "Subscription",
                                                                key,
                                                                ")");

            primaryInsertSubscription =     key;
            secondaryInsertSubscription =   ", @" + parameters[lowestSecondary];
            PrimarySecondary(ref primaryInsertSubscription, ", @", ref secondaryInsertSubscription, ", @");
            SQLInsertSubscription =         SQLHelper.Insert(   "Subscription",
                                                                primaryInsertSubscription,
                                                                secondaryInsertSubscription
                                                            );

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                using (SqlConnection objConnMax = AccessDataSQLServer.GetConnection())
                {
                    objConnMax.Open();
                    using (objCommandMax = new SqlCommand(SQLMax, objConnMax))
                    {
                        using ((subscriptionReader = objCommandMax.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            subscriptionReader.Read();
                            Int32.TryParse(subscriptionReader[0].ToString(), out max);
                        }
                    }
                    objConnMax.Close();
                }
                subscription.id =           max + 1;
                // 1



















                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                //
                using (SqlConnection objConnInsertSubscription = AccessDataSQLServer.GetConnection())
                {
                    objConnInsertSubscription.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Cod Logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program
                    using (objCommandInsertSubscription = new SqlCommand(SQLInsertSubscription, objConnInsertSubscription))
                    {
                        objCommandInsertSubscription.Parameters.AddWithValue('@' + parameters[0],   subscription.id     );
                        objCommandInsertSubscription.Parameters.AddWithValue('@' + parameters[1],   subscription.name   );
                        objCommandInsertSubscription.Parameters.AddWithValue('@' + parameters[2],   subscription.cost   );
                        //Step #3: return false if record was not added successfully
                        //         return true if record was added successfully  
                        rowsAffectedInsertSubscription =    objCommandInsertSubscription.ExecuteNonQuery();
                        if (rowsAffectedInsertSubscription > 0)
                        {
                            result =    true;   //Record was added successfully
                        }
                    }
                    objConnInsertSubscription.Close();
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

        /// <summary>
        /// Updates a record in the table with a Boolean returned status of True or False
        /// </summary>
        /// <param name="subscription">accepts a custom object of that type as a parameter</param>
        public static bool UpdateSubscription(Subscription subscription)
        {
            string      
                            primary,
                        
                            
                            secondary,
                        
                            
                            SQLStatement
                            
                            
                            ;

            SqlCommand      objCommand
                            
                            ;
            int             rowsAffected
                            ;
            bool            result =        false;










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




















































                using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
                {
                    objConn.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Code logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program
                    using (objCommand = new SqlCommand(SQLStatement, objConn))
                    {
                        objCommand.Parameters.AddWithValue('@' + parameters[0], subscription.id     );
                        objCommand.Parameters.AddWithValue('@' + parameters[1], subscription.name   );
                        objCommand.Parameters.AddWithValue('@' + parameters[2], subscription.cost   );
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
            return result;
        }

        /// <summary>
        /// Deletes a record from the database with a Boolean returned status of True or False
        /// </summary>
        /// <param name="subscription">accepts a custom object of that type as a parameter</param>
        public static bool DeleteSubscription(Subscription subscription)
        {
            string      primaryDeleteSubscription = string.Empty,


                        SQLDeleteSubscription
                        ;
            SqlCommand  objCommandDeleteSubscription
                        ;
            int         rowsAffectedDeleteSubscription
                        ;
            bool        result =                    false;



            SQLDeleteSubscription =     SQLHelper.Delete("Subscription",
                                                        key,
                                                        primaryDeleteSubscription
                                                        );


            
            
            
            
            
            
            //Step# 1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {






                using (SqlConnection objConnDeleteSubscription = AccessDataSQLServer.GetConnection())
                {
                    objConnDeleteSubscription.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Code logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program
                    using (objCommandDeleteSubscription = new SqlCommand(SQLDeleteSubscription, objConnDeleteSubscription))
                    {
                        objCommandDeleteSubscription.Parameters.AddWithValue('@' + parameters[0],   subscription.id);
                        //Step #3: return false if record was not added successfully
                        //         return true if record was added successfully
                        rowsAffectedDeleteSubscription =    objCommandDeleteSubscription.ExecuteNonQuery();
                        if (rowsAffectedDeleteSubscription > 0)
                        {
                            result =    true;   //Record was added successfully
                        }
                    }
                    objConnDeleteSubscription.Close();
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
