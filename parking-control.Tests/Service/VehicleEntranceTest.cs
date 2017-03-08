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
    public class VehicleEntranceTest
    {
        private VehicleEntrance GetVehicleTest()
        {
            string board = "ABC 8801";
            DateTime dateIn = new DateTime(2015, 9, 25, 15, 30, 0);
            return new VehicleEntrance(board, dateIn);
        }

        [TestMethod]
        public void StayTimeReturnDateOutInvalidTest()
        {
            VehicleEntrance entrance = GetVehicleTest();
            try
            {
                entrance.StayTime();
                Assert.Fail("Deveria ter lançado uma ArgumentException");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(true);                
            }                                    
        }

        [TestMethod]
        public void StayTimeReturn30MinuteTest()
        {
            VehicleEntrance entrance = GetVehicleTest();
            entrance.DateOut = new DateTime(2015, 9, 25, 16, 0, 0);
            int time = entrance.StayTime();
            Assert.IsTrue(time == 30, "Não está calculando corretamente a duração");
        }

        [TestMethod]
        public void GetPriceMinuteOutNullRaiseExceptionTest()
        {
            VehicleEntrance entrance = GetVehicleTest();            
            try
            {
                double price = entrance.PriceCharged;
                Assert.Fail("Deveria ter lançado uma ArgumentException");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void GetPrice15MinuteTest()
        {
            DateTime initialDateControl = new DateTime(2015, 8, 16, 0, 0, 0);
            DateTime finalDateControl = new DateTime(2015, 11, 15, 23, 59, 59);
            double hourPrice = 2;
            ValidityControl.AddDateControl(hourPrice, initialDateControl, finalDateControl);

            VehicleEntrance vehicleTest = GetVehicleTest();
            vehicleTest.DateOut = new DateTime(2015, 9, 25, 15, 45, 0);
            Assert.AreEqual(1, vehicleTest.PriceCharged);            

            ValidityControl.GetListDates()[0].HourPrice = 3;
            Assert.AreEqual(1.5, vehicleTest.PriceCharged);
        }
        
    }
}
