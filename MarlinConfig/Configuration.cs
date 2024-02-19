using System.Collections.Generic;

namespace MarlinConfig
{
    public record ConfigValue(string? Value, bool Enabled);

    public class Configuration
    {
        public Dictionary<string, ConfigValue> Values { get; set; } = [];
    }
}
