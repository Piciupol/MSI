using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using Newtonsoft.Json;
using ReductDetection;

namespace ApproximateSetsApp.Store
{
    public class DataStore
    {
        public List<string> AttributeNames { get; set; }
        public List<ElementData> Elements { get; set; }

        public DataStore(string fileName = null)
        {
            if (fileName == null)
            {
                AttributeNames = new List<string>();
                Elements = new List<ElementData>();
            }
            else
            {
                var serializer = new JsonSerializer();
                using (var file = File.OpenText(fileName))
                {
                    var inputData = (DataInput)serializer.Deserialize(file, typeof(DataInput));
                    AttributeNames = inputData.AttributeNames;
                    Elements = inputData.ElementDataInputs.Select(ToElementData).ToList();
                }
            }
        }

        private ElementData ToElementData(ElementDataInput input)
        {
            var elementData = new ElementData
            {
                DecisionValue = input.Decision,
                ConditionValues = new bool[AttributeNames.Count].ToList()
            };

            input.Conditions.ForEach(x => elementData.ConditionValues[x] = true);

            return elementData;
        }

        public void Save(string fileName)
        {
            var serializer = new JsonSerializer();
            using (var file = File.CreateText(fileName))
            {
                serializer.Serialize(file, this);
            }
        }
    }

    public class DataInput
    {
        public List<string> AttributeNames { get; set; } = new List<string>();
        public List<ElementDataInput> ElementDataInputs { get; set; } = new List<ElementDataInput>();

        public void Save(string fileName)
        {
            var serializer = new JsonSerializer();
            using (var file = File.CreateText(fileName))
            {
                serializer.Serialize(file, this);
            }
        }
    }

    public class ElementDataInput
    {
        public List<int> Conditions { get; set; } 
        public string Decision { get; set; }

    }
}
