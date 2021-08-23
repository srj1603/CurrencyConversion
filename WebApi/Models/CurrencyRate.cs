using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class CurrencyRate
    {
        public string From { get; set; }
        public string To { get; set; }
        public double Amount { get; set; }
        //public double convertedamount { get; set; }
      
    }
   
}

