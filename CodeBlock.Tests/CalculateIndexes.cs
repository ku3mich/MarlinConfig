using System.Collections.Generic;
using Xunit;

namespace CodeBlock.Tests
{
    public class CalculateIndexes
    {
        public static IEnumerable<object[]> Data = new[] {
            new [] { "asd\nfghj\r\n\n" },
            new [] { "asd\nfghj\r\n\n1" }
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void LineCount(string s)
        {
            var block = new TextBlock(s);
            Assert.Equal(3, block.LinesCount);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void DeleteLine(string s)
        {
            var block = new TextBlock(s);
            block.DeleteText(Cursor.Begin, 4);
            Assert.Equal(2, block.LinesCount);
        }

    }
}
