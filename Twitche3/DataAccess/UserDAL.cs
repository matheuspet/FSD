using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Twitche3.Models;

namespace Twitche3.DataAccess
{
    public class UserDAL
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Twitter;Integrated Security=True";

        public IEnumerable<User> GetAllUsers()
        {
            List<User> listUsers = new List<User>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Users", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User user = new User();

                    user.Id = rdr["Id"].ToString();
                    user.Name = rdr["Name"].ToString();
                    user.Email = rdr["Email"].ToString();
                    user.Username = rdr["Username"].ToString();
                    //user.Photo = rdr["Photo"].ToString();
                    user.Password = rdr["Password"].ToString();


                    listUsers.Add(user);
                }
                con.Close();
            }
            return listUsers;
        }

        public User GetUser(string id)
        {
            List<User> listUsers = new List<User>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "select * from Users where Id = @param1;";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 100).Value = id;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User user = new User();

                    user.Id = rdr["Id"].ToString();
                    user.Name = rdr["Name"].ToString();
                    user.FirstName = rdr["FirstName"].ToString();
                    user.LastName = rdr["LastName"].ToString();
                    user.Email = rdr["Email"].ToString();
                    user.Username = rdr["Username"].ToString();
                    user.Password = rdr["Password"].ToString();
                    user.Photo = rdr["Photo"].ToString();
                    user.Bio = rdr["Bio"].ToString();

                    listUsers.Add(user);
                }
                con.Close();
            }
            return listUsers[0];
        }

        public User GetUserByUsername(string username)
        {
            List<User> listUsers = new List<User>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "select * from Users where Username = @param1;";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 100).Value = username;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User user = new User();

                    user.Id = rdr["Id"].ToString();
                    user.Name = rdr["Name"].ToString();
                    user.FirstName = rdr["FirstName"].ToString();
                    user.LastName = rdr["LastName"].ToString();
                    user.Email = rdr["Email"].ToString();
                    user.Username = rdr["Username"].ToString();
                    user.Password = rdr["Password"].ToString();
                    user.Photo = rdr["Photo"].ToString();
                    user.Bio = rdr["Bio"].ToString();

                    listUsers.Add(user);
                }
                con.Close();
            }
            return listUsers[0];
        }

        public bool CreateUser(User user)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //string sql = "INSERT INTO klant(klant_id,naam,voornaam) VALUES(@param1,@param2,@param3)";

                string sql = "INSERT INTO Users (Id, Name, FirstName, LastName, Email, Username, Password, Photo, CreatedOn, DeletedOn, Deleted) VALUES (newId(), @param1, @param2, @param3, @param4, @param5, @param6, '', getDate(), null, 0);";
                //string sql = "INSERT INTO Users (Id, Name, FirstName, LastName, Email, Username, Password, CreatedOn, DeletedOn, Deleted) VALUES (newId(), 'Matheus Mattos', 'Matheus', 'Mattos', 'matheus_mattos@dell.com', 'username', 'password', getDate(), null, 0);";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 70).Value = user.FirstName + " " + user.LastName;
                cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 30).Value = user.FirstName;
                cmd.Parameters.Add("@param3", SqlDbType.NVarChar, 30).Value = user.LastName;
                cmd.Parameters.Add("@param4", SqlDbType.NVarChar, 100).Value = user.Email;
                cmd.Parameters.Add("@param5", SqlDbType.NVarChar, 20).Value = user.Username;
                cmd.Parameters.Add("@param6", SqlDbType.NVarChar, 20).Value = user.Password;


                con.Open();
                rows = cmd.ExecuteNonQuery();
                con.Close();
            }
            if (rows > 0)
            {
                return true;
            }
            return false;

        }

        public IEnumerable<string> GetFollowers(string userid)
        {
            List<string> listFollowers = new List<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Follow where UserId = @param1", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 50).Value = userid;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    listFollowers.Add(rdr["FollowedBy"].ToString());
                }
                con.Close();
            }
            return listFollowers;
        }

        public IEnumerable<string> GetFollowing(string userid)
        {
            List<string> listFollowing = new List<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Follow where FollowedBy = @param1", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 50).Value = userid;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    listFollowing.Add(rdr["UserId"].ToString());
                }
                con.Close();
            }
            return listFollowing;
        }

        public bool UpdateUser(User user)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //string sql = "INSERT INTO klant(klant_id,naam,voornaam) VALUES(@param1,@param2,@param3)";

                string sql = "UPDATE Users set Name = @param1, FirstName = @param2, LastName = @param3, Email = @param4, Username = @param5, Password = @param6, Bio = @param8, Photo = @param9 where Id = @param7";
                //string sql = "INSERT INTO Users (Id, Name, FirstName, LastName, Email, Username, Password, CreatedOn, DeletedOn, Deleted) VALUES (newId(), 'Matheus Mattos', 'Matheus', 'Mattos', 'matheus_mattos@dell.com', 'username', 'password', getDate(), null, 0);";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 50).Value = user.FirstName + " " + user.LastName;
                cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 20).Value = user.FirstName;
                cmd.Parameters.Add("@param3", SqlDbType.NVarChar, 20).Value = user.LastName;
                cmd.Parameters.Add("@param4", SqlDbType.NVarChar, 100).Value = user.Email;
                cmd.Parameters.Add("@param5", SqlDbType.NVarChar, 20).Value = user.Username;
                cmd.Parameters.Add("@param6", SqlDbType.NVarChar, 20).Value = user.Password;
                cmd.Parameters.Add("@param8", SqlDbType.NVarChar, 500).Value = user.Bio;
                cmd.Parameters.Add("@param9", SqlDbType.NVarChar).Value = user.Photo;
                cmd.Parameters.Add("@param7", SqlDbType.UniqueIdentifier).Value = new Guid(user.Id);



                con.Open();
                rows = cmd.ExecuteNonQuery();
                con.Close();
            }
            if (rows > 0)
            {
                return true;
            }
            return false;

        }

    }
}