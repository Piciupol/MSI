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
            var store = new DataInput();
            store.AttributeNames.Add("first");
            store.AttributeNames.Add("second");
            store.AttributeNames.Add("third");



            store.ElementDataInputs.Add(new ElementDataInput()
            {
                Conditions = new List<int>{ },
                Decision = "Result1"
            });

            store.ElementDataInputs.Add(new ElementDataInput()
            {
                Conditions = new List<int> { 1 },
                Decision = "Result2"
            });

            store.ElementDataInputs.Add(new ElementDataInput()
            {
                Conditions = new List<int> { 0 },
                Decision = "Result3"
            });

            store.ElementDataInputs.Add(new ElementDataInput()
            {
                Conditions = new List<int> { 0, 2},
                Decision = "Result4"
            });


            store.Save("test.json");
        }

    }
}
