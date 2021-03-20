using System;
using System.Collections.Generic;
using System.Linq;

namespace stin_cv2
{
    class Program
    {
        private static readonly string collumsData = "typ;vyrobce;cena;vaha;sirka;vyska;hloubka";
        private static readonly string data = "mycka;Samsung;9800;35;60;120;48\nlednice;Gorenje;12000;65;55;200;50\nmikrovlnna trouba;Elektorlux;2200;10;40;35;40\nsvetlo;Lumen;1250;2;30;15;10";

        private static Order CreateOrder(string name, Dictionary<int, int> itemsCounts)
        {
            var cols = collumsData.Split(';');
            var pr = data.Split('\n');

            var list = new List<Item>();
            foreach (var item in itemsCounts)
            {
                list.Add(DataHelper.ParseItem(pr[item.Key], cols, item.Value));
            }
            return new Order(name, list);
        }

        private static double CalculateTax(Item item)
        {
            double tax = item.Tax * 5;
            if (item.MaxDimention > 100)
            {
                if (item.Weight < 20)
                {
                    tax = item.Tax * 10;
                }
                else
                {
                    tax = item.Tax * item.Weight * 0.2;
                    if (item.Type == "lednice")
                    {
                        tax *= 2;
                    }
                }
            }
            return tax;
        }

        private static void PrintOrder(Order order)
        {
            Console.WriteLine($" {order.Name}");

            var totalTax = 0.0;
            for (int i = 0; i < order.Items.Count; i++)
            {
                var item = order.Items[i];
                double tax = CalculateTax(order.Items[i]);
                totalTax += tax;

                Console.WriteLine($" polozka: {item.Tax} x {item.Type} ({item.ProducedBy}) {tax}");
            }

            Console.WriteLine($" suma: {totalTax}\n");
        }

        private static void Main(string[] args)
        {
            var orders = new Order[] { CreateOrder("O001", new Dictionary<int, int> { { 0, 1 }, { 3, 2 } }), CreateOrder("O002", new Dictionary<int, int> { { 1, 2 }, { 2, 1 } }) };
            
            for (int i = 0; i < orders.Length; i++)
            {
                PrintOrder(orders[i]);
            }
        }
    }
}
