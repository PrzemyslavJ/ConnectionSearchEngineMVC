using ConnectionSearchEngineMVC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestsCSE
{
    [TestClass]
    public class UnitTests
    {
        AllRecords records = new AllRecords();

        [TestMethod]
        public void TestListsConnectionsResults()
        {
            var firstStation1List = records.SingleSchedule(1).First().Station;
            var firstStation2List = records.SingleSchedule(2).First().Station;
            var lastStation3List = records.SingleSchedule(3).Last().Station;
            var lastStation4List = records.SingleSchedule(4).Last().Station;
            var lastStation5List = records.SingleSchedule(5).Last().Station;
            var firstStation6List = records.SingleSchedule(6).First().Station;
            Assert.AreEqual("Kraków Lotnisko (AirPort)", firstStation1List);
            Assert.AreEqual("Wieliczka Rynek", firstStation2List);
            Assert.AreEqual("Kamieñczyce", lastStation3List);
            Assert.AreEqual("Kraków P³aszów", lastStation4List);
            Assert.AreEqual("Tarnów", lastStation5List);
            Assert.AreEqual("Krynica", firstStation6List);
        }

        [TestMethod]
        public void TestSearchingResults()
        {
            TimeSpan timeFirst = new TimeSpan(3, 0, 0);
            var resultsFirst = records.SearchResultRecords("Tunel","Miechów", timeFirst, records.GetAllRoutes);
            var resultSecond = records.SearchResultRecords("K³aj", "Kokotów", timeFirst, records.GetAllRoutes);
            Assert.AreEqual(17, resultsFirst.Count());
            Assert.AreEqual(21, resultSecond.Count());
        }
    }
}
