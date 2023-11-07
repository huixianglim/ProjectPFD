using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD.Models;
using System.Data.SqlClient;

namespace PFD.DAL
{
    public class ContactsDAL 
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public ContactsDAL()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("OCBC");

            conn = new SqlConnection(strConn);
        }
        public List<Contacts>? GetContacts(int UserID)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT * FROM Contacts WHERE UserID = @UserID";
            cmd.Parameters.AddWithValue("@UserID", UserID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Contacts> contacts = new List<Contacts>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    contacts.Add(
                    new Contacts
                    {
                        Name = reader.GetString(0),
                        ContactID = reader.GetInt32(1),
                        Number = reader.GetString(2),
                        UserID = reader.GetInt32(3)


                    });
                }
                    
            }
            conn.Close();
            return contacts;

            
        }

        public Contacts? GetDetails(int ContactID)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT * FROM Contacts WHERE ContactID = @ContactID";
            cmd.Parameters.AddWithValue("@ContactID", ContactID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Contacts? contact = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    contact = new Contacts();
                    contact.Name = reader.GetString(0);
                    contact.ContactID = reader.GetInt32(1);
                    contact.Number = reader.GetString(2);
                    contact.UserID = reader.GetInt32(3);

                }

            }
            conn.Close();
            return contact;


        }



    }



}

