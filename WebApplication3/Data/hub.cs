using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebApplication3.Models;
using System.Data.SqlClient;

namespace WebApplication3.Data
{
    public class ChatHub : Hub
    {
        private string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SHIMANODB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public async Task SendMessage()
        {
            
            List<ChartDataModel> returnlist = new();

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string sqlquery = "SELECT * FROM dbo.shimano";
                SqlCommand command = new SqlCommand(sqlquery, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ChartDataModel data = new();
                        data.roomtemp1 = reader.GetString(0);
                        data.roomtemp2 = reader.GetString(1);
                        data.machinetemp1 = reader.GetString(2);
                        data.machinetemp2 = reader.GetString(3);
                        data.Time = reader.GetInt32(4);
                        await Clients.All.SendAsync("ReceiveMessage", data.roomtemp1, data.roomtemp2, data.machinetemp1, data.machinetemp2);
                        returnlist.Add(data);

                    }
                }

            }


        }

    }
}
