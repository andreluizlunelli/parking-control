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
            try
            {
                VehicleEntrance entrance = GetVehicleTest();                
                Assert.Fail("Deveria ter lançado uma ArgumentException");
            }
            catch (NotFoundDateControl e)
            {
                Assert.IsTrue(true);                
            }                                    
        }                

        [TestMethod]
        public void GetPriceMinuteTest()
        {
            DateTime initialDateControl = new DateTime(2015, 8, 16, 0, 0, 0);
            DateTime finalDateControl = new DateTime(2015, 11, 15, 23, 59, 59);
            double hourPrice = 5;
            ValidityControl.AddDateControl(hourPrice, initialDateControl, finalDateControl);

            VehicleEntrance vehicleTest = GetVehicleTest();
            vehicleTest.DateOut = new DateTime(2015, 9, 25, 15, 45, 0); // 15m
            Assert.AreEqual(2.5, vehicleTest.PriceCharged);

            vehicleTest.DateOut = new DateTime(2015, 9, 25, 16, 0, 0); // 30m
            Assert.AreEqual(2.5, vehicleTest.PriceCharged);

            vehicleTest.DateOut = new DateTime(2015, 9, 25, 16, 30, 10); // 1H
            Assert.AreEqual(5, vehicleTest.PriceCharged);

            vehicleTest.DateOut = new DateTime(2015, 9, 25, 16, 40, 10); // 1H 10m
            Assert.AreEqual(5, vehicleTest.PriceCharged);

            vehicleTest.DateOut = new DateTime(2015, 9, 25, 16, 41, 0); // 1H 11m
            Assert.AreEqual(10, vehicleTest.PriceCharged);

            vehicleTest.DateOut = new DateTime(2015, 9, 25, 17, 35, 2); // 2H 5m
            Assert.AreEqual(10, vehicleTest.PriceCharged);

            vehicleTest.DateOut = new DateTime(2015, 9, 25, 17, 41, 0); // 2H 11m
            Assert.AreEqual(15, vehicleTest.PriceCharged);

        }
    }

    [TestClass]
    public class ManagerVehicleTest
    {

        [TestMethod]
        public void SetVehicleListTest()
        {
            DateTime initialDateControl = new DateTime(2015, 8, 16, 0, 0, 0);
            DateTime finalDateControl = new DateTime(2015, 11, 15, 23, 59, 59);
            double hourPrice = 5;
            ValidityControl.AddDateControl(hourPrice, initialDateControl, finalDateControl);

            string board = "ABC 8801";
            DateTime dateIn = new DateTime(2015, 9, 25, 15, 30, 0);
            VehicleEntrance vehicle = new VehicleEntrance(board, dateIn);

            ManagerVehicle.AddVehicle(vehicle);
            Assert.AreEqual(vehicle, ManagerVehicle.GeyVehicle(vehicle.Board));
        }
    }

    
}
