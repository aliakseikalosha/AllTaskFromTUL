using System;

namespace stin_cv1
{
    public class ConsoleWriter : BaseWriter
    {
        public override void Write(string data)
        {
            Console.Write(data);
        }

        public override void WriteLine(string data)
        {
            Console.WriteLine(data);
        }
    }
}
