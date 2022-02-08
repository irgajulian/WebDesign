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
        private string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=New Database;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public List<ChartDataModel> Fatchall()
        {
            List<ChartDataModel> returnlist = new();

            using SqlConnection connection = new(connectionstring);

            string sqlquery = "SELECT * FROM dbo.shimano";
            SqlCommand command = new SqlCommand(sqlquery, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ChartDataModel data = new();
                    data.roomtemp1 = reader.GetString(0);
                    data.roomtemp2 = reader.GetString(1);
                    data.mcahinetemp1 = reader.GetString(2);
                    data.maachinetemp2 = reader.GetString(3);
                    data.Time = reader.GetInt32(4);

                    returnlist.Add(data);

                }
            }

            return returnlist;
        }
    }
}
