using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Twitche3.Models.DataAccess
{
    public class TweetDAL
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Twitter;Integrated Security=True";

        public IEnumerable<Tweet> GetTweets()
        {
            List<Tweet> tweets = new List<Tweet>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Tweet order by createdon desc", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Tweet tt = new Tweet();


                    tt.Id = rdr["Id"].ToString();
                    tt.Owner = rdr["Owner"].ToString();
                    tt.OwnerId = rdr["OwnerId"].ToString();
                    tt.Description = rdr["Description"].ToString();
                    tt.Likes = (int)rdr["Likes"];
                    tt.Retweets = (int)rdr["Retweets"];
                    tt.CreatedOn = rdr["CreatedOn"].ToString();
                    tt.DeletedOn = rdr["DeletedOn"].ToString();
                    tt.Deleted = rdr["DeletedOn"].ToString();


                    tweets.Add(tt);
                }
                con.Close();
            }
            return tweets;
        }

        public Tweet GetTweet(string id)
        {
            List<Tweet> tweets = new List<Tweet>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Tweet where id = '" + id + "';", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Tweet tt = new Tweet();

                    tt.Id = rdr["Id"].ToString();
                    tt.Owner = rdr["Owner"].ToString();
                    tt.OwnerId = rdr["OwnerId"].ToString();
                    tt.Description = rdr["Description"].ToString();
                    tt.Likes = (int)rdr["Likes"];
                    tt.Retweets = (int)rdr["Retweets"];
                    tt.CreatedOn = rdr["CreatedOn"].ToString();
                    tt.DeletedOn = rdr["DeletedOn"].ToString();
                    tt.Deleted = rdr["DeletedOn"].ToString();


                    tweets.Add(tt);
                }
                con.Close();
            }
            return tweets[0];
        }

        public IEnumerable<Tweet> GetTweetsFrom(string ownerId)
        {
            List<Tweet> tweets = new List<Tweet>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Tweet where OwnerId = '" + ownerId + "';", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Tweet tt = new Tweet();

                    tt.Id = rdr["Id"].ToString();
                    tt.Owner = rdr["Owner"].ToString();
                    tt.OwnerId = rdr["OwnerId"].ToString();
                    tt.Description = rdr["Description"].ToString();
                    tt.Likes = (int)rdr["Likes"];
                    tt.Retweets = (int)rdr["Retweets"];
                    tt.CreatedOn = rdr["CreatedOn"].ToString();
                    tt.DeletedOn = rdr["DeletedOn"].ToString();
                    tt.Deleted = rdr["DeletedOn"].ToString();


                    tweets.Add(tt);
                }
                con.Close();
            }
            return tweets;
        }

        public void CreateTweet(Tweet tweet)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                string sql = "INSERT INTO Tweet (Id, Owner, OwnerId, Description, Likes, Retweets, CreatedOn, DeletedOn, Deleted) VALUES (newId(), @param1, @param2, @param3, 0, 0, getDate(), null, 0);";


                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 100).Value = tweet.Owner;
                cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 100).Value = tweet.OwnerId;
                cmd.Parameters.Add("@param3", SqlDbType.NVarChar, 280).Value = tweet.Description;


                con.Open();
                rows = cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void LikeTweet(String TweetId)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                string sql = "update Tweet set Likes = Likes+1 where Id =  @param1;";


                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 100).Value = TweetId;



                con.Open();
                rows = cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void ReTweet(String TweetId)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                string sql = "update Tweet set Retweets = Retweets+1 where Id =  @param1;";


                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 100).Value = TweetId;



                con.Open();
                rows = cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}