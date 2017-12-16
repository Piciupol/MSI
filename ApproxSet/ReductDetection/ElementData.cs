using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReductDetection
{
    class ElementData
    {
        public int Id { get; set; }

        public IList<AttributeValue> ConditionValues { get; private set; }

        public AttributeValue DecisionValue { get; set; }

        public ElementData()
        {
            ConditionValues = new List<AttributeValue>();
        }
    }
}
