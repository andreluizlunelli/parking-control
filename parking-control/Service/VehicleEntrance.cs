using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using parking_control.Models;
using parking_control.Service.Model;

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

        private DateTime dateOut = new DateTime();
        public DateTime DateOut
        {
            get { return dateOut; }
            set
            {
                if (DateTime.Compare(value, DateIn) > -1)
                    dateOut = value;
                else
                    throw new DateOutEarlierThanDateIn();
            }
        }
        private double priceCharged = 0;
        public double PriceCharged
        {
            get { return GetPriceCharged(); }
            set { this.priceCharged = value; }
        }

        // metodo não utiliza os segundos para o calculo
        private double GetPriceCharged()
        {
            if (InvalidDatetime(DateOut)) // não tem data de saida
            {
                return 0;
            }           
                
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

        public bool InvalidDatetime(DateTime time)
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

        public override string ToString()
        {
            return string.Format("ID:{0}, Placa: {1}, Hora Início:{2}, Hora Final:{3}, Preço final:{4}, Preço da hora:{5}", ID, Board, ToBRDatetime(DateIn), ToBRDatetime(DateOut), PriceCharged, HourPrice);
        }

    }

    public class DateOutEarlierThanDateIn : Exception
    {
    }

    public class VehicleControl
    {
        private static List<VehicleEntrance> listVehicle = new List<VehicleEntrance>();

        public static void Entry(string board, DateTime initialDate)
        {
            try
            {
                VehicleEntrance tmp = VehicleEntranceModel.Select(board);
                if (!tmp.InvalidDatetime(tmp.DateOut))
                { // veiculo já está no estacionamento
                    throw new VehicleIsInside();
                }
            }
            catch (NotExecuteCommandSql e)
            {
            }
            
            VehicleEntrance entrance = new VehicleEntrance(board, initialDate);
            AddVehicle(entrance);
            UpdateListDatesFromDB();
        }

        public static void Out(string board, DateTime finalDate)
        {
            VehicleEntrance ve = GetVehicleInside(board);
            ve.DateOut = finalDate;
            VehicleEntranceModel.Update(ve);
            UpdateListDatesFromDB();
        }

        public static void AddVehicle(VehicleEntrance vehicle)
        {
            VehicleEntranceModel.Insert(vehicle);
            listVehicle.Add(vehicle);
        }

        // DateOut é null
        public static VehicleEntrance GetVehicleInside(string board)
        {
            return listVehicle.Where((VehicleEntrance ve) => { return board == ve.Board && ve.InvalidDatetime(ve.DateOut); }).First();            
        }

        public static List<VehicleEntrance> GetListVehicles()
        {
            return listVehicle;
        }

        public static void UpdateListDatesFromDB()
        {
            listVehicle = VehicleEntranceModel.SelectAll();
        }

        public static void DeleteVehicleByID(int id)
        {
            VehicleEntranceModel.DeleteByID(id);
        }
    }

    public class VehicleIsInside : Exception
    {
    }
}