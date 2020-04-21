namespace CodeBlock
{
    public struct Cursor
    {
        public int Line { get; private set; }
        public int Pos { get; private set; }

        public Cursor(int line, int pos)
        {
            if (line < 0 || pos < 0)
                throw new TextIndexOutOfRangeException("negative values specified");

            Pos = pos;
            Line = line;
        }

        public static Cursor Set(int line, int pos) => new Cursor(line, pos);
        public static Cursor Begin => Set(0, 0);

        public void Deconstruct(out int line, out int pos)
        {
            line = Line;
            pos = Pos;
        }
    }
}
