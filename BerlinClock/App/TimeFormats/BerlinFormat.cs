using System;
using System.Text.RegularExpressions;
/*
 * Berlin Clock timeFormat.
 * Stores the default time as a size 24 bit string.
 */
namespace BerlinClockApp
{
    class BerlinFormat : ITimeFormat
    {
        private DateTime standardTime;
        private const int numLampFirstRow = 1;
        private const int numLampSecondRow = 4;
        private const int numLampThirdRow = 4;
        private const int numLampFourthRow = 11;
        private const int numLampFifthRow = 4;
        private const int fiveTimeUnits = 5;
        private const int onState = 1;
        private const int offState = 0;
        private const String timeFormat = "24 character bit string: e.g.: 100001111000000000001111";
        private const String invalidInputError = "Invalid input String to Berlin Format";

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
                int seconds = GetTimeUnitsFromRowString(time.Substring(0, numLampFirstRow));
                int fiveHours = GetTimeUnitsFromRowString(time.Substring(numLampFirstRow, numLampSecondRow));
                int oneHours = GetTimeUnitsFromRowString(time.Substring(numLampFirstRow + numLampSecondRow, numLampThirdRow));
                int elevenMinutes = GetTimeUnitsFromRowString(time.Substring(
                                                                    numLampFirstRow 
                                                                    + numLampSecondRow 
                                                                    + numLampThirdRow, 
                                                            numLampFourthRow));
                int oneMinutes = GetTimeUnitsFromRowString(time.Substring(
                                                                numLampFirstRow 
                                                                + numLampSecondRow 
                                                                + numLampThirdRow 
                                                                + numLampFourthRow, 
                                                        numLampFifthRow));

                standardTime = new DateTime(1, 1, 1, 
                                (fiveHours * fiveTimeUnits + oneHours), 
                                (elevenMinutes * fiveTimeUnits + oneMinutes), 
                                (seconds + 1)
                               );
                // Console.WriteLine("berlin set time standard time " + standardTime);
            }
            else
            {
                throw new TimeFormatException(invalidInputError);
            }
        }

        public String GetTimeFormatRepresentation()
        {
            return timeFormat;
        }

        private String GetHourString(int hours)
        {
            return GetLampStateBitString((hours / fiveTimeUnits), numLampSecondRow) + GetLampStateBitString((hours % fiveTimeUnits), numLampThirdRow);
        }

        private String GetMinuteString(int minutes)
        {
            return GetLampStateBitString((minutes / fiveTimeUnits), numLampFourthRow) + GetLampStateBitString((minutes % fiveTimeUnits), numLampFifthRow);
        }

        private String GetSecondsString(int seconds)
        {
            return GetLampStateBitString(((seconds + 1) % 2), numLampFirstRow);
        }

        /*
         * Get the bit string given the number of on lamps and total lamps in a berlin clock row.
         */
        private String GetLampStateBitString(int numOnLamps, int numTotalLamps)
        {
            String lampState = "";
            for (int i=numTotalLamps; i>0; i--)
            {
                if (numOnLamps > 0)
                {
                    lampState += onState;
                    numOnLamps--;
                } else
                {
                    lampState += offState;
                }
            }
            return lampState;
        }

        /*
         * Checks if the time string is valid size 24 bit string
         */
        private bool ValidateTimeString(String time)
        {
            return (
                (time.Length == (numLampFirstRow + numLampSecondRow + numLampThirdRow + numLampFourthRow + numLampFifthRow))
                && (Regex.IsMatch(time, "^[01]+$"))
            );
        }

        /*
         * Takes the time string for the row as input and returns number of on lamps as output.
         * Also throws exception if a lamp is on but the prevous lamp was off in the berlin time row. 
         */
        private int GetTimeUnitsFromRowString(String timeString)
        {
            int total = 0;
            // prevBit and currBit ensure that the berlin format string slice has no 1s after 0s
            int prevBit = 1;
            int currBit = 0;

            foreach (char c in timeString)
            {
                currBit = (int)Char.GetNumericValue(c);
                if (prevBit == offState && currBit == onState)
                {
                    // ToDo: Remove this check. Useful for debugging for now.
                    Console.WriteLine("Exception at berlin input: " + timeString);
                    throw new TimeFormatException(invalidInputError);
                }
                total += (int)Char.GetNumericValue(c);
                prevBit = currBit;
            }
            return total;
        }
    }
}
