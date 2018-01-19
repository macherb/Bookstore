using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    class Rentals
    {
        #region Private variables

        private static string[] parameters =        { "movie_number", "member_number", "media_checkout_date", "media_return_date" };
        private static string   foreignMovie =      "Rental." + parameters[0];
        private static string   foreignMember =     "Rental." + parameters[1];
        private static int      lowestSecondary =   3;

        #endregion

        #region Public variables

        public static string    key =               parameters[0];

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
        public static List<Rental> GetRentals()
        {
            List<Rental>    rentals =       new List<Rental>();
            string          primary,
                            secondary,
                            SQLStatement;
            SqlCommand      objCommand;
            SqlDataReader   rentalReader;

            primary =                       key;
            secondary =                     ", Rental." + parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", Rental.", ref secondary, ", Rental.");
            SQLStatement =                  SQLHelper.Select(
                                                            "Rental",
                                                            SQLHelper.Join(
                                                                            SQLHelper.Join(
                                                                                            " FROM " + "((" + "Rental",//TODO call future function that uses parenthesis counter
                                                                                            "Member",
                                                                                            ", Member." + Members.extra,
                                                                                            foreignMember,
                                                                                            Members.key
                                                                                          ),
                                                                            "Movie",
                                                                            ", Movie." + Movies.extra,
                                                                            foreignMovie,
                                                                            Movies.key
                                                                          ),
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
                        using ((rentalReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (rentalReader.Read())
                            {
                                Rental                          objRental =         new Rental();
                                int                             movie_number,
                                                                member_number;
                                DateTime                        media_checkout_date,
                                                                media_return_date;
                                Int32.TryParse(                                     rentalReader[parameters[0]].ToString(), out movie_number        );
                                objRental.movie_number =                            movie_number;
                                Int32.TryParse(                                     rentalReader[parameters[1]].ToString(), out member_number       );
                                objRental.member_number =                           member_number;
                                DateTime.TryParse(                                  rentalReader[parameters[2]].ToString(), out media_checkout_date );
                                objRental.media_checkout_date =                     media_checkout_date;
                                DateTime.TryParse(                                  rentalReader[parameters[3]].ToString(), out media_return_date   );
                                objRental.media_return_date =                       media_return_date;
                                objRental.movie_title =                             rentalReader[Movies.extra ].ToString();
                                objRental.login_name =                              rentalReader[Members.extra].ToString();
                                rentals.Add(objRental);
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
            return  rentals;
        }

        /// <summary>
        /// Returns a single record  from the table whose parameter matches a table field condition
        /// </summary>
        /// <param name="parameter">accepts a parameter to return a specific record</param>
        public static Rental GetRental(int parameter1, int parameter2, DateTime parameter3)//string parameter)
        {
            Rental          objRental =     null;
            string          primary =       key,
                            secondary =     ", Rental.",
                            SQLStatement;
            SqlCommand      objCommand;
            SqlDataReader   rentalReader;

            secondary +=                    parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", Rental.", ref secondary, ", Rental.");
            SQLStatement =                  SQLHelper.Select("Rental", 
                                                            " FROM " + "Rental", 
                                                            primary, 
                                                            secondary
                                                            ) + " WHERE ";
            if (parameter1 > -1)
            {
                SQLStatement +=             "Rental." + parameters[0] + " = @" + parameters[0];
                if ((parameter2 > -1) || (parameter3 > new DateTime(1753, 1, 1, 0, 0, 0)))
                    SQLStatement +=         " AND ";
            }

            if (parameter2 > -1)
            {
                SQLStatement +=             "Rental." + parameters[1] + " = @" + parameters[1];
                if (parameter3 > new DateTime(1753, 1, 1, 0, 0, 0))
                    SQLStatement +=         " AND ";
            }

            if (parameter3 > new DateTime(1753, 1, 1, 0, 0, 0))
            {
                SQLStatement +=             "Rental." + parameters[2] + " = @" + parameters[2];//TODO make four parameters
            }

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
                        objCommand.Parameters.AddWithValue('@' + parameters[0], parameter1);
                        objCommand.Parameters.AddWithValue('@' + parameters[1], parameter2);
                        objCommand.Parameters.AddWithValue('@' + parameters[2], parameter3);
                        //Step #3: Return the objtemp variable back to the calling UI 
                        using ((rentalReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            int i= 0;
                            while (rentalReader.Read())
                            {
                                objRental =                                             new Rental();
                                int                             movie_number,
                                                                member_number;
                                DateTime                        media_checkout_date =   new DateTime(1753, 1, 1, 0, 0, 0),
                                                                media_return_date =     new DateTime(1753, 1, 1, 0, 0, 0);

                                Int32.TryParse(                                         rentalReader[parameters[0]].ToString(), out movie_number        );
                                objRental.movie_number =                                movie_number;
                                Int32.TryParse(                                         rentalReader[parameters[1]].ToString(), out member_number       );
                                objRental.member_number =                               member_number;
                                DateTime.TryParse(                                      rentalReader[parameters[2]].ToString(), out media_checkout_date );
                                objRental.media_checkout_date =                         media_checkout_date;
                                DateTime.TryParse(                                      rentalReader[parameters[3]].ToString(), out media_return_date   );
                                objRental.media_return_date =                           media_return_date;
                                i++;
                            }
                            if (i > 1)
                                throw new Exception("More than one rental found.");
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
            return objRental;
        }

        /// <summary>
        /// Adds a record to the table with a Boolean returned status of True or False.
        /// </summary>
        /// <param name="rental">accepts a custom object of that type as a parameter</param>
        public static bool AddRental(Rental rental)
        {
            string          primary1,
                            primary2,
                            secondary1,
                            secondary2,
                            SQLStatement1,
                            SQLStatement2;

            int             rowsAffected1,
                            rowsAffected2;

            SqlCommand      objCommand1,
                            objCommand2;

            bool            result =        false;

            primary1 =                      Movies.key   + " = CASE WHEN copies_on_hand > 0 THEN @" + Movies.key + " ELSE -1 END";
            secondary1 =                    Movies.count + " = " + Movies.count + " - 1";
            SQLStatement1 =                 SQLHelper.Update(   "Movie",
                                                                primary1,
                                                                secondary1//"copies_on_hand = copies_on_hand - CASE WHEN copies_on_hand > 0 THEN 1 ELSE 0 END"
                                                            );
            
            primary2 =                      key;
            secondary2 =                    ", @" + parameters[lowestSecondary];
            PrimarySecondary(ref primary2, ", @", ref secondary2, ", @");
            SQLStatement2 =                 SQLHelper.Insert(   "Rental", 
                                                                primary2, 
                                                                secondary2
                                                            );//TODO make sure not before joindate, and if not returned make sure number of copies not zero

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                // 1












                //14
                using (SqlConnection objConn1 = AccessDataSQLServer.GetConnection())
                {
                    objConn1.Open();
                    using (objCommand1 = new SqlCommand(SQLStatement1, objConn1))
                    {
                        objCommand1.Parameters.AddWithValue('@' + parameters[0],    rental.movie_number );
                        rowsAffected1 = objCommand1.ExecuteNonQuery();
                        if (rowsAffected1 > 0)
                        {
                            result =    true;   //Record was added successfully
                        }
                    }
                    objConn1.Close();
                }
                if (!result)
                {
                    throw new Exception("There are no copies of this movie."); //Record was not added successfully
                }
                using (SqlConnection objConn2 = AccessDataSQLServer.GetConnection())
                {
                    objConn2.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Cod Logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program
                    using (objCommand2 = new SqlCommand(SQLStatement2, objConn2))
                    {
                        objCommand2.Parameters.AddWithValue('@' + parameters[0],    rental.movie_number         );
                        objCommand2.Parameters.AddWithValue('@' + parameters[1],    rental.member_number        );
                        objCommand2.Parameters.AddWithValue('@' + parameters[2],    rental.media_checkout_date  );
                        objCommand2.Parameters.AddWithValue('@' + parameters[3],    rental.media_return_date    );//TODO if after checkout, add to number of copies
                        //Step #3: return false if record was not added successfully
                        //         return true if record was added successfully  
                        rowsAffected2 = objCommand2.ExecuteNonQuery();
                        if (rowsAffected2 > 0)
                        {
                            result =    true;   //Record was added successfully
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
            return  result;
        }

        /// <summary>
        /// Updates a record in the table with a Boolean returned status of True or False
        /// </summary>
        /// <param name="rental">accepts a custom object of that type as a parameter</param>
        public static bool UpdateRental(Rental rental)
        {
            string      primary,
                        secondary,
                        SQLStatement;
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;

            primary =                   key                         + " = @" + key                        ;
            secondary =                 parameters[lowestSecondary] + " = @" + parameters[lowestSecondary];

            for (int i = 1; i < lowestSecondary; i++)
                primary +=              " AND " + parameters[i] + " = @" + parameters[i];
            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=            ", " + parameters[i] + " = @" + parameters[i];
            SQLStatement =              SQLHelper.Update(  "Rental",
                                                            primary,
                                                            secondary//TODO only if not blank
                                                        );//TODO Make it like GetRental where not all three are required
            //TODO exclude one of the WHERE's
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
                        objCommand.Parameters.AddWithValue('@' + parameters[0], rental.movie_number         );
                        objCommand.Parameters.AddWithValue('@' + parameters[1], rental.member_number        );
                        objCommand.Parameters.AddWithValue('@' + parameters[2], rental.media_checkout_date  );
                        objCommand.Parameters.AddWithValue('@' + parameters[3], rental.media_return_date    );//TODO only if not blank
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
        /// <param name="rental">accepts a custom object of that type as a parameter</param>
        public static bool DeleteRental(Rental rental)
        {
            string      primary1,
                        primary2 =      string.Empty,
                        secondary1,
                        SQLStatement1,
                        SQLStatement2;
            SqlCommand  objCommand1,
                        objCommand2;
            int         rowsAffected1,
                        rowsAffected2;
            bool        result =        false;

            primary1 =                  Movies.key   + " = @" + Movies.key  ;
            secondary1 =                Movies.count + " = " + Movies.count + " + 1";
            SQLStatement1 =             SQLHelper.Update(   "Movie",
                                                            primary1,
                                                            secondary1
                                                        );
            
            for (int i = 1; i < lowestSecondary; i++)
                primary2 +=             " AND " + parameters[i] + " = @" + parameters[i];
            SQLStatement2 =             SQLHelper.Delete("Rental",
                                                        key,
                                                        primary2
                                                        );

            //Step# 1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                using (SqlConnection objConn2 = AccessDataSQLServer.GetConnection())
                {
                    objConn2.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Code logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program
                    using (objCommand2 = new SqlCommand(SQLStatement2, objConn2))
                    {
                        objCommand2.Parameters.AddWithValue('@' + parameters[0],    rental.movie_number         );
                        objCommand2.Parameters.AddWithValue('@' + parameters[1],    rental.member_number        );
                        objCommand2.Parameters.AddWithValue('@' + parameters[2],    rental.media_checkout_date  );
                        //Step #3: return false if record was not added successfully
                        //         return true if record was added successfully
                        rowsAffected2 = objCommand2.ExecuteNonQuery();
                        if (rowsAffected2 > 0)
                        {
                            result =    true;   //Record was added successfully
                        }
                    }
                    objConn2.Close();
                }
                if (!result)
                {
                    throw new Exception("This rental does not exist."); //Record was not added successfully
                }
                using (SqlConnection objConn1 = AccessDataSQLServer.GetConnection())
                {
                    objConn1.Open();
                    using (objCommand1 = new SqlCommand(SQLStatement1, objConn1))
                    {
                        objCommand1.Parameters.AddWithValue('@' + parameters[0],    rental.movie_number );
                        rowsAffected1 = objCommand1.ExecuteNonQuery();
                        if (rowsAffected1 > 0)
                        {
                            result =    true;   //Record was added successfully
                        }
                    }
                    objConn1.Close();
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
