using System;
using System.Collections.Generic;

namespace CodeBlock
{
    public class TextBlock : ITextBlock
    {
        internal struct LineIndex
        {
            public int Start { get; set; }
            public int Length { get; set; }

            public LineIndex CheckPosition(int pos) =>
                pos < Length ? this : throw new TextIndexOutOfRangeException($"pos: {pos} is beyond of {Length})");

            public static LineIndex Specify(int s, int l) => new LineIndex
            {
                Start = s,
                Length = l
            };
        }

        private readonly List<LineIndex> LineIndexes = new List<LineIndex>();
        private char[] Text;

        public override string ToString()
        {
            string result;
            if (Text.Length < 64)
                result = GetText();
            else
                result = new string(Text, 0, 64) + "[...truncated]";

            return result.Replace("\n", "").Replace("\r", "");
        }

        public TextBlock(string text) : this(text.ToCharArray())
        {
        }

        public TextBlock(char[] text)
        {
            Text = text;
            CalculateLineIndexes(0);
        }

        private bool IsNewLine(char c) => "\r\n".IndexOf(c) != -1;
        private bool IsCR(char c) => '\r' == c;
        private bool IsLF(char c) => '\n' == c;

        private void CalculateLineIndexes(int line)
        {
            var l = line;
            int j = line == 0 ?
                0 :
                GetLineIndex(line).Start;

            var toRemove = LineIndexes.Count - line - 1;
            if (toRemove > 0)
                LineIndexes.RemoveRange(line, toRemove);

            int i = j, q = j;
            while (i < Text.Length - 1)
            {
                if (IsNewLine(Text[i]))
                {
                    LineIndexes.Add(LineIndex.Specify(q, i - q));
                    l++;

                    if (IsCR(Text[i]))
                    {
                        while (++i < Text.Length - 1 && IsLF(Text[i]))
                            ;

                        q = i;
                        continue;
                    }

                    q = i + 1;
                    if (i == Text.Length)
                        break;
                }

                i++;
            }

            if (l == LineIndexes.Count)
                LineIndexes.Add(LineIndex.Specify(q, i - q));
        }

        internal LineIndex GetLineIndex(int line)
        {
            if (line >= LineIndexes.Count || line < 0)
                throw new TextIndexOutOfRangeException($"line: {line} is beyond of block(lines={LineIndexes.Count})");

            return LineIndexes[line];
        }

        public string this[int line]
        {
            get
            {
                var i = GetLineIndex(line);
                return new string(Text, i.Start, i.Length);
            }
        }

        public int LinesCount => LineIndexes.Count;

        public void InsertText(Cursor position, string text)
        {
            var o = GetOffset(position);

            var charText = text.ToCharArray();
            var newText = new char[Text.Length + charText.Length];

            Buffer.BlockCopy(Text, 0, newText, 0, o * sizeof(char));
            Buffer.BlockCopy(charText, 0, newText, o * sizeof(char), charText.Length * sizeof(char));

            var rest = Text.Length - o;
            if (rest > 0)
                Buffer.BlockCopy(Text, o * sizeof(char), newText, (newText.Length - rest) * sizeof(char), rest * sizeof(char));

            Text = newText;
            CalculateLineIndexes(position.Line);
        }

        public void DeleteText(Cursor position, int length)
        {
            var ofs = GetOffset(position);

            if (ofs + length > Text.Length)
                throw new TextIndexOutOfRangeException($"invalid length: ${length}");

            var newText = new char[Text.Length - length];
            Buffer.BlockCopy(Text, 0, newText, 0, ofs * sizeof(char));
            Buffer.BlockCopy(Text, (ofs + length) * sizeof(char), newText, ofs * sizeof(char), (newText.Length - ofs) * sizeof(char));

            Text = newText;
            CalculateLineIndexes(position.Line);
        }


        public int GetOffset(Cursor position)
        {
            if (position.Line >= LinesCount)
                throw new TextIndexOutOfRangeException($"invalid line: {position.Line}; lines: {LinesCount}");

            var ofs = LineIndexes[position.Line];
            var pos = ofs.Start + position.Pos;
            return pos <= Text.Length ?
                pos :
                throw new TextIndexOutOfRangeException($"invalid offs: ${pos}; text length: {Text.Length}");
        }

        public Cursor GetPosition(int offset)
        {
            if (LinesCount == 0)
                throw new TextIndexOutOfRangeException("empty text");

            var i = 0;
            while (i < LinesCount)
            {
                if (LineIndexes[i].Start > offset)
                    break;
                i++;
            }

            return Cursor.Set(i, LineIndexes[i - 1].Start - offset);
        }

        public string GetText() => new string(Text);

        public string GetTextBlock(int offset, int length)
        {
            var index = offset + length;
            if (index > Text.Length)
                throw new TextIndexOutOfRangeException($"index: {index} is out of range: {Text.Length}");

            var res = new char[length];
            Buffer.BlockCopy(Text, offset * sizeof(char), res, 0, length * sizeof(char));

            return new string(res);
        }
    }
}
