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
        string sqlquery;

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
            
            Connectionstring();
            connection.Open();
           
            if (date.Range != null)
            {
                string[] input = date.Day.Split('-');
                string[] start = input[0].Split('/');
                string[] end = input[1].Split('/');
                int startdate = Convert.ToInt32(start[0]);
                int enddate = Convert.ToInt32(end[0]);
                date.Day = date.Day.Replace('/', '_');
                sqlquery = String.Format( "SELECT * from dbo.D{0}",date.Day);
                cmd.Connection = connection;
                cmd.CommandText = sqlquery;
            }

            if (sqlquery != null)
            {
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
                        list.Add(data);
                    }
                }
            }
            else
            {
                ChartDataModel data = new();
                data.RoomTemp1 = "0";
                data.RoomTemp2 = "0";
                data.MachineTemp1 = "0";
                data.MachineTemp2 = "0";
                data.RoomHumid1 = "0";
                data.RoomHumid2 = "0";
                data.MachineHumid1 = "0";
                data.MachineHumid2 = "0";
                data.Time = "0";
                list.Add(data);
            }

            connection.Close();
            return View(list);
        }
    }
}
