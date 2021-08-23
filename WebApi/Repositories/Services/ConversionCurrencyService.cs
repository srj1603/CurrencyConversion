using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repositories.Interface;
using WebApi.ViewModel;

namespace WebApi.Repositories.Services
{
    public class ConversionCurrencyService : ICurrencyConversion
    {
     
        IEnumerable<CurrencyRateResponse> ICurrencyConversion.ConversionCurrencyRate(List<CurrencyRate> currencyRate)
        {
            try
            {
               
                List<CurrencyRateResponse> currencyRateResponses = new List<CurrencyRateResponse>();



                foreach (var item in currencyRate)
                {
                    String URLString = "https://v6.exchangerate-api.com/v6/4cb40d1991f183f57e8b927c/latest/" + item.From;
                    using (var webClient = new System.Net.WebClient())
                    {
                        var responsejson = webClient.DownloadString(URLString);
                        ExchangeRateApiResponse conversionRateApi = JsonConvert.DeserializeObject<ExchangeRateApiResponse>(responsejson);
                        ConversionRates conversionRates = conversionRateApi.conversion_rates;
                        var currencyConvertTo = item.To;
                        var info = conversionRates.GetType().GetProperty(currencyConvertTo);
                        var getRate = info.GetValue(conversionRates, null);
                        string valueRate = getRate.ToString();
                        double amountConvert = Convert.ToDouble(valueRate);
                        double convertedamount = amountConvert * item.Amount;

                        CurrencyRateResponse currencyConversionRateResponse = new CurrencyRateResponse();
                        //CurrencyConversion currencyConversion = new CurrencyConversion();
                        currencyConversionRateResponse.from = item.From;
                        currencyConversionRateResponse.to = item.To;
                        currencyConversionRateResponse.amount = item.Amount;
                        currencyConversionRateResponse.convertedamount = convertedamount;
                        currencyRateResponses.Add(currencyConversionRateResponse);
                       
                    }

                }

                return currencyRateResponses;


            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

