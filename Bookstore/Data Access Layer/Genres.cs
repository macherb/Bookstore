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
    class Genres : BaseTable
    {
        #region Private variables

        private static string[] parameters =        { "id", "name" };


        private static int      lowestSecondary =   1;

        #endregion

        #region Public variables

        public static string    key =               parameters[ 0];
        public static string    extra =             parameters[ 1];


        public static string    SQLGetList =        SQLHelper.Select(
                                                                    "Genre",


                                                                                                    " FROM " + "Genre"









                                                                                   ,
                                                                    Primary(key, ", Genre."),
                                                                    Secondary(", Genre." + parameters[lowestSecondary], ", Genre.")
                                                                    );

        public static string    SQLGetGenre =       SQLHelper.Select("Genre",
                                                                    " FROM " + "Genre",
                                                                    Primary(string.Empty,   ", Genre."),
                                                                    Secondary(              parameters[lowestSecondary], ", Genre.")
                                                                    ) + " WHERE ";
        
        public static string    SQLAddGenre =       SQLHelper.Insert(   "Genre",
                                                                        Primary(key, ", @"),
                                                                        Secondary(", @" + parameters[lowestSecondary], ", @")
                                                                    );

        public static string    SQLUpdateGenre =    SQLHelper.Update(   "Genre",
                                                                        key + " = @" + key,
                                                                        AdditionalSecondary()
                                                                    );
            
        public static string    SQLDeleteGenre =    SQLHelper.Delete("Genre",
                                                                    key,
                                                                    string.Empty
                                                                    );

        #endregion

        #region Private functions

        /// <summary>
        /// Sets all the non-primary key(s) in a <see cref="Bookstore.Genre"/>
        /// </summary>
        /// <param name="genreReader">The <see cref="Bookstore.Genre"/> that was read from</param>
        /// <param name="objGenre">The <see cref="Bookstore.Genre"/> that will be written to</param>
        private static void SetSecondary(SqlDataReader genreReader, Genre objGenre)
        {
            objGenre.name = genreReader[parameters[ 1]].ToString();
        }

        /// <summary>
        /// Writes a <see cref="Bookstore.Genre"/> to the database's table
        /// </summary>
        /// <param name="SQLStatement">The command to write a <see cref="Bookstore.Genre"/></param>
        /// <param name="genre">The <see cref="Bookstore.Genre"/> that will have its data written to the table</param>
        /// <returns>Whether or not the <see cref="Bookstore.Genre"/> was successfully written</returns>
        private static bool WriteGenre(string SQLStatement, Genre genre)
        {
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;

            using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
            {
                objConn.Open();
                using (objCommand = new SqlCommand(SQLStatement, objConn))
                {
                    objCommand.Parameters.AddWithValue('@' + parameters[ 0],    genre.id    );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 1],    genre.name  );
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
        /// Sets the list of primary key field(s) that will be SELECT'ed
        /// </summary>
        /// <param name="primary">The list of primary key(s)</param>
        /// <param name="preprimary">What will be placed before every primary key</param>
        /// <returns>A list of primary key field(s)</returns>
        private static string Primary(string primary, string preprimary)
        {
            for (int i = 1                  ; i < lowestSecondary  ; i++)
                primary +=      preprimary + parameters[i];
            return  primary;
        }




        /// <summary>
        /// Sets the list of non-primary key field(s) that will be SELECT'ed
        /// </summary>
        /// <param name="secondary">The list of non-primary key(s)</param>
        /// <param name="presecondary">What will be placed before every non-primary key</param>
        /// <returns>A list of non-primary key field(s)</returns>
        private static string Secondary(string secondary, string presecondary)
        {
            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=    presecondary + parameters[i];
            return  secondary;
        }

        /// <summary>
        /// Sets the list of non-primary key field(s) and value(s) that will be UPDATE'd
        /// </summary>
        /// <returns>A list of non-primary key field(s) and value(s)</returns>
        private static string AdditionalSecondary()
        {
            string  secondary = parameters[lowestSecondary] + " = @" + parameters[lowestSecondary];

            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=    ", " + parameters[i] + " = @" + parameters[i];
            return  secondary;
        }










        #endregion

        #region Public functions

        /// <summary>
        /// Returns a list of generic  type objects from the table
        /// </summary>
        /// <returns>All fields of all <see cref="Bookstore.Genre"/>'s</returns>
        /// <exception cref="System.Exception" />
        public static List<Genre> GetGenres()
        {
            List<Genre>     genres =        new List<Genre>();
            SqlCommand      objCommand;
            SqlDataReader   genreReader;

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
                        using ((genreReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (genreReader.Read())
                            {
                                Genre           objGenre =  new Genre();
                                int             id;


                                Int32.TryParse(             genreReader[parameters[ 0]].ToString(), out id  );
                                objGenre.id =               id;

                                SetSecondary(genreReader, objGenre);




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
        /// <returns>All the fields (except the primary key) of a <see cref="Bookstore.Genre"/></returns>
        /// <exception cref="System.Exception" />
        public static Genre GetGenre(int parameter)//string parameter)
        {
            Genre           objGenre =      null;
            string          SQLStatement;
            SqlCommand      objCommand;
            SqlDataReader   genreReader;

            SQLStatement =                  SQLGetGenre;


            SQLStatement +=                 "Genre." + parameters[ 0] + " = @" + parameters[ 0];

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
                        using ((genreReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (genreReader.Read())
                            {
                                objGenre =      new Genre();

                                objGenre.id =   parameter;

                                SetSecondary(genreReader, objGenre);

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
        /// <returns>Whether or not the <see cref="Bookstore.Genre"/> was successfully written</returns>
        /// <exception cref="System.Exception" />
        public static bool AddGenre(Genre genre)
        {
            //TODO what if MAX is 32767
            bool            result;

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                genre.id =                  GetMax("Genre", key) + 1;

















                result =    WriteGenre(SQLAddGenre, genre);
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
        /// <returns>Whether or not the <see cref="Bookstore.Genre"/> was successfully written</returns>
        /// <exception cref="System.Exception" />
        public static bool UpdateGenre(Genre genre)
        {
            
            bool            result;

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {














































                result =    WriteGenre(SQLUpdateGenre, genre);
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
        /// <returns>Whether or not the <see cref="Bookstore.Genre"/> was successfully deleted</returns>
        /// <exception cref="System.Exception" />
        public static bool DeleteGenre(Genre genre)
        {
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
                    using (objCommand = new SqlCommand(SQLDeleteGenre, objConn))
                    {
                        objCommand.Parameters.AddWithValue('@' + parameters[ 0],    genre.id);
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
