using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Xunit.Extensions.DependencyInjection;

namespace Xunit.Extensions.Antlr4
{
    public class DebugListener(ITestLogger logger) : IParseTreeListener
    {
        int Indent = 0;

        protected void Output(string s, int indent)
        {
            logger.Write($"{new string(' ', indent)}{s}\n");
        }

        protected void IndentOutput(string s)
        {
            Output(s, Indent);
            Indent += 2;
        }
        protected void UnindentOutput(string s)
        {
            Indent -= 2;
            Output(s, Indent);
        }

        public void EnterEveryRule([NotNull] ParserRuleContext ctx)
        {
            IndentOutput($"{{ {ctx.GetType().Name}: @{ctx.Start?.Line}:{ctx.Start?.Column}");
        }

        public void ExitEveryRule([NotNull] ParserRuleContext ctx)
        {
            var text = ctx.GetText();
            if (text != null)
            {
                text = text.Replace("\r", "").Replace("\n", "$");
                if (text.Length > 60)
                    text = text.Substring(0, 60);
            }
            UnindentOutput($"}} {ctx.GetType().Name} @{ctx.Stop?.Line}:{ctx.Stop?.Column} = |{text}");
        }

        public void VisitErrorNode([NotNull] IErrorNode node)
        {
            // Method intentionally left empty.
        }

        public void VisitTerminal([NotNull] ITerminalNode node)
        {
            // Method intentionally left empty.
        }
    }
}
