using System;

namespace Clang
{
    public class SyntaxErrorException : Exception
    {
        public SyntaxErrorException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
