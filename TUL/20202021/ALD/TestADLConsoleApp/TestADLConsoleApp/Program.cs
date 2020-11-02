using System;
using System.Linq;
using System.Text;

namespace TestADLConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var grid = new Grid(ReadPos());
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                grid.PlaceMine(ReadPos());
            }
            n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                grid.PlaceClick(ReadPos());
            }
            Console.WriteLine(grid);
            grid.ApplyClicks();
            Console.WriteLine(grid.ToString(true));
        }

        public static Vec2Int ReadPos()
        {
            var numbers = Console.ReadLine().Split(" ").Select(c => int.Parse(c)).ToArray();
            return new Vec2Int(numbers[0], numbers[1]);
        }
    }

    public class Vec2Int
    {
        public int x;
        public int y;

        public Vec2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vec2Int operator -(Vec2Int a) => new Vec2Int(-a.x, -a.y);
        public static Vec2Int operator +(Vec2Int a, Vec2Int b) => new Vec2Int(a.x + b.x, a.y + b.y);
        public static Vec2Int operator -(Vec2Int a, Vec2Int b) => a + (-b);
    }

    public class Grid
    {
        private static readonly char mineSymbol = '*';
        private static readonly char closeSymbol = '?';
        private static readonly char openSymbol = '.';
        private static readonly char clickSymbol = 'o';

        private char[,] map;
        private Vec2Int size;
        public char this[Vec2Int index]
        {
            get
            {
                if (IsInside(index))
                {
                    return map[index.x, index.y];
                }
                return '\0';
            }
            private set
            {
                if (IsInside(index))
                {
                    map[index.x, index.y] = value;
                }
            }
        }
        public bool IsInside(Vec2Int index) => index.x >= 0 && index.x < size.x && index.y >= 0 && index.y < size.y;
        public Grid(Vec2Int size)
        {
            this.size = size;
            map = new char[size.x, size.y];
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    map[i, j] = closeSymbol;
                }
            }
        }

        public void PlaceMine(Vec2Int pos)
        {
            this[pos] = mineSymbol;
        }

        public void PlaceClick(Vec2Int pos)
        {
            this[pos] = clickSymbol;
        }

        public void ApplyClicks()
        {
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    var pos = new Vec2Int(i, j);
                    if (this[pos] == clickSymbol)
                    {
                        Open(pos);
                    }
                }
            }
        }

        private void Open(Vec2Int pos)
        {
            if (IsInside(pos))
            {
                if (this[pos] == openSymbol || this[pos] == mineSymbol)
                {
                    return;
                }
                if (HasMineNeighbor(pos))
                {
                    this[pos] = closeSymbol;
                    return;
                }
                this[pos] = openSymbol;
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        Open(pos + new Vec2Int(i, j));
                    }
                }
            }
        }

        public bool IsMine(Vec2Int pos)
        {
            return this[pos] == mineSymbol;
        }

        public bool HasMineNeighbor(Vec2Int pos)
        {
            if (IsInside(pos))
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (IsMine(pos + new Vec2Int(i, j)))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    sb.Append(map[i, j] + " ");
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }

        public string ToString(bool hiden)
        {
            return hiden ? ToString().Replace(mineSymbol, closeSymbol) : ToString();
        }
    }
}
