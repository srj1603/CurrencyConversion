﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.ViewModel;

namespace WebApi.Repositories.Interface
{
    public interface ICurrencyConversion
    {
        IEnumerable<CurrencyRateResponse> ConversionCurrencyRate(List<CurrencyRate> currencyRate);
    }
}
