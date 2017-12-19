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
            store.AttributeNames.Add("third");



            store.Elements.Add(new ReductDetection.ElementData()
            {
                Id = 0,
                ConditionValues = new List<bool> { false, false , false},
                DecisionValue = "Result1"
            });

            store.Elements.Add(new ReductDetection.ElementData()
            {
                Id = 1,
                ConditionValues = new List<bool> { false, true, false },
                DecisionValue = "Result2"
            });

            store.Elements.Add(new ReductDetection.ElementData()
            {
                Id = 2,
                ConditionValues = new List<bool> { true, false, false },
                DecisionValue = "Result3"
            });

            store.Elements.Add(new ReductDetection.ElementData()
            {
                Id = 3,
                ConditionValues = new List<bool> { true, false, true },
                DecisionValue = "Result4"
            });


            store.Save("test.json");
        }

    }
}
