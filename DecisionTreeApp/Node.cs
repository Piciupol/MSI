using System.Collections.Generic;

namespace DecisionTreeApp
{
    public class Node
    {
        public int? Parent, LeftChild, RightChild;
        public string Label;
        public double Rank = 0;

        public static Dictionary<int, Node> NodeDictionary = new Dictionary<int, Node>()
        {
            {0, new Node() {Label = "Rodzic", Parent = null, LeftChild = 1, RightChild = 2}},
            {1, new Node() {Label = "Lewe dziecko", Parent = 0, LeftChild = null, RightChild = null}},
            {2, new Node() {Label = "Prawy gówniak", Parent = 0, LeftChild = null, RightChild = null}}
        };

        public static Node GetNode(int id)
        {
            return NodeDictionary[id];
        }

    }
}