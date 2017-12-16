using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionTreeApp.Tree
{
    public class DecisionTree
    {
        private readonly SortedSet<Node> _nodesQueue;

        public Node CurrentNode { get; private set; }

        private readonly ICollection<Node> _answers;

        public IEnumerable<Node> Answers => _answers;

        private IFuzzyOperations _operations;
        private IFuzzyOperations FuzzyOperations => _operations ?? (_operations = new FirstFuzzyFunction());

        private readonly IDictionary<int, Node> _nodeDictionary;
        private Node GetNode(int id) => _nodeDictionary[id];


        public DecisionTree(Node[] nodes)
        {
            _nodesQueue = new SortedSet<Node>(new NodeComparer()) {nodes[0]};
            _answers = new SortedSet<Node>(new NodeComparer());
            _nodeDictionary = nodes.ToDictionary(n => n.Id);
        }

        public DecisionTree SetOperations(IFuzzyOperations ops)
        {
            _operations = ops;
            return this;
        }

        public QuestionNode GetQuestion(double minRank = 0.0)
        {
            if (_nodesQueue.Any() && _nodesQueue.First().Rank > minRank)
            {
                CurrentNode = _nodesQueue.First();
                return CurrentNode as QuestionNode;
            }

            return null;
        }

        public void ProvideAnswer(double leftAnswer, double rightAnswer)
        {
            if (CurrentNode is QuestionNode == false)
                throw new ArgumentException("Node is not a question");

            _nodesQueue.Remove(CurrentNode);

            ApplyAnswerToNode(GetNode(CurrentNode.LeftChild.Value), leftAnswer);
            ApplyAnswerToNode(GetNode(CurrentNode.RightChild.Value), rightAnswer);
        }

        public void Reset()
        {
            _nodesQueue.Clear();
            _answers.Clear();

            foreach (var node in _nodeDictionary.Values)
                node.Rank = 1.0;

            _nodesQueue.Add(_nodeDictionary[0]);
        }

        private void ApplyAnswerToNode(Node node, double leftAnswer)
        {
            node.Rank = FuzzyOperations.PNorm(CurrentNode.Rank, leftAnswer);

            if (node is QuestionNode)
            {
                if(node.Rank > 0)
                    _nodesQueue.Add(node);
            }
            else
                _answers.Add(node);
        }

    }    
}