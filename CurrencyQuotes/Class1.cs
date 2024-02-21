using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CurrencyQuotes
{
   
    internal class Class1
    {
        public string baseCurrency = "RUB"; // Основная валюта, в которой будем отображать котировку
        public string targetCurrency = "USD"; // Валюта, котировку которой хотите получить

        public string ResponseApi(string baseCurrency, string targetCurrency)
        {
            string apiUrl = $"https://api.exchangerate-api.com/v4/latest/{baseCurrency}";

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    string jsonData = webClient.DownloadString(apiUrl);
                    CurrencyData data = JsonConvert.DeserializeObject<CurrencyData>(jsonData);

                    if (data != null && data.Rates.ContainsKey(targetCurrency))
                    {
                        decimal rate = data.Rates[targetCurrency];
                        decimal convertedRate = 1 / rate;
                        return($"{targetCurrency},{convertedRate}");
                    }
                    else
                    {
                        return("Данные о котировке не найдены.");
                    }
                }
            }
            catch (Exception ex)
            {
                return($"Произошла ошибка: {ex.Message}");
            }
            return null;
        }
    } 
}
