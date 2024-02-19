using System.IO;

namespace MarlinConfig
{
    public static class ConfigurationReaderExtensions
    {
        private static MemoryStream StreamFrom(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }

        public static Configuration Read(this ConfigurationReader reader, string config)
        {
            using var stream = StreamFrom(config);
            return reader.Read(stream);
        }
    }
}