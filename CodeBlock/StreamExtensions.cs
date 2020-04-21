using System.IO;
using System.Text;

namespace CodeBlock
{
    public static class StreamExtensions 
    {
        public static char[] GetChars(this Stream s, Encoding encoding)
        {
            var text = new byte[s.Length];
            using (var t = new MemoryStream(text))
            {
                s.CopyTo(t);
            }
            return encoding.GetChars(text);
        }

    }
}
