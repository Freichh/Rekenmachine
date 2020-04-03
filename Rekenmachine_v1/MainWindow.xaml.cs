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
        StringBuilder enteredValue = new StringBuilder();

        // Opslag voor berekeningen
        double storedValue = 0;

        // Resultaat van laatste berekening
        double endResult = 0;
 
        private void button_1_Click(object sender, RoutedEventArgs e)
        {
            // Sla tag 'invoer' op in stringbuilder
            enteredValue.Append(button_1.Tag);
            Console.WriteLine("enteredValue: " + enteredValue);

            // show on display
            screenLabel.Content = null + enteredValue;

        }

        private void button_2_Click(object sender, RoutedEventArgs e)
        {

            enteredValue.Append(button_2.Tag);
            Console.WriteLine("enteredValue: " + enteredValue);

            screenLabel.Content = null + enteredValue;

        }

        private void button_3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_6_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_7_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_8_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_9_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_plus_Click(object sender, RoutedEventArgs e)
        {
            if (enteredValue.Length > 0)
            {
                // Sla huidige stringbuilder invoer op als double
                storedValue += double.Parse(enteredValue.ToString());
                Console.WriteLine("storedValue: " + storedValue);

                // Leeg huidige invoer variabele en wacht op nieuwe
                enteredValue.Clear();
            }
            else
            {
                // do nothing
            }


        }

        private void button_min_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_multiply_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_divide_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_result_Click(object sender, RoutedEventArgs e)
        {
            endResult = storedValue + double.Parse(enteredValue.ToString());
            Console.WriteLine("endResult: " + endResult);

            // Leeg invoer
            enteredValue.Clear().Append(0);

            // Sla op in geheugen voor verdere berekening
            storedValue = endResult;
            // laat zien op display
            screenLabel.Content = endResult;
        }


        private void StoreValue(StringBuilder passedValue)
        {


        }

    }
}
