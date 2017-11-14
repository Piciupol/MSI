using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionTreeApp.Tree
{
    public class DecisionTree
    {
        private readonly List<QuestionNode> _sets;
        private readonly List<Leaf> _leaves;
        private readonly List<QuestionNode> _activeSets;
        private readonly int _featuresCount;

        public int ActiveSetsCount => _activeSets.Count;

        public DecisionTree(List<QuestionNode> sets, List<Leaf> leaves)
        {
            _sets = sets;
            _leaves = leaves;

            _activeSets = new List<QuestionNode>(_sets);

            //_featuresCount = leaves[0].Features.Length;
        }


        public void UpdateLeafRanks(QuestionNode bestSet, double answer)
        {
            foreach (var leaf in _leaves)
            {
                //var score = Math.Pow(1.0 - Math.Abs(leaf.Features[bestSet.MapFeatureId] - answer), 2);
              / / leaf.Rank += score;
            }
        }

        public QuestionNode TakeBestSetToAsk()
        {
            double max = 0.0;
            QuestionNode bestSet = null;
            foreach (var set in _activeSets)
            {
                var sd = CalculateStandardDeviation(set.MapFeatureId);
                if (sd > max)
                {
                    max = sd;
                    bestSet = set;
                }
            }

            _activeSets.Remove(bestSet);
            return bestSet;
        }

        private double CalculateStandardDeviation(int featureIndex)
        {
            double sum = 0.0;
            foreach (var leaf in _leaves)
              //  sum += leaf.Features[featureIndex];

            double avg = sum / _featuresCount;

            sum = 0.0;
            foreach (var leaf in _leaves)
            //    sum += Math.Pow(leaf.Features[featureIndex] - avg, 2);

            var standardDeviation = Math.Sqrt(sum / (_featuresCount - 1));
            return standardDeviation;
        }

        public void Reset()
        {
            foreach (var leaf in _leaves)
                leaf.Rank = 0;

            _activeSets.AddRange(_sets);
        }

        public List<Leaf> GetTopLeaves(int number)
        {
            return _leaves.OrderByDescending(l => l.Rank).Take(number).ToList();
        }
    }

    class Tree
    {
        private Node root;
        private SortedSet<Node> nodesQueue;

        public Node CurrentNode { get; private set; }

        private ICollection<Node> answers;

        public IEnumerable<Node> Answers => answers;

        private IFuzzyOperations _operations;
        private IFuzzyOperations fuzzyOperations => _operations ?? (_operations = new FirstFuzzyFunction());
        
        public Tree(Node root)
        {
            this.root = root;
            nodesQueue = new SortedSet<Node>(new NodeComparer());
            answers = new SortedSet<Node>(new NodeComparer());
            CurrentNode = root;
        }

        public Tree SetOperations(IFuzzyOperations ops)
        {
            _operations = ops;
            return this;
        }

        public QuestionNode GetQuestion(double minRank = 0.0)
        {
            if (CurrentNode != null)
                return CurrentNode as QuestionNode;

            if (nodesQueue.Any() && nodesQueue.First().Rank > minRank)
                return nodesQueue.First() as QuestionNode;

            return null;
        }

        public void ProvideAnswer(double leftAnswer, double rightAnswer)
        {
            if (CurrentNode is QuestionNode == false)
                throw new ArgumentException("Node is not a question");

            ApplyAnswerToNode(Node.GetNode(CurrentNode.LeftChild.Value), leftAnswer);
            ApplyAnswerToNode(Node.GetNode(CurrentNode.RightChild.Value), rightAnswer);

            CurrentNode = null;
        }

        private void ApplyAnswerToNode(Node node, double leftAnswer)
        {
            node.Rank = fuzzyOperations.PNorm(CurrentNode.Rank, leftAnswer);

            if (node is QuestionNode)
            {
                if(node.Rank > 0)
                    nodesQueue.Add(node);
            }
            else
                answers.Add(node);
        }
    }    
}