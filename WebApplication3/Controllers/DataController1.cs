using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class DataController1 : Controller
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
