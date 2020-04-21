using System;
using System.Collections.Generic;
using Xunit;

namespace CodeBlock.Tests
{
    public class TextBlockMisc
    {
        public static IEnumerable<object[]> Data = new[] {
            new [] { TextBlocks.SingleLine  },
            new [] { TextBlocks.Multiline }
            };

        [Fact]
        public void MultiLineTest()
        {
            var b = new TextBlock(TextBlocks.Multiline);
            Assert.Equal(TextBlocks.MultiLineLines, b.LinesCount);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void GetText(string text)
        {
            var t = new TextBlock(text);
            var a = t.GetText();

            Assert.Equal(text, a);
        }
    }
}
