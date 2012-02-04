using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AVLTree
{
    class Node
    {
        public string data;
        public Node leftChild, rightChild;
        public Node parent;
        public int height;
        public int balanceFactor;

        //constructor
        public Node(string pData)
        {
            data = pData;
            height = Height(this);
            balanceFactor = BalanceFactor(this.leftChild, this.rightChild);
        }

        public int Height(Node n)
        {
            if (n == null)
                return 0;
            else
                return Math.Max(Height(n.leftChild), Height(n.rightChild)) + 1;
        }

        public int BalanceFactor(Node leftNode, Node rightNode)
        {
            try
            {
                return (leftNode.height - rightNode.height);
            }
            catch
            {
                return 0;
            }
        }
    }
}