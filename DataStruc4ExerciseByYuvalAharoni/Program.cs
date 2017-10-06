using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruc4ExerciseByYuvalAharoni
{
    public class AVLTreeCell
    {
        public int Key { get; set; }
        public int Height { get; set; }
        public int BalanceFactor { get; set; }
        public AVLTreeCell Parent { get; set; }
        public AVLTreeCell LeftChild { get; set; }
        public int LeftMaxHeight { get; set; }
        public AVLTreeCell RightChild { get; set; }
        public int RightMaxHeight { get; set; }

        public AVLTreeCell() { }

        public AVLTreeCell(AVLTreeCell Cell)
        {
            this.Key = Cell.Key;
            this.Height = Cell.Height;
            this.BalanceFactor = Cell.BalanceFactor;
            this.Parent = Cell.Parent;
            this.LeftChild = Cell.LeftChild;
            this.LeftMaxHeight = Cell.LeftMaxHeight;
            this.RightChild = Cell.RightChild;
            this.RightMaxHeight = Cell.RightMaxHeight;
        }

        public override string ToString()
        {
            return "( " + this.Key + " , " + (this.Parent != null ? this.Parent.Key.ToString() : "-1") + " , " +
                (this.LeftChild != null ? this.LeftChild.Key.ToString() : "-1") + " , " + 
                (this.RightChild != null ? this.RightChild.Key.ToString() : "-1") + " )";
            
        }
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
        public AVLTreeCell Root;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public AVLTreeCell getCellByKey(int Key)
        {
            if (Root == null)
            {
                return null;
            }

            return getCellByKey(Key, Root);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Node"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public bool insertCell(int Key)
        {
            if (Root == null)
            {
                Root = new AVLTreeCell();
                Root.Key = Key;
                Root.BalanceFactor = 0;
                return true;
            }

            int nBalance = insertCell(Key, Root);

            if (Math.Abs(nBalance) > 1)
            {
                doRotationToRoot();
            }

            return getCellByKey(Key) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Node"></param>
        /// <returns></returns>
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

                    Node.BalanceFactor += 1;

                    Node.LeftMaxHeight = 1;

                    if (Node.RightChild == null)
                    {
                        return 1;
                    }
                    
                    return 0;
                    
                }

                int nBalance = insertCell(Key, Node.LeftChild);

                if (Math.Abs(nBalance) > 1)
                {
                    doRotation(Node);

                }
                else if (nBalance != 0)
                {
                    Node.LeftMaxHeight += 1;
                }

                Node.BalanceFactor = Node.LeftMaxHeight - Node.RightMaxHeight;

                if (Math.Abs(Node.BalanceFactor) > 1)
                {
                    return Node.BalanceFactor;
                }

                return nBalance;
            }
            else
            {
                if (Node.RightChild == null)
                {
                    Node.RightChild = new AVLTreeCell();
                    Node.RightChild.Key = Key;
                    Node.RightChild.Parent = Node;
                    Node.RightChild.BalanceFactor = 0;

                    Node.BalanceFactor += -1;

                    Node.RightMaxHeight = 1;

                    if (Node.LeftChild == null)
                    {
                        return -1;
                    }

                    return 0;

                }

                int nBalance = insertCell(Key, Node.RightChild);

                if (Math.Abs(nBalance) > 1)
                {
                    doRotation(Node);

                }
                else if (nBalance != 0)
                {
                    Node.RightMaxHeight += 1;
                }

                Node.BalanceFactor = Node.LeftMaxHeight - Node.RightMaxHeight;

                if (Math.Abs(Node.BalanceFactor) > 1)
                {
                    return Node.BalanceFactor;
                }

                return nBalance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Node"></param>
        private void doRotation(AVLTreeCell Node)
        {
            if (Node.LeftChild != null && Math.Abs(Node.LeftChild.BalanceFactor) > 1)
            {
                if (Node.LeftChild.BalanceFactor > 0)
                {

                    if (Node.LeftChild.LeftChild.BalanceFactor < 0)
                    {
                        AVLTreeCell atcTempNode = Node.LeftChild.LeftChild.RightChild;

                        Node.LeftChild.LeftChild.RightChild = Node.LeftChild.LeftChild.RightChild.LeftChild;

                        atcTempNode.LeftChild = Node.LeftChild.LeftChild;

                        Node.LeftChild.LeftChild = atcTempNode;
                    }

                    AVLTreeCell atcFather = Node.LeftChild.LeftChild;

                    Node.LeftChild.LeftChild = atcFather.RightChild;

                    atcFather.RightChild = Node.LeftChild;

                    Node.LeftChild = atcFather;
                }
                else
                {

                    if (Node.LeftChild.RightChild.BalanceFactor > 0)
                    {
                        AVLTreeCell atcTempNode = Node.LeftChild.RightChild.LeftChild;

                        Node.LeftChild.RightChild.LeftChild = Node.LeftChild.RightChild.LeftChild.RightChild;

                        atcTempNode.RightChild = Node.LeftChild.RightChild;

                        Node.LeftChild.RightChild = atcTempNode;
                    }

                    AVLTreeCell atcFather = Node.LeftChild.RightChild;

                    Node.LeftChild.RightChild = atcFather.LeftChild;

                    atcFather.LeftChild = Node.LeftChild;

                    Node.LeftChild = atcFather;
                }

            }
            else
            {

                if (Node.RightChild.BalanceFactor > 0)
                {

                    if (Node.RightChild.LeftChild.BalanceFactor < 0)
                    {
                        AVLTreeCell atcTempNode = Node.RightChild.LeftChild.RightChild;

                        Node.RightChild.LeftChild.RightChild = Node.RightChild.LeftChild.RightChild.LeftChild;

                        atcTempNode.LeftChild = Node.RightChild.LeftChild;

                        Node.RightChild.LeftChild = atcTempNode;
                    }

                    AVLTreeCell atcFather = Node.RightChild.LeftChild;

                    Node.RightChild.LeftChild = atcFather.RightChild;

                    atcFather.RightChild = Node.RightChild;

                    Node.RightChild = atcFather;
                }
                else
                {

                    if (Node.RightChild.RightChild.BalanceFactor > 0)
                    {
                        AVLTreeCell atcTempNode = Node.RightChild.RightChild.LeftChild;

                        Node.RightChild.RightChild.LeftChild = Node.RightChild.RightChild.LeftChild.RightChild;

                        atcTempNode.RightChild = Node.RightChild.RightChild;

                        Node.RightChild.RightChild = atcTempNode;
                    }

                    AVLTreeCell atcFather = Node.RightChild.RightChild;

                    Node.RightChild.RightChild = atcFather.LeftChild;

                    atcFather.LeftChild = Node.RightChild;

                    Node.RightChild = atcFather;
                }

            }
            fixBalanceFactor(Root, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        private void doRotationToRoot()
        {
            if (Root.BalanceFactor > 0)
            {

                if (Root.LeftChild.BalanceFactor < 0)
                {
                    AVLTreeCell atcTempNode = Root.LeftChild.RightChild;

                    Root.LeftChild.RightChild = Root.LeftChild.RightChild.LeftChild;

                    atcTempNode.LeftChild = Root.LeftChild;

                    Root.LeftChild = atcTempNode;
                }

                AVLTreeCell atcFather = Root.LeftChild;

                Root.LeftChild = atcFather.RightChild;

                atcFather.RightChild = Root;

                Root = atcFather;
            }
            else
            {

                if (Root.RightChild.BalanceFactor > 0)
                {
                    AVLTreeCell atcTempNode = Root.RightChild.LeftChild;

                    Root.RightChild.LeftChild = Root.RightChild.LeftChild.RightChild;

                    atcTempNode.RightChild = Root.RightChild;

                    Root.RightChild = atcTempNode;
                }

                AVLTreeCell atcFather = Root.RightChild;

                Root.RightChild = atcFather.LeftChild;

                atcFather.LeftChild = Root;

                Root = atcFather;
            }

            fixBalanceFactor(Root, 0);
            Root.Parent = null;
        }

        public int fixBalanceFactor(AVLTreeCell Node, int Level)
        {
            if (Node.LeftChild == null)
            {
                Node.LeftMaxHeight = 0;
            }
            else
            {
                Node.LeftChild.Parent = Node;

                int nBalance = fixBalanceFactor(Node.LeftChild, Level + 1);

                Node.LeftMaxHeight = nBalance;
            }

            if (Node.RightChild == null)
            {
                Node.RightMaxHeight = 0;
            }
            else
            {
                Node.RightChild.Parent = Node;

                int nBalance = fixBalanceFactor(Node.RightChild, Level + 1);

                Node.RightMaxHeight = nBalance;
            }

            Node.BalanceFactor = Node.LeftMaxHeight - Node.RightMaxHeight;

            Node.Height = Level;

            return Math.Max(Node.LeftMaxHeight, Node.RightMaxHeight) + 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyToDelete"></param>
        /// <returns></returns>
        public bool deleteCell(int keyToDelete)
        {
            return getCellByKey(keyToDelete) == null;
        }

        public void printTree()
        {
            List<AVLTreeCell>[] totalTree = new List<AVLTreeCell>[7];

            for (int initIndex = 0; initIndex < 7; initIndex++)
            {
                totalTree[initIndex] = new List<AVLTreeCell>();
            }

            totalTree = getList(totalTree, Root);

            for (int index = 0; index < 7; index++)
            {
                Console.Write("Level " + index + " | ");
                foreach (AVLTreeCell Node in totalTree[index])
                {
                    Console.Write(Node.ToString());
                }
                Console.WriteLine(" |");
            }
        }

        public List<AVLTreeCell>[] getList(List<AVLTreeCell>[] ListTree, AVLTreeCell Node)
        {
            if (Node.LeftChild != null)
            {
                ListTree = getList(ListTree, Node.LeftChild);
            }

            if (Node.RightChild != null)
            {
                ListTree = getList(ListTree, Node.RightChild);
            }

            ListTree[Node.Height].Add(new AVLTreeCell(Node));

            return ListTree;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            AVLTree tree = new AVLTree();

            int rndNum = rnd.Next(101);

            Console.Write(rndNum);

            for (int counter = 0; counter < 25; counter++)
            {
                tree.insertCell(rndNum);

                rndNum = rnd.Next(1001);

                Console.Write(", " + rndNum);
            }

            Console.WriteLine();
            Console.WriteLine();

            tree.fixBalanceFactor(tree.Root, 0);

            tree.printTree();

            Console.ReadKey();
        }
    }
}

