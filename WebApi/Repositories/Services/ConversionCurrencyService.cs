
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
        public async Task<ExchangeRate> GetExchangeRate(string From, String To)
        {
            try
            {
                ConversionRequest conversion = new ConversionRequest();
                ExchangeRate exchangeRate = new ExchangeRate();
                String urlString = "https://v6.exchangerate-api.com/v6/4cb40d1991f183f57e8b927c/latest/" + From;
                var webClient = new WebClient();
                var responsejson = await webClient.DownloadStringTaskAsync(urlString);
                ExchangeRateApiResponse conversionRateApi = JsonConvert.DeserializeObject<ExchangeRateApiResponse>(responsejson);
                ConversionRates conversionRates = conversionRateApi.conversion_rates;
                var currencyConvertTo = To;
                var getApiInfo = conversionRates.GetType().GetProperty(currencyConvertTo);
                var getRate = getApiInfo.GetValue(conversionRates, null);
                string rateValue = getRate.ToString();
                double amountConvert = Convert.ToDouble(rateValue);
                exchangeRate.From = From;
                exchangeRate.To = To;
                exchangeRate.ConvertedRate = amountConvert;
                return exchangeRate;


            }
            catch (Exception ex)
            {

                throw;
            }


        
        }

    }


}


