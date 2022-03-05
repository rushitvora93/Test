using System;

namespace Common.Types.Exceptions
{
    public class EntryAlreadyExistsException : Exception
    {
        public EntryAlreadyExistsException(string exceptionText, Exception innerException) : base(exceptionText, innerException) { }
        public EntryAlreadyExistsException(string exceptionText) : base(exceptionText) { }
    }
}
