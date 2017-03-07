using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using parking_control.Service;

namespace parking_control.Tests.Service
{
    [TestClass]
    public class ValidityControlTest
    {
        [TestMethod]
        public void SetPrice01()
        {
            DateTime initialDateControl = new DateTime(2015, 8, 16, 0, 0, 0);
            DateTime finalDateControl = new DateTime(2015, 11, 15, 23, 59, 59);
            double price = 5;

            ValidityControl.AddDateControl(price, initialDateControl, finalDateControl);
            List<ValidityDateControl> list = ValidityControl.GetListDates();
            ValidityDateControl dateControl = list[0];

            Assert.IsTrue(DateTime.Compare(dateControl.InitialDate, initialDateControl) == 0
                          && DateTime.Compare(dateControl.FinalDate, finalDateControl) == 0
                          && dateControl.HourPrice == price, "Houve um erro, datas diferentes");

        }

        public void ReturnPrice01()
        {
            DateTime initialDateControl = new DateTime(2015, 8, 16, 0, 0, 0);
            DateTime finalDateControl = new DateTime(2015, 11, 15, 23, 59, 59);
            double hourPrice = 2;
            ValidityControl.AddDateControl(hourPrice, initialDateControl, finalDateControl);

            string board = "ABC 8801";
            DateTime dateIn = new DateTime(2015, 9, 25, 15, 30, 0);            
            VehicleEntrance entrance = new VehicleEntrance(board, dateIn);
            entrance.DateOut = new DateTime(2015, 9, 25, 16, 0, 0);

            ValidityControl.GetPrice(entrance);
            Assert.IsTrue(entrance.PriceCharged == 1, "Erro ao calcular o preço, deveria ser 1, retornado: "+entrance.PriceCharged);
        }
    }
}
