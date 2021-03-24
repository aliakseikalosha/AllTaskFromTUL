using System;

namespace stin_cv2
{
    public class Item
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Weight { get; private set; }
        public int Deep { get; private set; }
        public int Price { get; private set; }
        public string ProducedBy { get; private set; }
        public string Type { get; private set; }

        public int Tax { get; private set; }
        /// <summary>
        /// Biggest dimentions of an Item
        /// </summary>
        public int MaxDimention => Math.Max(Math.Max(Width, Height), Deep);

        public Item(int width, int height, int weight, int deep, int price, string producedBy, string type, int tax)
        {
            Width = width;
            Height = height;
            Weight = weight;
            Deep = deep;
            Price = price;
            ProducedBy = producedBy;
            Type = type;
            Tax = tax;
        }
    }
}
