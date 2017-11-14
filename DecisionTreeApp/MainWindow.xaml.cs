using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DecisionTreeApp.Tree;

namespace DecisionTreeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Tree.DecisionTree _decisionTree;
        private Set _currentSet;

        public MainWindow()
        {
            InitializeComponent();

            controlGrid.Visibility = Visibility.Collapsed;           
            resultGrid.Visibility = Visibility.Collapsed;
           
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


            var nodeDictionary = new Dictionary<int, Node>() {{1, new Node() { }}};

            _decisionTree = new Tree.DecisionTree(sets.ToList(), leaves.ToList());
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            startButton.Visibility = Visibility.Collapsed;
            controlGrid.Visibility = Visibility.Visible;
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
            if (_decisionTree.ActiveSetsCount > 0)
            {
                _currentSet = _decisionTree.TakeBestSetToAsk();
                questionLabel.Content = _currentSet.Question;
                answerSlider.Value = 0;
            }
            else
            {
                SetResultMessage();
                _decisionTree.Reset();
                resultGrid.Visibility = Visibility.Visible;
                controlGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void SetResultMessage()
        {
            var bestLeaves = _decisionTree.GetTopLeaves(5);
            var message = "TOP 5\n\n";
            for (int i = 0; i < bestLeaves.Count; i++)
                message += $"{i+1}. {bestLeaves[i].Label} : {bestLeaves[i].Rank:0.00} \n";

            resultTextBlock.Text = message;
        }


        private void okSliderAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            _decisionTree.UpdateLeafRanks(_currentSet, answerSlider.Value);
            UpdateSet();
        }

        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            resultGrid.Visibility = Visibility.Collapsed;
            controlGrid.Visibility = Visibility.Visible;
            UpdateSet();
        }
    }
}
