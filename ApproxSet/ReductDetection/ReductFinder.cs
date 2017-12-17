using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReductDetection
{
    public class JohnsonReductFinder : IReductFinder
    {
        public IList<int> GetReducts(Matrix matrix)
        {
            var result = new List<int>();

            var attributeCounters = new Dictionary<int, int>();
            var attributePositions = new Dictionary<int, IList<Cell>>();

            CountAttributeOccurencies(matrix, attributeCounters, attributePositions);
            int maxOccurency = attributeCounters.Values.Max();
            while (maxOccurency > 0)
            {
                int mostImportantAttribute = GetMostImportantAttribute(attributeCounters, maxOccurency);

                result.Add(mostImportantAttribute);
                EraseCells(matrix, attributePositions[mostImportantAttribute]);

                CountAttributeOccurencies(matrix, attributeCounters, attributePositions);
                maxOccurency = attributeCounters.Values.Max();
            }
            return result;
        }

        private static void EraseCells(Matrix matrix, IList<Cell> positions)
        {
            foreach (var position in positions)
            {
                matrix[position.Row, position.Column].Clear();
            }
        }

        private int GetMostImportantAttribute(Dictionary<int, int> attributeCounters, int maxOccurency)
        {
            return attributeCounters.Where(attr => attr.Value == maxOccurency).Select(attr => attr.Key).First();
        }

        private static void CountAttributeOccurencies(
            Matrix matrix,
            IDictionary<int, int> attributeCounters,
            IDictionary<int,IList<Cell>> attributePositions)
        {
            foreach (var key in attributeCounters.Keys)
            {
                attributeCounters[key] = 0;
                attributePositions[key].Clear();
            }

            for (int row = 0; row < matrix.Rows; ++row)
            {
                for (int col = 0; col < matrix.Columns; ++col)
                {
                    foreach (var attributeIndex in matrix[row, col])
                    {
                        if (attributeCounters.Keys.Contains(attributeIndex))
                            attributeCounters[attributeIndex]++;
                        else
                        {
                            attributeCounters.Add(attributeIndex, 1);
                            attributePositions.Add(attributeIndex, new List<Cell>());
                        }
                        attributePositions[attributeIndex].Add(new Cell(row, col));
                    }
                }
            }
        }
    }
}
