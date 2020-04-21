using Xunit;

namespace CodeBlock.Tests
{
    public class MultiLine
    {
        readonly TextBlock Block = new TextBlock(TextBlocks.Multiline);

        [Fact]
        public void GetLineIndex_0()
        {
            var t = Block.GetLineIndex(0);
            Assert.Equal(0, t.Start);
            Assert.Equal(TextBlocks.Multiline0Length, t.Length);
        }

        [Fact]
        public void Throws_Negative()
        {
            Assert.Throws<TextIndexOutOfRangeException>(() => Block.GetLineIndex(-1));
        }

        [Fact]
        public void Throws_Beyound()
        {
            Assert.Throws<TextIndexOutOfRangeException>(() => Block.GetLineIndex(TextBlocks.MultiLineLines));
        }

        [Fact]
        public void Multiline_1()
        {
            var t = Block.GetLineIndex(1);
            Assert.Equal(TextBlocks.Multiline1Start, t.Start);
            Assert.Equal(TextBlocks.Multiline1Length, t.Length);
        }
    }

}
