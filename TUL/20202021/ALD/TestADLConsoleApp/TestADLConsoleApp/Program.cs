using System;
using System.Collections.Generic;
using System.Linq;

namespace TestADLConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split(" ").Select(c => int.Parse(c)).ToList();
            var tree = new Tree();
            for (int i = 0; i < numbers.Count; i++)
            {
                tree.Add(numbers[i]);
                // Console.WriteLine($"============[{numbers[i]}]==========");
                if (numbers[i] == -1)
                {
                    break;
                }
            }

            Console.WriteLine("preorder".ToUpper());
            tree.Preorder(tree.Root);
            Console.WriteLine();
            Console.WriteLine("inorder".ToUpper());
            tree.Inorder(tree.Root);
            Console.WriteLine();
            Console.WriteLine("postorder".ToUpper());
            tree.Postorder(tree.Root);
            Console.WriteLine();
        }
    }

    public class Node : TreePrinter.IPrintableNode<Node>
    {
        public int Value { get; private set; }


        public string Text => Value.ToString();

        TreePrinter.IPrintableNode<Node> TreePrinter.IPrintableNode<Node>.Left => Left;

        TreePrinter.IPrintableNode<Node> TreePrinter.IPrintableNode<Node>.Right => Right;

        public Node Left;
        public Node Right;

        public Node(int v)
        {
            Value = v;
        }

        private Node Add(ref Node node, int v)
        {
            Node n;
            if (node == null)
            {
                node = new Node(v);
                n = node;
            }
            else
            {
                n = node.Add(v);
            }
            return n;
        }

        public Node Add(int v)
        {
            return Value >= v ? Add(ref Left, v) : Add(ref Right, v);
        }


        public override string ToString()
        {
            return $"Node : {Value} <Left {Left?.ToString()}> <Right{Right?.ToString()}>";
        }
    }

    public class Tree
    {
        public Node Root { get; private set; }

        public void Add(int v)
        {
            Root = Add(Root, v);
        }

        private Node Add(Node c, int v)
        {
            if (c == null)
            {
                c = new Node(v);
            }
            else if (c.Value >= v)
            {
                c.Left = Add(c.Left, v);
            }
            else
            {
                c.Right = Add(c.Right, v);
            }

            int balance = BalanceOf(c);
            if (balance >= 2)
            {
                if (v < c.Left.Value)
                {
                    return RotateR(c);
                }
                if (v > c.Left.Value)
                {
                    c.Left = RotateL(c.Left);
                    return RotateR(c);
                }
            }
            else if (balance <= -2)
            {
                if (v > c.Right.Value)
                {
                    return RotateL(c);
                }
                if (v < c.Right.Value)
                {
                    c.Right = RotateR(c.Right);
                    return RotateL(c);
                }
            }
            //TreePrinter.Print(Root);
            return c;
        }

        private Node RotateR(Node B)
        {
            //Node pivot = B.Right;
            //B.Right = pivot.Left;
            //pivot.Left = B;
            //return pivot;

            var A = B.Left;
            var y = A.Right;

            A.Right = B;
            B.Left = y;

            return A;
        }
        private Node RotateL(Node A)
        {
            //Node pivot = A.Left;
            //A.Left = pivot.Right;
            //pivot.Right = A;
            //return pivot;

            var B = A.Right;
            var y = B.Left;

            B.Left = A;
            A.Right = y;

            return B;
        }

        private int HeightOf(Node n)
        {
            int height;
            if (n == null)
            {
                height = 0;
            }
            else
            {
                int l = HeightOf(n.Left);
                int r = HeightOf(n.Right);
                height = Math.Max(l, r) + 1;
            }
            return height;
        }
        private int BalanceOf(Node n)
        {
            int l = HeightOf(n.Left);
            int r = HeightOf(n.Right);
            int bal = l - r;
            return bal;
        }

        public void Preorder(Node n)
        {
            if (n == null) return;
            Console.Write($"{n.Value},");
            Preorder(n.Left);
            Preorder(n.Right);
        }

        public void Inorder(Node n)
        {
            if (n == null) return;
            Inorder(n.Left);
            Console.Write($"{n.Value},");
            Inorder(n.Right);
        }

        public void Postorder(Node n)
        {
            if (n == null) return;
            Postorder(n.Left);
            Postorder(n.Right);
            Console.Write($"{n.Value},");
        }
    }

    public class TreePrinter
    {
        /** Node that can be printed */
        public interface IPrintableNode<T>
        {
            /** Get left child */
            IPrintableNode<T> Left { get; }


            /** Get right child */
            IPrintableNode<T> Right { get; }


            /** Get text to be printed */
            string Text { get; }
        }


        /**
         * Print a tree
         * 
         * @param root
         *            tree root node
         */
        public static void Print<T>(IPrintableNode<T> root)
        {
            List<List<string>> lines = new List<List<string>>();

            List<IPrintableNode<T>> level = new List<IPrintableNode<T>>();
            List<IPrintableNode<T>> next = new List<IPrintableNode<T>>();

            level.Add(root);
            int nn = 1;

            int widest = 0;

            while (nn != 0)
            {
                List<string> line = new List<string>();

                nn = 0;

                foreach (IPrintableNode<T> n in level)
                {
                    if (n == null)
                    {
                        line.Add(null);

                        next.Add(null);
                        next.Add(null);
                    }
                    else
                    {
                        string aa = n.Text;
                        line.Add(aa);
                        if (aa.Length > widest) widest = aa.Length;

                        next.Add(n.Left);
                        next.Add(n.Right);

                        if (n.Left != null) nn++;
                        if (n.Right != null) nn++;
                    }
                }

                if (widest % 2 == 1) widest++;

                lines.Add(line);

                List<IPrintableNode<T>> tmp = level;
                level = next;
                next = tmp;
                next.Clear();
            }

            int perpiece = lines[lines.Count - 1].Count * (widest + 4);
            for (int i = 0; i < lines.Count; i++)
            {
                List<string> line = lines[i];
                int hpw = (int)Math.Floor(perpiece / 2f) - 1;

                if (i > 0)
                {
                    for (int j = 0; j < line.Count; j++)
                    {

                        // split node
                        char c = ' ';
                        if (j % 2 == 1)
                        {
                            if (line[j - 1] != null)
                            {
                                c = (line[j] != null) ? '┴' : '┘';
                            }
                            else
                            {
                                if (j < line.Count && line[j] != null) c = '└';
                            }
                        }
                        Console.Write(c);

                        // lines and spaces
                        if (line[j] == null)
                        {
                            for (int k = 0; k < perpiece - 1; k++)
                            {
                                Console.Write(" ");
                            }
                        }
                        else
                        {

                            for (int k = 0; k < hpw; k++)
                            {
                                Console.Write(j % 2 == 0 ? " " : "─");
                            }
                            Console.Write(j % 2 == 0 ? "┌" : "┐");
                            for (int k = 0; k < hpw; k++)
                            {
                                Console.Write(j % 2 == 0 ? "─" : " ");
                            }
                        }
                    }
                    Console.WriteLine();
                }

                // print line of numbers
                for (int j = 0; j < line.Count; j++)
                {

                    string f = line[j];
                    if (f == null) f = "";
                    int gap1 = (int)Math.Ceiling(perpiece / 2f - f.Length / 2f);
                    int gap2 = (int)Math.Floor(perpiece / 2f - f.Length / 2f);

                    // a number
                    for (int k = 0; k < gap1; k++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(f);
                    for (int k = 0; k < gap2; k++)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();

                perpiece /= 2;
            }
        }
    }
}