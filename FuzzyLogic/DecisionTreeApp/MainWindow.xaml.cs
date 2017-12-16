using System;
using System.Globalization;
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
            sliderLabel.Visibility = Visibility.Collapsed;

            var nodes = new[]
            {
                new QuestionNode()
                {
                    Question = "Czy jesteś umysłem ścisłym ?",
                    Id = 0,
                    LeftChild = 1,
                    RightChild = 2,
                    IsFuzzy = true
                },
                new QuestionNode() {Question = "Czy interesuje cię ekonomia ?", Id = 1, LeftChild = 3, RightChild = 4},
                new QuestionNode()
                {
                    Question = "Wolisz teorię niż praktykę ?",
                    Id = 2,
                    LeftChild = 5,
                    RightChild = 6,
                    IsFuzzy = true
                },
                new QuestionNode()
                {
                    Question = "Lubisz uczyć sie języków obcych ?",
                    Id = 3,
                    LeftChild = 7,
                    RightChild = 8
                },
                new QuestionNode() {Question = "Lubisz zarządzać ludźmi ?", Id = 4, LeftChild = 9, RightChild = 10},
                new QuestionNode()
                {
                    Question = "Interesuje cię budowa maszyn ?",
                    Id = 5,
                    LeftChild = 11,
                    RightChild = 12,
                    IsFuzzy = true
                },
                new QuestionNode()
                {
                    Question = "Interesuje cię przyroda ?",
                    Id = 6,
                    LeftChild = 13,
                    RightChild = 14,
                    IsFuzzy = true
                },
                new QuestionNode()
                {
                    Question = "Jednostka ważniejsza niż społeczeństwo ?",
                    Id = 7,
                    LeftChild = 15,
                    RightChild = 16
                },
                new QuestionNode()
                {
                    Question = "Jesteś przywiazany do Europy ?",
                    Id = 8,
                    LeftChild = 17,
                    RightChild = 18
                },
                new QuestionNode() {Question = "Interesuje cię polityka ?", Id = 9, LeftChild = 19, RightChild = 20},
                new Node() {Label = "Zarządzanie", Id = 10},
                new QuestionNode()
                {
                    Question = "Interesuje cię elektronika ?",
                    Id = 11,
                    LeftChild = 21,
                    RightChild = 22,
                    IsFuzzy = true
                },
                new QuestionNode()
                {
                    Question = "Interesuje cię budowa pojazdów ?",
                    Id = 12,
                    LeftChild = 23,
                    RightChild = 24
                },
                new QuestionNode()
                {
                    Question = "Zastanawia cię jak powstał wszechświat ?",
                    Id = 13,
                    LeftChild = 25,
                    RightChild = 26,
                    IsFuzzy = true
                },
                new QuestionNode()
                {
                    Question = "Człowiek jest najważniejszy ?",
                    Id = 14,
                    LeftChild = 27,
                    RightChild = 28,
                    IsFuzzy = true
                },
                new QuestionNode()
                {
                    Question = "Lubisz poznawać inne kultury ?",
                    Id = 15,
                    LeftChild = 29,
                    RightChild = 30
                },
                new QuestionNode()
                {
                    Question = "Interesuje cię co myślą i czują inne osoby ?",
                    Id = 16,
                    LeftChild = 31,
                    RightChild = 32
                },
                new Node() {Label = "Sinologia", Id = 17},
                new QuestionNode()
                {
                    Question = "Podoba ci cię kultura śródziemnomorska ?",
                    Id = 18,
                    LeftChild = 33,
                    RightChild = 34
                },
                new QuestionNode()
                {
                    Question = "Interesuje cię nowe technologie ?",
                    Id = 19,
                    LeftChild = 35,
                    RightChild = 36,
                    IsFuzzy = true
                },
                new Node() {Label = "Stosunki międzynarodowe", Id = 20},
                new QuestionNode()
                {
                    Question = "Interesuje cię projektowanie budynków ?",
                    Id = 21,
                    LeftChild = 37,
                    RightChild = 38
                },
                new Node() {Label = "Elektronika", Id = 22},
                new Node() {Label = "Mechanika i budowa maszyn", Id = 23},
                new QuestionNode()
                {
                    Question = "Napęd elektryczny do przyszłość ?",
                    Id = 24,
                    LeftChild = 39,
                    RightChild = 40,
                    IsFuzzy = true
                },
                new QuestionNode()
                {
                    Question = "Interesuje cię teoria algorytmów i obliczeń ?",
                    Id = 25,
                    LeftChild = 41,
                    RightChild = 42,
                    IsFuzzy = true
                },
                new QuestionNode()
                {
                    Question = "Czy wiesz co to nitrogliceryna ?",
                    Id = 26,
                    LeftChild = 43,
                    RightChild = 44
                },
                new Node() {Label = "Medycyna", Id = 27},
                new Node() {Label = "Biologia", Id = 28},
                new Node() {Label = "Kulturoznastwo", Id = 29},
                new QuestionNode()
                {
                    Question = "Interesujesz się historią ?",
                    Id = 30,
                    LeftChild = 45,
                    RightChild = 46,
                    IsFuzzy = true
                },
                new Node() {Label = "Pedagogika", Id = 31},
                new Node() {Label = "Psychologia", Id = 32},
                new Node() {Label = "Anglistyka", Id = 33},
                new Node() {Label = "Romanistyka", Id = 34},
                new QuestionNode()
                {
                    Question = "Lubisz papierkową robotę ?",
                    Id = 35,
                    LeftChild = 47,
                    RightChild = 48,
                    IsFuzzy = true
                },
                new Node() {Label = "Informatyka i ekonometria", Id = 36},
                new QuestionNode()
                {
                    Question = "Ciekawi cię medycyna ?",
                    Id = 37,
                    LeftChild = 49,
                    RightChild = 50,
                    IsFuzzy = true
                },
                new QuestionNode()
                {
                    Question = "Interesują cię systemy oszczędzania energii ?",
                    Id = 38,
                    LeftChild = 51,
                    RightChild = 52
                },
                new Node() {Label = "Lotnictwo i kosmonautyka", Id = 39},
                new Node() {Label = "Inżynieria pojazdów elektrycznych i hybrydowych", Id = 40},
                new Node() {Label = "Matematyka", Id = 41},
                new Node() {Label = "Informatyka", Id = 42},
                new Node() {Label = "Fizyka", Id = 43},
                new Node() {Label = "Chemia", Id = 44},
                new QuestionNode()
                {
                    Question = "Polityka wydaje ci się ciekawa ?",
                    Id = 45,
                    LeftChild = 53,
                    RightChild = 54,
                    IsFuzzy = true
                },
                new Node() {Label = "Historia", Id = 46},
                new Node() {Label = "Ekonomia", Id = 47},
                new Node() {Label = "Finanse i rachunkowość", Id = 48},
                new Node() {Label = "Energetyka", Id = 49},
                new Node() {Label = "Inżynieria biomedyczna", Id = 50},
                new Node() {Label = "Budownictwo", Id = 51},
                new Node() {Label = "Inżynieria środowiska", Id = 52},
                new Node() {Label = "Socjologia", Id = 53},
                new Node() {Label = "Politologia", Id = 54}
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
            if (questionNode != null && _decisionTree.Answers.Count() < 5)
            {
                questionLabel.Content = questionNode.Question;
                if (questionNode.IsFuzzy)
                {
                    answerSlider.Visibility = Visibility.Visible;
                    okSliderAnswerButton.Visibility = Visibility.Visible;
                    sliderLabel.Visibility = Visibility.Visible;

                    yesAnswerButton.Visibility = Visibility.Collapsed;
                    noAnswerButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    answerSlider.Visibility = Visibility.Collapsed;
                    okSliderAnswerButton.Visibility = Visibility.Collapsed;
                    sliderLabel.Visibility = Visibility.Collapsed;

                    yesAnswerButton.Visibility = Visibility.Visible;
                    noAnswerButton.Visibility = Visibility.Visible;
                }
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
            var answers = _decisionTree.Answers.OrderByDescending(a => a.Rank).Take(5).ToList();
            var message = "TOP ANSWERS\n\n";
            for (int i = 0; i < answers.Count; i++)
                message += $"{i + 1}. {answers[i].Label} : {answers[i].Rank:0.00} \n";

            resultTextBlock.Text = message;
        }


        private void okSliderAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            var answer = Math.Round(answerSlider.Value, 2);
            _decisionTree.ProvideAnswer(1.0 - answer, answer);
            UpdateSet();
        }

        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            resultGrid.Visibility = Visibility.Collapsed;
            controlGrid.Visibility = Visibility.Visible;
            UpdateSet();
        }

        private void answerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sliderLabel.Content = Math.Round(answerSlider.Value, 2);
        }
    }
}
