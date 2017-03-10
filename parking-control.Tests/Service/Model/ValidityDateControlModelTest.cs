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
    public class ValidityDateControlModelTest
    {
        [TestCleanup]
        public void clean()
        {
            ValidityControlTest.RemoveItens();
        }

        [TestMethod]
        public void test01()
        {
            DateTime initialDateControl = new DateTime(2015, 8, 16, 0, 0, 0);
            DateTime finalDateControl = new DateTime(2015, 11, 15, 23, 59, 59);
            double price = 5;

            ValidityDateControl dateControl = new ValidityDateControl(price, initialDateControl, finalDateControl);
            
            //Insert
            ValidityDateControlModel.Insert(dateControl);
            //Select initial date
            ValidityDateControl dateControlSelected = ValidityDateControlModel.Select(initialDateControl);
            Assert.IsTrue(DateTime.Compare(dateControl.InitialDate, dateControlSelected.InitialDate) == 0
              && DateTime.Compare(dateControl.FinalDate, dateControlSelected.FinalDate) == 0
              && dateControl.HourPrice == dateControlSelected.HourPrice, "Houve um erro, datas diferentes");

            //Select by id
            dateControlSelected = ValidityDateControlModel.Select(dateControlSelected.ID);
            Assert.IsTrue(DateTime.Compare(dateControl.InitialDate, dateControlSelected.InitialDate) == 0
              && DateTime.Compare(dateControl.FinalDate, dateControlSelected.FinalDate) == 0
              && dateControl.HourPrice == dateControlSelected.HourPrice, "Houve um erro, datas diferentes");

            //Update with exception
            try
            {
                ValidityDateControlModel.Update(dateControl); // tem que depois passar sem id pra gerar uma exceção
                Assert.Fail("Deveria ter gerado uma exceção NotFoundIDEntity quando passa para atualizar sem uma chave primária");
            }
            catch (NotFoundIDEntity e)
            {
                Assert.IsTrue(true);                
            }

            //Update
            dateControl.ID = dateControlSelected.ID;
            dateControl.HourPrice = 7;
            dateControl.FinalDate = new DateTime(2015, 12, 15, 23, 59, 59);
            ValidityDateControlModel.Update(dateControl);
            dateControlSelected = ValidityDateControlModel.Select(dateControlSelected.ID);
            Assert.IsTrue(DateTime.Compare(dateControl.InitialDate, dateControlSelected.InitialDate) == 0
              && DateTime.Compare(dateControl.FinalDate, dateControlSelected.FinalDate) == 0
              && dateControl.HourPrice == dateControlSelected.HourPrice, "Houve um erro, datas diferentes");

            //Delete
            ValidityDateControlModel.Delete(dateControl);
            try
            {
                ValidityDateControlModel.Select(dateControl.ID);
                Assert.Fail("Deveria ter lançado uma exceção pois não deveria existir objeto na base");
            }
            catch (NotExecuteCommandSql e)
            {
                Assert.IsTrue(true);                
            }
            
        }


    }

    
}
