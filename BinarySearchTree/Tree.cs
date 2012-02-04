using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AVLTree
{
    class Tree
    {
        public Node rootNode;

        public Tree(Node rootNode)
        {
            rootNode = this.rootNode;
        }

        public void InsertNode(Node insertedNode, Node parentNode)  //recursive node insert
        {
            if (this.rootNode == null)
                rootNode = insertedNode;
            //reference: http://csharp.net-informations.com/string/csharp-string-compare.htm
            else if (string.Compare(insertedNode.data, parentNode.data) < 0)  //if string0 is less than string1
            {
                if (parentNode.leftChild == null)
                {
                    parentNode.leftChild = insertedNode;
                    insertedNode.parent = parentNode;
                }
                else
                    InsertNode(insertedNode, parentNode.leftChild);
            }
            else if (string.Compare(insertedNode.data, parentNode.data) > 0)  //if string0 is greater than string1
            {
                if (parentNode.rightChild == null)
                {
                    parentNode.rightChild = insertedNode;
                    insertedNode.parent = parentNode;
                }
                else
                    InsertNode(insertedNode, parentNode.rightChild);
            }
            else { }
                //Console.WriteLine("Value already in Tree");
                //throw new Exception("Inserted node value equals parent node value, discard value");
        }

        public void InOrderTraversal(Node current)
        {
            if (current != null)
            {
                // Visit the left child...
                InOrderTraversal(current.leftChild);

                // Output the value of the current node
                Console.WriteLine(current.data);

                // Visit the right child...
                InOrderTraversal(current.rightChild);
            }
        }

        public void Delete(Node nodeToDelete, Node[] nodeList)
        {
            if (nodeToDelete.rightChild == null)
            {
                nodeToDelete = nodeToDelete.leftChild;
            }
            else if (nodeToDelete.rightChild.leftChild == null)
            {
                nodeToDelete = nodeToDelete.rightChild;
            }
            else
            {
                Node tempNode = getLeftMost(nodeToDelete.rightChild.leftChild);
                Delete(nodeToDelete.rightChild.leftChild, nodeList);
                nodeToDelete.data = tempNode.data;
            }
            Program.ReassignHeightsAndBalanceFactors(nodeList);
            Program.BalanceTreeIfUnbalanced(nodeList);
        }

        public Node getLeftMost(Node selectedNode)
        {
            if (selectedNode.leftChild == null) return selectedNode;

            else return getLeftMost(selectedNode.leftChild);
        }

        public Node getRightMost(Node selectedNode)
        {
            if (selectedNode.rightChild == null) return selectedNode;

            else return getRightMost(selectedNode.rightChild);
        }
    }
}