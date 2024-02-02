using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD.Models;
using System.Data.SqlClient;

namespace PFD.DAL
{
    public class EmailDAL
    {

        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public EmailDAL()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("OCBC");

            conn = new SqlConnection(strConn);
        }

        public string GetEmail(int userID)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"
                SELECT Email
                FROM Emails
                WHERE UserID = @UserID";

            cmd.Parameters.AddWithValue("@UserID", userID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            string email = "";

            while (reader.Read())
            {
                email = reader.GetString(0);
            }

            return email;
        }

        public DateTime GetLastUpdatedEmail(int userID)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"
                SELECT LastUpdatedEmail
                FROM Emails
                WHERE UserID = @UserID";

            cmd.Parameters.AddWithValue("@UserID", userID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            DateTime LastUpdatedEmail = DateTime.MinValue;

            while (reader.Read())
            {
                LastUpdatedEmail = reader.GetDateTime(0);
            }

            return LastUpdatedEmail;
        }

        public bool UpdateEmail(int UserID, string NewEmail)
        {
            if (UserID != null && NewEmail != null)
            {

                try
                {
                    // Open a connection to the database
                    conn.Open();

                    // Create a SqlCommand object for the UPDATE SQL statement
                    SqlCommand cmd = new SqlCommand("UPDATE Emails SET Email = @NewEmail, LastUpdatedEmail = GETDATE() WHERE UserID = @UserID", conn);

                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@NewEmail", NewEmail);
                    cmd.Parameters.AddWithValue("@UserID", UserID);

                    // Execute the UPDATE SQL statement
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Close the connection
                    conn.Close();

                    // Check if any rows were affected
                    return true;
                }
                catch (Exception ex)
                {
                    // Handle exceptions (log, throw, etc.)
                    // For simplicity, you can log the exception and return false
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
            return false;
        }

    }
}
