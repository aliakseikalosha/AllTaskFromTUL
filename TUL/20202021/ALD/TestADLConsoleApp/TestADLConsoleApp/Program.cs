using System;

namespace TestADLConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            string s = "";
            for (int i = 1; i <= n; i++)
            {
                s += $"{i}:";
                for (int j = 1; j <= i; j++)
                {
                    if (i % j == 0)
                    {
                        s += $" {j},";
                    }
                }
                s = s.Substring(0, s.Length - 1) + "\n";
            }
            Console.WriteLine(s);
        }
    }
}
//class p{static void Main(){var b=System.Numerics.BigInteger.Pow(100,1000)*2;var e=b/2;for(int i=3333;i-->0;e=(e+b/e)/2);System.Console.Write((""+e).Insert(1,"."));}}
