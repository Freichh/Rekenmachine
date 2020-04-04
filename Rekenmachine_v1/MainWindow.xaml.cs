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

namespace Rekenmachine_v1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // default 0 state
            screenLabel.Content = 0;
        }

        // Invoer opslag variabele
        string enteredValue = "";

        // Opslag voor berekeningen
        double storedValue = 0;

        // Resultaat van laatste berekening
        double endResult = 0;

        Operation operation = Operation.None;
 
        private void button_1_Click(object sender, RoutedEventArgs e) { ButtonInput("1"); }
        private void button_2_Click(object sender, RoutedEventArgs e) { ButtonInput("2"); }
        private void button_3_Click(object sender, RoutedEventArgs e) { ButtonInput("3"); }
        private void button_4_Click(object sender, RoutedEventArgs e) { ButtonInput("4"); }
        private void button_5_Click(object sender, RoutedEventArgs e) { ButtonInput("5"); }
        private void button_6_Click(object sender, RoutedEventArgs e) { ButtonInput("6"); }
        private void button_7_Click(object sender, RoutedEventArgs e) { ButtonInput("7"); }
        private void button_8_Click(object sender, RoutedEventArgs e) { ButtonInput("8"); }
        private void button_9_Click(object sender, RoutedEventArgs e) { ButtonInput("9"); }
        private void button_0_Click(object sender, RoutedEventArgs e) { ButtonInput("0"); }

        private void button_plus_Click(object sender, RoutedEventArgs e)
        {
            operation = Operation.Plus;
            Console.WriteLine("Operation " + operation);
            ProcessInput();
        }

        private void button_min_Click(object sender, RoutedEventArgs e)
        {
            ProcessInput();
            operation = Operation.Min;
            Console.WriteLine("Operation " + operation);
        }

        private void button_multiply_Click(object sender, RoutedEventArgs e)
        {
            ProcessInput();
            operation = Operation.Multiply;
            Console.WriteLine("Operation " + operation);
        }

        private void button_divide_Click(object sender, RoutedEventArgs e)
        {
            ProcessInput();
            operation = Operation.Divide;
            Console.WriteLine("Operation " + operation);
        }

        private void button_result_Click(object sender, RoutedEventArgs e) 
        { 
            CalculateResult(); 
        }

        private void button_clear_Click(object sender, RoutedEventArgs e)
        {
            screenLabel.Content = 0;
            enteredValue = "";
            storedValue = 0;
            endResult = 0;
        }

        private void button_clearentry_Click(object sender, RoutedEventArgs e)
        {
            enteredValue = "";
            screenLabel.Content = 0;
        }

        private void button_percent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_euro_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_del_Click(object sender, RoutedEventArgs e)
        {
            if (enteredValue.Length > 0)
            {
                Console.WriteLine("Backspace, old " + enteredValue);
                enteredValue = enteredValue.Remove(enteredValue.Length-1);
                Console.WriteLine("New enteredValue: " + enteredValue);

                // laat zien op display
                screenLabel.Content = null + enteredValue;

                if (enteredValue.Length == 0)
                {
                    screenLabel.Content = 0;
                    enteredValue = "";
                }
            }
        }


        // METHODS
        private void ButtonInput(string value)
        {
            enteredValue += value;
            Console.WriteLine("enteredValue: " + enteredValue);

            // laat zien op display
            screenLabel.Content = null + enteredValue;
        }

        private void ProcessInput()
        {
            // maak 0 als vanaf 0 wordt berekend zonder 1e invoer
            if (enteredValue == "" && storedValue == 0)
            {
                Console.WriteLine("No input, set 0:");
                enteredValue = "0";
            }

            // Converteer invoer naar double en sla op als nog leeg is onder storedValue
            if (storedValue == 0)
            {
                storedValue += double.Parse(enteredValue);
                Console.WriteLine("storedValue: " + storedValue);

                // set operation?
            }

            // bereken als er 2 getallen zijn en er een operatie i.p.v. '=' wordt gekozen 
            else if (enteredValue.Length > 0)
            {
                Console.WriteLine("Calculate: " + enteredValue + " and " + storedValue);
                CalculateResult();
            }
            else
            {
                // do nothing
            }

            // Leeg invoer
            enteredValue = "";
            Console.WriteLine("clear enteredValue: " + enteredValue);
        }

        private void CalculateResult()
        {
            // Bereken alleen wanneer er een enteredValue en storedValue zijn
            if (enteredValue.Length > 0)
            {
                Console.WriteLine("Result operation " + operation);
                switch (operation)
                {
                    case Operation.Plus:
                        endResult = storedValue + double.Parse(enteredValue);
                        Console.WriteLine("endResult Plus: " + endResult);
                        break;
                    case Operation.Min:
                        endResult = storedValue - double.Parse(enteredValue);
                        Console.WriteLine("endResult Min: " + endResult);
                        break;
                    case Operation.Multiply:
                        endResult = storedValue * double.Parse(enteredValue);
                        Console.WriteLine("endResult Multi: " + endResult);
                        break;
                    case Operation.Divide:
                        endResult = storedValue / double.Parse(enteredValue);
                        Console.WriteLine("endResult Divide: " + endResult);
                        break;
                    case Operation.Percent:
                        endResult = storedValue * double.Parse(enteredValue);
                        Console.WriteLine("endResult Percent: " + endResult);
                        break;
                    default:
                        break;
                }

                // Leeg invoer
                enteredValue = "";

                // Sla op in geheugen voor verdere berekening
                storedValue = endResult;

                // laat zien op display
                screenLabel.Content = endResult;
            }
        }

        enum Operation
        {
            None,
            Plus,
            Min,
            Multiply,
            Divide,
            Percent
        }


    }
}
