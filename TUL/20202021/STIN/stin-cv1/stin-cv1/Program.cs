using System.IO;
using System.Linq;
using System.Text;

namespace stin_cv1
{
    partial class Program
    {
        static void Main(string[] args)
        {
            IWriter w = new MultipleWriter(new ConsoleWriter(), new FileWriter("test.txt", "\r\n"), new MemoryWriter(new MemoryStream(1024), UnicodeEncoding.Unicode, "\r\n"));
            w.WriteLine("new line");
            w.Write("string");
            w.Write(15.9f);
        }
    }
}
