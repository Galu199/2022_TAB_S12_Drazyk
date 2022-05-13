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

        public List<User> GetUsers()
        {
            var items = new List<User>();
            using (SqlConnection con = new SqlConnection(myDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" + "SELECT * FROM Users;" + "COMMIT;", con);
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
                SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" + "SELECT * FROM Posts WHERE Deleted = 0 ORDER BY DATE DESC;" + "COMMIT;", con);
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
                try
                {
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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
                try
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return items;
        }

        public int QueryAggregate(string query)
        {
            int result = 0;
            using (var con = new SqlConnection(myDbConnectionString))
            using (var cmd = new SqlCommand(
                "BEGIN TRANSACTION;" +
                query + ";" +
                "COMMIT;"
                , con))
            {
                try
                {
                    con.Open();
                    result = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return result;
            }
        }

        public void AddUser(User item)
        {
            using (var con = new SqlConnection(myDbConnectionString))
            using (var cmd = new SqlCommand(
                "BEGIN TRANSACTION;" +
                @"INSERT INTO Users VALUES ( @Id,@Email,@UserName,@PhoneNumber,@Ban,@ModedBy );" +
                "COMMIT;"
                , con))
            {
                cmd.Parameters.Add("@Id", SqlDbType.VarChar, 450).Value = item.Id;
                cmd.Parameters.Add("@Email", SqlDbType.Text).Value = item.Email;
                cmd.Parameters.Add("@UserName", SqlDbType.Text).Value = item.UserName;
                cmd.Parameters.Add("@PhoneNumber", SqlDbType.Text).Value = item.PhoneNumber;
                cmd.Parameters.Add("@Ban", SqlDbType.Bit).Value = item.Ban;
                cmd.Parameters.Add("@ModedBy", SqlDbType.VarChar, 450).Value = item.ModedBy;
                foreach (IDataParameter param in cmd.Parameters)
                {
                    if (param.Value == null) param.Value = DBNull.Value;
                }
                try
                {
                    con.Open();
                    /*var rdr = */
                    cmd.ExecuteNonQuery();
                    //Console.WriteLine("RowsAffected: {0}", rdr);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            //{
            //SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" + $"INSERT INTO Users ({item.Id},{item.Email},{item.UserName},{item.PhoneNumber},{item.Ban},{item.ModedBy});" + "COMMIT;");
            //cmd.CommandType = CommandType.Text;
            //con.Open();
            //SqlDataReader rdr = cmd.ExecuteReader();
            //}
        }

        public void AddPost(Post item)
        {
            using (var con = new SqlConnection(myDbConnectionString))
            using (var cmd = new SqlCommand(
                "BEGIN TRANSACTION;" +
                @"INSERT INTO Posts VALUES ( @Title, @Text, @Rating, @DATE, @Pinned, @Deleted, @Users_Id, @ModedBy );" +
                "COMMIT;"
                , con))
            {
                cmd.Parameters.Add("@Title", SqlDbType.Text).Value = item.Title;
                cmd.Parameters.Add("@Text", SqlDbType.Text).Value = item.Text;
                cmd.Parameters.Add("@Rating", SqlDbType.Int).Value = item.Rating;
                cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = item.Date;
                cmd.Parameters.Add("@Pinned", SqlDbType.Bit).Value = item.Pinned;
                cmd.Parameters.Add("@Deleted", SqlDbType.Bit).Value = item.Deleted;
                cmd.Parameters.Add("@Users_Id", SqlDbType.VarChar, 450).Value = item.Users_Id;
                cmd.Parameters.Add("@ModedBy", SqlDbType.VarChar, 450).Value = item.ModedBy;
                foreach (IDataParameter param in cmd.Parameters)
                {
                    if (param.Value == null) param.Value = DBNull.Value;
                }
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void AddComments(Comment item)
        {
            using (var con = new SqlConnection(myDbConnectionString))
            using (var cmd = new SqlCommand(
                "BEGIN TRANSACTION;" +
                @"INSERT INTO Comments VALUES ( @Text, @DATE, @Deleted, @Users_Id, @Posts_Id, @ModedBy );" +
                "COMMIT;"
                , con))
            {
                cmd.Parameters.Add("@Text", SqlDbType.Text).Value = item.Text;
                cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = item.Date;
                cmd.Parameters.Add("@Deleted", SqlDbType.Bit).Value = item.Deleted;
                cmd.Parameters.Add("@Users_Id", SqlDbType.VarChar, 450).Value = null;
                cmd.Parameters.Add("@Posts_Id", SqlDbType.VarChar, 450).Value = null;
                cmd.Parameters.Add("@ModedBy", SqlDbType.VarChar, 450).Value = null;
                foreach (IDataParameter param in cmd.Parameters)
                {
                    if (param.Value == null) param.Value = DBNull.Value;
                }
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void AddTag(Tag item)
        {
            using (var con = new SqlConnection(myDbConnectionString))
            using (var cmd = new SqlCommand(
                "BEGIN TRANSACTION;" +
                @"INSERT INTO Tags VALUES ( @Text,@Posts_Id );" +
                "COMMIT;"
                , con))
            {
                cmd.Parameters.Add("@Text", SqlDbType.Text).Value = item.Text;
                cmd.Parameters.Add("@Posts_Id", SqlDbType.Int).Value = item.Posts_Id;

                foreach (IDataParameter param in cmd.Parameters)
                {
                    if (param.Value == null) param.Value = DBNull.Value;
                }
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void DeleteTag(int Id)
        {
            using (var con = new SqlConnection(myDbConnectionString))
            using (var cmd = new SqlCommand(
                "BEGIN TRANSACTION;" +
                @"DELETE FROM Tags WHERE Id=@ID;" +
                "COMMIT;"
                , con))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Id;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }
}
