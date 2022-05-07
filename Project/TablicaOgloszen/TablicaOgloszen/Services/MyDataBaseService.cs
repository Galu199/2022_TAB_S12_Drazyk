using System;
using System.Collections.Generic;
using System.Data;
using TablicaOgloszen.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace TablicaOgloszen.Services
{
    public class MyDataBaseService
    {
        private readonly string ConnectionName = "DefaultConnection";
        private readonly IConfiguration _configuration;
        private readonly string myDbConnectionString;

        public MyDataBaseService(IConfiguration configuration)
        {
            _configuration = configuration;
            myDbConnectionString = _configuration.GetConnectionString(ConnectionName);
        }

        public static T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T); // returns the default value for the type
            }
            else
            {
                return (T)obj;
            }
        }

        public void AddUser(User item)
        {
            using (SqlConnection con = new SqlConnection(myDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" + $"INSERT INTO Users ({item.Id},{item.Email},{item.UserName},{item.PhoneNumber},{item.Ban},{item.ModedBy});" + "COMMIT;");
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
            }
        }

        public void AddPost(Post item)
        {
            using (SqlConnection con = new SqlConnection(myDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" + $"INSERT INTO Users ({item.Id},{item.Title},{item.Text},{item.Rating},{item.Date},{item.Pinned},{item.Deleted},{item.Users_Id},{item.ModedBy});" + "COMMIT;");
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
            }
        }

        public void AddComment(Comment item)
        {
            using (SqlConnection con = new SqlConnection(myDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" + $"INSERT INTO Users ({item.Id},{item.Text},{item.Date},{item.Deleted},{item.Users_Id},{item.Posts_Id},{item.ModedBy});" + "COMMIT;");
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
            }
        }

        public void AddTag(Tag item)
        {
            using (SqlConnection con = new SqlConnection(myDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" + $"INSERT INTO Users ({item.Id},{item.Text},{item.Posts_Id});" + "COMMIT;");
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
            }
        }

        public List<User> GetUsers()
        {
            var items = new List<User>();
            using (SqlConnection con = new SqlConnection(myDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" + "SELECT * FROM Users WHERE Ban = 0;" + "COMMIT;", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var item = new User();
                    item.Id = ConvertFromDBVal<string>(rdr["Id"]);
                    item.Email = ConvertFromDBVal<string>(rdr["Email"]);
                    item.UserName = ConvertFromDBVal<string>(rdr["UserName"]);
                    item.PhoneNumber = ConvertFromDBVal<string>(rdr["PhoneNumber"]);
                    item.Ban = ConvertFromDBVal<bool>(rdr["Ban"]);
                    item.ModedBy = ConvertFromDBVal<string>(rdr["ModedBy"]);
                    items.Add(item);
                }
            }
            return items;
        }

        public List<Post> GetPosts()
        {
            var items = new List<Post>();
            using (SqlConnection con = new SqlConnection(myDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" + "SELECT * FROM Posts WHERE Deleted = 0;" + "COMMIT;", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var item = new Post();
                    item.Id = ConvertFromDBVal<int>(rdr["Id"]);
                    item.Title = ConvertFromDBVal<string>(rdr["Title"]);
                    item.Text = ConvertFromDBVal<string>(rdr["Text"]);
                    item.Rating = ConvertFromDBVal<int>(rdr["Rating"]);
                    item.Date = ConvertFromDBVal<DateTime>(rdr["Date"]);
                    item.Pinned = ConvertFromDBVal<bool>(rdr["Pinned"]);
                    item.Deleted = ConvertFromDBVal<bool>(rdr["Deleted"]);
                    item.Users_Id = ConvertFromDBVal<string>(rdr["Users_Id"]);
                    item.ModedBy = ConvertFromDBVal<string>(rdr["ModedBy"]);
                    items.Add(item);
                }
            }
            return items;
        }

        public List<Comment> GetComments()
        {
            var items = new List<Comment>();
            using (SqlConnection con = new SqlConnection(myDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" + "SELECT * FROM Comments WHERE Deleted = 0;" + "COMMIT;", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var item = new Comment();
                    item.Id = ConvertFromDBVal<int>(rdr["Id"]);
                    item.Text = ConvertFromDBVal<string>(rdr["Text"]);
                    item.Date = ConvertFromDBVal<DateTime>(rdr["Date"]);
                    item.Deleted = ConvertFromDBVal<bool>(rdr["Deleted"]);
                    item.Users_Id = ConvertFromDBVal<string>(rdr["Users_Id"]);
                    item.Posts_Id = ConvertFromDBVal<int>(rdr["Posts_Id"]);
                    item.ModedBy = ConvertFromDBVal<string>(rdr["ModedBy"]);
                    items.Add(item);
                }
            }
            return items;
        }

        public List<Tag> GetTags()
        {
            var items = new List<Tag>();
            using (SqlConnection con = new SqlConnection(myDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" + "SELECT * FROM Tags" + "COMMIT;", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var item = new Tag();
                    item.Id = ConvertFromDBVal<int>(rdr["Id"]);
                    item.Text = ConvertFromDBVal<string>(rdr["Text"]);
                    item.Posts_Id = ConvertFromDBVal<int>(rdr["Posts_Id"]);
                    items.Add(item);
                }
            }
            return items;
        }

        public List<User> QueryUsers(string query)
        {
            var items = new List<User>();
            using (SqlConnection con = new SqlConnection(myDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" + query + ";" + "COMMIT;", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var item = new User();
                    item.Id = ConvertFromDBVal<string>(rdr["Id"]);
                    item.Email = ConvertFromDBVal<string>(rdr["Email"]);
                    item.UserName = ConvertFromDBVal<string>(rdr["UserName"]);
                    item.PhoneNumber = ConvertFromDBVal<string>(rdr["PhoneNumber"]);
                    item.Ban = ConvertFromDBVal<bool>(rdr["Ban"]);
                    item.ModedBy = ConvertFromDBVal<string>(rdr["ModedBy"]);
                    items.Add(item);
                }
            }
            return items;
        }

        public List<Post> QueryPosts(string query)
        {
            var items = new List<Post>();
            using (SqlConnection con = new SqlConnection(myDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" + query + ";" + "COMMIT;", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var item = new Post();
                    item.Id = ConvertFromDBVal<int>(rdr["Id"]);
                    item.Title = ConvertFromDBVal<string>(rdr["Title"]);
                    item.Text = ConvertFromDBVal<string>(rdr["Text"]);
                    item.Rating = ConvertFromDBVal<int>(rdr["Rating"]);
                    item.Date = ConvertFromDBVal<DateTime>(rdr["Date"]);
                    item.Pinned = ConvertFromDBVal<bool>(rdr["Pinned"]);
                    item.Deleted = ConvertFromDBVal<bool>(rdr["Deleted"]);
                    item.Users_Id = ConvertFromDBVal<string>(rdr["Users_Id"]);
                    item.ModedBy = ConvertFromDBVal<string>(rdr["ModedBy"]);
                    items.Add(item);
                }
            }
            return items;
        }

        public List<Comment> QueryComments(string query)
        {
            var items = new List<Comment>();
            using (SqlConnection con = new SqlConnection(myDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" + query + ";" + "COMMIT;", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var item = new Comment();
                    item.Id = ConvertFromDBVal<int>(rdr["Id"]);
                    item.Text = ConvertFromDBVal<string>(rdr["Text"]);
                    item.Date = ConvertFromDBVal<DateTime>(rdr["Date"]);
                    item.Deleted = ConvertFromDBVal<bool>(rdr["Deleted"]);
                    item.Users_Id = ConvertFromDBVal<string>(rdr["Users_Id"]);
                    item.Posts_Id = ConvertFromDBVal<int>(rdr["Posts_Id"]);
                    item.ModedBy = ConvertFromDBVal<string>(rdr["ModedBy"]);
                    items.Add(item);
                }
            }
            return items;
        }

        public List<Tag> QueryTags(string query)
        {
            var items = new List<Tag>();
            using (SqlConnection con = new SqlConnection(myDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" + query + ";" + "COMMIT;", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var item = new Tag();
                    item.Id = ConvertFromDBVal<int>(rdr["Id"]);
                    item.Text = ConvertFromDBVal<string>(rdr["Text"]);
                    item.Posts_Id = ConvertFromDBVal<int>(rdr["Posts_Id"]);
                    items.Add(item);
                }
            }
            return items;
        }

    }
}
