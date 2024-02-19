using System.IO;
using Antlr4.Runtime;

namespace MarlinConfig
{
    public class ConfigurationReader
    {
        public Configuration Read(Stream stream)
        {
            var configuration = new Configuration();
            var lexer = new Parser.MarlinConfigLexer(new AntlrInputStream(stream));
            var tokenStream = new CommonTokenStream(lexer);
            tokenStream.Fill();
            var parser = new Parser.MarlinConfigParser(tokenStream);
            parser.AddErrorListener(new SyntaxErrorThrower());
            var visitor = new ConfigVisitor(configuration);
            var ast = parser.file();
            visitor.Visit(ast);

            return configuration;
        }
    }
}