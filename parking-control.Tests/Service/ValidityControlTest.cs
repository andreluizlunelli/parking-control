using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using parking_control.Models;

namespace parking_control.Tests.Service
{
    [TestClass]
    class ValidityControlTest
    {
        [TestMethod]
        public void SetPrice01()
        {
            DateTime initialDateControl = new DateTime(2015, 8, 16, 0, 0, 0);
            DateTime finalDateControl = new DateTime(2015, 11, 15, 23, 59, 59);
            double price = 5;

            ValidityControl.SetDateControl(price, initialDateControl, finalDateControl);
            Assert.IsTrue(, "deu ruin");
        }

        public void ReturnPrice01()
        {
            DateTime initialDateIn = new DateTime(2015, 9, 10, 14, 0, 0);
            DateTime initialDateOut = new DateTime(2015, 11, 15, 16, 0, 0);

            ValidityControl.GetReturnPrice();

        }
    }
}
