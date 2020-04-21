using Antlr4.Runtime.Misc;
using Clang;

namespace MarlingConfig
{
    public class Collector : ClangParserBaseListener
    {
        public Collector()
        {

        }

        public override void ExitDefine([NotNull] ClangParser.DefineContext context)
        {

        }
    }
}
