using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    class Movies
    {
        #region Private variables

        private static string[] parameters = { "movie_number", "movie_title", "Description", "movie_year_made", "genre_id", "movie_rating", "media_type", "movie_retail_cost", "copies_on_hand", "image", "trailer" };

        #endregion

        #region Public functions

        /// <summary>
        /// Returns a list of generic  type objects from the table
        /// </summary>
        public static List<Movie> GetMovies()
        {
            List<Movie>     movies =        new List<Movie>();
            string          secondary =     string.Empty,
                            SQLStatement;
            SqlCommand      objCommand;
            SqlDataReader   movieReader;

            for (int i = 1; i < parameters.Length; i++)
                secondary +=                ", Movie." + parameters[i];
            SQLStatement =                  SQLHelper.Select(
                                                            "Movie",
                                                            SQLHelper.Join(
                                                                            "Movie",
                                                                            " FROM " + "(" + "Movie",
                                                                            "Genre",
                                                                            ", Genre." + "name",//TODO extra2 needs to be special parameter [1]
                                                                            parameters[4],//TODO joiner1 needs to be special parameter (foreign)
                                                                            "id"//TODO joiner2 needs to be special parameter [0]
                                                                          ),
                                                            parameters[0],
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
                        using ((movieReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (movieReader.Read())
                            {
                                Movie                   objMovie =          new Movie();
                                int                     movie_number,
                                                        genre_id,
                                                        movie_year_made,
                                                        copies_on_hand;
                                float                   movie_retail_cost;

                                Int32.TryParse(                             movieReader[parameters[ 0]].ToString(), out movie_number        );
                                objMovie.movie_number =                     movie_number;
                                objMovie.movie_title =                      movieReader[parameters[ 1]].ToString();
                                objMovie.Description =                      movieReader[parameters[ 2]].ToString();
                                Int32.TryParse(                             movieReader[parameters[ 3]].ToString(), out movie_year_made     );
                                objMovie.movie_year_made =                  movie_year_made;
                                Int32.TryParse(                             movieReader[parameters[ 4]].ToString(), out genre_id            );
                                objMovie.genre_id =                         genre_id;
                                objMovie.movie_rating =                     movieReader[parameters[ 5]].ToString();
                                objMovie.media_type =                       movieReader[parameters[ 6]].ToString();
                                float.TryParse(                             movieReader[parameters[ 7]].ToString(), out movie_retail_cost   );
                                objMovie.movie_retail_cost =                movie_retail_cost;
                                Int32.TryParse(                             movieReader[parameters[ 8]].ToString(), out copies_on_hand      );
                                objMovie.copies_on_hand =                   copies_on_hand;
                                objMovie.image =                            movieReader[parameters[ 9]].ToString();
                                objMovie.trailer =                          movieReader[parameters[10]].ToString();
                                objMovie.name =                             movieReader["name"].ToString();//TODO extra2 needs to be special parameter [1]

                                movies.Add(objMovie);
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
            return movies;
        }

        /// <summary>
        /// Returns a single record  from the table whose parameter matches a table field condition
        /// </summary>
        /// <param name="parameter">accepts a parameter to return a specific record</param>
        public static Movie GetMovie(int parameter)//string parameter)
        {
            Movie           objMovie =      null;
            string          secondary =     string.Empty,
                            SQLStatement;
            SqlCommand      objCommand;
            SqlDataReader   movieReader;

            secondary =                     parameters[1];
            for (int i = 2; i < parameters.Length; i++)
                secondary +=                ", Movie." + parameters[i];
            SQLStatement =                  SQLHelper.Select("Movie", 
                                                            " FROM " + "Movie", 
                                                            "", 
                                                            secondary
                                                            ) + " WHERE Movie." + parameters[0] + " = @" + parameters[0];

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
                        using ((movieReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (movieReader.Read())
                            {
                                objMovie =                      new Movie();
                                int                             //movie_number,
                                                                genre_id,
                                                                movie_year_made,
                                                                copies_on_hand;
                                float                           movie_retail_cost;

                                //Int32.TryParse(                 movieReader[parameters[ 0]].ToString(), out movie_number        );
                                objMovie.movie_number =         parameter;//movie_number;
                                objMovie.movie_title =          movieReader[parameters[ 1]].ToString();
                                objMovie.Description =          movieReader[parameters[ 2]].ToString();
                                Int32.TryParse(                 movieReader[parameters[ 3]].ToString(), out movie_year_made     );
                                objMovie.movie_year_made =      movie_year_made;
                                Int32.TryParse(                 movieReader[parameters[ 4]].ToString(), out genre_id            );
                                objMovie.genre_id =             genre_id;
                                objMovie.movie_rating =         movieReader[parameters[ 5]].ToString();
                                objMovie.media_type =           movieReader[parameters[ 6]].ToString();
                                float.TryParse(                 movieReader[parameters[ 7]].ToString(), out movie_retail_cost   );
                                objMovie.movie_retail_cost =    movie_retail_cost;
                                Int32.TryParse(                 movieReader[parameters[ 8]].ToString(), out copies_on_hand      );
                                objMovie.copies_on_hand =       copies_on_hand;
                                objMovie.image =                movieReader[parameters[ 9]].ToString();
                                objMovie.trailer =              movieReader[parameters[10]].ToString();
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
            return  objMovie;
        }

        /// <summary>
        /// Adds a record to the table with a Boolean returned status of True or False.
        /// </summary>
        /// <param name="movie">accepts a custom object of that type as a parameter</param>
        public static bool AddMovie(Movie movie)
        {
            string      secondary =     string.Empty,
                        SQLStatement;
            int         rowsAffected;
            SqlCommand  objCommand;
            bool        result =        false;

            for (int i = 1; i < parameters.Length; i++)
                secondary +=            ", @" + parameters[i];
            SQLStatement =              SQLHelper.Insert("Movie", parameters[0], secondary);

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
                {
                    objConn.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Cod Logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program
                    using (objCommand = new SqlCommand(SQLStatement, objConn))
                    {
                        objCommand.Parameters.AddWithValue('@' + parameters[ 0], movie.movie_number     );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 1], movie.movie_title      );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 2], movie.Description      );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 3], movie.movie_year_made  );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 4], movie.genre_id         );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 5], movie.movie_rating     );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 6], movie.media_type       );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 7], movie.movie_retail_cost);
                        objCommand.Parameters.AddWithValue('@' + parameters[ 8], movie.copies_on_hand   );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 9], movie.image            );
                        objCommand.Parameters.AddWithValue('@' + parameters[10], movie.trailer          );
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

        /// <summary>
        /// Updates a record in the table with a Boolean returned status of True or False
        /// </summary>
        /// <param name="movie">accepts a custom object of that type as a parameter</param>
        public static bool UpdateMovie(Movie movie)
        {
            string      primary =       parameters[0] + " = @" + parameters[0],
                        secondary,
                        SQLStatement;
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;

            secondary =                 parameters[1] + " = @" + parameters[1];
            for (int i = 2; i < parameters.Length; i++)
                secondary +=            ", " + parameters[i] + " = @" + parameters[i];
            SQLStatement =              SQLHelper.Update("Movie", primary, secondary);

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
                        objCommand.Parameters.AddWithValue('@' + parameters[ 0], movie.movie_number     );//TODO Does primary need to be moved to the end?
                        objCommand.Parameters.AddWithValue('@' + parameters[ 1], movie.movie_title      );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 2], movie.Description      );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 3], movie.movie_year_made  );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 4], movie.genre_id         );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 5], movie.movie_rating     );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 6], movie.media_type       );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 7], movie.movie_retail_cost);
                        objCommand.Parameters.AddWithValue('@' + parameters[ 8], movie.copies_on_hand   );
                        objCommand.Parameters.AddWithValue('@' + parameters[ 9], movie.image            );
                        objCommand.Parameters.AddWithValue('@' + parameters[10], movie.trailer          );
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

        /// <summary>
        /// Deletes a record from the database with a Boolean returned status of True or False
        /// </summary>
        /// <param name="movie">accepts a custom object of that type as a parameter</param>
        public static bool DeleteMovie(Movie movie)
        {
            string      SQLStatement =  SQLHelper.Delete("Movie", parameters[0], string.Empty);
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
                        objCommand.Parameters.AddWithValue('@' + parameters[0], movie.movie_number);
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
