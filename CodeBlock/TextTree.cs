using System.Collections.Generic;

namespace CodeBlock
{
    public class TextTree : ITextTree
    {
        public ITextTree Parent { get; set; }
        public IEnumerable<ITextTree> Leafs { get; set; }
    }
}
