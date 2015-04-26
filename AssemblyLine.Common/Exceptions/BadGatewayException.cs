namespace AssemblyLine.Common.Exceptions
{
    public class BadGatewayException : AssemblyLineException
    {
        public BadGatewayException(string message)
            : base(message)
        {
        }
    }
}