using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    class Genres : BaseTable
    {
        #region Private variables

        private static string[] parameters =        { "id", "name" };


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
        /// <param name="genreReader"></param>
        /// <param name="objGenre"></param>
        private static void SetSecondary(SqlDataReader genreReader, Genre objGenre)
        {
            objGenre.name = genreReader[parameters[ 1]].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQLStatement"></param>
        /// <param name="genre"></param>
        /// <returns></returns>
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
        public static List<Genre> GetGenres()
        {
            List<Genre>     genres =        new List<Genre>();
            string          primary,
                            secondary,
                            SQLStatement;
            SqlCommand      objCommand;
            SqlDataReader   genreReader;

            primary =                       key;
            secondary =                     ", Genre." + parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", Genre.", ref secondary, ", Genre.");
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
            SQLStatement =                  SQLHelper.Select("Genre",
                                                            " FROM " + "Genre",
                                                            primary,
                                                            secondary
                                                            ) + " WHERE ";


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
        public static bool AddGenre(Genre genre)
        {
            string          primary,
                            secondary,
                            SQLStatement;
            //TODO what if MAX is 32767
             bool            result;

            primary =                       key;
            secondary =                     ", @" + parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", @", ref secondary, ", @");
            SQLStatement =                  SQLHelper.Insert(   "Genre",
                                                                primary,
                                                                secondary
                                                            );

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                genre.id =                  GetMax("Genre", key) + 1;
                



                









                result =    WriteGenre(SQLStatement, genre);
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
            
            string          primary,                            
                            secondary,
                            SQLStatement;
            bool            result;

            primary =                       key                         + " = @" + key                        ;
            secondary =                     parameters[lowestSecondary] + " = @" + parameters[lowestSecondary];
            


            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=                ", " + parameters[i] + " = @" + parameters[i];
            SQLStatement =                  SQLHelper.Update(   "Genre", 
                                                                primary,
                                                                secondary
                                                            );
            
            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {






                



                
















                




                









                result =    WriteGenre(SQLStatement, genre);
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
