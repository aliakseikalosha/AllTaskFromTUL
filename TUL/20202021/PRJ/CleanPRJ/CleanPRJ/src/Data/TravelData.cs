using System;
using System.Collections.Generic;

namespace CleanPRJ.src.Data
{
    [Serializable]
    public class TravelData
    {
        public List<TimedSampledData<float>> Distance { get; private set; } = new List<TimedSampledData<float>>();
    }
}
