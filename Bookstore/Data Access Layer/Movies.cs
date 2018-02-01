using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    class Movies : BaseTable
    {
        #region Private variables

        private static string[] parameters =        { "movie_number", "movie_title", "Description", "movie_year_made", "genre_id", "movie_rating", "media_type", "movie_retail_cost", "copies_on_hand", "image", "trailer" };
        private static string   foreign =           "Movie." + parameters[4];

        private static int      lowestSecondary =   1;

        #endregion

        #region Public variables

        public static string    key =               parameters[ 0];
        public static string    extra =             parameters[ 1];
        public static string    count =             parameters[ 8];

        #endregion

        #region Private functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movieReader"></param>
        /// <param name="objMovie"></param>
        private static void SetSecondary(SqlDataReader movieReader, Movie objMovie)
        {
            int                     genre_id,
                                    movie_year_made,
                                    copies_on_hand;
            float                   movie_retail_cost;

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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQLStatement"></param>
        /// <param name="movie"></param>
        /// <returns></returns>
        private static bool WriteMovie(string SQLStatement, Movie movie)
        {
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;

            using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
            {
                objConn.Open();
                using (objCommand = new SqlCommand(SQLStatement, objConn))
                {
                    objCommand.Parameters.AddWithValue('@' + parameters[ 0],    movie.movie_number      );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 1],    movie.movie_title       );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 2],    movie.Description       );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 3],    movie.movie_year_made   );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 4],    movie.genre_id          );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 5],    movie.movie_rating      );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 6],    movie.media_type        );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 7],    movie.movie_retail_cost );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 8],    movie.copies_on_hand    );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 9],    movie.image             );
                    objCommand.Parameters.AddWithValue('@' + parameters[10],    movie.trailer           );
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
        public static List<Movie> GetMovies()
        {
            List<Movie>     movies =        new List<Movie>();
            string          primary,
                            secondary,
                            SQLStatement;
            SqlCommand      objCommand;
            SqlDataReader   movieReader;
            
            primary =                       key;
            secondary =                     ", Movie." + parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", Movie.", ref secondary, ", Movie.");
            SQLStatement =                  SQLHelper.Select(
                                                            "Movie",
                                                            SQLHelper.Join(

                                                                                            " FROM " + "(" + "Movie"




                                                                                           ,
                                                                            "Genre",
                                                                            ", Genre." + Genres.extra,
                                                                            foreign,
                                                                            Genres.key
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
                        using ((movieReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (movieReader.Read())
                            {
                                Movie                   objMovie =      new Movie();
                                int                     movie_number;


                                Int32.TryParse(                         movieReader[parameters[ 0]].ToString(), out movie_number        );
                                objMovie.movie_number =                 movie_number;

                                SetSecondary(movieReader, objMovie);
                                objMovie.name =                         movieReader[Genres.extra  ].ToString();



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
            string          primary =       string.Empty,
                            secondary =     string.Empty,
                            SQLStatement;
            SqlCommand      objCommand;
            SqlDataReader   movieReader;

            secondary +=                    parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", Movie.", ref secondary, ", Movie.");
            SQLStatement =                  SQLHelper.Select("Movie", 
                                                            " FROM " + "Movie",
                                                            primary, 
                                                            secondary
                                                            ) + " WHERE ";


            SQLStatement +=                 "Movie." + parameters[ 0] + " = @" + parameters[ 0];

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
                        using ((movieReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (movieReader.Read())
                            {
                                objMovie =              new Movie();

                                objMovie.movie_number = parameter;

                                SetSecondary(movieReader, objMovie);

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
            string          primary,
                            secondary,
                            SQLStatement;
            
            bool            result;

            primary =                       key;
            secondary =                     ", @" + parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", @", ref secondary, ", @");
            SQLStatement =                  SQLHelper.Insert(   "Movie",
                                                                primary,
                                                                secondary
                                                            );

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                movie.movie_number =        GetMax("Movie", key) + 1;
                



                









                result =    WriteMovie(SQLStatement, movie);
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

            string          primary,
                            secondary,                        
                            SQLStatement;
            bool            result;

            primary =                       key                         + " = @" + key                        ;
            secondary =                     parameters[lowestSecondary] + " = @" + parameters[lowestSecondary];



            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=                ", " + parameters[i] + " = @" + parameters[i];
            SQLStatement =                  SQLHelper.Update(   "Movie",
                                                                primary,
                                                                secondary
                                                            );

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {






                



                
















                














                result =    WriteMovie(SQLStatement, movie);
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
            string      primary =       string.Empty,
                        SQLStatement;
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;



            SQLStatement =              SQLHelper.Delete("Movie",
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
                        objCommand.Parameters.AddWithValue('@' + parameters[ 0],    movie.movie_number  );
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
