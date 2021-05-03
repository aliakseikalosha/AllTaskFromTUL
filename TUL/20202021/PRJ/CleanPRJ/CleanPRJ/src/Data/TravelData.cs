using System;
using System.Collections.Generic;
using System.Text;

namespace CleanPRJ.src.Data
{
    public class TravelData
    {
        public List<TimedSampledData<float>> Distance { get; private set; } = new List<TimedSampledData<float>>();
    }
}
