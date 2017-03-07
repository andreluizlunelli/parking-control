using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace parking_control.Service
{
    public class VehicleEntrance
    {
        public VehicleEntrance(string board, DateTime dateIn)
        {
            Board = board;
            DateIn = dateIn;
        }

        public string Board { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public double PriceCharged { get; set; }

        // int is minute
        public int StayTime()
        {                        
            if (InvalidDatetime(DateOut))
            {
                throw new ArgumentException();
            }
            return (DateOut - DateIn).Minutes;
        }

        private bool InvalidDatetime(DateTime time)
        {
            return (time.Year == 1 && time.Month == 1 && time.Day == 1 && time.Hour == 0 && time.Minute == 0 && time.Second == 0);
        }
    }

}