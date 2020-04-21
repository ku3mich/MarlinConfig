using Xunit;

namespace CodeBlock.Tests
{

    public class Insert
    {
        readonly TextBlock Block = new TextBlock("asd");

        [Fact]
        public void AtBegin()
        {
            Block.InsertText(Cursor.Set(0, 0), "fgh");
            var s = Block.GetText();
            Assert.Equal("fghasd", s);
        }

        [Fact]
        public void AtEnd()
        {
            Block.InsertText(Cursor.Set(0, 3), "fgh");
            var s = Block.GetText();
            Assert.Equal("asdfgh", s);
        }

        [Fact]
        public void InMiddleEnd()
        {
            Block.InsertText(Cursor.Set(0, 2), "fgh");
            var s = Block.GetText();
            Assert.Equal("asfghd", s);
        }
    }

}
