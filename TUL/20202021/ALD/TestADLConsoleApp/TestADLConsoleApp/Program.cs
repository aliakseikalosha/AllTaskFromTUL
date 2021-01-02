using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace TestADLConsoleApp
{
    class Program
    {
        class Reader
        {
            private static Thread inputThread;
            private static AutoResetEvent getInput, gotInput;
            private static string input;

            static Reader()
            {
                getInput = new AutoResetEvent(false);
                gotInput = new AutoResetEvent(false);
                inputThread = new Thread(reader);
                inputThread.IsBackground = true;
                inputThread.Start();
            }

            private static void reader()
            {
                while (true)
                {
                    getInput.WaitOne();
                    input = Console.ReadLine();
                    gotInput.Set();
                }
            }

            // omit the parameter to read a line without a timeout
            public static string ReadLine(int timeOutMillisecs = Timeout.Infinite)
            {
                getInput.Set();
                bool success = gotInput.WaitOne(timeOutMillisecs);
                if (success)
                {
                    return input;
                }
                else
                {
                    return null;
                   // throw new TimeoutException("User did not provide input within the timelimit.");
                }
            }
        }
        static void Main(string[] args)
        {
            var text = Console.ReadLine();
            for (int i = 0; i < 20; i++)
            {
                text += Reader.ReadLine(50);
            }
            
            var sentences = text.Trim().Split('.').Where(c => c.Length > 1).Select(c => c.Trim() + '.').ToList();

            Print("Uppercases", FindMatch(text, new Regex(@"\b[A-Z]\w*")));
            Console.WriteLine();
            Print("I love", FindMatch(text, new Regex(@"\b[i]\s\b[love]\w+", RegexOptions.IgnoreCase)));
            Console.WriteLine();
            Print("Sentences", FindMatch(text, new Regex(@"\b((?!=|\.).)+(.)\b."))); ;
        }

        private static List<string> FindMatch(string text, Regex regex)
        {
            return regex.Matches(text).Select(c => c.Value.Trim()).ToList();
        }

        private static void Print(string name, List<string> data)
        {
            Console.WriteLine($"{name} {data.Count}x:");
            for (int i = 0; i < data.Count; i++)
            {
                Console.WriteLine($"  {i + 1}) \'{data[i]}\'");
            }
        }
    }
}
/*
  	Powder cupcake I love. Souffle sesame snaps cupcake chocolate cake lemon drops tootsie roll. Chocolate cake jelly-o jelly beans chocolate bar bear claw I love lollipop lemon drops dragee. Dessert jelly-o tart cake marzipan.
	
	Chocolate bar brownie pudding tiramisu. Cookie lemon drops chocolate bar pie I love jelly beans gummies cotton candy gummi bears. Lollipop candy canes candy canes I love topping liquorice sugar plum tart.
	
	Candy canes I love wafer chocolate cake topping dessert Bear claw toffee. Caramels muffin souffle sugar plum I love. Marshmallow muffin muffin candy canes candy donut bear claw.
	
	Candy canes pudding chocolate bar cupcake. Bear claw cheesecake chocolate cake bear claw gingerbread. Oat cake halvah pie pie. Chupa chups macaroon cotton candy icing sweet roll I love.
	
	Jelly chocolate bar dessert. I LOVE bear claw Jelly jelly-o icing. Chocolate bar wafer pudding macaroon tart Jelly-o lemon drops brownie. Jelly beans cookie cookie.
 */