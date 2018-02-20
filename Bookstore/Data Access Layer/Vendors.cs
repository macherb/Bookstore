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
    class Vendors : BaseTable
    {
        #region Private variables

        private static string[] parameters =        { "id", "name" };


        private static int      lowestSecondary =   1;

        #endregion

        #region Public variables

        public static string    key =               parameters[ 0];



        public static string    SQLGetList =        SQLHelper.Select(
                                                                    "Vendor",


                                                                                                    " FROM " + "Vendor"









                                                                                   ,
                                                                    Primary(key, ", Vendor."),
                                                                    Secondary(", Vendor." + parameters[lowestSecondary], ", Vendor.")
                                                                    );
            
        #endregion

        #region Private functions

        /// <summary>
        /// Sets all the non-primary key(s) in a <see cref="Bookstore.Vendor"/>
        /// </summary>
        /// <param name="vendorReader">The <see cref="Bookstore.Vendor"/> that was read from</param>
        /// <param name="objVendor">The <see cref="Bookstore.Vendor"/> that will be written to</param>
        private static void SetSecondary(SqlDataReader vendorReader, Vendor objVendor)
        {
            objVendor.name =    vendorReader[parameters[ 1]].ToString();
        }

        /// <summary>
        /// Writes a <see cref="Bookstore.Vendor"/> to the database's table
        /// </summary>
        /// <param name="SQLStatement">The command to write a <see cref="Bookstore.Vendor"/></param>
        /// <param name="vendor">The <see cref="Bookstore.Vendor"/> that will have its data written to the table</param>
        /// <returns>Whether or not the <see cref="Bookstore.Vendor"/> was successfully written</returns>
        private static bool WriteVendor(string SQLStatement, Vendor vendor)
        {
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;

            using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
            {
                objConn.Open();
                using (objCommand = new SqlCommand(SQLStatement, objConn))
                {
                    objCommand.Parameters.AddWithValue('@' + parameters[ 0],    vendor.id   );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 1],    vendor.name );
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
        /// Sets the list of field(s) that will be SELECT'ed
        /// </summary>
        /// <param name="primary">The list of primary key(s)</param>
        /// <param name="preprimary">What will be placed before every primary key</param>
        /// <param name="secondary">The list of non-primary key(s)</param>
        /// <param name="presecondary">What will be placed before every non-primary key</param>
        /// <returns></returns>
        private static string Primary(string primary, string preprimary)
        {
            for (int i = 1                  ; i < lowestSecondary  ; i++)
                primary +=      preprimary + parameters[i];
            return  primary;
        }
        private static string Secondary(string secondary, string presecondary)
        {
            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=    presecondary + parameters[i];
            return  secondary;
        }










        #endregion

        #region Public functions

        /// <summary>
        /// Returns a list of generic  type objects from the table
        /// </summary>
        /// <returns>All fields of all <see cref="Bookstore.Vendor"/>'s</returns>
        /// <exception cref="System.Exception" />
        public static List<Vendor> GetVendors()
        {
            List<Vendor>    vendors =       new List<Vendor>();
            SqlCommand      objCommand;
            SqlDataReader   vendorReader;

            

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
                        using ((vendorReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (vendorReader.Read())
                            {
                                Vendor          objVendor = new Vendor();
                                int             id;


                                Int32.TryParse(             vendorReader[parameters[ 0]].ToString(),    out id  );
                                objVendor.id =              id;

                                SetSecondary(vendorReader, objVendor);




                                vendors.Add(objVendor);
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
            return  vendors;
        }

        /// <summary>
        /// Returns a single record  from the table whose parameter matches a table field condition
        /// </summary>
        /// <param name="parameter">accepts a parameter to return a specific record</param>
        /// <returns>All the fields (except the primary key) of a <see cref="Bookstore.Vendor"/></returns>
        /// <exception cref="System.Exception" />
        public static Vendor GetVendor(int parameter)//(string parameter)
        {
            Vendor          objVendor =     null;
            string          //primary =       string.Empty,
                            //secondary =     string.Empty,
                            SQLStatement;
            SqlCommand      objCommand;
            SqlDataReader   vendorReader;

            //secondary +=                    parameters[lowestSecondary];
            //PrimarySecondary(ref primary, ", Vendor.", ref secondary, ", Vendor.");
            SQLStatement =                  SQLHelper.Select("Vendor", 
                                                            " FROM " + "Vendor",
                                                            Primary(string.Empty,   ", Vendor."), 
                                                            Secondary(              parameters[lowestSecondary], ", Vendor.")
                                                            ) + " WHERE ";


            SQLStatement +=                 "Vendor." + parameters[ 0] + " = @" + parameters[ 0];

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
                        using ((vendorReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (vendorReader.Read())
                            {
                                objVendor =         new Vendor();

                                objVendor.id =      parameter;

                                SetSecondary(vendorReader, objVendor);

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
            return  objVendor;
        }

        /// <summary>
        /// Adds a record to the table with a Boolean returned status of True or False.
        /// </summary>
        /// <param name="vendor">accepts a custom object of that type as a parameter</param>
        /// <returns>Whether or not the <see cref="Bookstore.Vendor"/> was successfully written</returns>
        /// <exception cref="System.Exception" />
        public static bool AddVendor(Vendor vendor)
        {
            string          //primary,
                            //secondary,
                            SQLStatement;
            //TODO what if MAX is 32767
            bool            result;

            //primary =                       key;
            //secondary =                     ", @" + parameters[lowestSecondary];
            //PrimarySecondary(ref primary, ", @", ref secondary, ", @");
            SQLStatement =                  SQLHelper.Insert(   "Vendor",
                                                                Primary(key, ", @"),
                                                                Secondary(", @" + parameters[lowestSecondary], ", @")
                                                            );

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                vendor.id =                 GetMax("Vendor", key) + 1;

















                result =    WriteVendor(SQLStatement, vendor);
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
        /// <param name="vendor">accepts a custom object of that type as a parameter</param>
        /// <returns>Whether or not the <see cref="Bookstore.Vendor"/> was successfully written</returns>
        /// <exception cref="System.Exception" />
        public static bool UpdateVendor(Vendor vendor)
        {
            
            string          primary,
                            secondary,
                            SQLStatement;
            bool            result;

            primary =                       key                         + " = @" + key                        ;
            secondary =                     parameters[lowestSecondary] + " = @" + parameters[lowestSecondary];



            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=                ", " + parameters[i] + " = @" + parameters[i];
            SQLStatement =                  SQLHelper.Update(   "Vendor",
                                                                primary,
                                                                secondary
                                                            );

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {














































                result =    WriteVendor(SQLStatement, vendor);
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
        /// <param name="vendor">accepts a custom object of that type as a parameter</param>
        /// <returns>Whether or not the <see cref="Bookstore.Vendor"/> was successfully deleted</returns>
        /// <exception cref="System.Exception" />
        public static bool DeleteVendor(Vendor vendor)
        {
            string      primary =       string.Empty,
                        SQLStatement;
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;



            SQLStatement =              SQLHelper.Delete("Vendor",
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
                        objCommand.Parameters.AddWithValue('@' + parameters[ 0],    vendor.id   );
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
