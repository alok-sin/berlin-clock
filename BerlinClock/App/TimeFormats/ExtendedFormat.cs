using System;
/*
 * Timeformat: [hh]:[mm]:[ss] (https://en.wikipedia.org/wiki/ISO_8601#Times)
 */
namespace BerlinClockApp
{
    class ExtendedFormat : ITimeFormat
    {
        private DateTime standardTime;
        private const String timeFormat = "hh:mm:ss";

        public DateTime GetStandardTime()
        {
            return standardTime;
        }

        public String GetTime()
        {
            return standardTime.Hour+":"+standardTime.Minute+":"+standardTime.Second;
        }

        public void SetStandardTime(DateTime time)
        {
            standardTime = time;
        }

        public void SetTime(String time)
        {
            string[] timeSlices = time.Split(new string[] { ":" }, StringSplitOptions.None);
            standardTime = new DateTime(1, 1, 1, 
                                Int32.Parse(timeSlices[0]), 
                                Int32.Parse(timeSlices[1]), 
                                Int32.Parse(timeSlices[2])
                           );
        }

        public String GetTimeFormatRepresentation()
        {
            return timeFormat;
        }
    }
}
