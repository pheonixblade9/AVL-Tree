using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AVLTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Problem I:");
            List<string> problemI = new List<string>() { "MARCH", "MAY", "NOVEMBER", "AUGUST", 
                "APRIL", "JANUARY", "DECEMBER", "JULY", "FEBRUARY", "JUNE", "OCTOBER", "SEPTEMBER" };
            Node[] problemINodes = new Node[problemI.Count];
            AssignNodes(problemI, problemINodes);
            Tree problemITree = new Tree(problemINodes[0]);
            BuildTree(problemITree, problemINodes);
            problemITree.InOrderTraversal(problemITree.rootNode);
            Console.WriteLine("--------------");
            ClearTreeByDeletion(problemITree, problemINodes);

            Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Problem II:");
            List<string> problemII = new List<string>() { "DECEMBER", "JANUARY", "APRIL", "MARCH", 
                "JULY", "AUGUST", "OCTOBER", "FEBRUARY", "NOVEMBER", "MAY", "JUNE", "SEPTEMBER" };
            Node[] problemIINodes = new Node[problemII.Count];
            AssignNodes(problemII, problemIINodes);
            Tree problemIITree = new Tree(problemIINodes[0]);
            BuildTree(problemIITree, problemIINodes);
            problemIITree.InOrderTraversal(problemIITree.rootNode);
            Console.WriteLine("--------------");
            ClearTreeByDeletion(problemIITree, problemIINodes);

            Console.ReadLine();
            Console.WriteLine();

            int size = 10;
            bool lowerCase = false;
            List<string> problemIII = new List<string>();
            BuildRandomStringList(size, lowerCase, problemIII);
            Node[] problemIIINodes = new Node[problemIII.Count];
            AssignNodes(problemIII, problemIIINodes);
            Tree problemIIITree = new Tree(problemIIINodes[0]);
            BuildTree(problemIIITree, problemIIINodes);
            problemIIITree.InOrderTraversal(problemIIITree.rootNode);
            Console.WriteLine("--------------");
            ClearTreeByDeletion(problemIIITree, problemIIINodes);
            Console.ReadLine();
        }

        private static void BuildRandomStringList(int size, bool lowerCase, List<string> problemIII)
        {
            for (int i = 0; i < 1000; i++)
            {
                problemIII.Add(RandomString(size, lowerCase));
                Thread.Sleep(15); //sleep for 15ms so threads aren't identical
            }
        }

        ///referenced from http://www.c-sharpcorner.com/UploadFile/mahesh/RandomNumber11232005010428AM/RandomNumber.aspx
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        private static void AssignNodes(List<string> data, Node[] nodes)
        {
            for (int i = 0; i < data.Count; i++)
                nodes[i] = new Node(data[i]);
        }

        private static void ClearTreeByDeletion(Tree t, Node[] nodeList)
        {
            for (int i = 0; i <= nodeList.Length - 1; i++)
            {
                Console.WriteLine(nodeList[i].data);
                t.Delete(nodeList[i], nodeList);
                for (int j = 0; j < nodeList.Length; j++)
                //reassign heights and balance factors each time a node is inserted
                {
                    nodeList[j].height = nodeList[j].Height(nodeList[j]);
                    nodeList[j].balanceFactor = nodeList[j].BalanceFactor(nodeList[j].leftChild, nodeList[j].rightChild);
                }
                for (int j = 0; j < nodeList.Length; j++)
                {
                    if (nodeList[j].balanceFactor == 2 || nodeList[j].balanceFactor == -2)
                    {
                        BalanceTree(nodeList[j]);
                        //rebalance tree after each node is inserted if it is unbalanced
                    }
                }
            }
        }

        private static void BuildTree(Tree t, Node[] nodeList)
        {
            for (int i = 0; i < nodeList.Length - 1; i++)
            {
                t.InsertNode(nodeList[i], nodeList[0]);
                ReassignHeightsAndBalanceFactors(nodeList);
                BalanceTreeIfUnbalanced(nodeList);
            }
        }

        public static void BalanceTreeIfUnbalanced(Node[] nodeList)
        {
            for (int j = 0; j < nodeList.Length; j++)
                if (nodeList[j].balanceFactor == 2 || nodeList[j].balanceFactor == -2)
                    BalanceTree(nodeList[j]);
        }

        public static void ReassignHeightsAndBalanceFactors(Node[] nodeList)
        {//reassign heights and balance factors each time a node is inserted
            for (int j = 0; j < nodeList.Length; j++)
            {
                nodeList[j].height = nodeList[j].Height(nodeList[j]);
                nodeList[j].balanceFactor = nodeList[j].BalanceFactor(nodeList[j].leftChild, nodeList[j].rightChild);
            }
        }

        private static void BalanceTree(Node nodeToBalance)
        {
            if (nodeToBalance.balanceFactor == 2)
            {
                if (nodeToBalance.rightChild.balanceFactor == 1)  //RR
                {//do a left rotation
                    Console.WriteLine(nodeToBalance.data + " was RR unbalanced");
                    //nodeToBalance = LeftRotation(nodeToBalance);
                }
                else if (nodeToBalance.rightChild.balanceFactor == -1)  //RL
                {//do a double left rotation
                    Console.WriteLine(nodeToBalance.data + " was RL unbalanced");
                    //nodeToBalance = LeftRotation(nodeToBalance);
                    //nodeToBalance = LeftRotation(nodeToBalance);
                }
            }
            else
            {
                if (nodeToBalance.leftChild.balanceFactor == 1)  //LR
                {//do a right rotation
                    Console.WriteLine(nodeToBalance.data + " was LR unbalanced");
                    //nodeToBalance = RightRotation(nodeToBalance);
                }
                else if (nodeToBalance.leftChild.balanceFactor == -1)  //LL
                {//do a double right rotation
                    Console.WriteLine(nodeToBalance.data + " was LL unbalanced");
                    //nodeToBalance = RightRotation(nodeToBalance);
                    //nodeToBalance = RightRotation(nodeToBalance);
                }
            }
        }

        private static Node RightRotation(Node nodeToBalance)
        {
            Node one = nodeToBalance;
            Node two = nodeToBalance.leftChild;
            Node three = nodeToBalance.leftChild.leftChild;
            two.parent = one.parent;
            one.parent = two;
            two.rightChild = one;
            //nodeToBalance = one;
            //nodeToBalance.parent = two;
            //nodeToBalance.parent.leftChild = three;
            return nodeToBalance;
        }

        private static Node LeftRotation(Node nodeToBalance)
        {
            Node one = nodeToBalance;
            Node two = nodeToBalance.rightChild;
            Node three = nodeToBalance.rightChild.rightChild;
            two.parent = one.parent;
            one.parent = two;
            two.leftChild = one;
            //nodeToBalance = one;
            //nodeToBalance.parent = two;
            //nodeToBalance.parent.rightChild = three;
            return nodeToBalance;
        }
    }
}
