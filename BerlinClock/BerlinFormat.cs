using System;
using System.Text.RegularExpressions;
/*
 * Berlin Clock timeFormat.
 * Stores the default time as a bit string.
 */
namespace BerlinClockApp
{
    class BerlinFormat : ITimeFormat
    {
        private DateTime standardTime;

        public DateTime GetStandardTime()
        {
            return standardTime;
        }

        public string GetTime()
        {
            return GetSecondsString(standardTime.Second)
                    + GetHourString(standardTime.Hour)
                    + GetMinuteString(standardTime.Minute);
        }

        public void SetStandardTime(DateTime time)
        {
            standardTime = time;
        }

        /*
         * Function receives the berlin time bit string as input and saves the time.
         */
        public void SetTime(string time)
        {
            if (ValidateTimeString(time))
            {
                int seconds = GetTimeUnitsFromString(time.Substring(0, 1));
                int fiveHours = GetTimeUnitsFromString(time.Substring(1, 4));
                int oneHours = GetTimeUnitsFromString(time.Substring(5, 4));
                int elevenMinutes = GetTimeUnitsFromString(time.Substring(9, 11));
                int oneMinutes = GetTimeUnitsFromString(time.Substring(20, 4));

                standardTime = new DateTime(1, 1, 1, 
                                (fiveHours * 5 + oneHours), 
                                (elevenMinutes * 5 + oneMinutes), 
                                seconds
                               );
                Console.WriteLine("berlin set time standard time " + standardTime);
            }
            else
            {
                throw new ApplicationException("Invalid input String to Berlin Format");
            }
        }

        public String GetTimeFormatRepresentation()
        {
            return "100001111000000000001111";
        }

        private String GetHourString(int hours)
        {
            return GetLampStateBitString((hours / 5), 4) + GetLampStateBitString((hours % 5), 4);
        }

        private String GetMinuteString(int minutes)
        {
            return GetLampStateBitString((minutes / 5), 11) + GetLampStateBitString((minutes % 5), 4);
        }

        private String GetSecondsString(int seconds)
        {
            return GetLampStateBitString((seconds+1 % 2), 1);
        }

        private String GetLampStateBitString(int numOnLamps, int numTotalLamps)
        {
            String lampState = "";
            for (int i=numTotalLamps; i>0; i--)
            {
                if (numOnLamps > 0)
                {
                    lampState += 1;
                    numOnLamps--;
                } else
                {
                    lampState += 0;
                }
            }
            return lampState;
        }

        private bool ValidateTimeString(String time)
        {
            return ((time.Length == 24) && (Regex.IsMatch(time, "^[01]+$")));
        }

        private int GetTimeUnitsFromString(String timeString)
        {
            int total = 0;
            // prevBit and currBit ensure that the berlin format string slice has no 1s after 0s
            int prevBit = 1;
            int currBit = 0;

            foreach (char c in timeString)
            {
                currBit = (int)Char.GetNumericValue(c);
                if (prevBit == 0 && currBit == 1)
                {
                    Console.WriteLine("Exception at berlin input: " + timeString);
                    throw new ApplicationException("Invalid Berlin time format");
                }
                total += (int)Char.GetNumericValue(c);
                prevBit = currBit;
            }
            return total;
        }
    }
}
