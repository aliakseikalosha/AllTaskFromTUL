using System;
using System.Collections.Generic;

namespace DataGrabber.src.Data
{
    [Serializable]
    public class BatteryData
    {
        public List<TimedSampledData<float>> ChargeLevel { get; private set; } = new List<TimedSampledData<float>>();
    }
}
