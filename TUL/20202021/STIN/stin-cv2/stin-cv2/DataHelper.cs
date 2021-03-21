using System;

namespace stin_cv2
{
    public static class DataHelper
    {
        public static int IndexOf<T>(this T[] collums, T collumName) where T : IComparable
        {
            for (int i = 0; i < collums.Length; i++)
            {
                if (collums[i].Equals(collumName))
                {
                    return i;
                }
            }
            return -1;
        }

        public static Item ParseItem(string text, string[] colums, int tax)
        {
            var data = text.Split(';');
            var width = int.Parse(data[colums.IndexOf(CategoryName.Width)]);
            var weight = int.Parse(data[colums.IndexOf(CategoryName.Weight)]);
            var height = int.Parse(data[colums.IndexOf(CategoryName.Height)]);
            var deep = int.Parse(data[colums.IndexOf(CategoryName.Deep)]);
            var type = data[colums.IndexOf(CategoryName.Type)];
            var produceBy = data[colums.IndexOf(CategoryName.ProduceBy)];
            var price = int.Parse(data[colums.IndexOf(CategoryName.Price)]);
            return new Item(width, height, weight, deep, price, produceBy, type, tax);
        }
    }
}
