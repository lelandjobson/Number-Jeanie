using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Number_Jeanie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Run_Click(object sender, RoutedEventArgs e)
        {
            // Code to run when I click the button
            var questionNumbers = ParseQuestion(tbx_Question.Text);
            var reducedNumber = AddUpDigits(questionNumbers.Sum());
            var pyra = Calculate(questionNumbers);
            var pyraAns = ShowWork(pyra); 
            AnswerQuestion(reducedNumber, pyraAns);
        }

        private void AnswerQuestion(int reducedTop, int pyraBot)
        {
            tb_JeanieAnswer.Text = string.Empty;
            tb_JeanieAnswer.Text = string.Format("Jeanie: Your answers are {0} and {1}.", reducedTop, pyraBot);
        }

        private int ShowWork(List<List<int>> pyramid)
        {
            tb_Answer.Text = string.Empty;

            foreach(var intL in pyramid)
            {
                foreach(var intItem in intL)
                {
                    tb_Answer.Text += intItem + " ";

                }

                tb_Answer.Text += System.Environment.NewLine;
            }
            pyramid.Reverse();

            return pyramid[0][0];
        }

        private int AddUpDigits(int numberToSplit)
        {
            // Shatter digit
            var numberStr = numberToSplit.ToString();
            int numberMemory = 0;

            while (numberStr.Length > 1)
            {
                numberMemory = 0;

                foreach (var numChar in numberStr.ToCharArray())
                {
                    numberMemory += int.Parse(numChar.ToString());
                }

                numberStr = numberMemory.ToString();
            }

            return int.Parse(numberStr);
        }

        private List<List<int>> Calculate(List<int> questionNumbers)
        {
            // Reduce to one number
            var reducedNumber = AddUpDigits(questionNumbers.Sum());

            // Set up number of rows
            // Number of rows is totalamt-1
            List<List<int>> numberPyra = new List<List<int>>();
            List<int> memoryNumber = new List<int>(questionNumbers);
            for (int i = 0; i<questionNumbers.Count; i++)
            {
                numberPyra.Add(memoryNumber);
                var newNumbers = CreateNumberRow(memoryNumber);
                memoryNumber = newNumbers;
            }

            return numberPyra;
        }

        private List<int> CreateNumberRow(List<int> previousNumberRow)
        {
            List<int> numberRow = new List<int>();

            // Add up pairs
            for (int i = 0; i < previousNumberRow.Count - 1; i++)
            {
                var addedNumber = previousNumberRow[i] + previousNumberRow[i + 1];
                if (addedNumber.ToString().Length > 1) { addedNumber = AddUpDigits(addedNumber); }
                numberRow.Add(addedNumber);
            }

            return numberRow;
        }

        private List<int> ParseQuestion(string question)
        {
            // List to store results
            var numbersOut = new List<int>();
            
            // Split the question into words
            var splitQuestion = question.Split(' ');

            // Count number of words
            numbersOut.Add(AddUpDigits(splitQuestion.Length));
            
            // Count the size of each string
            foreach(var word in splitQuestion)
            {
                if (word.Contains(' '))
                {
                    continue;
                }
                var wordLength = word.Length;
                if (word.Contains('?'))
                {
                    wordLength -= 1;
                }

                numbersOut.Add(wordLength);
            }

            // Return the numbers
            return numbersOut;

        }

    }
}
