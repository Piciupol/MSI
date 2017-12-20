using System;
using System.Windows;
using ApproximateSetsApp.Logic;
using ReductDetection;

namespace DecisionTreeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly DecisionMaker decisionMaker;

        public MainWindow()
        {
            InitializeComponent();

            controlGrid.Visibility = Visibility.Collapsed;
            resultGrid.Visibility = Visibility.Collapsed;

            decisionMaker = new DecisionMaker("baza.json", new JohnsonReductFinder());
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            startButton.Visibility = Visibility.Collapsed;
            controlGrid.Visibility = Visibility.Visible;
            UpdateSet();
        }

        private void yesAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            decisionMaker.SetAnswer(true);
            UpdateSet();
        }

        private void noAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            decisionMaker.SetAnswer(false);
            UpdateSet();
        }

        public void UpdateSet()
        {

            if (decisionMaker.IsFinished == false)
            {
                var attributeName = decisionMaker.GetAttributeToAsk();
                questionLabel.Content = attributeName + " ?";

                    yesAnswerButton.Visibility = Visibility.Visible;
                    noAnswerButton.Visibility = Visibility.Visible;

                return;
            }

            SetResultMessage();
            decisionMaker.Reset();
            resultGrid.Visibility = Visibility.Visible;
            controlGrid.Visibility = Visibility.Collapsed;

        }

        private void SetResultMessage()
        {
            var answers = decisionMaker.GetResult();
            var message = "ANSWERS\n\n";
            for (int i = 0; i < answers.Count; i++)
                message += $"{i + 1}. {answers[i]}\n";

            resultTextBlock.Text = message;
        }


        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            resultGrid.Visibility = Visibility.Collapsed;
            controlGrid.Visibility = Visibility.Visible;
            UpdateSet();
        }
    }
}
