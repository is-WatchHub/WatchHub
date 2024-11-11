namespace Infrastructure.Exceptions;

internal class NotFoundException : Exception
{
    public NotFoundException() { }

    public NotFoundException(string message)
        : base(message) { }
}
