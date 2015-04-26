namespace AssemblyLine.Common.Exceptions
{
    public class ForbiddenException : AssemblyLineException
    {
        public ForbiddenException(string message)
            : base(message)
        {
        }
    }
}