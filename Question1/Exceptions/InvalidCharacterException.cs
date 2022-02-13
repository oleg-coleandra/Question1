using System;

namespace Question1.Exceptions
{
    public class InvalidCharacterException : Exception
    {
        public InvalidCharacterException(string message) : base(message)
        {

        }
    }
}
