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
            HourPrice = ValidityControl.GetPriceByDate(dateIn);
        }

        public VehicleEntrance(int id, string board, DateTime dateIn)
        {
            ID = id;
            Board = board;
            DateIn = dateIn;
            HourPrice = ValidityControl.GetPriceByDate(dateIn);
        }

        public int ID { get; set; }
        public double HourPrice { get; set; }
        public string Board { get; private set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        private double priceCharged = 0;
        public double PriceCharged
        {
            get { return GetPriceCharged(); }
            set { this.priceCharged = value; }
        }

        // metodo não utiliza os segundos para o calculo
        private double GetPriceCharged()
        {
            if (InvalidDatetime(DateOut))            
                throw new ArgumentException();

            TimeSpan diff = (DateOut - DateIn);
            if (diff.TotalMinutes <= 30)
            {
                return HourPrice / 2;
            }
            else if (diff.Minutes <= 10)
            {                
                return HourPrice * diff.Hours;
            }
            else if (diff.Minutes > 10)
            {
                return HourPrice * (diff.Hours + 1);
            }            
            return HourPrice * diff.Hours;
        }               

        private bool InvalidDatetime(DateTime time)
        {
            return (time.Year == 1 && time.Month == 1 && time.Day == 1 && time.Hour == 0 && time.Minute == 0 && time.Second == 0);
        }

        public static string ToBRDatetime(DateTime date)
        {
            return date.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public bool IsSameVehicle(VehicleEntrance other)
        {
            return this.Board == other.Board
                   && DateTime.Compare(this.DateIn, other.DateIn) == 0
                   && DateTime.Compare(this.DateOut, other.DateOut) == 0
                   && this.HourPrice == other.HourPrice
                   && this.PriceCharged == other.PriceCharged;
        }

    }

    public class ManagerVehicle
    {
        private static Dictionary<string, VehicleEntrance> DictVehicle = new Dictionary<string, VehicleEntrance>();

        public static void AddVehicle(VehicleEntrance vehicle)
        {
            DictVehicle.Add(vehicle.Board, vehicle);
        }

        public static VehicleEntrance GeyVehicle(string board)
        {
            return DictVehicle[board];
        }
    }

}