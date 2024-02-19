using System;

namespace MarlinConfig;

public class SyntaxErrorException : Exception
{
    public SyntaxErrorException(string message, Exception inner) : base(message, inner)
    {
    }
}
