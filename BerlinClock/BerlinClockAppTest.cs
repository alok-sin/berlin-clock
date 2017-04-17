using System;
using System.Linq;

namespace BerlinClockApp
{
    class BerlinClockAppTest
    {
        static void Main()
        {
            // Keep the console window open in debug mode.
            Console.WriteLine("Choose timeformat for input:\n 1. hh:mm:ss \n 2. Berlin Format as 24 character bit string");
            int inputFormat = 0;
            try
            {
                inputFormat = Int32.Parse(Console.ReadLine());
                if (!((inputFormat == 1) || (inputFormat == 2)))
                {
                    EndApplication("Incorrect response.");
                }
            }
            catch (Exception e)
            {
                EndApplication("Input format incorrect.");
            }

            Console.WriteLine("Choosing Berlin Clock for output. Add other clocks for more options.");

            String inputTime = inputFormat == 1 ? "extended" : "berlin";
            var timeConverter = new TimeConverterConcreteWithClock(inputTime, "berlin");

            Console.WriteLine("Enter time in "+ timeConverter.GetInputTimeString());
            String userInputTime = Console.ReadLine();
            // userInputTime = "110001111111100000001100";
            try
            {
                timeConverter.SetInputTime(userInputTime);
            }
            catch (Exception e)
            {
                EndApplication("Incorrect time format.");
            }
            // Console.WriteLine(timeConverter.GetOutputFormat());
            Console.WriteLine("Set Some customizations for your Clock:");
            String[] customizations = timeConverter.GetClockCustomizations();
            if (customizations.Length > 1)
            {
                foreach (string property in customizations)
                {
                    Console.WriteLine(property);
                    String newValue = Console.ReadLine();
                    timeConverter.SetClockCustomization(property, newValue);
                }
            }
            Console.WriteLine(timeConverter.ShowClockTime());
            Console.ReadKey();
        }

        static void EndApplication(String message)
        {
            Console.WriteLine(message+" Terminating...");
            Console.ReadKey();
            System.Environment.Exit(1);
        }
    }
}
