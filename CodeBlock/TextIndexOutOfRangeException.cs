using System;

namespace CodeBlock
{
    public class TextIndexOutOfRangeException : Exception
    {
        public TextIndexOutOfRangeException(string message) : base(message)
        {
        }
    }
}
