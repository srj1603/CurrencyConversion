﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class CurrencyRateResponse
    {
        public string from { get; set; }
        public string to { get; set; }
        public double amount { get; set; }
        public double convertedamount { get; set; }
    }
}
