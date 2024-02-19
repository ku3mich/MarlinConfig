using Antlr4.Runtime;

namespace Xunit.Extensions.Antlr4
{
    public interface IParserFactory
    {
        Parser Create(ITokenStream stream);
    }
}
