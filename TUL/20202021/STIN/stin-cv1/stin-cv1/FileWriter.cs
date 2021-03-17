using System.IO;

namespace stin_cv1
{
    public class FileWriter : BaseWriter
    {
        private string pathToFile = null;
        private string endLineSymbols = null;

        public FileWriter(string path, string endLine)
        {
            pathToFile = path;
            endLineSymbols = endLine;

        }
        public override void Write(string data)
        {
            using (var writer = File.AppendText(pathToFile))
            {
                writer.Write(data);
            }
        }

        public override void WriteLine(string data)
        {
            Write(data + endLineSymbols);
        }
    }
}
