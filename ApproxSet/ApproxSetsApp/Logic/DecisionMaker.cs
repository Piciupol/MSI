using System.Collections.Generic;
using System.Linq;
using ReductDetection;

namespace ApproximateSetsApp.Logic
{
    public class DecisionMaker
    {
        private List<ElementData> _elements;
        private List<string> _attributeNames;
        private List<int> _reductIndices;

        private readonly string _dataFileName;
        private readonly IReductFinder _reductFinder;

        private int _currentReduct;

        //todo

        public bool IsFinished => _currentReduct == _reductIndices.Count;

        public DecisionMaker(string dataFileName, IReductFinder reductFinder)
        {
            _dataFileName = dataFileName;
            _reductFinder = reductFinder;

            LoadData();
            ReduceAttributes();
        }

        public void Reset()
        {
            LoadData();
            ReduceAttributes();
        }

        public string GetAttributeToAsk()
        {
            return _attributeNames[_reductIndices[_currentReduct]];
        }

        public void SetAnswer(bool value)
        {
            _elements.RemoveAll(x => x.ConditionValues[_reductIndices[_currentReduct]] != value);

            ++_currentReduct;
            ReduceAttributes();
        }

        public List<string> GetResult()
        {
            return _elements.Select(x => x.DecisionValue).Distinct().ToList();
        }

        private void ReduceAttributes()
        {
            var matrix = new Matrix(_elements);
            var newReducts = _reductFinder.GetReducts(matrix).ToList();
            newReducts.Sort();

            if (_reductIndices == null || !_reductIndices.SequenceEqual(newReducts))
            {
                _reductIndices = newReducts;
                _currentReduct = 0;
            }
        }

        private void LoadData()
        {
            var store = new Store.DataStore(_dataFileName);
            _elements = store.Elements;
            _attributeNames = store.AttributeNames;
        }
    }
}
