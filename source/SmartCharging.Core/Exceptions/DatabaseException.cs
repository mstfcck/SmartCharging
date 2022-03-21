namespace SmartCharging.Core.Exceptions;

public class DatabaseException : Exception
{
    public DatabaseException(string message) : base(message)
    {
    }
    
    public DatabaseException(string message, Exception exception) : base(message, exception)
    {
    }
}