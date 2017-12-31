using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using ModernAppCalcul.Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace ModernAppCalcul.Web.Controllers
{
    public class CalculController : Controller
    {

        const string UriServiceCalcul = "https://[your-api].azure-api.net/CalculMensualite";
        const string CleServiceApim = "[your-key]";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Resultat(CalculMensualite calculMens)
        {
            string resultat = "";

            try
            {
                var response = await AppelResultatAsync(calculMens);
                if (response.IsSuccessStatusCode)
                {
                    string resultatTemp = await response.Content.ReadAsStringAsync();
                    CalculMensualiteResponse calculResponse = JsonConvert.DeserializeObject<CalculMensualiteResponse>(resultatTemp);
                    resultat = calculResponse.Resultat.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("fr-fr"));
                }
                else
                {
                    resultat = "(Impossible de calculer)";
                }
            }
            catch (Exception)
            {
                //TODO log
                resultat = "(Impossible de calculer)";
            }


            ViewData["Resultat"] = resultat;
            return View();
        }

       
        private async Task<HttpResponseMessage> AppelResultatAsync(CalculMensualite calculMens)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", CleServiceApim);

    
            dynamic calcul = new
            {
                calculMensualite = calculMens
            };

            string calculSerialized = JsonConvert.SerializeObject(calcul) ;
            var stringContent = new StringContent(calculSerialized, Encoding.UTF8, "application/json");
            return await client.PostAsync(UriServiceCalcul, stringContent);
            
        }
    }
}