using ApproximateSetsApp.Store;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DecisionTreeApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var store = new DataStore();
            store.AttributeNames.Add("first");
            store.AttributeNames.Add("second");
            store.AttributeNames.Add("decision");

            store.Elements.Add(new ReductDetection.ElementData()
            {
                Id = 0,
                ConditionValues = new List<bool> { true, false },
                DecisionValue = "result"
            });

            store.Elements.Add(new ReductDetection.ElementData()
            {
                Id = 1,
                ConditionValues = new List<bool> { true, false },
                DecisionValue = "otherResult"
            });

            store.Save("test.json");
        }

    }
}
