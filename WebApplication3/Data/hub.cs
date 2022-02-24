using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebApplication3.Models;
using System.Data.SqlClient;

namespace WebApplication3.Data
{
    public class ChatHub : Hub
    {
        public string conn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SHIMANODB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection = new();
        SqlCommand cmd = new();
        SqlDataReader dr;

        void Connectionstring()
        {
            connection.ConnectionString = conn;
        }

        public async Task SendMessage()
        {
            Console.WriteLine("sendmessage");
            List<ChartDataModel> returnlist = new();
            Connectionstring();
            connection.Open();

            Random val = new();
            int temp1 = val.Next(0, 100);
            int temp2 = val.Next(0, 100);
            int temp3 = val.Next(0, 100);
            int temp4 = val.Next(0, 100);
            int humid1 = val.Next(0, 100);
            int humid2 = val.Next(0, 100);
            int humid3 = val.Next(0, 100);
            int humid4 = val.Next(0, 100);

            DateTime thisDay = DateTime.Today;
            string date = thisDay.ToString("d");
            string time = thisDay.ToString();
            time = time.Remove(0, 11);
            string today = date.Replace('/', '_');

            string sqlinsertdata = String.Format("UPDATE dbo.shimano SET roomTemp1 = '{0}', roomTemp2 = '{1}', machineTemp1 = '{2}', machineTemp2 = '{3}', roomHumid1 = '{4}', roomHumid2 = '{5}', machineHumid1 = '{6}', machineHumid2 = '{7}', Time = '{8}' WHERE ID = 1", temp1, temp2, temp3, temp4, humid1, humid2, humid3, humid4, time);
            string sqlcreatettable = String.Format("CREATE TABLE dbo.D{0} ([ID] INT  IDENTITY (1, 1) NOT NULL,[roomTemp1] TEXT NULL, [roomTemp2] TEXT NULL, [machineTemp1] TEXT NULL, [machineTemp2] TEXT NULL, [roomHumid1] TEXT NULL, [roomHumid2] TEXT NULL, [machineHumid1] TEXT NULL, [machineHumid2] TEXT NULL, [time] TEXT NULL, PRIMARY KEY CLUSTERED([ID] ASC)); ", today);
            string sqlchecktable = String.Format("SELECT CASE WHEN OBJECT_ID('D{0}', 'U') IS NOT NULL THEN 'true' ELSE 'false' END;", today);
            string sqlgetdata = "SELECT * FROM dbo.shimano";

            bool b = false;
            b = execute(sqlchecktable);
            if (b == false)
            {
                Execnonquery(sqlcreatettable);
            }

            dr = Execquery(sqlgetdata);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ChartDataModel data = new();
                    data.RoomTemp1 = dr.GetString(1);
                    data.RoomTemp2 = dr.GetString(2);
                    data.MachineTemp1 = dr.GetString(3);
                    data.MachineTemp2 = dr.GetString(4);
                    data.RoomHumid1 = dr.GetString(5);
                    data.RoomHumid2 = dr.GetString(6);
                    data.MachineHumid1 = dr.GetString(7);
                    data.MachineHumid2 = dr.GetString(8);
                    data.Time = dr.GetString(9);
                    await Clients.All.SendAsync("ReceiveMessage", data.RoomTemp1, data.RoomTemp2, data.MachineTemp1, data.MachineTemp2, data.RoomHumid1, data.RoomHumid2, data.MachineHumid1, data.MachineHumid2);
                    returnlist.Add(data);

                }
            }
            connection.Close();
        }
        int Execnonquery(string query = null)
        {
            SqlCommand cmd = new(query, connection);
            return cmd.ExecuteNonQuery();
        }
        SqlDataReader Execquery(string query = null)
        {
            SqlCommand cmd = new(query, connection);
            return cmd.ExecuteReader();
        }
        bool execute(string query = null)
        {
            SqlCommand cmd = new(query, connection);
            return Convert.ToBoolean(cmd.ExecuteScalar());
        }

        public async Task InputData()
        {
            Console.WriteLine("inputdata");
            Connectionstring();
            connection.Open();
            Random val = new();
            int temp1 = val.Next(0, 100);
            int temp2 = val.Next(0, 100);
            int temp3 = val.Next(0, 100);
            int temp4 = val.Next(0, 100);
            int humid1 = val.Next(0, 100);
            int humid2 = val.Next(0, 100);
            int humid3 = val.Next(0, 100);
            int humid4 = val.Next(0, 100);

            DateTime thisDay = DateTime.Today;
            string date = thisDay.ToString("d");
            string time = thisDay.ToString();
            time = time.Remove(0, 11);
            string today = date.Replace('/', '_');

            string sqlinsertdata = String.Format("UPDATE dbo.shimano SET roomTemp1 = '{0}', roomTemp2 = '{1}', machineTemp1 = '{2}', machineTemp2 = '{3}', roomHumid1 = '{4}', roomHumid2 = '{5}', machineHumid1 = '{6}', machineHumid2 = '{7}', Time = '{8}' WHERE ID = 1", temp1, temp2, temp3, temp4, humid1, humid2, humid3, humid4, time);
            Execnonquery(sqlinsertdata);

            connection.Close();
        }


    }
}
