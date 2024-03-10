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
using System.Windows.Markup;
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
        public static MainWindow _mainWindow;
        private string placeholder_text;
        CurrencyData data;

        Class1 response = new Class1();
        public MainWindow()
        {
            InitializeComponent();

            _mainWindow = this;
            placeholder_text = QuotaOne.Text;

            QuotaOne.GotFocus += new RoutedEventHandler(ShowPlaceholder);
            QuotaOne.LostFocus += new RoutedEventHandler(ShowPlaceholder);

            HintComboBox.Text = "Напишите валюту, чтобы увидеть список";

            TrigerOne.Click += (sender, e) =>
            {

               data = response.ResponseApiData(QuotaOne.Text);
               HintComboBox.Text = "Выберите подходящую валюту из выпадающего списка";
               QuotaTwo.SelectedIndex = 0;
            };

            QuotaTwo.SelectionChanged += (sender, e) =>
            {
                string words = response.ResponseApi(data);
                string[] mass = words.Split(' ');
                if (mass.Length > 1)
                {
                    Text1.Text = mass[0];
                    Text2.Text = $"{mass[1]}{QuotaOne.Text}";
                }
            };
        }


        public void ShowPlaceholder(object sender, EventArgs e)
        {
            var quota = sender as TextBox;

            if (quota != null && string.IsNullOrWhiteSpace(quota.Text))
                quota.Text = placeholder_text;
            else if (quota.Text == placeholder_text) quota.Text = "";

        }
    }
}
