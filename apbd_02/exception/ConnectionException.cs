namespace apbd_02.exception;

public class ConnectionException : Exception
{
    public ConnectionException() : base()
    {
        Console.WriteLine("Cannot connect to the networkname. Maybe because it does not contain MD Ltd.?");
    }
}