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
                        DateOfTransaction = reader.GetDateTime(4)


                    });
                }

            }
            conn.Close();
            return transactions;

        }



    }
}
