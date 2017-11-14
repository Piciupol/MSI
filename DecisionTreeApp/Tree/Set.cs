using System;

namespace DecisionTreeApp.Tree
{
    public class QuestionNode : Node
    {
        public string Question { get; set; }

        public bool IsFuzzy { get; set; }

        [Obsolete]
        public string Name { get; set; }

        [Obsolete]
        public int MapFeatureId { get; set; }
    }
}