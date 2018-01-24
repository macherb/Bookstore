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
                                                                                            ", Member." + Members.extra2 + ", Member." + Members.extra1,
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
                                                                media_return_date,
                                                                joindate;
                                Int32.TryParse(                                     rentalReader[parameters[0]].ToString(), out movie_number        );
                                objRental.movie_number =                            movie_number;
                                Int32.TryParse(                                     rentalReader[parameters[1]].ToString(), out member_number       );
                                objRental.member_number =                           member_number;
                                DateTime.TryParse(                                  rentalReader[parameters[2]].ToString(), out media_checkout_date );
                                objRental.media_checkout_date =                     media_checkout_date;
                                DateTime.TryParse(                                  rentalReader[parameters[3]].ToString(), out media_return_date   );
                                objRental.media_return_date =                       media_return_date;
                                objRental.movie_title =                             rentalReader[Movies.extra ].ToString();
                                DateTime.TryParse(                                  rentalReader[Members.extra1].ToString(), out joindate   );
                                objRental.joindate =                                joindate;
                                objRental.login_name =                              rentalReader[Members.extra2].ToString();
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
                    int i = 0;
                    using (objCommand = new SqlCommand(SQLStatement, objConn))
                    {
                        objCommand.Parameters.AddWithValue('@' + parameters[0], parameter1);
                        objCommand.Parameters.AddWithValue('@' + parameters[1], parameter2);
                        objCommand.Parameters.AddWithValue('@' + parameters[2], parameter3);
                        //Step #3: Return the objtemp variable back to the calling UI 
                        using ((rentalReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
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
                        }
                    }
                    objConn.Close();
                    if (i > 1)
                        throw new Exception("More than one rental found.");
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
            string          primaryUpdateMovie,
                            primaryInsertRental,
                            secondaryUpdateMovie,
                            secondaryInsertRental,
                            SQLSelectMember,
                            SQLUpdateMovie,//TODO same as SQLUpdateMovieSubtract
                            SQLInsertRental;
            SqlDataReader   memberReader;
            int             rowsAffectedUpdateMovie,
                            rowsAffectedInsertRental
                            ;
            SqlCommand      objCommandSelectMember,
                            objCommandUpdateMovie,
                            objCommandInsertRental;
            bool            result =        false;

            SQLSelectMember =               SQLHelper.Select("Member",
                                                            " FROM " + "Member",
                                                            string.Empty,
                                                            Members.extra1
                                                            ) + " WHERE ";


            SQLSelectMember +=              "Member." + Members.key + " = @" + Members.key;

            primaryUpdateMovie =            Movies.key   + " = CASE WHEN " + Movies.count + " > 0 THEN @" + Movies.key + " ELSE -1 END";
            secondaryUpdateMovie =          Movies.count + " = " + Movies.count + " - 1";
            SQLUpdateMovie =                SQLHelper.Update(   "Movie",
                                                                primaryUpdateMovie,
                                                                secondaryUpdateMovie//"copies_on_hand = copies_on_hand - CASE WHEN copies_on_hand > 0 THEN 1 ELSE 0 END"
                                                            );

            primaryInsertRental =           key;
            secondaryInsertRental =         ", @" + parameters[lowestSecondary];
            PrimarySecondary(ref primaryInsertRental, ", @", ref secondaryInsertRental, ", @");
            SQLInsertRental =               SQLHelper.Insert(   "Rental",
                                                                primaryInsertRental,
                                                                secondaryInsertRental
                                                            );//TODO make sure not before joindate, and if not returned make sure number of copies not zero

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {














                using (SqlConnection objConnSelectMember = AccessDataSQLServer.GetConnection())
                {
                    objConnSelectMember.Open();
                    int i = 0;
                    using (objCommandSelectMember = new SqlCommand(SQLSelectMember, objConnSelectMember))
                    {
                        objCommandSelectMember.Parameters.AddWithValue('@' + Members.key,   rental.member_number);
                        using ((memberReader = objCommandSelectMember.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (memberReader.Read())
                            {
                                DateTime            joindate =  new DateTime(1753, 1, 1, 0, 0, 0);

                                DateTime.TryParse(memberReader[Members.extra1].ToString(), out joindate);
                                rental.joindate =               joindate;
                            }
                        }
                    }
                    objConnSelectMember.Close();
                    if (i > 1)
                        throw new Exception("More than one rental found.");
                }
                if (rental.joindate > rental.media_checkout_date)
                {
                    throw new Exception("The check out date is before the join date."); //Record was not added successfully
                }

                if (rental.media_return_date < rental.media_checkout_date) //Only when the movie hasn't been returned
                {
                    using (SqlConnection objConnUpdateMovie = AccessDataSQLServer.GetConnection())
                    {
                        objConnUpdateMovie.Open();
                        using (objCommandUpdateMovie = new SqlCommand(SQLUpdateMovie, objConnUpdateMovie))
                        {
                            objCommandUpdateMovie.Parameters.AddWithValue('@' + parameters[0], rental.movie_number);
                            rowsAffectedUpdateMovie =   objCommandUpdateMovie.ExecuteNonQuery();
                            if (rowsAffectedUpdateMovie > 0)
                            {
                                result = true;   //Record was added successfully
                            }
                        }
                        objConnUpdateMovie.Close();
                    }
                    if (!result)
                    {
                        throw new Exception("There are no copies of this movie."); //Record was not added successfully
                    }
                }
                using (SqlConnection objConnInsertRental = AccessDataSQLServer.GetConnection())
                {
                    objConnInsertRental.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Cod Logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program
                    using (objCommandInsertRental = new SqlCommand(SQLInsertRental, objConnInsertRental))
                    {
                        objCommandInsertRental.Parameters.AddWithValue('@' + parameters[0], rental.movie_number         );
                        objCommandInsertRental.Parameters.AddWithValue('@' + parameters[1], rental.member_number        );
                        objCommandInsertRental.Parameters.AddWithValue('@' + parameters[2], rental.media_checkout_date  );
                        objCommandInsertRental.Parameters.AddWithValue('@' + parameters[3], rental.media_return_date    );//TODO if after checkout, add to number of copies
                        //Step #3: return false if record was not added successfully
                        //         return true if record was added successfully  
                        rowsAffectedInsertRental =  objCommandInsertRental.ExecuteNonQuery();
                        if (rowsAffectedInsertRental > 0)
                        {
                            result =    true;   //Record was added successfully
                        }
                    }
                    objConnInsertRental.Close();
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
        public static bool UpdateRental(ref Rental newRental)
        {
            Rental          oldRental;
            string          primaryUpdateRental,
                            primaryUpdateMovieAdd,
                            primaryUpdateMovieSubtract,
                            secondaryUpdateRental,
                            secondaryUpdateMovieAdd,
                            secondaryUpdateMovieSubtract,
                            SQLSelectMember,
                            SQLUpdateRental,
                            SQLUpdateMovieAdd,
                            SQLUpdateMovieSubtract;
            SqlDataReader   memberReader;
            SqlCommand      objCommandSelectMember,
                            objCommandUpdateRental,
                            objCommandUpdateMovie;
            int             rowsAffectedUpdateRental,
                            rowsAffectedUpdateMovie;
            bool            result =        false;

            SQLSelectMember =               SQLHelper.Select("Member",
                                                            " FROM " + "Member",
                                                            string.Empty,
                                                            Members.extra1
                                                            ) + " WHERE ";


            SQLSelectMember +=              "Member." + Members.key + " = @" + Members.key;

            primaryUpdateRental =           key                         + " = @" + key                        ;
            secondaryUpdateRental =         parameters[lowestSecondary] + " = @" + parameters[lowestSecondary];

            for (int i = 1; i < lowestSecondary; i++)
                primaryUpdateRental +=      " AND " + parameters[i] + " = @" + parameters[i];
            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondaryUpdateRental +=    ", " + parameters[i] + " = @" + parameters[i];
            SQLUpdateRental =               SQLHelper.Update(  "Rental",
                                                                primaryUpdateRental,
                                                                secondaryUpdateRental//TODO only if not blank
                                                            );
            //TODO exclude one of the WHERE's
            primaryUpdateMovieAdd =         Movies.key   + " = @" + Movies.key  ;
            secondaryUpdateMovieAdd =       Movies.count + " = " + Movies.count + " + 1";
            SQLUpdateMovieAdd =             SQLHelper.Update(   "Movie",
                                                                primaryUpdateMovieAdd,
                                                                secondaryUpdateMovieAdd
                                                            );

            primaryUpdateMovieSubtract =    Movies.key   + " = CASE WHEN " + Movies.count + " > 0 THEN @" + Movies.key + " ELSE -1 END";
            secondaryUpdateMovieSubtract =  Movies.count + " = " + Movies.count + " - 1";
            SQLUpdateMovieSubtract =        SQLHelper.Update(   "Movie",
                                                                primaryUpdateMovieSubtract,
                                                                secondaryUpdateMovieSubtract//"copies_on_hand = copies_on_hand - CASE WHEN copies_on_hand > 0 THEN 1 ELSE 0 END"
                                                            );
            
            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                oldRental =                 GetRental(newRental.movie_number, newRental.member_number, newRental.media_checkout_date);
                if (oldRental == null)
                {
                    throw new Exception("This rental does not exist."); //Record was not added successfully
                }

                using (SqlConnection objConnSelectMember = AccessDataSQLServer.GetConnection())
                {
                    objConnSelectMember.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Code logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program            
                    int i = 0;
                    using (objCommandSelectMember = new SqlCommand(SQLSelectMember, objConnSelectMember))
                    {
                        objCommandSelectMember.Parameters.AddWithValue('@' + Members.key,   oldRental.member_number);
                        //Step #3: Return the objtemp variable back to the calling UI 
                        using ((memberReader = objCommandSelectMember.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (memberReader.Read())
                            {
                                DateTime joindate = new DateTime(1753, 1, 1, 0, 0, 0);

                                DateTime.TryParse(memberReader[Members.extra1].ToString(), out joindate);
                                oldRental.joindate = joindate;
                            }
                        }
                    }
                    objConnSelectMember.Close();
                    if (i > 1)
                        throw new Exception("More than one rental found.");
                }
                if (oldRental.joindate > newRental.media_checkout_date)
                {
                    throw new Exception("The check out date is before the join date."); //Record was not added successfully
                }

                if (newRental.movie_number <= -1)
                {
                    newRental.movie_number =    oldRental.movie_number;
                }

                if (newRental.member_number <= -1)
                {
                    newRental.member_number =   oldRental.member_number;
                }

                if (newRental.media_checkout_date <= new DateTime(1753, 1, 1, 0, 0, 0))
                {
                    newRental.media_checkout_date = oldRental.media_checkout_date;
                }

                using (SqlConnection objConnUpdateRental = AccessDataSQLServer.GetConnection())
                {
                    objConnUpdateRental.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Code logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program
                    using (objCommandUpdateRental = new SqlCommand(SQLUpdateRental, objConnUpdateRental))
                    {
                        objCommandUpdateRental.Parameters.AddWithValue('@' + parameters[0], newRental.movie_number          );
                        objCommandUpdateRental.Parameters.AddWithValue('@' + parameters[1], newRental.member_number         );
                        objCommandUpdateRental.Parameters.AddWithValue('@' + parameters[2], newRental.media_checkout_date   );
                        objCommandUpdateRental.Parameters.AddWithValue('@' + parameters[3], newRental.media_return_date     );//TODO only if not blank
                        //Step #3: return false if record was not added successfully
                        //         return true if record was added successfully           
                        rowsAffectedUpdateRental =  objCommandUpdateRental.ExecuteNonQuery();
                        if (rowsAffectedUpdateRental > 0)
                        {
                            result =    true;   //Record was added successfully
                        }
                    }
                    objConnUpdateRental.Close();
                }

                if      ((oldRental.media_return_date <  oldRental.media_checkout_date) &&  //Only when the movie hasn't been returned
                         (newRental.media_return_date >= newRental.media_checkout_date)   )
                {
                    using (SqlConnection objConnUpdateMovie = AccessDataSQLServer.GetConnection())
                    {
                        objConnUpdateMovie.Open();
                        using (objCommandUpdateMovie = new SqlCommand(SQLUpdateMovieAdd, objConnUpdateMovie))
                        {
                            objCommandUpdateMovie.Parameters.AddWithValue('@' + parameters[0],  newRental.movie_number);
                            rowsAffectedUpdateMovie =   objCommandUpdateMovie.ExecuteNonQuery();
                            if (rowsAffectedUpdateMovie > 0)
                            {
                                result = true;   //Record was added successfully
                            }
                        }
                        objConnUpdateMovie.Close();
                    }
                }
                else if ((oldRental.media_return_date >= oldRental.media_checkout_date) &&  //Only when the movie hasn't been returned
                         (newRental.media_return_date <  newRental.media_checkout_date)   )
                {
                    using (SqlConnection objConnUpdateMovie = AccessDataSQLServer.GetConnection())
                    {
                        objConnUpdateMovie.Open();
                        using (objCommandUpdateMovie = new SqlCommand(SQLUpdateMovieSubtract, objConnUpdateMovie))
                        {
                            objCommandUpdateMovie.Parameters.AddWithValue('@' + parameters[0],  newRental.movie_number);
                            rowsAffectedUpdateMovie =   objCommandUpdateMovie.ExecuteNonQuery();
                            if (rowsAffectedUpdateMovie > 0)
                            {
                                result = true;   //Record was added successfully
                            }
                        }
                        objConnUpdateMovie.Close();
                    }
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
        /// Deletes a record from the database with a Boolean returned status of True or False
        /// </summary>
        /// <param name="rental">accepts a custom object of that type as a parameter</param>
        public static bool DeleteRental(ref Rental rental)
        {
            string      primaryDeleteRental =   string.Empty,
                        primaryUpdateMovie,
                        secondaryUpdateMovie,
                        SQLDeleteRental,
                        SQLUpdateMovie;//TODO same as SQLUpdateMovieAdd
            SqlCommand  objCommandDeleteRental,
                        objCommandUpdateMovie;
            int         rowsAffectedDeleteRental,
                        rowsAffectedUpdateMovie;
            bool        result =                false;

            for (int i = 1; i < lowestSecondary; i++)
                primaryDeleteRental +=  " AND " + parameters[i] + " = @" + parameters[i];
            SQLDeleteRental =           SQLHelper.Delete("Rental",//"SELECT media_return_date FROM "
                                                        key,
                                                        primaryDeleteRental
                                                        );

            primaryUpdateMovie =        Movies.key   + " = @" + Movies.key  ;
            secondaryUpdateMovie =      Movies.count + " = " + Movies.count + " + 1";
            SQLUpdateMovie =            SQLHelper.Update(   "Movie",
                                                            primaryUpdateMovie,
                                                            secondaryUpdateMovie
                                                        );
            
            //Step# 1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                rental =                GetRental(rental.movie_number, rental.member_number, rental.media_checkout_date);
                if (rental == null)
                {
                    throw new Exception("This rental does not exist."); //Record was not added successfully
                }

                using (SqlConnection objConnDeleteRental = AccessDataSQLServer.GetConnection())
                {
                    objConnDeleteRental.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Code logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program
                    using (objCommandDeleteRental = new SqlCommand(SQLDeleteRental, objConnDeleteRental))
                    {
                        objCommandDeleteRental.Parameters.AddWithValue('@' + parameters[0], rental.movie_number         );
                        objCommandDeleteRental.Parameters.AddWithValue('@' + parameters[1], rental.member_number        );
                        objCommandDeleteRental.Parameters.AddWithValue('@' + parameters[2], rental.media_checkout_date  );
                        //Step #3: return false if record was not added successfully
                        //         return true if record was added successfully
                        rowsAffectedDeleteRental =  objCommandDeleteRental.ExecuteNonQuery();
                        if (rowsAffectedDeleteRental > 0)
                        {
                            result =    true;   //Record was added successfully
                        }
                    }
                    objConnDeleteRental.Close();
                }
                if (!result)
                {
                    throw new Exception("This rental does not exist."); //Record was not added successfully
                }
                if (rental.media_return_date < rental.media_checkout_date) //Only when the movie hasn't been returned
                {
                    using (SqlConnection objConnUpdateMovie = AccessDataSQLServer.GetConnection())
                    {
                        objConnUpdateMovie.Open();
                        using (objCommandUpdateMovie = new SqlCommand(SQLUpdateMovie, objConnUpdateMovie))
                        {
                            objCommandUpdateMovie.Parameters.AddWithValue('@' + parameters[0],  rental.movie_number);
                            rowsAffectedUpdateMovie =   objCommandUpdateMovie.ExecuteNonQuery();
                            if (rowsAffectedUpdateMovie > 0)
                            {
                                result = true;   //Record was added successfully
                            }
                        }
                        objConnUpdateMovie.Close();
                    }
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
