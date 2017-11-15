using System.Linq;
using System.Windows;
using DecisionTreeApp.Tree;

namespace DecisionTreeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Tree.DecisionTree _decisionTree;
        private QuestionNode _currentSet;

        public MainWindow()
        {
            InitializeComponent();

            controlGrid.Visibility = Visibility.Collapsed;           
            resultGrid.Visibility = Visibility.Collapsed;

            var nodes = new[]
            {
                new  QuestionNode(){ Question = "Czy lubisz chleb ?", Id = 0, LeftChild = 1, RightChild = 2 },
                new  QuestionNode(){ Question = "Czy masz łupież ?", Id = 1, LeftChild = 3, RightChild = 4},
                new  QuestionNode(){ Question = "Pomidor ?", Id = 2, LeftChild = 5, RightChild = 6},
                new Node() {Label = "Dziennikarstwo", Id= 3},
                new Node() {Label = "MiNI", Id= 4},
                new Node() {Label = "Ekonomia", Id= 5},
                new Node() {Label = "Turystyka i rekreacja", Id= 6}
            };


            _decisionTree = new DecisionTree(nodes);
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            startButton.Visibility = Visibility.Collapsed;
            controlGrid.Visibility = Visibility.Visible;
            UpdateSet();
        }

        private void yesAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            _decisionTree.ProvideAnswer(0.0, 1.0);
            UpdateSet();
        }

        private void noAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            _decisionTree.ProvideAnswer(1.0, 0.0);
            UpdateSet();
        }

        public void UpdateSet()
        {
            var questionNode = _decisionTree.GetQuestion();
            if (questionNode != null  && _decisionTree.Answers.Count() < 3)
            {
                questionLabel.Content = questionNode.Question;
                answerSlider.Value = 0;
                return;
            }

                SetResultMessage();
                _decisionTree.Reset();
                resultGrid.Visibility = Visibility.Visible;
                controlGrid.Visibility = Visibility.Collapsed;
            
        }

        private void SetResultMessage()
        {
            var answers = _decisionTree.Answers.OrderByDescending(a => a.Rank).ToList();
            var message = "TOP 3\n\n";
            for (int i = 0; i < answers.Count; i++)
                message += $"{i+1}. {answers[i].Label} : {answers[i].Rank:0.00} \n";

            resultTextBlock.Text = message;
        }


        private void okSliderAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            var answer = answerSlider.Value;
            _decisionTree.ProvideAnswer(1.0 - answer, answer);
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
