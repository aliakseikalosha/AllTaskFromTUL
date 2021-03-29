using System;
using System.Collections.Generic;

namespace CleanPRJ
{
    public class Ride
    {
        public List<TimedSampledData<float>> SpeedData { get; protected set; } = new List<TimedSampledData<float>>();
        public List<TimedSampledData<float>> BatteryData { get; protected set; } = new List<TimedSampledData<float>>();
    }

    public class TimedSampledData<T>
    {
        public T Data { get; protected set; }
        public DateTime DateUTC { get; protected set; }

        public TimedSampledData(T data, DateTime dateUTC)
        {
            Data = data;
            DateUTC = dateUTC;
        }
    }
}
