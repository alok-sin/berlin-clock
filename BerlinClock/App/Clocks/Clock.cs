using System;
/*
 * Abstract clock class defines base clock functionalities.
 */
namespace BerlinClockApp
{
    abstract class Clock
    {
        public ITimeFormat timeFormat;

        public Clock() {
            // Code to initialize class goes here.
        }
        abstract public void ShowTime();
        abstract public String[] GetCustomizableProperties();
        abstract public void SetCustomizableProperty(String property, String newValue);
    }
}
