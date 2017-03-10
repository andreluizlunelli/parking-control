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
        [TestInitialize]
        public void init()
        {
            ValidityControl.ClearListDates();
        }

        [TestCleanup]
        public void clean()
        {
            ConnectMysql.Close();
        }

        [TestMethod]
        public void test01()
        {
            DateTime initialDateControl = new DateTime(2015, 8, 16, 0, 0, 0);
            DateTime finalDateControl = new DateTime(2015, 11, 15, 23, 59, 59);
            double price = 5;
            ValidityControl.AddDateControl(price, initialDateControl, finalDateControl);

            //Insert
            VehicleEntrance vehicle = new VehicleEntrance("ABC 1234", new DateTime(2015, 8, 16, 15, 0, 0));
            vehicle.DateOut = new DateTime(2015, 8, 16, 15, 30, 0);
            VehicleEntranceModel.Insert(vehicle);

            //Select by board
            VehicleEntrance selectedVehicle = VehicleEntranceModel.Select(vehicle.Board);
            Assert.IsTrue(vehicle.IsSameVehicle(selectedVehicle), "Objeto de veiculo esperado é diferente do retornado");

            selectedVehicle = VehicleEntranceModel.Select(selectedVehicle.ID);
            Assert.IsTrue(vehicle.IsSameVehicle(selectedVehicle), "Objeto de veiculo esperado é diferente do retornado");
            vehicle.ID = selectedVehicle.ID;

            //Update
            vehicle.HourPrice = 100;
            VehicleEntranceModel.Update(vehicle);
            VehicleEntrance updatedVehicle = VehicleEntranceModel.Select(vehicle.Board);
            Assert.IsTrue( !selectedVehicle.IsSameVehicle(updatedVehicle));
        }
       
    }
}
