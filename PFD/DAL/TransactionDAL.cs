using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD.Models;
using System.Data.SqlClient;

namespace PFD.DAL
{
    public class TransactionDAL 
    {

        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public TransactionDAL()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("OCBC");

            conn = new SqlConnection(strConn);
        }

        public List<Transaction>? GetTransactions(int UserID)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT * FROM Transactions WHERE UserID = @UserID ORDER BY DateOfTransaction DESC";
            cmd.Parameters.AddWithValue("@UserID", UserID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Transaction> transactions = new List<Transaction>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    transactions.Add(
                    new Transaction
                    {
                        TransactionID = reader.GetInt32(0),
                        Type = reader.GetString(1),
                        Amount = reader.GetDecimal(2),
                        UserID = reader.GetInt32(3),
                        DateOfTransaction = reader.GetDateTime(4),
                        Location = reader.GetString(5)


                    });
                }

            }
            conn.Close();
            return transactions;

        }
        public bool CreateTransactions(int UserID, string Type, double Amount, string? Location)
        {
            if (UserID != null && Amount != null)
            {

                //Create a SqlCommand object from connection object
                SqlCommand cmd = conn.CreateCommand();
                //Specify an INSERT SQL statement which will
                //return the auto-generated StaffID after insertion
                cmd.CommandText = @"INSERT INTO Transactions (Type,Amount,UserID,DateOfTransaction, Location) 
                           
                            VALUES(@type, @amount, @userid, @date,@location)";
                //Define the parameters used in SQL statement, value for each parameter
                //is retrieved from respective class's property.
                cmd.Parameters.AddWithValue("@type", Type);
                cmd.Parameters.AddWithValue("@amount", Amount);
                cmd.Parameters.AddWithValue("@userid", UserID);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                cmd.Parameters.AddWithValue("@location", Location);


                //A connection to database must be opened before any operations made.
                conn.Open();
                //ExecuteScalar is used to retrieve the auto-generated
                //StaffID after executing the INSERT SQL statement
                cmd.ExecuteScalar();
                //A connection should be closed after operations.
                conn.Close();

                return true;




            }
            return false;
            return true;
            
        }
        

         



    }
}
