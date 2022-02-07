using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class ChartDataModel
    {
        public string ChartName { get; set; }

        public List<string> ChartLabel { get; set; }

        public List<int> ChartData { get; set; }
    }
}
