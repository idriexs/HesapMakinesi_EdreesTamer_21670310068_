namespace HesapMakinesi_EdreesTamer_21670310068
{
    public partial class MainPage : ContentPage
    {
        double currentNumber = 0;
        double result = 0;
        string currentOperator = null;
        bool isNewEntry = true;

        public MainPage()
        {
            InitializeComponent();
        }

        void OnClearClicked(object sender, EventArgs e)
        {
            Display.Text = "0";
            currentNumber = 0;
            result = 0;
            currentOperator = null;
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

            if ((Display.Text == "0" && pressed != ".") || isNewEntry)
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
                if (currentOperator != null && !isNewEntry)
                {
                    CalculateResult();
                }
                else
                {
                    result = currentNumber; 
                }
            }

            currentOperator = pressedOperator;
            isNewEntry = true;
        }

        void OnEqualClicked(object sender, EventArgs e)
        {
            if (currentOperator != null && double.TryParse(Display.Text, out currentNumber))
            {
                CalculateResult(); 
                currentOperator = null;
                Display.Text = result.ToString();
                isNewEntry = true;
            }
        }

        void CalculateResult()
        {
            switch (currentOperator)
            {
                case "+":
                    result += currentNumber;
                    break;
                case "−":
                    result -= currentNumber;
                    break;
                case "×":
                    result *= currentNumber;
                    break;
                case "÷":
                    if (currentNumber == 0)
                    {
                        DisplayAlert("Error", "Cannot divide by zero", "OK");
                        return;
                    }
                    result /= currentNumber;
                    break;
                case "mod":
                    if (currentNumber == 0)
                    {
                        DisplayAlert("Error", "Cannot mod by zero", "OK");
                        return;
                    }
                    result %= currentNumber; 
                    break;
            }

            Display.Text = result.ToString(); 
            isNewEntry = true;
        }
    }
}
