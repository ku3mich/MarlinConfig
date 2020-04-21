namespace CodeBlock
{
    public static class TextBlockExtensions
    {
        public static string GetTextBlock(this ITextBlock block, Cursor from, Cursor to)
        {
            var fromOffs = block.GetOffset(from);
            var toOffs = block.GetOffset(to);
            return block.GetTextBlock(fromOffs, toOffs - fromOffs);
        }
    }
}
