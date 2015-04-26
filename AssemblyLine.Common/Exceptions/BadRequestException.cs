namespace AssemblyLine.Common.Exceptions
{
    public class BadRequestException : AssemblyLineException
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}