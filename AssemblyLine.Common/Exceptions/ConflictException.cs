namespace AssemblyLine.Common.Exceptions
{
    public class ConflictException : AssemblyLineException
    {
        public ConflictException(string message)
            : base(message)
        {
        }
    }
}