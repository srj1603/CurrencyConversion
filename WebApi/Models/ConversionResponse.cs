using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ConversionResponse
    {
        public List<CurrencyRateResponse> rates {get; set;}
      
    }
    public class CurrencyRateResponse
    {
        public string From { get; set; }
        public string To { get; set; }
        public double Amount { get; set; }
        public double ConvertedAmount { get; set; }

    }
}
