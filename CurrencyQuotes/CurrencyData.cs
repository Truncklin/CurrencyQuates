using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyQuotes
{
    internal class CurrencyData
    {
        [JsonProperty("rates")]
        public Dictionary<string, float> add { get; set; }
    }
}
