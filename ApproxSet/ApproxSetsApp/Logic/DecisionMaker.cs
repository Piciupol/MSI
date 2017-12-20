using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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

        public bool IsFinished => _reductIndices.Count == 0;

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
            return _attributeNames[_reductIndices.First()];
        }

        public void SetAnswer(bool value)
        {
            _elements.RemoveAll(x => x.ConditionValues[_reductIndices.First()] != value);
            _reductIndices.RemoveAt(0);

            if(_reductIndices.Count > 0)
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

            _reductIndices = newReducts;
        }

        private void LoadData()
        {
            var store = new Store.DataStore(_dataFileName);
            _elements = store.Elements;
            _attributeNames = store.AttributeNames;
        }
    }
}
