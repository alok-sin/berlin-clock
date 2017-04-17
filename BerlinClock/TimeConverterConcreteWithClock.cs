using System;

namespace BerlinClockApp
{
    class TimeConverterConcreteWithClock : TimeConverterConcrete
    {
        private Clock clock;
        public TimeConverterConcreteWithClock(string inputFormat, string outputFormat) : base(inputFormat, outputFormat) { }

        public override void SetInputTime(string time)
        {
            base.SetInputTime(time);
            this.clock = this.getClockFromOutputFormat();
        }
        public String ShowClockTime()
        {
            return clock.ShowTime();
        }

        private Clock getClockFromOutputFormat()
        {
            if (this.outputFormat.GetType().Name == "BerlinFormat")
            {
                return new BerlinClock(outputFormat.GetStandardTime());
            }
            return null;
        }

        public String[] GetClockCustomizations()
        {
            return clock.GetCustomizableProperties();
        }

        public void SetClockCustomization(String property, String newValue)
        {
            clock.SetCustomizableProperty(property, newValue);
        }
    }
}
