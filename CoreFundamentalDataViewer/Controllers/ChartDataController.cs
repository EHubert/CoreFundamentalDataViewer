using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreFundamentalDataViewer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CoreFundamentalDataViewer.Controllers
{
    public class ChartDataController : Controller
    {
        public ChartDataController()
        {

        }

        public IActionResult GetBars(string ticker)
        {
            List<BalanceSheet> balanceSheets = new List<BalanceSheet>();

            if (!string.IsNullOrEmpty(ticker))
            {
                BalanceSheet balanceSheet = new BalanceSheet();
                var client = new RestClient("https://last10k-company-v1.p.rapidapi.com/v1/company/balancesheet");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-rapidapi-host", "last10k-company-v1.p.rapidapi.com");
                request.AddHeader("x-rapidapi-key", "b3327e6d54mshe467a3859431c87p12cc69jsnfa3da2dbf3f6");
                request.AddParameter("ticker", ticker, ParameterType.QueryString);

                for(int i = 0; i < 10; i++)
                {
                    request.AddParameter("filingOrder", i, ParameterType.QueryString);
                    IRestResponse response = client.Execute(request);
                    var result = JsonConvert.DeserializeObject<JObject>(response.Content);

                    if(result != null)
                    {
                        balanceSheet.FilingName = result["data"]["attributes"]["company"]["name"].ToString();
                        balanceSheet.FilingDate = Convert.ToDateTime(result["data"]["attributes"]["filing"]["filingDate"].ToString()).ToString("MM/dd/yyyy");
                        balanceSheet.WorkingCapital = Convert.ToDecimal(result["data"]["attributes"]["result"]["AssetsCurrent"].ToString()) - Convert.ToDecimal(result["data"]["attributes"]["result"]["LiabilitiesCurrent"].ToString());

                        balanceSheets.Add(balanceSheet);
                    }
                }
            }

            return Json(balanceSheets);
        }
    }
}