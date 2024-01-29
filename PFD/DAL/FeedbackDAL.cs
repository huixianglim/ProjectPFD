using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD.Models;
using System.Data.SqlClient;

namespace PFD.DAL
{
    public class FeedbackDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public FeedbackDAL()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("OCBC");

            conn = new SqlConnection(strConn);
        }


        public bool Create(Feedback feedback)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"INSERT INTO Feedback(GestureFeedback, ChatBotFeedback, 
									VoiceFeedback, GestureScore, VoiceScore, ChatBotScore)
									VALUES(@gestureFeedback, @chatBotFeedback, @voiceFeedback, @gestureScore, @voiceScore,
									@chatBotScore)";

            cmd.Parameters.AddWithValue("@gestureFeedback", feedback.GestureFeedback);
            cmd.Parameters.AddWithValue("@chatBotFeedback", feedback.ChatBotFeedback);
            cmd.Parameters.AddWithValue("@voiceFeedback", feedback.VoiceFeedback);
            cmd.Parameters.AddWithValue("@gestureScore", feedback.GestureScore);
            cmd.Parameters.AddWithValue("@voiceScore", feedback.VoiceScore);
            cmd.Parameters.AddWithValue("@chatBotScore", feedback.ChatBotScore);
 

            conn.Open();

            cmd.ExecuteScalar();
            conn.Close();
            return true;
        }


        public List<Feedback> GetFeedbacks()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Feedback";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            List<Feedback> feedbacks = new List<Feedback>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    feedbacks.Add(
                    new Feedback
                    {
                        GestureFeedback = reader.GetString(0),
                        VoiceFeedback = reader.GetString(1),
                        ChatBotFeedback = reader.GetString(2),
                        GestureScore = reader.GetInt32(4),
                        VoiceScore = reader.GetInt32(5),
                        ChatBotScore = reader.GetInt32(6)

                    });
                }

            }
            conn.Close();

            return feedbacks;




        }






    }
}
