using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    class Members
    {
        #region Private variables

        private static string[] parameters =        { "number", "joindate", "firstname", "lastname", "address", "city", "state", "zipcode", "phone", "member_status", "login_name", "password", "email", "contact_method", "subscription_id", "photo" };
        private static string   foreign =           "Member." + parameters[14];
        private static int      lowestSecondary =   1;

        #endregion

        #region Public variables

        public static string    key =               parameters[ 0];
        public static string    extra =             parameters[10];

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
                                Member                  objMember =     new Member();
                                int                     number,
                                                        contact_method,
                                                        subscription_id;
                                DateTime                joindate;
                                Int32.TryParse(                         memberReader[parameters[ 0]     ].ToString(), out number         );
                                objMember.number =                      number;
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
                                objMember.name =                        memberReader[Subscriptions.extra].ToString();
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


            SQLStatement +=                 "Member." + parameters[0] + " = @" + parameters[0];

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
                        using ((memberReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            while (memberReader.Read())
                            {
                                objMember =                             new Member();
                                int                     contact_method,
                                                        subscription_id;
                                DateTime                joindate =      new DateTime(1753, 1, 1, 0, 0, 0);

                                objMember.number =                      parameter;
                                DateTime.TryParse(                      memberReader[parameters[ 1]].ToString(), out joindate       );
                                objMember.joindate =                    joindate;
                                objMember.firstname =                   memberReader[parameters[ 2]].ToString();
                                objMember.lastname =                    memberReader[parameters[ 3]].ToString();
                                objMember.address =                     memberReader[parameters[ 4]].ToString();
                                objMember.city =                        memberReader[parameters[ 5]].ToString();
                                objMember.state =                       memberReader[parameters[ 6]].ToString();
                                objMember.zipcode =                     memberReader[parameters[ 7]].ToString();
                                objMember.phone =                       memberReader[parameters[ 8]].ToString();
                                objMember.member_status =               memberReader[parameters[ 9]].ToString();
                                objMember.login_name =                  memberReader[parameters[10]].ToString();
                                objMember.password =                    memberReader[parameters[11]].ToString();
                                objMember.email =                       memberReader[parameters[12]].ToString();
                                Int32.TryParse(                         memberReader[parameters[13]].ToString(), out contact_method );
                                objMember.contact_method =              contact_method;
                                Int32.TryParse(                         memberReader[parameters[14]].ToString(), out subscription_id);
                                objMember.subscription_id =             subscription_id;
                                objMember.photo =                       memberReader[parameters[15]].ToString();
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
            return objMember;
        }

        /// <summary>
        /// Adds a record to the table with a Boolean returned status of True or False.
        /// </summary>
        /// <param name="member">accepts a custom object of that type as a parameter</param>
        public static bool AddMember(Member member)
        {
            string          
                            primary2,
                            
                            secondary2,
                            SQLStatement1,
                            SQLStatement2;

            int             
                            rowsAffected,
                            max;
            SqlCommand      objCommand1,
                            objCommand2;
            SqlDataReader   memberReader;
            bool            result =        false;



            SQLStatement1 =                 SQLHelper.Select("MAX(Member", " FROM " + "Member", key, ")");




            primary2 =                      key;
            secondary2 =                    ", @" + parameters[lowestSecondary];
            PrimarySecondary(ref primary2, ", @", ref secondary2, ", @");
            SQLStatement2 =                 SQLHelper.Insert(   "Member",
                                                                primary2,
                                                                secondary2
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
                        using ((memberReader = objCommand1.ExecuteReader(CommandBehavior.CloseConnection)))
                        {
                            memberReader.Read();
                            Int32.TryParse(memberReader[0].ToString(), out max);
                        }
                    }
                    objConn1.Close();
                }
                member.number =             max + 1;


















                using (SqlConnection objConn2 = AccessDataSQLServer.GetConnection())
                {
                    objConn2.Open();
                    //Step #2: Code logic to create appropriate SQL Server objects calls
                    //         Cod Logic to retrieve data from database
                    //         Add Try..Catch appropriate block and throw exception back to calling program
                    using (objCommand2 = new SqlCommand(SQLStatement2, objConn2))
                    {
                        objCommand2.Parameters.AddWithValue('@' + parameters[ 0],   member.number           );
                        objCommand2.Parameters.AddWithValue('@' + parameters[ 1],   member.joindate         );
                        objCommand2.Parameters.AddWithValue('@' + parameters[ 2],   member.firstname        );
                        objCommand2.Parameters.AddWithValue('@' + parameters[ 3],   member.lastname         );
                        objCommand2.Parameters.AddWithValue('@' + parameters[ 4],   member.address          );
                        objCommand2.Parameters.AddWithValue('@' + parameters[ 5],   member.city             );
                        objCommand2.Parameters.AddWithValue('@' + parameters[ 6],   member.state            );
                        objCommand2.Parameters.AddWithValue('@' + parameters[ 7],   member.zipcode          );
                        objCommand2.Parameters.AddWithValue('@' + parameters[ 8],   member.phone            );
                        objCommand2.Parameters.AddWithValue('@' + parameters[ 9],   member.member_status    );
                        objCommand2.Parameters.AddWithValue('@' + parameters[10],   member.login_name       );
                        objCommand2.Parameters.AddWithValue('@' + parameters[11],   member.password         );
                        objCommand2.Parameters.AddWithValue('@' + parameters[12],   member.email            );
                        objCommand2.Parameters.AddWithValue('@' + parameters[13],   member.contact_method   );
                        objCommand2.Parameters.AddWithValue('@' + parameters[14],   member.subscription_id  );
                        objCommand2.Parameters.AddWithValue('@' + parameters[15],   member.photo            );
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
        /// <param name="member">accepts a custom object of that type as a parameter</param>
        public static bool UpdateMember(Member member)
        {
            string      primary,
                        secondary,
                        SQLStatement;
            SqlCommand  objCommand;
            int         rowsAffected;
            bool        result =        false;

            primary =                   key                         + " = @" + key                        ;
            secondary =                 parameters[lowestSecondary] + " = @" + parameters[lowestSecondary];


            
            for (int i = lowestSecondary + 1; i < parameters.Length; i++)
                secondary +=            ", " + parameters[i] + " = @" + parameters[i];
            SQLStatement =              SQLHelper.Update(   "Member",
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
        /// <param name="member">accepts a custom object of that type as a parameter</param>
        public static bool DeleteMember(Member member)
        {
            string      
                        primary =       string.Empty,
                        
                        
                        SQLStatement;
            SqlCommand  
                        objCommand;
            int         
                        rowsAffected;
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
                        objCommand.Parameters.AddWithValue('@' + parameters[0], member.number);
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
