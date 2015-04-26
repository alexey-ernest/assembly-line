namespace AssemblyLine.Common.Exceptions
{
    public class NotFoundException : AssemblyLineException
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}