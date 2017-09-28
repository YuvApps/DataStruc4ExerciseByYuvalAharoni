using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruc4ExerciseByYuvalAharoni
{
    public enum NodeStatus
    {
        LeftControl,
        Equaled,
        RightControl
    }
    public class AVLTreeCell
    {
        public int Key { get; set; }
        public NodeStatus Status { get; set; }
        public int BalanceFactor { get; set; }
        public AVLTreeCell Parent { get; set; }
        public AVLTreeCell LeftChild { get; set; }
        public AVLTreeCell RightChild { get; set; }

    }

    public class Queue
    {
        public LinkedList queue;


    }

    public class LinkedListCell
    {
        public int Key { get; set; }
        public LinkedListCell Next { get; set; }

        public LinkedListCell(int Key)
        {
            this.Key = Key;
        }
    }

    public class LinkedList
    {
        public LinkedListCell First { get; set; }
        public LinkedListCell List { get; set; }

        public LinkedListCell getFirst()
        {
            if (First != null)
            {
                return First;
            }
            return null;
        }

        public bool addCell(int Key)
        {
            if (List == null || List.Next == null)
            {
                if (First == null)
                {
                    First = new LinkedListCell(Key);
                    First.Next = First;
                }

                List.Next = new LinkedListCell(Key);
                List.Next.Next = First;
                return true;
            }
            else
            {
                List = List.Next;
                return addCell(Key);
            }
        }
    }

    public class AVLTree
    {
        AVLTreeCell Root;

        public AVLTreeCell getCellByKey(int Key)
        {
            if (Root == null)
            {
                return null;
            }

            return getCellByKey(Key, Root);
        }

        private AVLTreeCell getCellByKey(int Key, AVLTreeCell Node)
        {
            if (Key == Node.Key)
            {
                return Node;
            }
            else if (Key < Node.Key && Node.LeftChild != null)
            {
                return getCellByKey(Key, Node.LeftChild);
            }
            else if (Key > Node.Key && Node.RightChild != null)
            {
                return getCellByKey(Key, Node.RightChild);
            }
            else
            {
                return null;
            }
        }

        public bool insertCell(int Key)
        {
            if (Root == null)
            {
                Root = new AVLTreeCell();
                Root.Key = Key;
                Root.BalanceFactor = 0;
                return true;
            }

            Root.BalanceFactor += insertCell(Key, Root);



            return getCellByKey(Key) != null;
        }

        private int insertCell(int Key, AVLTreeCell Node)
        {
            if (Key <= Node.Key)
            {
                if (Node.LeftChild == null)
                {
                    Node.LeftChild = new AVLTreeCell();
                    Node.LeftChild.Key = Key;
                    Node.LeftChild.Parent = Node;
                    Node.LeftChild.BalanceFactor = 0;
                    Node.BalanceFactor = 1;
                }
                else
                {
                    int nBalace = insertCell(Key, Node.LeftChild);
                    Node.BalanceFactor += nBalace;
                }
                return 1;
            }
            else
            {
                if (Node.RightChild == null)
                {
                    Node.RightChild = new AVLTreeCell();
                    Node.RightChild.Key = Key;
                    Node.RightChild.Parent = Node;
                    Node.RightChild.BalanceFactor = 0;
                    Node.BalanceFactor = -1;
                }
                else
                {
                    int nBalace = insertCell(Key, Node.RightChild);
                    Node.BalanceFactor += nBalace;
                }
                return -1;
            }
        }

        public bool deleteCell(int keyToDelete)
        {
            return getCellByKey(keyToDelete) == null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            AVLTree tree = new AVLTree();

            for (int counter = 0; counter < 25; counter++)
            {
                tree.insertCell(rnd.Next(101));
            }
        }
    }
}

