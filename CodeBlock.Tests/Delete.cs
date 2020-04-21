using Xunit;

namespace CodeBlock.Tests
{
    public class Delete
    {
        readonly TextBlock Block = new TextBlock("asd");

        [Fact]
        public void AtBegin()
        {
            Block.DeleteText(Cursor.Set(0, 0), 1);
            var s = Block.GetText();
            Assert.Equal("sd", s);
        }

        [Fact]
        public void AtEnd()
        {
            Block.DeleteText(Cursor.Set(0, 2), 1);
            var s = Block.GetText();
            Assert.Equal("as", s);
        }

        [Fact]
        public void InMiddleEnd()
        {
            Block.DeleteText(Cursor.Set(0, 1), 2);
            var s = Block.GetText();
            Assert.Equal("a", s);
        }

        [Fact]
        public void LineOutOfText()
        {
            Assert.Throws<TextIndexOutOfRangeException>(() => Block.DeleteText(Cursor.Set(1, 1), 2));
        }

        [Fact]
        public void PositionOutOfLine()
        {
            Assert.Throws<TextIndexOutOfRangeException>(() => Block.DeleteText(Cursor.Set(0, 3), 2));
        }

        [Fact]
        public void LengthOutOfText()
        {
            Assert.Throws<TextIndexOutOfRangeException>(() => Block.DeleteText(Cursor.Set(0, 0), 4));
        }
    }
}
