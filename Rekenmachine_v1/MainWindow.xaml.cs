using System;
using System.Collections.Generic;
using System.Globalization;
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
        bool enteredValueEmpty = true;

        string enteredEuroValue = "";
        bool useEuros = false;

        // Opslag voor berekeningen
        double storedValue = 0;
        bool storedValueEmpty = true;

        // Resultaat van laatste berekening
        double endResult = 0;
        bool endResultEmpty = true;

        bool allEmpty = true;

        CultureInfo nlEuro = new CultureInfo("nl-NL");

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
            ProcessInput();
            operation = Operation.Plus;
            Console.WriteLine("Operation " + operation);
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
            // Alleen 1e invoer wordt resultaat
            if (enteredValueEmpty == false && storedValueEmpty == true)
            {
                operation = Operation.None;
                Console.WriteLine("Operation " + operation);
                CalculateResult();
            }
            // Meerdere invoer
            else
            {
                CalculateResult();
                // Operation default state
                operation = Operation.None;
                Console.WriteLine("Operation " + operation);
            }
        }

        private void button_clear_Click(object sender, RoutedEventArgs e)
        {
            enteredValue = "";
            enteredValueEmpty = true; 
            storedValue = 0;
            storedValueEmpty = true;
            endResult = 0;
            endResultEmpty = true;
            allEmpty = true;
            if (useEuros)
            {
                screenLabel.Content = 0.ToString("C", nlEuro);
            }
            else
            {
                screenLabel.Content = 0;
            }
        }

        private void button_clearentry_Click(object sender, RoutedEventArgs e)
        {
            enteredValue = "";
            enteredValueEmpty = true;
            if (useEuros)
            {
                screenLabel.Content = 0.ToString("C", nlEuro);
            }
            else
            {
                screenLabel.Content = 0;
            }
        }

        private void button_percent_Click(object sender, RoutedEventArgs e)
        {
            // % als linker invoer
            if (enteredValue.Length > 0 && storedValue == 0)
            {
                double percentageValue = double.Parse(enteredValue) / 100;
                enteredValue = percentageValue.ToString();
                Console.WriteLine("Percentage value = " + enteredValue);
                screenLabel.Content = enteredValue;
            }
            // % als rechter invoer
            else if (enteredValue.Length > 0 && storedValue > 0)
            {
                switch (operation)
                {
                    case Operation.Plus:
                        double percentageCalc = (storedValue / 100) * double.Parse(enteredValue);
                        Console.WriteLine(percentageCalc);
                        enteredValue = percentageCalc.ToString();
                        screenLabel.Content = enteredValue;
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
            }
            else
            {
                // do nothing
                Console.WriteLine("Error");
            }
            
        }

        private void button_euro_Click(object sender, RoutedEventArgs e)
        {
            // switch to toggle usage and display of euro currency
            if (useEuros == false)
            {
                useEuros = true;

                // laat € 0 zien als switch zonder invoeren
                if (enteredValue == "" && storedValue == 0)
                {
                    screenLabel.Content = 0.ToString("C", nlEuro);
                }
                // Als er alleen een storedValue is, zoals bij endResult...
                else if (enteredValue == "" && storedValue != 0)
                {
                    enteredEuroValue = (storedValue).ToString("C", nlEuro);
                    Console.WriteLine("storedEuroValue " + enteredEuroValue);

                    // laat euro's zien op display
                    screenLabel.Content = enteredEuroValue;
                }
                // Als er een storedValue en enteredValue zijn...
                else
                {
                    enteredEuroValue = double.Parse(enteredValue).ToString("C", nlEuro);
                    Console.WriteLine("enteredEuroValue " + enteredEuroValue);
                    // laat euro's zien op display
                    screenLabel.Content = enteredEuroValue;
                }
            }
            else
            {
                useEuros = false;

                // Laat weer getallen zien op display
                // wanneer geen invoer
                if (enteredValue == "" && storedValue == 0)
                {
                    screenLabel.Content = 0;
                }
                // wanneer alleen storedValue
                else if (enteredValue == "" && storedValue != 0)
                {
                    screenLabel.Content = storedValue;
                }
                // wanneer overige
                else
                {
                    screenLabel.Content = enteredValue;
                }
            }

            Console.WriteLine("Using euros = " + useEuros);
        }

        private void button_del_Click(object sender, RoutedEventArgs e)
        {
            // Backspace t/m laatste getal
            if (enteredValue.Length > 0)
            {
                Console.WriteLine("Backspace, old " + enteredValue);
                enteredValue = enteredValue.Remove(enteredValue.Length-1);
                Console.WriteLine("New enteredValue: " + enteredValue);

                // laat zien op display
                screenLabel.Content = enteredValue;

                // Maak invoer leeg en laat 0 zien bij backspace laatste getal
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
            enteredValueEmpty = false;
            allEmpty = false;

            if (useEuros == true)
            {
                enteredEuroValue = double.Parse(enteredValue).ToString("C", nlEuro);
                Console.WriteLine("enteredEuroValue " + enteredEuroValue);

                // laat euro's zien op display
                screenLabel.Content = enteredEuroValue;
            }
            else
            {
                // laat getallen zien op display
                screenLabel.Content = enteredValue;
                Console.WriteLine("enteredValue " + enteredValue);
            }

        }

        private void ProcessInput()
        {
            // Als vanaf 0 wordt berekend zonder 1e invoer (enteredValue): maak storedValue 0 en wacht... 
            if (allEmpty)
            {
                Console.WriteLine("#1 - No input given, set storedValue to 0: ");
                storedValue = 0;
                storedValueEmpty = false;
                allEmpty = false;
            }

            // Als er al een 1e invoer is: converteer invoer naar double, en sla op onder storedValue als die nog leeg is en wacht...
            if (enteredValueEmpty == false && storedValueEmpty == true) // && endResultEmpty == true
            {
                storedValue += double.Parse(enteredValue);
                Console.WriteLine("#2 - storedValue = enteredValue " + storedValue);
                storedValueEmpty = false;
            }
            // Anders, bereken als er 2 getallen zijn en er een 'operatie' i.p.v. '=' wordt gekozen 
            else if (enteredValueEmpty == false && storedValueEmpty == false)
            {
                Console.WriteLine("#3 - Calculate: " + enteredValue + " and " + storedValue);
                CalculateResult();
            }

            // Als er al een endResult is en direct vervolgoperatie: zet over in storedValue en wacht op invoer (enteredValue)
            if (endResultEmpty == false && enteredValueEmpty == true)
            {
                storedValue = endResult;
                storedValueEmpty = false;
                Console.WriteLine("#4 - endResult >> storedValue = " + storedValue);
                endResult = 0;
                endResultEmpty = true;
            }

            // Leeg invoer en wacht op nieuwe
            enteredValue = "";
            enteredValueEmpty = true;
            Console.WriteLine("Clearing (process), enteredValue = " + enteredValue);
        }

        private void CalculateResult()
        {
            // Bereken alleen wanneer er een enteredValue en storedValue zijn
            if (enteredValueEmpty == false)
            {
                switch (operation)
                {
                    case Operation.None:
                        endResult = storedValue + double.Parse(enteredValue);
                        Console.WriteLine("None endResult = " + endResult);
                        break;
                    case Operation.Plus:
                        endResult = storedValue + double.Parse(enteredValue);
                        Console.WriteLine("Plus endResult = " + endResult);
                        break;
                    case Operation.Min:
                        endResult = storedValue - double.Parse(enteredValue);
                        Console.WriteLine("Min endResult = " + endResult);
                        break;
                    case Operation.Multiply:
                        endResult = storedValue * double.Parse(enteredValue);
                        Console.WriteLine("Multiply endResult = " + endResult);
                        break;
                    case Operation.Divide:
                        endResult = storedValue / double.Parse(enteredValue);
                        Console.WriteLine("Divide endResult = " + endResult);
                        break;
                    case Operation.Percent:
                        endResult = storedValue * double.Parse(enteredValue);
                        Console.WriteLine("Percent endResult = " + endResult);
                        break;
                    default:
                        break;
                }

                // Leeg invoer en wacht op nieuwe
                enteredValue = "";
                enteredValueEmpty = true;
                Console.WriteLine("Clearing (calculate), enteredValue = " + enteredValue);

                // Leeg storedValue
                storedValue = 0;
                storedValueEmpty = true;

                // Laatste resultaat wordt bewaard onder 'endResult'
                endResultEmpty = false;

                // laat zien op display
                if (useEuros)
                {
                    screenLabel.Content = endResult.ToString("C", nlEuro);
                }
                else
                {
                    screenLabel.Content = endResult;
                }
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
