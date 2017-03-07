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

        [TestMethod]
        public void StayTimeReturnDateOutInvalid()
        {
            string board = "ABC 8801";
            DateTime dateIn = new DateTime(2015, 9, 25, 15, 30, 0);
            VehicleEntrance entrance = new VehicleEntrance(board, dateIn);
            try
            {
                int time = entrance.StayTime();
                Assert.Fail("Deveria ter lançado uma ArgumentException");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(true);
                
            }                                    
        }

        [TestMethod]
        public void StayTimeReturn30Minute()
        {
            string board = "ABC 8801";
            DateTime dateIn = new DateTime(2015, 9, 25, 15, 30, 0);
            VehicleEntrance entrance = new VehicleEntrance(board, dateIn);
            entrance.DateOut = new DateTime(2015, 9, 25, 16, 0, 0);
            int time = entrance.StayTime();
            Assert.IsTrue(time == 30, "Não está calculando corretamente a duração");
        }

        
    }
}
