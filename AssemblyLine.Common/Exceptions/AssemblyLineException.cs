using System;

namespace AssemblyLine.Common.Exceptions
{
    public class AssemblyLineException : Exception
    {
        public AssemblyLineException(string message)
            : base(message)
        {
        }
    }
}