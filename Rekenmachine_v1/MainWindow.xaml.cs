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

            // Converteer naar double en sla laatste invoer op als nog leeg is
            if (storedValue == 0)
            {
                storedValue += double.Parse(enteredValue);
                Console.WriteLine("storedValue: " + storedValue);
            }
            else if (enteredValue.Length > 0)
            {
                CalculateResult();
            }
            else
            {
                // do nothing
            }

            // Leeg huidige invoer variabele en wacht op nieuwe
            enteredValue = "";

        }

        private void button_min_Click(object sender, RoutedEventArgs e)
        {

            operation = Operation.Min;
            Console.WriteLine("Operation " + operation);

            // Converteer naar double en sla laatste invoer op als nog leeg is
            if (storedValue == 0)
            {
                storedValue += double.Parse(enteredValue.ToString());
                Console.WriteLine("storedValue: " + storedValue);
            }
            else if (enteredValue.Length > 0)
            {
                CalculateResult();
            }
            else
            {
                // do nothing
            }

            // Leeg huidige invoer variabele en wacht op nieuwe
            enteredValue = "";

        }

        private void button_multiply_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_divide_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_result_Click(object sender, RoutedEventArgs e) 
        { 
            CalculateResult(); 
        }

        private void ButtonInput(string value)
        {
            // Sla tag 'invoer' op in stringbuilder
            enteredValue += value;
            Console.WriteLine("enteredValue: " + enteredValue);

            // laat zien op display
            screenLabel.Content = null + enteredValue;
        }

        private void CalculateResult()
        {
            // Bereken alleen wanneer er een enteredValue en storedValue zijn
            if (enteredValue.Length > 0 && storedValue > 0)
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
                        break;
                    case Operation.Divide:
                        break;
                    case Operation.Quadrant:
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
            Quadrant
        }

    }
}
