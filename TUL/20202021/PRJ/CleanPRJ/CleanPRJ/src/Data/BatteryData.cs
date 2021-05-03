using Microcharts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanPRJ.src.Data
{
    public class BatteryData
    {
        public List<TimedSampledData<float>> ChargeLevel { get; private set; } = new List<TimedSampledData<float>>();

        internal List<ChartEntry> Select(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
