using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DecisionTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DecisionTree _decisionTree;
        private Set _currentSet;

        public MainWindow()
        {
            InitializeComponent();

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

            _decisionTree = new DecisionTree(sets.ToList(), leaves.ToList());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            UpdateSet();
        }

        private void yesAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            _decisionTree.UpdateLeafRanks(_currentSet, 1.0);
            UpdateSet();
        }

        private void noAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            _decisionTree.UpdateLeafRanks(_currentSet, 0.0);
            UpdateSet();
        }

        public void UpdateSet()
        {
            _currentSet = _decisionTree.TakeBestSetToAsk();
            questionLabel.Content = _currentSet.Question;
            answerSlider.Value = 0;
        }

        private void answerSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _decisionTree.UpdateLeafRanks(_currentSet, answerSlider.Value);
            UpdateSet();
        }
    }
}
