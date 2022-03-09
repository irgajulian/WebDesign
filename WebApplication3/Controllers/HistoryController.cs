using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
namespace WebApplication3.Controllers
{
    public class HistoryController : Controller
    {
        //public string conn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SHIMANODB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string[] line = System.IO.File.ReadAllLines(@"C:\Users\Judi\Documents\GitHub\WebApplication3\WebDesign\WebApplication3\bin\Debug\Config.txt");
 
        SqlConnection connection = new();
        SqlCommand cmd = new();
        SqlDataReader dr;
        List<ChartDataModel> list = new();

        void Connectionstring()
        {
            connection.ConnectionString = line[0];
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult History(DatePicker date)
        {
            date.Range = date.Range.Replace('/', '_');
            var a = date.Month;
            var b = date.Year;
            string sqlquery;
            Connectionstring();
            connection.Open();
            if (date.Day == null)
            {
                sqlquery = String.Format( "SELECT * from dbo.D{0}",date.Range);
                cmd.Connection = connection;
                cmd.CommandText = sqlquery;
            }
            //SqlCommand cmd = new(query, connection);
            dr = cmd.ExecuteReader();

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
                    //await Clients.All.SendAsync("ReceiveMessage", data.RoomTemp1, data.RoomTemp2, data.MachineTemp1, data.MachineTemp2, data.RoomHumid1, data.RoomHumid2, data.MachineHumid1, data.MachineHumid2);
                    list.Add(data);
                }
            }
            connection.Close();
            return View(list);
        }
    }
}
