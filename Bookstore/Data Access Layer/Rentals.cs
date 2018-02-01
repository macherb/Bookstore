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
        private static string   foreignMovie =      "Rental." + parameters[ 0];
        private static string   foreignMember =     "Rental." + parameters[ 1];
        private static int      lowestSecondary =   3;

        #endregion

        #region Public variables

        public static string    key =               parameters[ 0];



        #endregion

        #region Private functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rentalReader"></param>
        /// <param name="objRental"></param>
        private static void SetSecondary(SqlDataReader rentalReader, Rental objRental)
        {
            DateTime                        media_return_date;

            DateTime.TryParse(                                  rentalReader[parameters[ 3]].ToString(),    out media_return_date   );
            objRental.media_return_date =                       media_return_date;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQLStatement"></param>
        /// <param name="rental"></param>
        /// <returns></returns>
        private static bool WriteRental(string SQLStatement, Rental rental)
        {
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;

            using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
            {
                objConn.Open();
                using (objCommand = new SqlCommand(SQLStatement, objConn))
                {
                    objCommand.Parameters.AddWithValue('@' + parameters[ 0],    rental.movie_number         );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 1],    rental.member_number        );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 2],    rental.media_checkout_date  );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 3],    rental.media_return_date    );
                    rowsAffected =  objCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        result =    true;   //Record was added successfully
                    }
                }
                objConn.Close();
            }
            return result;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        private static bool ReadJoindate(Rental rental)
        {
            string          SQLStatement;
            SqlCommand      objCommand;
            SqlDataReader   memberReader;
            bool            result =        false;

            SQLStatement =                  SQLHelper.Select("Member",
                                                            " FROM " + "Member",
                                                            string.Empty,
                                                            Members.extra1
                                                            ) + " WHERE ";

            SQLStatement +=                 "Member." + Members.key + " = @" + Members.key;

            using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
            {
                objConn.Open();
                using (objCommand = new SqlCommand(SQLStatement, objConn))
                {
                    objCommand.Parameters.AddWithValue('@' + Members.key, rental.member_number);
                    using ((memberReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                    {
                        while (memberReader.Read())
                        {
                            DateTime joindate = new DateTime(1753, 1, 1, 0, 0, 0);

                            DateTime.TryParse(memberReader[Members.extra1].ToString(), out joindate);
                            rental.joindate = joindate;
                        }
                    }
                }
                objConn.Close();
            }
            if (rental.joindate > rental.media_checkout_date)
            {
                result =    true;
            }

            return  result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        private static bool SQLUpdateMovieSubtract(Rental rental)
        {
            string          primary,
                            secondary,
                            SQLStatement;
            SqlCommand      objCommand;
            int             rowsAffected;
            bool            result =        false;

            primary =                       Movies.key   + " = CASE WHEN " + Movies.count + " > 0 THEN @" + Movies.key + " ELSE -1 END";
            secondary =                     Movies.count + " = " + Movies.count + " - 1";
            SQLStatement =                  SQLHelper.Update(   "Movie",
                                                            primary,
                                                            secondary//"copies_on_hand = copies_on_hand - CASE WHEN copies_on_hand > 0 THEN 1 ELSE 0 END"
                                                            );

            using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
            {
                objConn.Open();
                using (objCommand = new SqlCommand(SQLStatement, objConn))
                {
                    objCommand.Parameters.AddWithValue('@' + parameters[ 0],    rental.movie_number );
                    rowsAffected =  objCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        result = true;   //Record was added successfully
                    }
                }
                objConn.Close();
            }

            return          result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        private static bool SQLUpdateMovieAdd(Rental rental)
        {
            string          primary,
                            secondary,
                            SQLStatement;
            SqlCommand      objCommand;
            int             rowsAffected;
            bool            result =        false;
            
            primary =                       Movies.key   + " = @" + Movies.key  ;
            secondary =                     Movies.count + " = " + Movies.count + " + 1";
            SQLStatement =                  SQLHelper.Update(   "Movie",
                                                            primary,
                                                            secondary
                                                            );
            
            using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
            {
                objConn.Open();
                using (objCommand = new SqlCommand(SQLStatement, objConn))
                {
                    objCommand.Parameters.AddWithValue('@' + parameters[ 0],    rental.movie_number );
                    rowsAffected =  objCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        result = true;   //Record was added successfully
                    }
                }
                objConn.Close();
            }

            return          result;
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
                                                                joindate;
                                Int32.TryParse(                                     rentalReader[parameters[ 0]].ToString(),    out movie_number        );
                                objRental.movie_number =                            movie_number;
                                Int32.TryParse(                                     rentalReader[parameters[ 1]].ToString(),    out member_number       );
                                objRental.member_number =                           member_number;
                                DateTime.TryParse(                                  rentalReader[parameters[ 2]].ToString(),    out media_checkout_date );
                                objRental.media_checkout_date =                     media_checkout_date;
                                SetSecondary(rentalReader, objRental);
                                objRental.movie_title =                             rentalReader[Movies.extra  ].ToString();
                                DateTime.TryParse(                                  rentalReader[Members.extra1].ToString(),    out joindate            );
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
        /// <param name="parameter1">accepts a parameter to return a specific record, movie_number</param>
        /// <param name="parameter2">member_number</param>
        /// <param name="parameter3">media_checkout_date</param>
        /// <returns></returns>
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
                SQLStatement +=             "Rental." + parameters[ 0] + " = @" + parameters[ 0];
                if ((parameter2 > -1) || (parameter3 > new DateTime(1753, 1, 1, 0, 0, 0)))
                    SQLStatement +=         " AND ";
            }

            if (parameter2 > -1)
            {
                SQLStatement +=             "Rental." + parameters[ 1] + " = @" + parameters[ 1];
                if (parameter3 > new DateTime(1753, 1, 1, 0, 0, 0))
                    SQLStatement +=         " AND ";
            }

            if (parameter3 > new DateTime(1753, 1, 1, 0, 0, 0))
            {
                SQLStatement +=             "Rental." + parameters[ 2] + " = @" + parameters[ 2];//TODO make four parameters
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
                        objCommand.Parameters.AddWithValue('@' + parameters[ 0],    parameter1  );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 1],    parameter2  );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 2],    parameter3  );
                        //Step #3: Return the objtemp variable back to the calling UI 
                        using ((rentalReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (rentalReader.Read())
                            {
                                objRental =                                             new Rental();
                                int                             movie_number,
                                                                member_number;
                                DateTime                        media_checkout_date =   new DateTime(1753, 1, 1, 0, 0, 0);

                                Int32.TryParse(                                         rentalReader[parameters[ 0]].ToString(),    out movie_number        );
                                objRental.movie_number =                                movie_number;
                                Int32.TryParse(                                         rentalReader[parameters[ 1]].ToString(),    out member_number       );
                                objRental.member_number =                               member_number;
                                DateTime.TryParse(                                      rentalReader[parameters[ 2]].ToString(),    out media_checkout_date );
                                objRental.media_checkout_date =                         media_checkout_date;
                                SetSecondary(rentalReader, objRental);
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
            return  objRental;
        }

        /// <summary>
        /// Adds a record to the table with a Boolean returned status of True or False.
        /// </summary>
        /// <param name="rental">accepts a custom object of that type as a parameter</param>
        public static bool AddRental(Rental rental)
        {
            string          primary,
                            secondary,
                            SQLStatement; 

            bool            result =        false;

            primary =                       key;
            secondary =                     ", @" + parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", @", ref secondary, ", @");
            SQLStatement =                  SQLHelper.Insert(   "Rental",
                                                                primary,
                                                                secondary
                                                            );//TODO make sure not before joindate, and if not returned make sure number of copies not zero

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {

                result =    ReadJoindate(rental);
                if (result)
                {
                    throw new Exception("The check out date is before the join date."); //Record was not added successfully
                }

                if (rental.media_return_date < rental.media_checkout_date) //Only when the movie hasn't been returned
                {
                    result =    SQLUpdateMovieSubtract(rental);
                    if (!result)
                    {
                        throw new Exception("There are no copies of this movie."); //Record was not added successfully
                    }
                }
                result =    WriteRental(SQLStatement, rental);
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
        /// <param name="newRental">accepts a custom object of that type as a parameter</param>
        public static bool UpdateRental(ref Rental newRental)
        {
            Rental          oldRental;
            string          primary,
                            secondary,
                            SQLStatement;
            bool            result;

            primary =                       key                         + " = @" + key                        ;
            secondary =                     parameters[lowestSecondary] + " = @" + parameters[lowestSecondary];

            for (int i = 1; i < lowestSecondary; i++)
                primary +=                  " AND " + parameters[i] + " = @" + parameters[i];
            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=                ", " + parameters[i] + " = @" + parameters[i];
            SQLStatement =                  SQLHelper.Update(  "Rental",
                                                                primary,
                                                                secondary//TODO only if not blank
                                                            );
            //TODO exclude one of the WHERE's
            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                oldRental =                 GetRental(newRental.movie_number, newRental.member_number, newRental.media_checkout_date);
                if (oldRental == null)
                {
                    throw new Exception("This rental does not exist."); //Record was not added successfully
                }

                result =    ReadJoindate(oldRental);
                if (result)
                {
                    throw new Exception("The check out date is before the join date."); //Record was not added successfully
                }

                if (newRental.movie_number <= -1)
                {
                    newRental.movie_number =        oldRental.movie_number;
                }

                if (newRental.member_number <= -1)
                {
                    newRental.member_number =       oldRental.member_number;
                }

                if (newRental.media_checkout_date <= new DateTime(1753, 1, 1, 0, 0, 0))
                {
                    newRental.media_checkout_date = oldRental.media_checkout_date;
                }

                if      ((oldRental.media_return_date <  oldRental.media_checkout_date) &&  //Only when the movie hasn't been returned
                         (newRental.media_return_date >= newRental.media_checkout_date)   )
                {
                    result =    SQLUpdateMovieAdd(newRental);
                }
                else if ((oldRental.media_return_date >= oldRental.media_checkout_date) &&  //Only when the movie hasn't been returned
                         (newRental.media_return_date <  newRental.media_checkout_date)   )
                {
                    result =    SQLUpdateMovieSubtract(newRental);
                    if (!result)
                    {
                        throw new Exception("There are no copies of this movie."); //Record was not added successfully
                    }
                }

                result =    WriteRental(SQLStatement, newRental);
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
            string      primary =       string.Empty,
                        SQLStatement;
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;

            for (int i = 1; i < lowestSecondary; i++)
                primary +=              " AND " + parameters[i] + " = @" + parameters[i];
            SQLStatement =              SQLHelper.Delete("Rental",//"SELECT media_return_date FROM "
                                                        key,
                                                        primary
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

                using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
                {
                    objConn.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Code logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program
                    using (objCommand = new SqlCommand(SQLStatement, objConn))
                    {
                        objCommand.Parameters.AddWithValue('@' + parameters[ 0],    rental.movie_number         );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 1],    rental.member_number        );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 2],    rental.media_checkout_date  );
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
                if (!result)
                {
                    throw new Exception("This rental does not exist."); //Record was not added successfully
                }
                if (rental.media_return_date < rental.media_checkout_date) //Only when the movie hasn't been returned
                {
                    result =    SQLUpdateMovieAdd(rental);
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
