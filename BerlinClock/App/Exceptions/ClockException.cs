using System;

namespace BerlinClockApp
{
    public class ClockException : Exception
    {
        public ClockException()
        {
        }

        public ClockException(string message) : base(message)
        {
        }

        public ClockException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}