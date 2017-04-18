using System;

namespace BerlinClockApp
{
    public class TimeConverterConcreteWithClock : TimeConverterConcrete
    {
        private Clock clock;
        public const String berlinFormatClassName = "BerlinFormat";

        public TimeConverterConcreteWithClock(string inputFormat, string outputFormat) : base(inputFormat, outputFormat) { }

        public override void SetInputTime(string time)
        {
            base.SetInputTime(time);
            this.clock = this.GetClockFromOutputFormat();
        }

        public String ShowClockTime()
        {
            return clock.ShowTime();
        }

        private Clock GetClockFromOutputFormat()
        {
            if (this.outputFormat.GetType().Name == berlinFormatClassName)
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
