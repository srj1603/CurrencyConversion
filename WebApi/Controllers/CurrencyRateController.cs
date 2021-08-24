using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repositories.Interface;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyRateController : ControllerBase
    {
        private readonly IConversionCurrency _ConversionCurrencyService;
        public CurrencyRateController(IConversionCurrency currencyConversion)
        {
            _ConversionCurrencyService = currencyConversion;
        }
        [HttpPost("GetConversionCurrencyRate")]
        public ConversionResponse GetConversionCurrencyRate(ConversionRequest req)
        {
            ConversionResponse res = new ConversionResponse();
            res.rates = new List<CurrencyRateResponse>();
            foreach (var rate in req.rates)
            {
                double conversionRate = _ConversionCurrencyService.ExchangeRateService(rate.From, rate.To);
                CurrencyRateResponse currres = new CurrencyRateResponse();
                currres.From = rate.From;
                currres.To = rate.To;
                currres.Amount = rate.Amount;
                currres.ConvertedAmount = rate.Amount * conversionRate;
                res.rates.Add(currres);
          
                
            }
            return res;
           
        }
    }

}
