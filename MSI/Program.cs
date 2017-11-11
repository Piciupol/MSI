using System.CodeDom;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace MSI
{
    public class Program
    {
        
        static void Main()
        {
            var sets = new[]
            {
                new  Set(){ Question = "Czy lubisz chleb ?", MapFeatureId = 0},
                new  Set(){ Question = "Czy masz łupież ?", MapFeatureId = 1},
                new  Set(){ Question = "Pomidor ?", MapFeatureId = 2}
            };
            var leaves = new[]
            {
                new  Leaf(){ Label = "Student MiNI", Features = new [] { 0.0, 1.0, 0.5 }},
                new Leaf{ Label = "Student Ekonomii", Features = new [] { 0.5, 0.5, 0.5 }},
                new Leaf{ Label = "Student Dziennikarstwa", Features = new [] { 0.3, 0.4, 1.0 }}
            };

            var decisionTree = new DecisionTree(sets.ToList(), leaves.ToList());
            decisionTree.Run();
        }
    }
}
