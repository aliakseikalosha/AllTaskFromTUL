using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestADLConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 1; j < 6; j++)
                {
                    Console.WriteLine(Path.Combine("Osoba3", $"c{i}_p0000_s0{j}.wav"));
                }
            }
        }
    }
}