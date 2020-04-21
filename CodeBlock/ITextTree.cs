using System.Collections.Generic;

namespace CodeBlock
{
    public interface ITextTree
    {
        ITextTree Parent { get; }
        IEnumerable<ITextTree> Leafs { get; }
    }
}
