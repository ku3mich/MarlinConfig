namespace CodeBlock
{
    public class NestedTextBlock : TextBlock, INestedTextBlock
    {
        public Cursor Position { get; set; }

        public NestedTextBlock(Cursor position, char[] text) : base(text)
        {
            Position = position;
        }
    }
}
