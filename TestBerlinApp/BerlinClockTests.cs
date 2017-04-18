using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BerlinClockApp;

namespace TestBerlinApp
{
    [TestClass]
    public class BerlinClockTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange  
            var timeConverter = new TimeConverterConcreteWithClock("extended", "berlin");

            // act  
            timeConverter.SetInputTime("10:45:32");
            var actualTime = timeConverter.ShowClockTime();

            // assert
            Assert.AreEqual("Y RROO OOOO YYRYYRYYROO OOOO", actualTime, "Incorrect output");
        }
    }
}
