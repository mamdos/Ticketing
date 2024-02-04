namespace Data.Common.Exceptions;

internal class InvalidEntityStateException : Exception
{
    internal InvalidEntityStateException(string message) : base(message)
    { }
}
