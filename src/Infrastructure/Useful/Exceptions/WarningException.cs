using System;

namespace Useful.Exceptions
{
    public class WarningException : Exception
    {
        public WarningException(string message) : base(message) { }
    }
}