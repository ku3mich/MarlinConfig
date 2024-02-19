using Antlr4.Runtime;
using MarlinConfig.Parser;
using Xunit.Extensions.Antlr4;

namespace MarlinConfig.Tests;

public class ParserFactory : IParserFactory
{
    public Antlr4.Runtime.Parser Create(ITokenStream stream) => new MarlinConfigParser(stream);
}