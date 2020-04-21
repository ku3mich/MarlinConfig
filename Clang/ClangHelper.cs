using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clang
{
    public static class ClangHelper
    {
        public static CommonTokenStream CreateTokenStream(ICharStream stream) => new CommonTokenStream(new ClangLexer(stream));

    }
}
