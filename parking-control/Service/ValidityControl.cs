using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using parking_control.Service.Model;

namespace parking_control.Service
{    

    public class ValidityDateControl
    {
        public int ID { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public double HourPrice { get; set; }

        public ValidityDateControl(double price, DateTime InitialDate, DateTime FinalDate)
        {
            this.HourPrice = price;
            this.InitialDate = InitialDate;
            this.FinalDate = FinalDate;
        }

        public ValidityDateControl(int id, double price, DateTime InitialDate, DateTime FinalDate)
        {
            this.ID = id;
            this.HourPrice = price;
            this.InitialDate = InitialDate;
            this.FinalDate = FinalDate;
        }

        public override string ToString()
        {
            return string.Format("ID:{0}, Preço:{1}, Hora Início:{2}, Hora Final:{3}", ID, HourPrice, ToBRDatetime(InitialDate), ToBRDatetime(FinalDate));
        }

        public static string ToBRDatetime(DateTime date)
        {
            return date.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public bool IsSameDate(ValidityDateControl other)
        {
            return HourPrice == other.HourPrice
                   && DateTime.Compare(InitialDate, other.InitialDate) == 0
                   && DateTime.Compare(FinalDate, other.FinalDate) == 0;
        }


    }

    public class ValidityControl
    {
        private static List<ValidityDateControl> listDates = new List<ValidityDateControl>();        

        public static void AddDateControl(double price, DateTime initialDateControl, DateTime finalDateControl)
        {
            ValidityDateControl validityDateControl = new ValidityDateControl(price, initialDateControl, finalDateControl);
            ValidityDateControlModel.Insert(validityDateControl);
            listDates.Add(validityDateControl);            
        }

        public static void AddDateControl(ValidityDateControl dateControl)
        {
            AddDateControl(dateControl.HourPrice, dateControl.InitialDate, dateControl.FinalDate);
        }

        public static List<ValidityDateControl> GetListDates()
        {
            return listDates;
        }

        public static void ClearListDates()
        {
            listDates.Clear();
        }

        public static double GetPriceByDate(DateTime dateTime)
        {
            ValidityDateControl filtered;
            try
            {
                filtered = listDates.Where((ValidityDateControl vdc) => { return dateTime >= vdc.InitialDate; }).First();
            }
            catch (InvalidOperationException e)
            {                
                throw new NotFoundDateControl();
            }            
            return filtered.HourPrice;
        }

        // atualiza a lista com informação vinda do banco
        public static void UpdateListDates()
        {
            listDates = ValidityDateControlModel.SelectAll();
        }
    }

    public class NotFoundDateControl : Exception
    {
    }
}