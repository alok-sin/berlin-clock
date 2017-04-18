using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BerlinClockApp;

namespace TestBerlinApp
{
    [TestClass]
    public class BerlinClockTests
    {
        [TestMethod]
        public void TestExtendedToBerlinTen()
        {
            Tuple<String, String>[] tuples =
            {
                Tuple.Create("10:45:32", "Y RROO OOOO YYRYYRYYROO OOOO"),
                Tuple.Create("16:37:16", "Y RRRO ROOO YYRYYRYOOOO YYOO"),
                Tuple.Create("16:37:56", "Y RRRO ROOO YYRYYRYOOOO YYOO"),
                Tuple.Create("10:44:12", "Y RROO OOOO YYRYYRYYOOO YYYY"),
                Tuple.Create("23:44:12", "Y RRRR RRRO YYRYYRYYOOO YYYY"),
                Tuple.Create("3:44:12", "Y OOOO RRRO YYRYYRYYOOO YYYY"),
                Tuple.Create("3:13:21", "O OOOO RRRO YYOOOOOOOOO YYYO"),
                Tuple.Create("3:3:56", "Y OOOO RRRO OOOOOOOOOOO YYYO"),
                Tuple.Create("3:59:6", "Y OOOO RRRO YYRYYRYYRYY YYYY"),
                Tuple.Create("23:3:55", "O RRRR RRRO OOOOOOOOOOO YYYO")
            };
    
            foreach (Tuple<String, String>t in tuples)
            {
                // arrange  
                var timeConverter = new TimeConverterConcreteWithClock("extended", "berlin");

                // act  
                timeConverter.SetInputTime(t.Item1);
                var actualTime = timeConverter.ShowClockTime();

                // assert
                Assert.AreEqual(t.Item2, actualTime, "Incorrect output");
            }
        }
    }
}
