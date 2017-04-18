using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerlinClockApp
{
    public class TimeConverterConcrete : TimeConverter
    {
        public const String extendedTimeFormatRepresentation = "extended";
        public const String berlinTimeFormatRepresentation = "berlin";

        public TimeConverterConcrete(String inputFormat, String outputFormat)
        {
            this.inputFormat = this.GetTimeFormatFromString(inputFormat);
            this.outputFormat = this.GetTimeFormatFromString(outputFormat);
        }

        public override string GetOutputFormat()
        {
            return outputFormat.GetTime();
        }

        public override void SetInputTime(string time)
        {
            inputFormat.SetTime(time);
            outputFormat.SetStandardTime(inputFormat.GetStandardTime());
        }

        public String GetInputTimeString()
        {
            return inputFormat.GetTimeFormatRepresentation();
        }

        private ITimeFormat GetTimeFormatFromString(String timeFormat)
        {
            switch(timeFormat)
            {
                case extendedTimeFormatRepresentation: return new ExtendedFormat();
                case berlinTimeFormatRepresentation: return new BerlinFormat();
                default: return new ExtendedFormat();
            }
        }
    }
}
