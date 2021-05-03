using Microcharts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanPRJ.src.Data
{
    public static class DataHelper
    {
        private static Random rnd = new Random();
        public static BatteryData MockupBateryData
        {
            get
            {
                var data = new BatteryData();
                int n = 15, m = 5;
                for (int i = 0; i < n; i++)
                {
                    data.ChargeLevel.Add(new TimedSampledData<float>(100 - i * 5, DateTime.UtcNow.AddHours(i - n - m)));
                }
                for (int i = 0; i < m; i++)
                {
                    data.ChargeLevel.Add(new TimedSampledData<float>(data.ChargeLevel[n - 1].Data + i * 10, DateTime.UtcNow.AddHours(i - m)));
                }
                return data;
            }
        }

        public static TravelData MockupTravelData
        {
            get
            {
                var data = new TravelData();
                int n = 15;
                for (int i = 0; i < n; i++)
                {
                    data.Distance.Add(new TimedSampledData<float>(20 * (float)rnd.NextDouble(), DateTime.UtcNow.AddDays(i - n)));
                }
                return data;
            }
        }

        public static List<ChartEntry> ConverToChartEntry<T>(this List<T> datas, Func<T, ChartEntry> convert)
        {
            return datas.Select(c => convert(c)).ToList();
        }

        public static T Random<T>(this List<T> data)
        {
            return data[rnd.Next(data.Count)];
        }
        public static T Random<T>(this T[] data)
        {
            return data[rnd.Next(data.Length)];
        }

        public static List<T> Last<T>(this List<T> data, int count)
        {
            count = Math.Min(count, data.Count - 1);
            return data.GetRange(data.Count - count, count);
        }
    }
}
