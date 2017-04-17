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
        private String[] customizableProperties = new String[] { "Yellow Color", "Red Color", "Off Color" };
        
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
        public override string ShowTime()
        {
            String berlinFormat = timeFormat.GetTime();
            Tuple<String, String, int>[] tuples =
            {
                Tuple.Create("first", yellowColor, 1),
                Tuple.Create("second", redColor, 4),
                Tuple.Create("third", redColor, 4),
                Tuple.Create("fourth", yellowColor, 2),
                Tuple.Create("fourth", redColor, 1),
                Tuple.Create("fourth", yellowColor, 2),
                Tuple.Create("fourth", redColor, 1),
                Tuple.Create("fourth", yellowColor, 2),
                Tuple.Create("fourth", redColor, 1),
                Tuple.Create("fourth", yellowColor, 2),
                Tuple.Create("fifth", yellowColor, 4),
            };

            String output = "";
            String currRow = "first";
            int currentOffset = 0;
            foreach (Tuple<String, String, int> t in tuples)
            {
                if (currRow != t.Item1)
                {
                    output += " ";
                    currRow = t.Item1;
                }
                output += GetFomattedTimeSlice(berlinFormat.Substring(currentOffset, t.Item3), t.Item2);
                currentOffset += t.Item3;
            }

            return output;
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
            // this.GetType().GetProperty(customizableProperties[property]).SetValue(newValue);
            switch(property)
            {
                case "Yellow Color": yellowColor = newValue; break;
                case "Red Color": redColor = newValue; break;
                case "Off Color": offLampColor = newValue; break;
                default: throw new ApplicationException("Invalid property referenced.");
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
