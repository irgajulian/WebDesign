using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using System.Data.SqlClient;
namespace WebApplication3.Controllers
{
    public class AccountController : Controller
    {
        public string conn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SHIMANODB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection = new();
        SqlCommand cmd = new();
        SqlDataReader dr;
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        void Connectionstring()
        {
            connection.ConnectionString = conn;
        }
        [HttpPost]
        public IActionResult Verify(Account acc)
        {
            Connectionstring();
            connection.Open();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM dbo.Tbl_login WHERE username = '"+acc.Name+"' and password = '"+acc.Password+"'";
            dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                connection.Close();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                connection.Close();
                return View("Login");
            }
            
            
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
