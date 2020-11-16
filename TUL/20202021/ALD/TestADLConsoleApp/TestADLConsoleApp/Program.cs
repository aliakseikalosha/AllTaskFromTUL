using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestADLConsoleApp
{
    public class Program
    {
        private const string end = "---";

        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<Student> students = new List<Student>();
            for (int i = 0; i < n; i++)
            {
                students.Add(ReadHuman());
            }
            string sort;
            while ((sort = Console.ReadLine()) != end)
            {
                PrintSortedBy(students, sort);
            }
        }

        public static Student ReadHuman()
        {
            var data = Console.ReadLine().Split(" ");
            return new Student(data[0], data[1], data[2], data[3], int.Parse(data[4]), data[5]);
        }

        public static void PrintSortedBy(List<Student> students, string order)
        {
            var sb = new StringBuilder();
            sb.Append($"Trideni dle {order.ToUpper().Replace('-', '_')}\n");
            var sortKeys = students.Select(c => c.GetValueOf(order)).Distinct().ToList();
            for (int i = 0; i < sortKeys.Count; i++)
            {
                sb.Append($"-skupina {sortKeys[i]}:\n");
                var list = students.Where(c => c.GetValueOf(order) == sortKeys[i]).ToList();
                for (int j = 0; j < list.Count; j++)
                {
                    sb.Append($"-- {list[j]}\n");
                }
            }
            Console.Write(sb.ToString());
        }
    }

    public class Student
    {
        private const string genderKey = "pohlavi";
        private const string groupKey = "skupina";
        private const string yearKey = "rok-studia";
        private const string languageKey = "programovaci-jazyk";
        public string firstName { get; private set; }
        public string lastName { get; private set; }
        public char gender { get; private set; }
        public int year { get; private set; }
        public string group { get; private set; }
        public string language { get; private set; }

        public Student(string firstName, string lastName, string group, string gender, int year, string language)
        {
            this.firstName = firstName;
            this.lastName = lastName.ToUpper();
            this.gender = gender.ToUpper()[0];
            this.year = year;
            this.group = group;
            this.language = language;
        }

        public string GetValueOf(string order)
        {
            switch (order)
            {
                case genderKey:
                    return gender.ToString();
                case groupKey:
                    return group;
                case yearKey:
                    return year.ToString();
                case languageKey:
                    return language;
            }
            return string.Empty;
        }
        public override string ToString()
        {
            return $"{lastName} {firstName} ({gender}, {year}. @ {group[0]}{group[1]}): {language}";
        }
    }
}
