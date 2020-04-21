namespace CodeBlock.Tests
{
    public static class TextBlocks
    {
        public const string SingleLine = "asdfg";
        public static readonly int SingleLineLength = SingleLine.Length;

        public const string Multiline = "asdfg\nWqwertxY\nhgfT";
        public const int MultiLineLines = 3;
        public static readonly int MultilineLength = Multiline.Length;
        public static readonly int Multiline0Length = 5;
        public static readonly int Multiline1Length = 8;
        public static readonly int Multiline1Start = 6;
    }
}
