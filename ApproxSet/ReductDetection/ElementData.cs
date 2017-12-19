using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReductDetection
{
    public class ElementData
    {
        public int Id { get; set; }

        public IList<bool> ConditionValues { get; set; }

        public string DecisionValue { get; set; }

        public ElementData()
        {
            ConditionValues = new List<bool>();
        }
    }
}
