using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class DatePicker
    {
        public string Range { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public bool Year { get; set; }

    }
}
