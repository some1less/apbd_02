namespace apbd_02.exception;

public class EmptyBatteryException : Exception
{
    public EmptyBatteryException() : base()
    {
        Console.WriteLine("Cannot turn on. Battery is empty. Charge your device and try again :>");
    }
}