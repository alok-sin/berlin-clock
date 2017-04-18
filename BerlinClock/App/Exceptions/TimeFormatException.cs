using System;

namespace BerlinClockApp
{
    public class TimeFormatException : Exception
    {
        public TimeFormatException()
        {
        }

        public TimeFormatException(string message) : base(message)
        {
        }

        public TimeFormatException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}