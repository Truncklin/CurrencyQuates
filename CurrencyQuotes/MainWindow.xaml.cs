using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace CurrencyQuotes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string placeholder_text;
        Class1 response = new Class1();
        public MainWindow()
        {
            InitializeComponent();

            placeholder_text = QuotaOne.Text;
            QuotaOne.GotFocus += new RoutedEventHandler(HidePlaceholder);
            QuotaOne.LostFocus += new RoutedEventHandler(ShowPlaceholder);
            QuotaTwo.GotFocus += new RoutedEventHandler(HidePlaceholder);
            QuotaTwo.LostFocus += new RoutedEventHandler(ShowPlaceholder);

            TrigerOne.Click += (sender, e) =>
            {
                if (QuotaTwo.Text == placeholder_text || QuotaTwo.Text == "")
                {
                    QuotaTwo.Text = "RUB";
                    string words = response.ResponseApi(QuotaOne.Text, QuotaTwo.Text);
                    string[] mass = words.Split(',');
                    Text1.Text = mass[0];
                    Text2.Text = mass[1];
                }
            };
            TrigerTwo.Click += (sender, e) =>
            {
                if (QuotaOne.Text == placeholder_text || QuotaOne.Text == "")
                {
                    QuotaOne.Text = "RUB";
                    Text2.Text = response.ResponseApi(QuotaOne.Text, QuotaTwo.Text);
                }
            };

        }

        public void HidePlaceholder(object sender, EventArgs e)
        {
            if (sender == QuotaOne)
            {
                if (QuotaOne.Text == placeholder_text)
                {
                    QuotaOne.Text = "";
                }
            }
            if (sender == QuotaTwo)
            {
                if (QuotaTwo.Text == placeholder_text)
                {
                    QuotaTwo.Text = "";
                }
            }
        }

        public void ShowPlaceholder(object sender, EventArgs e)
        {
            if (sender == QuotaOne)
            {
                if (string.IsNullOrWhiteSpace(QuotaOne.Text))
                    QuotaOne.Text = placeholder_text;
            }
            if (sender == QuotaTwo)
            {
                if (string.IsNullOrWhiteSpace(QuotaTwo.Text))
                    QuotaTwo.Text = placeholder_text;
            }
        }
    
        private void QuotaOne_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender == QuotaOne) QuotaOne.Clear();
            if (sender == QuotaTwo) QuotaTwo.Clear();

        }
        private void QuotaTwo_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender == QuotaOne) QuotaOne.Clear();
            if (sender == QuotaTwo) QuotaTwo.Clear();
        }
    }
}
