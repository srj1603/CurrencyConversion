using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ConversionRequest
    {
        public List<CurrencyRate> rates { get; set; }
    }
}
