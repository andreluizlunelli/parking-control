using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using parking_control.Models;
using parking_control.Service;
using parking_control.Service.Model;

namespace parking_control.Tests.Service.Model
{
    [TestClass]
    public class VehicleEntranceModelTest
    {

        [TestCleanup]
        public void clean()
        {
            ConnectMysql.Close();
        }

        [TestMethod]
        public void test01()
        {
            VehicleEntrance vehicle = new VehicleEntrance("ABC 1234", new DateTime(2015, 8, 16, 0, 0, 0));
            VehicleEntranceModel.Insert(vehicle);
        }
    }
}
