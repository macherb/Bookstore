using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    class Genres
    {
        #region Private variables

        private static string[] parameters =        { "id", "name" };
        private static int      lowestSecondary =   1;

        #endregion

        #region Public variables

        public static string    key =               parameters[0];
        public static string    extra =             parameters[1];

        #endregion
        private static void PrimarySecondary(ref string primary, string preprimary, ref string secondary, string presecondary)
        {
            for (int i = 1; i < lowestSecondary; i++)
                primary +=                  preprimary + parameters[i];
            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=                presecondary + parameters[i];
        }
        #region Public functions
//TODO get max should be its own function
        /// <summary>
        /// Returns a list of generic  type objects from the table
        /// </summary>
        public static List<Genre> GetGenres()
        {
            List<Genre>     genres =        new List<Genre>();
            string          primary,
                            secondary =     string.Empty,
                            SQLStatement;
            SqlCommand      objCommand;
            SqlDataReader   genreReader;

            primary =                       parameters[0];
            secondary +=                    ", Genre." + parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", Genre.", ref secondary, ", Genre.");
            /*

            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=                ", Genre." + parameters[i];*/
            SQLStatement =                  SQLHelper.Select(
                                                            "Genre",


                                                                                            " FROM " + "Genre"









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
                        using ((genreReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (genreReader.Read())
                            {
                                Genre           objGenre =  new Genre();
                                int             id;
                                Int32.TryParse(             genreReader[parameters[0]].ToString(), out id);
                                objGenre.id =               id;
                                objGenre.name =             genreReader[parameters[1]].ToString();
                                genres.Add(objGenre);
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
            return genres;
        }

        /// <summary>
        /// Returns a single record  from the table whose parameter matches a table field condition
        /// </summary>
        /// <param name="parameter">accepts a parameter to return a specific record</param>
        public static Genre GetGenre(int parameter)//string parameter)
        {
            Genre           objGenre =      null;
            string          primary =       string.Empty,
                            secondary =     string.Empty,
                            SQLStatement;
            SqlCommand      objCommand;
            SqlDataReader   genreReader;

            secondary +=                    parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", Genre.", ref secondary, ", Genre.");
            /*
            
            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=                ", Genre." + parameters[i];*/
            SQLStatement =                  SQLHelper.Select("Genre",
                                                            " FROM " + "Genre",
                                                            primary,
                                                            secondary
                                                            ) + " WHERE ";


            SQLStatement +=                 "Genre." + parameters[0] + " = @" + parameters[0];

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
                        using ((genreReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (genreReader.Read())
                            {
                                objGenre =      new Genre();
                                //int             id;
                                //Int32.TryParse( genreReader[parameters[0]].ToString(), out id);
                                objGenre.id =   parameter;//id;
                                objGenre.name = genreReader[parameters[1]].ToString();
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
            return  objGenre;
        }

        /// <summary>
        /// Adds a record to the table with a Boolean returned status of True or False.
        /// </summary>
        /// <param name="genre">accepts a custom object of that type as a parameter</param>
        public static bool AddGenre(Genre genre)
        {
            string          primary,
                            secondary =     string.Empty,
                            SQLStatement1 = "SELECT MAX(id) AS max_genre FROM Genre",//Helper.Select("Genre", "MAX(" + parameters[0] + ") AS max_genre", ""),//"SELECT MAX(id) FROM Genre",
                            SQLStatement2 = "INSERT INTO Genre VALUES (max_genre, @name)";//Helper.Insert("Genre", parameters[0], ", @" + parameters[1]);
            //TODO what if MAX is nothing?
            int             rowsAffected,
                            max;
            SqlCommand      objCommand1,
                            objCommand2;
            SqlDataReader   genreReader;
            bool            result =        false;

            primary =                       parameters[0];
            secondary +=                    ", @" + parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", @", ref secondary, ", @");
            /*

            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=                ", @" + parameters[i];*/
            SQLStatement2 =                 SQLHelper.Insert(   "Genre",
                                                                primary,
                                                                secondary
                                                            );

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                using (SqlConnection objConn1 = AccessDataSQLServer.GetConnection())
                {
                    objConn1.Open();
                    using (objCommand1 = new SqlCommand(SQLStatement1, objConn1))
                    {
                        using ((genreReader = objCommand1.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            genreReader.Read();
                            Int32.TryParse(genreReader[0].ToString(), out max);
                        }
                    }
                    objConn1.Close();
                }
                using (SqlConnection objConn2 = AccessDataSQLServer.GetConnection())
                {
                    objConn2.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Cod Logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program
                    using (objCommand2 = new SqlCommand(SQLStatement2, objConn2))
                    {
                        objCommand2.Parameters.AddWithValue('@' + parameters[0], max + 1    );
                        objCommand2.Parameters.AddWithValue('@' + parameters[1], genre.name );
                        //Step #3: return false if record was not added successfully
                        //         return true if record was added successfully  
                        rowsAffected =  objCommand2.ExecuteNonQuery();
                        if (rowsAffected > 0)
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
        /// <param name="genre">accepts a custom object of that type as a parameter</param>
        public static bool UpdateGenre(Genre genre)
        {
            string      primary,
                        secondary =     string.Empty,
                        SQLStatement;
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;

            primary =                   parameters[0              ] + " = @" + parameters[0              ];
            secondary +=                parameters[lowestSecondary] + " = @" + parameters[lowestSecondary];
            

            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=            ", " + parameters[i] + " = @" + parameters[i];
            SQLStatement =              SQLHelper.Update(   "Genre", 
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
                        objCommand.Parameters.AddWithValue('@' + parameters[0], genre.id    );
                        objCommand.Parameters.AddWithValue('@' + parameters[1], genre.name  );
                        //Step #3: return false if record was not added successfully
                        //         return true if record was added successfully
                        rowsAffected =  objCommand.ExecuteNonQuery();
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

        /// <summary>
        /// Deletes a record from the database with a Boolean returned status of True or False
        /// </summary>
        /// <param name="genre">accepts a custom object of that type as a parameter</param>
        public static bool DeleteGenre(Genre genre)
        {
            string      primary =       string.Empty,
                        SQLStatement;
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;



            SQLStatement =              SQLHelper.Delete("Genre",
                                                        parameters[0],
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
                        objCommand.Parameters.AddWithValue('@' + parameters[0], genre.id);
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
