using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAPW08
{
    public class Person
    {
        public string name;
        public string email;
        public List<Thing> things = new List<Thing>();
        public double TotalPrice => things.Count > 0 ? things.Select(c => c.price * c.count).Aggregate((a, b) => a + b) : 0;
        public override string ToString()
        {
            return $"{name}\n{email}";
        }

        internal void Add(Thing thing)
        {
            var t = things.FirstOrDefault(t => t.name == thing.name);
            if (t != null)
            {
                t.count++;
            }
            else
            {
                things.Add(thing);
            }
        }
        internal void Remove(Thing thing)
        {
            var t = things.FirstOrDefault(t => t.name == thing.name);
            if (t != null)
            {
                t.count--;
            }
        }
    }

    public class Thing
    {
        public string name;
        public double price;
        public int count;

        public override string ToString()
        {
            return $"{name} cost : {price}, count : {count}";
        }
    }
}
