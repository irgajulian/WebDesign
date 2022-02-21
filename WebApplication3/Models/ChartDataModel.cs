using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class ChartDataModel
    {
        public string RoomTemp1 { get; set; }
        public string RoomTemp2 { get; set; }
        public string MachineTemp1 { get; set; }
        public string MachineTemp2 { get; set; }
        public string RoomHumid1 { get; set; }
        public string RoomHumid2 { get; set; }
        public string MachineHumid1 { get; set; }
        public string MachineHumid2 { get; set; }
        public string Time { get; set; }
    }
}
