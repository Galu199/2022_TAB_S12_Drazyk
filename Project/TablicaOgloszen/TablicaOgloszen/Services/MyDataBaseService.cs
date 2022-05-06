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
        private readonly IConfiguration _configuration;

        public MyDataBaseService(IConfiguration configuration)
        {
            _configuration = configuration;
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

        public List<User> Query(string query)
        {
            List<User> users = new List<User>();
            string myDbConnectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(myDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var user = new User();
                    user.Id = ConvertFromDBVal<String>(rdr["Id"]);//  (rdr["Id"]).ToString();
                    user.Email = ConvertFromDBVal<String>(rdr["Email"]);
                    user.UserName = ConvertFromDBVal<String>(rdr["UserName"]);//rdr["UserName"].ToString();
                    user.PhoneNumber = ConvertFromDBVal<String>(rdr["PhoneNumber"]);//rdr["PhoneNumber"].ToString();
                    user.Ban = ConvertFromDBVal<bool>(rdr["Ban"]);
                    user.ModedBy = ConvertFromDBVal<User>(rdr["ModedBy"]);
                    //if (rdr["Ban"] == null || rdr["Ban"] == DBNull.Value)
                    //    user.Ban = false;
                    //else
                    //    user.Ban = (bool)rdr["Ban"];

                    //if (rdr["ModedBy"] == null || rdr["ModedBy"] == DBNull.Value)
                    //    user.ModedBy = null;
                    //else
                    //    user.ModedBy = (User)rdr["ModedBy"];
                    users.Add(user);
                }
            }
            return users;
        }
    }
}
