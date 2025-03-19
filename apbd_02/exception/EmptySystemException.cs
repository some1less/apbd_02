using System.Reflection;

namespace apbd_02.exception;

public class EmptySystemException : Exception
{
    public EmptySystemException() : base()
    {
        Console.WriteLine("Can't launch computer, because there is no installed operating system");
    }
}