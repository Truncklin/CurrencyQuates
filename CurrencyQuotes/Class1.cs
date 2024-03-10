using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CurrencyQuotes
{

    internal class Class1
    {
        public string baseCurrency; // Основная валюта, в которой будем отображать котировку
        public string targetCurrency; // Валюта, котировку которой хотите получить


        public CurrencyData ResponseApiData(string baseCurrency)
        {

            string apiUrl = $"https://api.exchangerate-api.com/v4/latest/{baseCurrency}";

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    string jsonData = webClient.DownloadString(apiUrl);
                    CurrencyData data = JsonConvert.DeserializeObject<CurrencyData>(jsonData);

                    List<string> dataKey = new List<string>();
                    foreach (var mass in data.add)
                    {
                        string key = $"{mass.Key}";
                        dataKey.Add(key);
                    }
                    MainWindow._mainWindow.QuotaTwo.ItemsSource = dataKey;
                    return data;
                }
            }
            catch (Exception ex)
            {
                MainWindow._mainWindow.TextError.Text = $"Произошла ошибка:\n {ex.Message}";
                return null;
            }
        }
        public string ResponseApi(CurrencyData data)
        {
            try
            {
                if (data != null)
                {
                    string targetCurrency = MainWindow._mainWindow.QuotaTwo.SelectedItem.ToString();
                    float rate = data.add[targetCurrency];
                    Console.WriteLine(rate);
                    float convertedRate = 1.0f / rate;
                    Console.WriteLine(convertedRate);
                    Console.WriteLine(Math.Round(convertedRate, 4));
                    string currency = $"1\n{targetCurrency} {Math.Round(convertedRate, 4)}\n{baseCurrency}";
                    return currency;
                }
                else
                {
                    MainWindow._mainWindow.TextError.Text = $"Неверный индекс квоты";
                    return "";
                }
            }
            catch (Exception ex)
            {
                MainWindow._mainWindow.TextError.Text = $"Произошла ошибка:\n {ex.Message}";
                return "";
            }
        }
    }
}
