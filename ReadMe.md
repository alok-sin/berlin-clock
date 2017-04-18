## Berlin Clock - Mengenlehreuhr

### Structure:

 1. `App\TimeConverterFactory`: The app uses an abstract factory design pattern for abstracting out time conversions 
    ```TimeConverter -> TimeConverterConcrete -> TimeConverterConcreteWithClock```
 
 2. `App\TimeFormats`: These are basic time formats whoch can be used by themselves or in clocks.
    ```ITimeFormat -> ExtendedFormat | BerlinFormat```
 
 3. `App\Clocks`: These classes have a time format and other properties (which can be modified.)
    ```Clock -> BerlinClock```
 
 4. `TimeConverterConcreteWithClock` is used to take a `ITimeFormat` object as input and shows a `Clock` object as output.

 5. `BerlinClockAppTest.cs` is the `main()` class to run this application.
