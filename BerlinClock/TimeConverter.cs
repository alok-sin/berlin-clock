namespace BerlinClockApp
{
    abstract class TimeConverter
    {
        protected ITimeFormat inputFormat;
        protected ITimeFormat outputFormat;

        public abstract void SetInputTime(string time);
        public abstract string GetOutputFormat();
    }
}
