using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebApplication3.Models;
using System.Data.SqlClient;

namespace WebApplication3.Data
{
    public class ChatHuba : Hub
    {
        public string conn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SHIMANODB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //public string conn = @"Data Source=192.168.0.3;Initial Catalog=DB_Shimano;User Id=irgajulian;Password=123123";
        SqlConnection connection = new();
        SqlCommand cmd = new();
        SqlDataReader dr;

        void Connectionstring()
        {
            connection.ConnectionString = conn;
        }

        public List<ChartDataModel> getdata()
        {
            Connectionstring();
            connection.Open();

            List<ChartDataModel> returnlist = new();

            DateTime thisDay = DateTime.Today;
            string date = thisDay.ToString("d");
            //string time = thisDay.ToString();
            //time = time.Remove(0, 11);
            string today = date.Replace('/', '_');
            string sqlgetdata = String.Format("SELECT * FROM dbo.D{0}",today);
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
                    returnlist.Add(data);

                }
            }
            connection.Close();
            return returnlist;
        }

        public List<DataModel> fetchdata()
        {
            Connectionstring();
            //connection.ConnectionString = @"Data Source=192.168.0.3;Initial Catalog=DB_Shimano;User Id=irgajulian;Password=123123";
            connection.Open();

            List<DataModel> returnlist = new();

            DateTime thisDay = DateTime.Today;
            string date = thisDay.ToString("d");
            //string time = thisDay.ToString();
            //time = time.Remove(0, 11);
            string today = date.Replace('/', '_');
            string sqlgetdata = String.Format("SELECT * FROM dbo.tb_subscribe");
            //string sqlgetdata = String.Format("SELECT * FROM dbo.shimano");
            dr = Execquery(sqlgetdata);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataModel data = new();
                    data.item = dr.GetString(0);
                    data.Values = dr.GetString(1);
                    returnlist.Add(data);
                }
            }
            connection.Close();
            return returnlist;
        }

        public async Task SendMessage()
        {
            Console.WriteLine("sendmessage");
            List<DataModel> returnlist = new();
            //Connectionstring();
            connection.ConnectionString = @"Data Source=192.168.0.3;Initial Catalog=DB_Shimano;User Id=irgajulian;Password=123123";
            //connection.Open();

            DateTime thisDay = DateTime.Today;
            string date = thisDay.ToString("d");
            string time = thisDay.ToString();
            time = time.Remove(0, 11);
            string today = date.Replace('/', '_');

            //string sqlcreatettable = String.Format("CREATE TABLE dbo.D{0} ([ID] INT  IDENTITY (1, 1) NOT NULL,[roomTemp1] TEXT NULL, [roomTemp2] TEXT NULL, [machineTemp1] TEXT NULL, [machineTemp2] TEXT NULL, [roomHumid1] TEXT NULL, [roomHumid2] TEXT NULL, [machineHumid1] TEXT NULL, [machineHumid2] TEXT NULL, [time] TEXT NULL, PRIMARY KEY CLUSTERED([ID] ASC)); ", today);
            //string sqlchecktable = String.Format("SELECT CASE WHEN OBJECT_ID('D{0}', 'U') IS NOT NULL THEN 'true' ELSE 'false' END;", today);
            //string sqlgetdata = "SELECT * FROM dbo.tb_subscribe";
            //string sqlgetdata = "SELECT * FROM dbo.shimano";

            //bool b = false;
            //b = execute(sqlchecktable);
            //if (b == false)
            //{
            //    //Execnonquery(sqlcreatettable);
            //}

            //dr = Execquery(sqlgetdata);
            //if (dr.HasRows)
            //{
            //    while (dr.Read())
            //    {
            //        DataModel data = new();
            //        data.item = dr.GetString(0);
            //        data.Values = dr.GetString(1);
            //        returnlist.Add(data);
            //    }
            //}
            returnlist = fetchdata();
            var mchntemp1= returnlist.ElementAt(0);
            var mchnhumid1 = returnlist.ElementAt(1);
            var mchntemp2 = returnlist.ElementAt(2);
            var mchnhumid2 = returnlist.ElementAt(3);
            var romtemp1 = returnlist.ElementAt(4);
            var romhumid1 = returnlist.ElementAt(5);
            var romtemp2 = returnlist.ElementAt(6);
            var romhumid2 = returnlist.ElementAt(7);
            
            await Clients.All.SendAsync("ReceiveMessage", Convert.ToString(romtemp1.Values), Convert.ToString(romtemp2.Values), Convert.ToString(mchntemp1.Values), Convert.ToString(mchntemp2.Values), Convert.ToString(romhumid1.Values), Convert.ToString(romhumid2.Values), Convert.ToString(mchnhumid1.Values), Convert.ToString(mchnhumid2.Values));
            //connection.Close();
            //string values = String.Format("'{0}','{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'", romtemp1.Values, romtemp2.Values, mchntemp1.Values, mchntemp2.Values, romhumid1.Values, romhumid2.Values, mchnhumid1.Values, mchnhumid2.Values);
            //await InputData(values);
        } 
        
        public async Task InputData()
        {
            Console.WriteLine("inputdata");
            Connectionstring();
            //connection.Open();

            List<DataModel> returnlist = new();
            DateTime thisDay = DateTime.Now;
            string date = thisDay.ToString("d");
            string time = thisDay.ToString("T");
            string today = date.Replace('/', '_');

            returnlist = fetchdata();
            var mchntemp1 = returnlist.ElementAt(0);
            var mchnhumid1 = returnlist.ElementAt(1);
            var mchntemp2 = returnlist.ElementAt(2);
            var mchnhumid2 = returnlist.ElementAt(3);
            var romtemp1 = returnlist.ElementAt(4);
            var romhumid1 = returnlist.ElementAt(5);
            var romtemp2 = returnlist.ElementAt(6);
            var romhumid2 = returnlist.ElementAt(7);

            string values = String.Format("'{0}','{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'", romtemp1.Values, romtemp2.Values, mchntemp1.Values, mchntemp2.Values, romhumid1.Values, romhumid2.Values, mchnhumid1.Values, mchnhumid2.Values);

            string sqlinsertdata = String.Format("INSERT INTO dbo.D{0} (roomTemp1, roomTemp2, machineTemp1, machineTemp2, roomHumid1, roomHumid2, machineHumid1, machineHumid2, Time) VALUES ({1},'{2}')", today, values, time);
            string sqlcreatettable = String.Format("CREATE TABLE dbo.D{0} ([ID] INT  IDENTITY (1, 1) NOT NULL,[roomTemp1] TEXT NULL, [roomTemp2] TEXT NULL, [machineTemp1] TEXT NULL, [machineTemp2] TEXT NULL, [roomHumid1] TEXT NULL, [roomHumid2] TEXT NULL, [machineHumid1] TEXT NULL, [machineHumid2] TEXT NULL, [time] TEXT NULL, PRIMARY KEY CLUSTERED([ID] ASC)); ", today);
            string sqlchecktable = String.Format("SELECT CASE WHEN OBJECT_ID('D{0}', 'U') IS NOT NULL THEN 'true' ELSE 'false' END;", today);
            
            connection.Open();
            bool b = false;
            b = execute(sqlchecktable);
            if (b == false)
            {
                Execnonquery(sqlcreatettable);
            }
            else
            {
                Execnonquery(sqlinsertdata);
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

    }
}
