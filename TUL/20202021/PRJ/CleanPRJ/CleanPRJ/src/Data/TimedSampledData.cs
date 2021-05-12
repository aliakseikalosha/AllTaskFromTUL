using System;

namespace CleanPRJ
{
    [Serializable]
    public class TimedSampledData<T>
    {
        public T Data;
        public DateTime DateUTC;

        public TimedSampledData(T data, DateTime dateUTC)
        {
            Data = data;
            DateUTC = dateUTC;
        }
    }
}
