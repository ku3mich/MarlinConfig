using Xunit;

namespace CodeBlock.Tests
{
    public class EmptyString
    {
        readonly TextBlock Block = new TextBlock("");

        [Fact]
        public void GetLineIndex()
        {
            var t = Block.GetLineIndex(0);
            Assert.Equal(0, t.Start);
            Assert.Equal(0, t.Length);
        }
    }

}
