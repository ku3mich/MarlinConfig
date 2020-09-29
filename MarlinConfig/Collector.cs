using Antlr4.Runtime.Misc;
using Clang;

namespace MarlinConfig
{

    public class Collector : ClangParserBaseListener
    {
        private readonly Configuration Configuration;

        public Collector(Configuration configuration)
        {
            Configuration = configuration;
        }

        public override void ExitDefine([NotNull] ClangParser.DefineContext context)
        {
            Configuration.Defines.Add(context.Identifier, context.Value);
        }
    }
}
