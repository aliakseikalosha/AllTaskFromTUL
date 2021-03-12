using System.Collections.Generic;

namespace stin_cv1
{
    public class MultipleWriter : BaseWriter
    {
        private List<IWriter> writers = new List<IWriter>();

        public MultipleWriter(params IWriter[] writers)
        {
            foreach (var writer in writers)
            {
                this.writers.Add(writer);
            }
        }

        public override void Write(string data)
        {
            writers.ForEach(c => c.Write(data));
        }

        public override void WriteLine(string data)
        {
            writers.ForEach(c => c.WriteLine(data));
        }
    }
}
