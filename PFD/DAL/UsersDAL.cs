using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD.Models;
using System.Data.SqlClient;

namespace PFD.DAL
{
    public class UsersDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public UsersDAL()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("OCBC");

            conn = new SqlConnection(strConn);
        }

        public Users? Login(string Email, string Password)
        {
            SqlCommand cmd = conn.CreateCommand();
            
            cmd.CommandText = @"SELECT * FROM Users WHERE Email = @Email";
            cmd.Parameters.AddWithValue("@Email", Email);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Users? user = null;


            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user = new Users();
                    user.Name = reader.GetString(0);
                    user.Email = reader.GetString(1);
                    user.UserID = reader.GetInt32(2);
                    user.Password = reader.GetString(3);
                    user.Money = reader.GetDecimal(4);
                    user.LastLoggedIn = reader.GetDateTime(5);

                }
            }

            reader.Close();
            conn.Close();

            return user;
        }

        public void UpdateLastLoggedIn(int userId)
        {
            using (conn)
            {
                conn.Open();

                // Create a SQL command with the update query
                using (SqlCommand command = new SqlCommand($"UPDATE Users SET LastLoggedIn = GETDATE() WHERE UserID = {userId}", conn))
                {
                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }

            conn.Close();
        }

        public bool Logger(string Email, string Password)
        {
            bool authenticated = false;
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM Users WHERE Email = @Email";
            cmd.Parameters.AddWithValue("@Email", Email);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end
            while (reader.Read())
            {
                // Convert email address to lowercase for comparison
                // Password comparison is case-sensitive
                if ((reader.GetString(0).ToLower() == Email) &&
                   (reader.GetString(2) == Password))
                {
                    authenticated = true;
                    break; // Exit the while loop
                }
            }

            reader.Close();
            conn.Close();

            return authenticated;
            
        }











    }
}
