namespace stin_cv1
{
    public abstract class BaseWriter : IWriter
    {
        public virtual void Write(float data)
        {
            Write(data.ToString());
        }
        public virtual void WriteLine(float data)
        {
            WriteLine(data.ToString());
        }

        public abstract void Write(string data);

        public abstract void WriteLine(string data);
    }
}
