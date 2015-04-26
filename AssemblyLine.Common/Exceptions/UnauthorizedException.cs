namespace AssemblyLine.Common.Exceptions
{
    public class UnauthorizedException : AssemblyLineException
    {
        public UnauthorizedException(string message)
            : base(message)
        {
        }
    }
}