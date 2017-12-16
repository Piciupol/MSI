using System;
using System.Collections.Generic;

namespace DecisionTreeApp.Tree
{
    class NodeComparer : IComparer<Node>
    {
        const double DoubleTolerance = 1e-3;

        public int Compare(Node x, Node y)
        {
            if (Math.Abs(x.Rank - y.Rank) < DoubleTolerance)
                return 0;
            if (x.Rank > y.Rank)
                return 1;
            return -1;
        }
    }
}
