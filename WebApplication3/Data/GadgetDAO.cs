
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
        //private string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SHIMANODB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string[] line = System.IO.File.ReadAllLines(@"C:\Users\Judi\Documents\GitHub\WebApplication3\WebDesign\WebApplication3\bin\Debug\Config.txt");
        SqlConnection connection = new();
        public List<Config> Fatchall()
        {
            List<Config> returnlist = new();
            connection.ConnectionString = line[0];
          
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
                        data.lowtempmachine = reader.GetString(1);
                        data.hitempmachine = reader.GetString(2);
                        
                        returnlist.Add(data);

                    }
                }

            connection.Close();
            return returnlist;
        }
    }
}
