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

        // Begin or cleared state
        bool allEmpty = true;

        // Used for the memory
        double? memoryValue = null;

        CultureInfo nlEuro = new CultureInfo("nl-NL");

        Operation operation = Operation.None;

        // Alle keyboard input
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D0 || e.Key == Key.NumPad0) { button_0_Click(sender, e); }
            if (e.Key == Key.D1 || e.Key == Key.NumPad1) { button_1_Click(sender, e); }
            if (e.Key == Key.D2 || e.Key == Key.NumPad2) { button_2_Click(sender, e); }
            if (e.Key == Key.D3 || e.Key == Key.NumPad3) { button_3_Click(sender, e); }
            if (e.Key == Key.D4 || e.Key == Key.NumPad4) { button_4_Click(sender, e); }
            if (e.Key == Key.D5 || e.Key == Key.NumPad5) { button_5_Click(sender, e); }
            if (e.Key == Key.D6 || e.Key == Key.NumPad6) { button_6_Click(sender, e); }
            if (e.Key == Key.D7 || e.Key == Key.NumPad7) { button_7_Click(sender, e); }
            if (e.Key == Key.D8 || e.Key == Key.NumPad8) { button_8_Click(sender, e); }
            if (e.Key == Key.D9 || e.Key == Key.NumPad9) { button_9_Click(sender, e); }
            if (e.Key == Key.Add || e.Key == Key.OemPlus) { button_plus_Click(sender, e); }
            if (e.Key == Key.Subtract || e.Key == Key.OemMinus) { button_min_Click(sender, e); }
            if (e.Key == Key.Multiply) { button_multiply_Click(sender, e); }
            if (e.Key == Key.Divide) { button_divide_Click(sender, e); }
            if (e.Key == Key.Enter) { button_result_Click(sender, e); }
            if (e.Key == Key.Back) { button_del_Click(sender, e); }
            if (e.Key == Key.OemComma) { button_comma_Click(sender, e); }
        }

        // Alle button input
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
        private void button_comma_Click(object sender, RoutedEventArgs e) { ButtonInput(","); }
        
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
            // Alleen 1e invoer, wordt resultaat
            if (enteredValueEmpty == false && storedValueEmpty == true)
            {
                operation = Operation.None;
                Console.WriteLine("Only enteredValue");
                CalculateResult();
            }
            // Alleen storedValue, wordt resultaat
            else if (enteredValueEmpty == true && storedValueEmpty == false)
            {
                operation = Operation.None;
                Console.WriteLine("Only storedValue");
                CalculateResult();
            }
            // Meerdere invoeren, worden resultaat
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
            // % als linker (1e) invoer
            if (enteredValueEmpty == false && storedValueEmpty == true)
            {
                double percentageValue = double.Parse(enteredValue) / 100;
                enteredValue = percentageValue.ToString();
                Console.WriteLine("Percentage value = " + enteredValue);
                screenLabel.Content = enteredValue;
            }
            // % als rechter (2e) invoer
            else if (enteredValueEmpty == false && storedValueEmpty == false)
            {
                switch (operation)
                {
                    case Operation.Plus:
                        double plusPercentageCalc = (storedValue / 100) * double.Parse(enteredValue);
                        Console.WriteLine("plusPercentageCalc = " + plusPercentageCalc);
                        enteredValue = plusPercentageCalc.ToString();
                        break;
                    case Operation.Min:
                        double minPercentageCalc = (storedValue / 100) * double.Parse(enteredValue);
                        Console.WriteLine("minPercentageCalc = " + minPercentageCalc);
                        enteredValue = minPercentageCalc.ToString();
                        break;
                    case Operation.Multiply:
                        double multiplyPercentageCalc = double.Parse(enteredValue) / 100;
                        Console.WriteLine("multiplyPercentageCalc = " + multiplyPercentageCalc);
                        enteredValue = multiplyPercentageCalc.ToString();
                        break;
                    case Operation.Divide:
                        double dividePercentageCalc = double.Parse(enteredValue) / 100;
                        Console.WriteLine("dividePercentageCalc = " + dividePercentageCalc);
                        enteredValue = dividePercentageCalc.ToString();
                        break;
                    default:
                        break;
                }
                screenLabel.Content = enteredValue;
            }
            else
            {
                // do nothing
                Console.WriteLine("Error");
            }
            
        }

        private void button_euro_Click(object sender, RoutedEventArgs e)
        {
            // switch om alles weer te geven in euro's. Berekeningen blijven echter hetzelfde en gebruiken de oude waardes!
            if (useEuros == false)
            {
                useEuros = true;

                // laat € 0 zien als geen invoeren
                if (allEmpty)
                {
                    Console.WriteLine("No input, set to 0");
                    screenLabel.Content = 0.ToString("C", nlEuro);
                }

                // Alleen een enteredValue...
                else if (enteredValueEmpty == false)
                {
                    enteredEuroValue = double.Parse(enteredValue).ToString("C", nlEuro);
                    Console.WriteLine("enteredEuroValue " + enteredEuroValue);
                    // laat euro's zien op display
                    screenLabel.Content = enteredEuroValue;
                }
                // Alleen een storedValue...
                else if (storedValueEmpty == false)
                {
                    enteredEuroValue = (storedValue).ToString("C", nlEuro);
                    Console.WriteLine("storedEuroValue " + enteredEuroValue);
                    // laat euro's zien op display
                    screenLabel.Content = enteredEuroValue;
                }
                // Alleen een endResult...
                else if (endResultEmpty == false)
                {
                    enteredEuroValue = (endResult).ToString("C", nlEuro);
                    Console.WriteLine("endResultEuroValue " + enteredEuroValue);
                    // laat euro's zien op display
                    screenLabel.Content = enteredEuroValue;
                }
            }
            else
            {
                useEuros = false;

                // Laat weer getallen zien op display
                // wanneer geen invoer
                if (allEmpty)
                {
                    screenLabel.Content = 0;
                }
                // wanneer alleen storedValue
                else if (enteredValueEmpty == true && storedValueEmpty == false)
                {
                    screenLabel.Content = storedValue;
                }
                // Alleen een endResult...
                else if (endResultEmpty == false)
                {
                    screenLabel.Content = endResult;
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

        private void button_plusormin_Click(object sender, RoutedEventArgs e) 
        {
            if (enteredValueEmpty == false)
            {
                double negativeValue = double.Parse(enteredValue) * -1;
                Console.WriteLine(negativeValue);
                enteredValue = negativeValue.ToString();
                Console.WriteLine(enteredValue);
                screenLabel.Content = enteredValue;
            }
            else if (storedValueEmpty == false)
            {
                double negativeValue = storedValue * -1;
                Console.WriteLine(negativeValue);
                storedValue = negativeValue;
                Console.WriteLine(storedValue);
                screenLabel.Content = storedValue;
            }
            else if (endResultEmpty == false)
            {
                double negativeValue = endResult * -1;
                Console.WriteLine(negativeValue);
                endResult = negativeValue;
                Console.WriteLine(endResult);
                screenLabel.Content = endResult;
            }
        }

        /*
        MC = Memory Clear sets the memory to 0
        MR = Memory Recall uses the number in memory, acts as if you had keyed in that number yourself
        MS = Memory Store puts the number on the display into the memory
        M+ = Memory Add takes the number on the display, adds it to the memory, and puts the result into memory
        */

        private void button_mr_Click(object sender, RoutedEventArgs e)
        {
            if (memoryValue != null)
            {
                enteredValue = memoryValue.ToString();
                screenLabel.Content = enteredValue;
                enteredValueEmpty = false;
            }
        }

        private void button_mPlus_Click(object sender, RoutedEventArgs e)
        {
            // Als enteredValue op display
            if (storedValueEmpty == true && enteredValueEmpty == false)
            {
                memoryValue += double.Parse(enteredValue);
                Console.WriteLine("memoryvalue = " + memoryValue);
            }
            // Als storedValue op display
            if (storedValueEmpty == false && enteredValueEmpty == true)
            {
                memoryValue += storedValue;
                Console.WriteLine("memoryvalue = " + memoryValue);
            }
            // Als endResult op display
            if (endResultEmpty == false && enteredValueEmpty == true)
            {
                memoryValue += endResult;
                Console.WriteLine("memoryvalue = " + memoryValue);
            }
        }

        private void button_mMin_Click(object sender, RoutedEventArgs e)
        {
            // Als enteredValue op display
            if (storedValueEmpty == true && enteredValueEmpty == false)
            {
                memoryValue -= double.Parse(enteredValue);
                Console.WriteLine("memoryvalue = " + memoryValue);
            }
            // Als storedValue op display
            if (storedValueEmpty == false && enteredValueEmpty == true)
            {
                memoryValue -= storedValue;
                Console.WriteLine("memoryvalue = " + memoryValue);
            }
            // Als endResult op display
            if (endResultEmpty == false && enteredValueEmpty == true)
            {
                memoryValue -= endResult;
                Console.WriteLine("memoryvalue = " + memoryValue);
            }
        }

        private void button_ms_Click(object sender, RoutedEventArgs e)
        {
            // Als enteredValue op display
            if (storedValueEmpty == true && enteredValueEmpty == false)
            {
                memoryValue = double.Parse(enteredValue);
                Console.WriteLine("memoryvalue = " + memoryValue);
            }
            // Als storedValue op display
            if (storedValueEmpty == false && enteredValueEmpty == true)
            {
                memoryValue = storedValue;
                Console.WriteLine("memoryvalue = " + memoryValue);
            }
            // Als endResult op display
            if (endResultEmpty == false && enteredValueEmpty == true)
            {
                memoryValue = endResult;
                Console.WriteLine("memoryvalue = " + memoryValue);
            }

        }

        private void button_mc_Click(object sender, RoutedEventArgs e)
        {
            memoryValue = null;
        }


        // METHODS
        private void ButtonInput(string value)
        {
            enteredValue += value;
            enteredValueEmpty = false;
            allEmpty = false;

            // omzetten naar euro's als aangeswitched
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
                Console.WriteLine("#2 - storedValue = " + storedValue);
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
            Console.WriteLine("Clearing (process)enteredValue = " + enteredValue);
        }

        private void CalculateResult()
        {
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
                Console.WriteLine("Clearing (calculate)enteredValue = " + enteredValue);

                // Leeg storedValue
                storedValue = 0;
                storedValueEmpty = true;

                // Laatste resultaat wordt bewaard onder 'endResult' en boolean switch
                endResultEmpty = false;

                // laat zien op display, euro's of getallen
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
