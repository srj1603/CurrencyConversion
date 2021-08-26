using Jaeger.Thrift;
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
        public ConversionResponse GetExchangeAmount(ConversionRequest req)
        {
            ConversionResponse res = new ConversionResponse();
            res.rates = new List<CurrencyRateResponse>();
            List<CurrencyRate> List = req.rates;
            do
            {
                var splittted = List.Select((v, i) => new { val = v, idx = i })
                                   .GroupBy(x => x.idx / 3)
                                   .Select(g => g.Select(y => y.val).ToList())
                                   .ToList();

                foreach (var batchOfThree in splittted)
                {
                    List<Task<ExchangeRate>> tasks = new List<Task<ExchangeRate>>();
                    foreach (var rate in batchOfThree)
                    {

                        var t = _ConversionCurrencyService.GetExchangeRate(rate.From, rate.To);
                        tasks.Add(t);
                    }
                    Task[] listOfArray = tasks.ToArray();
                    Task.WaitAll(listOfArray);
                    foreach (var item in tasks)
                    {
                        ConversionRequest conversionRequest = new ConversionRequest();
                        ExchangeRate exgRate = item.Result;
                        var matchRequest = req.rates.Find(x => x.From == exgRate.From && x.To == exgRate.To);
                        var convertedAmount = matchRequest.Amount * exgRate.ConvertedRate;
                        CurrencyRateResponse currencyRateResponse = new CurrencyRateResponse();
                        currencyRateResponse.From = exgRate.From;
                        currencyRateResponse.To = exgRate.To;
                        currencyRateResponse.Amount = matchRequest.Amount;
                        currencyRateResponse.ConvertedAmount = exgRate.ConvertedRate * convertedAmount;
                        res.rates.Add(currencyRateResponse);

                    }


                }

                return res;
            } while (List.Count > 0);
           
        }


    }
}






