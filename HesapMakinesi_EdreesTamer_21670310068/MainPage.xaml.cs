using System.Collections.Generic;

namespace HesapMakinesi_EdreesTamer_21670310068
{
    public partial class MainPage : ContentPage
    {
        List<double> numbers = new List<double>();
        List<string> operators = new List<string>();
        double currentNumber = 0;
        bool isNewEntry = true;

        public MainPage()
        {
            InitializeComponent();
        }

        void OnClearClicked(object sender, EventArgs e)
        {
            Display.Text = "0";
            numbers.Clear();
            operators.Clear();
            currentNumber = 0;
            isNewEntry = true;
        }

        void OnToggleSignClicked(object sender, EventArgs e)
        {
            if (double.TryParse(Display.Text, out currentNumber))
            {
                currentNumber = -currentNumber;
                Display.Text = currentNumber.ToString();
            }
        }

        void OnNumberClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;

            if (isNewEntry)
            {
                Display.Text = "";
                isNewEntry = false;
            }

            Display.Text += pressed;
        }

        void OnDecimalClicked(object sender, EventArgs e)
        {
            if (!Display.Text.Contains("."))
            {
                Display.Text += ".";
                isNewEntry = false;
            }
        }

        void OnOperatorClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressedOperator = button.Text;

            if (double.TryParse(Display.Text, out currentNumber))
            {
                numbers.Add(currentNumber);
                operators.Add(pressedOperator);
            }

            
            isNewEntry = true;
        }

        void OnEqualClicked(object sender, EventArgs e)
        {
            if (double.TryParse(Display.Text, out currentNumber))
            {
                numbers.Add(currentNumber);
            }

            CalculateWithPrecedence();
            Display.Text = numbers[0].ToString();
            numbers.Clear();
            operators.Clear();
            isNewEntry = true;
        }

        void CalculateWithPrecedence()
        {
            
            for (int i = 0; i < operators.Count; i++)
            {
                if (operators[i] == "×" || operators[i] == "÷")
                {
                    double result = operators[i] == "×"
                        ? numbers[i] * numbers[i + 1]
                        : numbers[i] / numbers[i + 1];
                    numbers[i] = result;
                    numbers.RemoveAt(i + 1);
                    operators.RemoveAt(i);
                    i--;
                }
            }

            
            for (int i = 0; i < operators.Count; i++)
            {
                if (operators[i] == "+")
                    numbers[i] += numbers[i + 1];
                else if (operators[i] == "−")
                    numbers[i] -= numbers[i + 1];
                numbers.RemoveAt(i + 1);
                operators.RemoveAt(i);
                i--;
            }
        }
    }
}
