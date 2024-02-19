using Antlr4.Runtime.Misc;
using MarlinConfig.Parser;

namespace MarlinConfig
{
    public class ConfigVisitor(Configuration configuration) : MarlinConfigParserBaseVisitor<object>
    {
        public override object VisitDirective_define([NotNull] MarlinConfigParser.Directive_defineContext context)
        {
            var symbol = context.Symbol.Trim();
            var value = new ConfigValue(context.Value?.Trim(), !context.HasParentOf<IComment>());

            if (configuration.Values.ContainsKey(symbol))
            {
                // todo: warning redefine
                configuration.Values[context.Symbol] = value;
            }
            else
                configuration.Values.Add(context.Symbol, value);

            return base.VisitDirective_define(context);
        }
    }

}
