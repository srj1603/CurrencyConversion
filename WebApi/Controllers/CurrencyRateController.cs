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
        private readonly ICurrencyConversion _ConversionCurrencyService;
        public CurrencyRateController(ICurrencyConversion currencyConversion)
        {
            _ConversionCurrencyService = currencyConversion;
        }
        [HttpPost("GetConversionCurrencyRate")]
        public ActionResult GetConversionCurrencyRate([FromBody] List<CurrencyRate> currencyRate)
        {
            var Result = _ConversionCurrencyService.ConversionCurrencyRate(currencyRate);
            return Ok(Result);
        }
    }

}
