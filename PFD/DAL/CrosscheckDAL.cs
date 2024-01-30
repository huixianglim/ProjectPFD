using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD.Models;
using System.Data.SqlClient;

namespace PFD.DAL
{
    public class CrosscheckDAL 
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public CrosscheckDAL()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("OCBC");

            conn = new SqlConnection(strConn);
        }
        public Crosschecks? GetUserDetails(string CrosscheckID)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT * from Crosscheck where check_id =  @crosscheckID";
            cmd.Parameters.AddWithValue("@crosscheckID", CrosscheckID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Crosschecks? check = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    check = new Crosschecks();
                    check.check_id = reader.GetString(0);
                    check.user_id = reader.GetInt32(1);


                }

            }
            conn.Close();
            return check;


        }
    }
}
