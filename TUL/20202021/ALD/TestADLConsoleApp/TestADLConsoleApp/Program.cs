using System;
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
            }
            Console.WriteLine("preorder");
            tree.Preorder(tree.Root);
            Console.WriteLine("inorder");
            tree.Inorder(tree.Root);
            Console.WriteLine("postorder");
            tree.Postorder(tree.Root);
        }
    }

    public class Node
    {
        public int Value { get; private set; }
        public Node Left;
        public Node Right;

        public Node(int v)
        {
            Value = v;
        }

        private void Add(ref Node node, int v)
        {
            if(node == null)
            {
                node = new Node(v);
            }
            else
            {
                node.Add(v);
            }
        }

        public void Add(int v)
        {
            if (Value > v)
            {
                Add(ref Left, v);
            }
            else
            {
                Add(ref Right, v);
            }
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
            if (Root == null)
            {
                Root = new Node(v);
            }
            else
            {
                Root.Add(v);
            }
        }
        public void Preorder(Node n)
        {
            if (n == null) return;
            Console.WriteLine(n.Value);
            Preorder(n.Left);
            Preorder(n.Right);
        }

        public void Inorder(Node n)
        {
            if (n == null) return;
            Inorder(n.Left);
            Console.WriteLine(n.Value);
            Inorder(n.Right);
        }

        public void Postorder(Node n)
        {
            if (n == null) return;
            Postorder(n.Left);
            Postorder(n.Right);
            Console.WriteLine(n.Value);
        }
    }

}
