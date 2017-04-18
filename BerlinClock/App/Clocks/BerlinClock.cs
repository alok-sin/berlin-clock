using System;
using System.Text.RegularExpressions;
using System.Text;

namespace BerlinClockApp
{
    class BerlinClock : Clock
    {
        private String yellowColor = "Y";
        private String redColor = "R";
        private String offLampColor = "O";
        private readonly String[] customizableProperties = new String[] { "Yellow Color", "Red Color", "Off Color" };
        private const int numLampFirstRow = 1;
        private const int numLampSecondRow = 4;
        private const int numLampThirdRow = 4;
        private const int numLampFourthRow = 11;
        private const int numConsecutiveLampSameColorFourthRow = 2; 
        private const int numSingleLampFourthRow = 1; 
        private const int numLampFifthRow = 4;
        private const int fiveTimeUnits = 5;
        private const int onState = 1;
        private const int offState = 0;

        public BerlinClock(BerlinFormat time)
        {
            timeFormat = time;
        }

        public BerlinClock(DateTime time)
        {
            BerlinFormat berlinFormat = new BerlinFormat();
            berlinFormat.SetStandardTime(time);
            timeFormat = berlinFormat;
        }

        public BerlinClock(BerlinFormat time, String topLampColor, String hourLampColor, String offLampColor)
        {
            timeFormat = time;
            this.yellowColor = topLampColor;
            this.redColor = hourLampColor;
            this.offLampColor = offLampColor;
        }

        public override void ShowTime()
        {
            String berlinFormat = timeFormat.GetTime();
            // Generate an array of tuples where each tuple represents a consecutive set of lamps
            // with same on color. The first item in tuple is the row number.
            Tuple<String, String, int>[] tuples =
            {
                Tuple.Create("first", yellowColor, numLampFirstRow),
                Tuple.Create("second", redColor, numLampSecondRow),
                Tuple.Create("third", redColor, numLampThirdRow),
                Tuple.Create("fourth", yellowColor, numConsecutiveLampSameColorFourthRow),
                Tuple.Create("fourth", redColor, numSingleLampFourthRow),
                Tuple.Create("fourth", yellowColor, numConsecutiveLampSameColorFourthRow),
                Tuple.Create("fourth", redColor, numSingleLampFourthRow),
                Tuple.Create("fourth", yellowColor, numConsecutiveLampSameColorFourthRow),
                Tuple.Create("fourth", redColor, numSingleLampFourthRow),
                Tuple.Create("fourth", yellowColor, numConsecutiveLampSameColorFourthRow),
                Tuple.Create("fifth", yellowColor, numLampFifthRow),
            };

            String output = "";
            String currRow = "first";
            int currentOffset = 0;
            foreach (Tuple<String, String, int> timeSlice in tuples)
            {
                if (currRow != timeSlice.Item1)
                {
                    output += " ";
                    currRow = timeSlice.Item1;
                }
                output += GetFomattedTimeSlice(
                                berlinFormat.Substring(currentOffset, timeSlice.Item3), 
                                timeSlice.Item2
                          );
                currentOffset += timeSlice.Item3;
            }

            Console.WriteLine(output);
        }

        public override String[] GetCustomizableProperties()
        {
            return customizableProperties;
        }

        /*
         * Changes the customizable properties of the Clock.
         */
        public override void SetCustomizableProperty(String property, String newValue)
        {
            // @ToDo: change customizableProperties to a map and do this in one line:
            // this.GetType().GetProperty(customizableProperties[property]).SetValue(newValue);
            switch (property)
            {
                case "Yellow Color": yellowColor = newValue; break;
                case "Red Color": redColor = newValue; break;
                case "Off Color": offLampColor = newValue; break;
                default: throw new ClockException("Invalid property referenced.");
            }
        }

        private String GetFomattedTimeSlice(String timeSlice, String onColor)
        {
            String formattedTime = "";
            foreach (char c in timeSlice)
            {
                if (c.Equals('1'))
                    formattedTime += onColor;
                else
                    formattedTime += offLampColor;
            }

            return formattedTime;                
        }
    }
}
