using System.Collections.Generic;
using System.IO;
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
            if(fileName == null) return;

            var serializer = new JsonSerializer();
            using (var file = File.OpenText(fileName))
            {
                var store = (DataStore) serializer.Deserialize(file, typeof(DataStore));
                Elements = store.Elements;
                AttributeNames = store.AttributeNames;
            }
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
}
