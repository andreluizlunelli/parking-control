using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace parking_control.Models
{
    public class VehicleEntrance
    {
        public String Board { get; set; }
        public DateTime GetIn { get; set; }
        public DateTime GetOut { get; set; }
        public double PriceCharged { get; set; }
    }
}