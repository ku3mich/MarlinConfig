using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace Clang.Tests
{
    public class ErrorThrower : BaseErrorListener
    {
        public override void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] IToken offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e)
        {
            throw new System.Exception($"syntax error at {line}:{charPositionInLine} = {msg}", e);
        }
    }
}
