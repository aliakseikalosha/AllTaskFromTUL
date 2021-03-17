using System.IO;
using System.Text;

namespace stin_cv1
{
    partial class Program
    {
        public class MemoryWriter : BaseWriter
        {
            private MemoryStream stream;
            private Encoding encoding;
            private string endLineSymbols = null;
            public MemoryWriter(MemoryStream stream, Encoding encoding, string endLine)
            {
                this.stream = stream;
                this.encoding = encoding;
                endLineSymbols = endLine;

            }

            public override void Write(string data)
            {
                stream.Write(encoding.GetBytes(data));
            }

            public override void WriteLine(string data)
            {
                stream.Write(encoding.GetBytes(data + endLineSymbols));
            }
        }
    }
}
