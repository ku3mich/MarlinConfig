using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Xunit.Abstractions;

namespace Clang.Tests
{
    public class DebugListener : IParseTreeListener
    {
        private readonly ITestOutputHelper OutputHelper;

        public DebugListener(ITestOutputHelper output)
        {
            OutputHelper = output;
        }

        int Indent = 0;

        protected void Output(string s, int indent)
        {
            OutputHelper.WriteLine($"{new string(' ', indent)}{s}");
        }

        protected void IndentOuput(string s)
        {
            Output(s, Indent);
            Indent += 2;
        }
        protected void UnindentOuput(string s)
        {
            Indent -= 2;
            Output(s, Indent);
        }

        public void EnterEveryRule([NotNull] ParserRuleContext ctx)
        {
            IndentOuput($"{{ {ctx.GetType().Name}: @{ctx.Start?.Line}:{ctx.Start?.Column} ");
        }

        public void ExitEveryRule([NotNull] ParserRuleContext ctx)
        {
            UnindentOuput($"}} {ctx.GetType().Name} @{ctx.Stop?.Line}:{ctx.Stop?.Column}");
        }

        public void VisitErrorNode([NotNull] IErrorNode node)
        {
            /*
            var e = new System.Exception("* error");
            e.Data["node"] = node;
            // throw e;
            */
        }

        public void VisitTerminal([NotNull] ITerminalNode node)
        {
            // Method intentionally left empty.
        }
    }
}
