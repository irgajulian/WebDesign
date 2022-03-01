
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public class GadgetDAO 
    {
        private string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SHIMANODB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public List<Config> Fatchall()
        {
            List<Config> returnlist = new();

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string sqlquery = "SELECT * FROM dbo.Config";
                SqlCommand command = new SqlCommand(sqlquery, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Config data = new();
                        data.lowtemproom = reader.GetString(1);
                        data.hitemproom = reader.GetString(2);
                        data.lowtempmachine = reader.GetString(3);
                        data.hitempmachine = reader.GetString(4);
                        
                        returnlist.Add(data);

                    }
                }

            }
            return returnlist;
        }
    }
}
