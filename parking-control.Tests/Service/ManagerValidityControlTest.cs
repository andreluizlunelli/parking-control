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
    public class ManagerValidityControlTest
    {
        [TestMethod]
        public void SetPriceTest()
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

        [TestMethod]
        public void GetPriceByDateTest()
        {
            DateTime initialDateControl = new DateTime(2015, 8, 16, 0, 0, 0);
            DateTime finalDateControl = new DateTime(2015, 11, 15, 23, 59, 59);
            double price = 5;
            ValidityControl.AddDateControl(price, initialDateControl, finalDateControl);

            initialDateControl = new DateTime(2015, 8, 18, 0, 0, 0);
            finalDateControl = new DateTime(2015, 11, 15, 23, 59, 59);
            price = 9;
            ValidityControl.AddDateControl(price, initialDateControl, finalDateControl);

            double returnedPrice = ValidityControl.GetPriceByDate(new DateTime(2015, 8, 17, 0, 0, 0));
            Assert.AreEqual(5, returnedPrice);
        }

        [TestMethod]
        public void GetPriceByDateRaiseExceptionTest()
        {
            ValidityControl.ClearListDates();
            try
            {
                ValidityControl.GetPriceByDate(new DateTime(2015, 8, 17, 0, 0, 0));
                Assert.Fail("Não lançou a exception");
            }
            catch (NotFoundDateControl e)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
