using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var chartDataModel = new ChartDataModel()
            {
                ChartData = new List<int>() { 12, 19, 3, 5, 2, 3 },
                ChartLabel = new List<string>() { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" },
                ChartName = "Chart JS demo"
            };
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Data()
        {
            return View();
        }
        public IActionResult History()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Inline()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
