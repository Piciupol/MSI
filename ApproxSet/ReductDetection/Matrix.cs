using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReductDetection
{
    public class Matrix
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        private IList<int>[,] attributes;

        public Matrix(IList<ElementData> elements)
        {
            Rows = elements.Count();
            Columns = elements.Count();

            FillMatrix(elements);
        }

        private void FillMatrix(IList<ElementData> elements)
        {
            attributes = new IList<int>[elements.Count(), elements.Count()];
            for(var i=0; i < attributes.GetLength(0); i++)
                for(var j=0; j < attributes.GetLength(1); j++)
                    attributes[i,j] = new List<int>();

            Parallel.For(0, elements.Count(), row =>
            {
                for (int col = 0; col < elements.Count(); ++col)
                {
                    //Compare only elements with different decisions
                    if (elements[row].DecisionValue == elements[col].DecisionValue)
                        continue;

                    for (int i = 0; i < elements[row].ConditionValues.Count; ++i)
                    {
                        if (elements[row].ConditionValues[i] != elements[col].ConditionValues[i])
                            attributes[row, col].Add(i);
                    }
                }
            });
        }

        public IList<int> this[int row, int column] => attributes[row, column];
    }
}
