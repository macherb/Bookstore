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
    class Members : BaseTable
    {
        #region Private variables

        private static string[] parameters =        { "number", "joindate", "firstname", "lastname", "address", "city", "state", "zipcode", "phone", "member_status", "login_name", "password", "email", "contact_method", "subscription_id", "photo" };
        private static string   foreign =           "Member." + parameters[14];

        private static int      lowestSecondary =   1;
        //TODO put all SQLStatements here
        #endregion

        #region Public variables

        public static string    key =               parameters[ 0];
        public static string    extra1 =            parameters[ 1];
        public static string    extra2 =            parameters[ 9];
        public static string    extra3 =            parameters[10];

        #endregion

        #region Private functions

        /// <summary>
        /// Sets all the non-primary key(s) in a <see cref="Bookstore.Member"/>
        /// </summary>
        /// <param name="memberReader">The <see cref="Bookstore.Member"/> that was read from</param>
        /// <param name="objMember">The <see cref="Bookstore.Member"/> that will be written to</param>
        private static void SetSecondary(SqlDataReader memberReader, Member objMember)
        {
            int                     contact_method,
                                    subscription_id;
            DateTime                joindate;

            DateTime.TryParse(                      memberReader[parameters[ 1]     ].ToString(), out joindate       );
            objMember.joindate =                    joindate;
            objMember.firstname =                   memberReader[parameters[ 2]     ].ToString();
            objMember.lastname =                    memberReader[parameters[ 3]     ].ToString();
            objMember.address =                     memberReader[parameters[ 4]     ].ToString();
            objMember.city =                        memberReader[parameters[ 5]     ].ToString();
            objMember.state =                       memberReader[parameters[ 6]     ].ToString();
            objMember.zipcode =                     memberReader[parameters[ 7]     ].ToString();
            objMember.phone =                       memberReader[parameters[ 8]     ].ToString();
            objMember.member_status =               memberReader[parameters[ 9]     ].ToString();
            objMember.login_name =                  memberReader[parameters[10]     ].ToString();
            objMember.password =                    memberReader[parameters[11]     ].ToString();
            objMember.email =                       memberReader[parameters[12]     ].ToString();
            Int32.TryParse(                         memberReader[parameters[13]     ].ToString(), out contact_method );
            objMember.contact_method =              contact_method;
            Int32.TryParse(                         memberReader[parameters[14]     ].ToString(), out subscription_id);
            objMember.subscription_id =             subscription_id;
            objMember.photo =                       memberReader[parameters[15]     ].ToString();
        }

        /// <summary>
        /// Writes a <see cref="Bookstore.Member"/> to the database's table
        /// </summary>
        /// <param name="SQLStatement">The command to write a <see cref="Bookstore.Member"/></param>
        /// <param name="member">The <see cref="Bookstore.Member"/> that will have its data written to the table</param>
        /// <returns>Whether or not the <see cref="Bookstore.Member"/> was successfully written</returns>
        private static bool WriteMember(string SQLStatement, Member member)
        {
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;

            using (SqlConnection objConn = AccessDataSQLServer.GetConnection())
            {
                objConn.Open();
                using (objCommand = new SqlCommand(SQLStatement, objConn))
                {
                    objCommand.Parameters.AddWithValue('@' + parameters[ 0],    member.number           );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 1],    member.joindate         );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 2],    member.firstname        );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 3],    member.lastname         );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 4],    member.address          );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 5],    member.city             );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 6],    member.state            );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 7],    member.zipcode          );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 8],    member.phone            );
                    objCommand.Parameters.AddWithValue('@' + parameters[ 9],    member.member_status    );
                    objCommand.Parameters.AddWithValue('@' + parameters[10],    member.login_name       );
                    objCommand.Parameters.AddWithValue('@' + parameters[11],    member.password         );
                    objCommand.Parameters.AddWithValue('@' + parameters[12],    member.email            );
                    objCommand.Parameters.AddWithValue('@' + parameters[13],    member.contact_method   );
                    objCommand.Parameters.AddWithValue('@' + parameters[14],    member.subscription_id  );
                    objCommand.Parameters.AddWithValue('@' + parameters[15],    member.photo            );
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
        /// <returns>All fields of all <see cref="Bookstore.Member"/>'s, plus the extras from <see cref="Bookstore.Subscription"/></returns>
        /// <exception cref="System.Exception" />
        public static List<Member> GetMembers()
        {
            List<Member>    members =       new List<Member>();
            string          primary,
                            secondary,
                            SQLStatement;
            SqlCommand      objCommand;
            SqlDataReader   memberReader;
            
            primary =                       key;
            secondary =                     ", Member." + parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", Member.", ref secondary, ", Member.");
            SQLStatement =                  SQLHelper.Select(
                                                            "Member",
                                                            SQLHelper.Join(

                                                                                            " FROM " + "(" + "Member"




                                                                                           ,
                                                                            "Subscription",
                                                                            ", Subscription." + Subscriptions.extra,
                                                                            foreign,
                                                                            Subscriptions.key
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
                        using ((memberReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (memberReader.Read())
                            {
                                Member              objMember = new Member();
                                int                 number;


                                Int32.TryParse(                 memberReader[parameters[ 0]     ].ToString(),   out number  );
                                objMember.number =              number;

                                SetSecondary(memberReader, objMember);
                                objMember.name =                memberReader[Subscriptions.extra].ToString();



                                members.Add(objMember);
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
            return  members;
        }

        /// <summary>
        /// Returns a single record  from the table whose parameter matches a table field condition
        /// </summary>
        /// <param name="parameter">accepts a parameter to return a specific record</param>
        /// <returns>All the fields (except the primary key) of a <see cref="Bookstore.Member"/></returns>
        /// <exception cref="System.Exception" />
        public static Member GetMember(int parameter)//string parameter)
        {
            Member          objMember =     null;
            string          primary =       string.Empty,
                            secondary =     string.Empty,
                            SQLStatement;
            SqlCommand      objCommand;
            SqlDataReader   memberReader;

            secondary +=                    parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", Member.", ref secondary, ", Member.");
            SQLStatement =                  SQLHelper.Select("Member", 
                                                            " FROM " + "Member",
                                                            primary, 
                                                            secondary
                                                            ) + " WHERE ";


            SQLStatement +=                 "Member." + parameters[ 0] + " = @" + parameters[ 0];

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
                        using ((memberReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (memberReader.Read())
                            {
                                objMember =         new Member();

                                objMember.number =  parameter;

                                SetSecondary(memberReader, objMember);

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
            return  objMember;
        }

        /// <summary>
        /// Adds a record to the table with a Boolean returned status of True or False.
        /// </summary>
        /// <param name="member">accepts a custom object of that type as a parameter</param>
        /// <returns>Whether or not the <see cref="Bookstore.Member"/> was successfully written</returns>
        /// <exception cref="System.Exception" />
        public static bool AddMember(Member member)
        {
            string          primary,
                            secondary,
                            SQLStatement;
            
            bool            result;

            primary =                       key;
            secondary =                     ", @" + parameters[lowestSecondary];
            PrimarySecondary(ref primary, ", @", ref secondary, ", @");
            SQLStatement =                  SQLHelper.Insert(   "Member",
                                                                primary,
                                                                secondary
                                                            );

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {
                member.number =             GetMax("Member", key) + 1;

















                result =    WriteMember(SQLStatement, member);
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
        /// <param name="member">accepts a custom object of that type as a parameter</param>
        /// <returns>Whether or not the <see cref="Bookstore.Member"/> was successfully written</returns>
        /// <exception cref="System.Exception" />
        public static bool UpdateMember(Member member)
        {

            string          primary,
                            secondary,
                            SQLStatement;
            bool            result;

            primary =                       key                         + " = @" + key                        ;
            secondary =                     parameters[lowestSecondary] + " = @" + parameters[lowestSecondary];


            
            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=                ", " + parameters[i] + " = @" + parameters[i];
            SQLStatement =                  SQLHelper.Update(   "Member",
                                                                primary,
                                                                secondary
                                                            );

            //Step #1: Add code to call the appropriate method from the inherited AccessDataSQLServer class
            //To return a database connection object
            try
            {














































                result =    WriteMember(SQLStatement, member);
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
        /// <param name="member">accepts a custom object of that type as a parameter</param>
        /// <returns>Whether or not the <see cref="Bookstore.Member"/> was successfully deleted</returns>
        /// <exception cref="System.Exception" />
        public static bool DeleteMember(Member member)
        {
            string      primary =       string.Empty,
                        SQLStatement;
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;



            SQLStatement =              SQLHelper.Delete("Member",
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
                        objCommand.Parameters.AddWithValue('@' + parameters[ 0],    member.number   );
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
