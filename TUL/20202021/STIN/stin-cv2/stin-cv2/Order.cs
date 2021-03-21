using System.Collections.Generic;

namespace stin_cv2
{
    public class Order
    {
        public string Name { get; private set; }
        public List<Item> Items { get; private set; }

        public Order(string name, List<Item> items)
        {
            Items = items;
            Name = name;
        }
    }
}
