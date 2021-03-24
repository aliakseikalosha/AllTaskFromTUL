using System;

namespace stin_cv2
{
    public static class DataHelper
    {
        /// <summary>
        /// Return -1 if element not found in array or index of this element
        /// </summary>
        /// <typeparam name="T">type of elements in array</typeparam>
        /// <param name="array">array of elements</param>
        /// <param name="element">to search for</param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] array, T element) where T : IComparable
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(element))
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Convert raw data of Item
        /// </summary>
        /// <param name="text">string with Item description</param>
        /// <param name="colums">array of collum names in same order they are in text</param>
        /// <param name="tax">Ammount of tax for this Item</param>
        /// <returns></returns>
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
