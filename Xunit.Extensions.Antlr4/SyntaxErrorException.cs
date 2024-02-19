using System;

namespace Xunit.Extensions.Antlr4;

public class SyntaxErrorException : Exception
{
    public SyntaxErrorException(string message, Exception inner) : base(message, inner)
    {
    }
}
