using Antlr4.Runtime.Tree;

namespace MarlinConfig.Parser
{
    public static class RuleNodeExtensions
    {
        public static bool HasParentOf<T>(this IRuleNode? node) => node is T || (node != null && node.Parent.HasParentOf<T>());
    }
}
