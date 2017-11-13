namespace DecisionTreeApp.Tree
{
    public class Leaf
    {
        public string Label { get; set; }
        public double[] Features { get; set; }

        public double Rank;
    }
}