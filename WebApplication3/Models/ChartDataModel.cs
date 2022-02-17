using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class ChartDataModel
    {
        public string roomtemp1 { get; set; }
        public string roomtemp2 { get; set; }
        public string machinetemp1 { get; set; }
        public string machinetemp2 { get; set; }
        public Int32 Time { get; set; }
    }
}
