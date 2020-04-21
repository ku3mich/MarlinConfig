using Antlr4.Runtime.Misc;

namespace Clang
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
