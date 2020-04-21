using Xunit;

namespace CodeBlock.Tests
{
    public class TextBlockCreates
    {
        [Fact]
        public void From_EmptyString()
        {
            var b = new TextBlock("");
            Assert.NotNull(b);
        }

        [Fact]
        public void From_MultiLineString()
        {
            var b = new TextBlock("asd\ndfg");
            Assert.NotNull(b);
        }

        [Fact]
        public void From_String()
        {
            var b = new TextBlock("asd");
            Assert.NotNull(b);
        }
    }
}
