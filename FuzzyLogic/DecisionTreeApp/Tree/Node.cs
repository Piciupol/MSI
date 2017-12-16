namespace DecisionTreeApp.Tree
{
    public class Node
    {
        public int Id;
        public int?  LeftChild, RightChild;
        public string Label;
        public double Rank = 1.0;
    }
}