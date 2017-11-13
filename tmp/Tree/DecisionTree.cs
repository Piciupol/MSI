using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionTree
{
    public class DecisionTree
    {
        private readonly List<Set> _sets;
        private readonly List<Leaf> _leaves;
        private readonly List<Set> _activeSets;
        private readonly int _featuresCount;

        public int ActiveSetsCount => _activeSets.Count;

        public DecisionTree(List<Set> sets, List<Leaf> leaves)
        {
            _sets = sets;
            _leaves = leaves;

            _activeSets = new List<Set>(_sets);

            _featuresCount = leaves[0].Features.Length;
        }


        public void UpdateLeafRanks(Set bestSet, double answer)
        {
            foreach (var leaf in _leaves)
            {
                var score = Math.Pow(1.0 - Math.Abs(leaf.Features[bestSet.MapFeatureId] - answer), 2);
                leaf.Rank += score;
            }
        }

        public Set TakeBestSetToAsk()
        {
            double max = 0.0;
            Set bestSet = null;
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
                sum += leaf.Features[featureIndex];

            double avg = sum / _featuresCount;

            sum = 0.0;
            foreach (var leaf in _leaves)
                sum += Math.Pow(leaf.Features[featureIndex] - avg, 2);

            var standardDeviation = Math.Sqrt(sum / (_featuresCount - 1));
            return standardDeviation;
        }
    }
}