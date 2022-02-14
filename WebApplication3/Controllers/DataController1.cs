using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class DataController1 : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            List<ChartDataModel> data = new();
            GadgetDAO datatable = new();

            data = datatable.Fatchall();

            return View("index",data);
        }
        
    }
}
