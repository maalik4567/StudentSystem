using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;

namespace HR_Managament_System.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

        //private static readonly string strConnectionString = "Data Source=DESKTOP-P7354T7\\SQLEXPRESS;Initial Catalog=HRSystem;Integrated Security=True"
        private static readonly string strConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=StudentDB;Trusted_Connection=True;Integrated Security=SSPI;";

        // Method to check if the database is connected
        public static bool IsDatabaseConnected()
        {
            try
            {
                using (var con = new SqlConnection(strConnectionString))
                {
                    con.Open(); // Attempt to open the connection
                    return con.State == ConnectionState.Open; // Check if the connection is open
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to the database: " + ex.Message);
                return false;
            }
        }

        public static bool checkValidateUser(string userName, string password)
        {

            if (!IsDatabaseConnected())
            {
                Console.WriteLine("Database connection failed.");
                return false;
            }

            try
            {
                using (IDbConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var user = con.QueryFirstOrDefault("ValidateUser", new { UserName = userName, Password = password }, commandType: CommandType.StoredProcedure);
                    return user != null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error validating user: " + ex.Message);
                return false;
            }
        }
    }
}