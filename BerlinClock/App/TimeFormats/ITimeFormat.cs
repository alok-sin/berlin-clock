using System;

namespace BerlinClockApp
{
    interface ITimeFormat
    {
        DateTime GetStandardTime();

        void SetStandardTime(DateTime time);

        // getTimeFormatString();
        String GetTime();

        void SetTime(String time);

        String GetTimeFormatRepresentation();
    }
}
