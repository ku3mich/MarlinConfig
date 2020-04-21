namespace CodeBlock
{
    public interface ITextBlock
    {
        int LinesCount { get; }
        string this[int line] { get; }
        void InsertText(Cursor position, string text);
        void DeleteText(Cursor position, int length);
        int GetOffset(Cursor position);
        Cursor GetPosition(int offset);
        string GetText();
        string GetTextBlock(int offset, int length);
    }
}
