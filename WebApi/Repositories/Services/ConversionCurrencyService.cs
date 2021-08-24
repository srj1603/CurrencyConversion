using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repositories.Interface;
using WebApi.ViewModel;

namespace WebApi.Repositories.Services
{
    public class ConversionCurrencyService : IConversionCurrency
    {
        public double ExchangeRateService(string from, String to)
        {
            String urlString = "https://v6.exchangerate-api.com/v6/4cb40d1991f183f57e8b927c/latest/" + from;
            var webClient = new WebClient();
            var responsejson = webClient.DownloadString(urlString);
            ExchangeRateApiResponse conversionRateApi = JsonConvert.DeserializeObject<ExchangeRateApiResponse>(responsejson);
            ConversionRates conversionRates = conversionRateApi.conversion_rates;
            var currencyConvertTo = to;
            var getApiInfo = conversionRates.GetType().GetProperty(currencyConvertTo);
            var getRate = getApiInfo.GetValue(conversionRates, null);
            string rateValue = getRate.ToString();
            double amountConvert = Convert.ToDouble(rateValue);
            return amountConvert;
         
            
        }

    }

        
 }


