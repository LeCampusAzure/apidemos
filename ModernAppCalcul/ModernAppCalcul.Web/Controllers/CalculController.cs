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
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Resultat(double capital, double tauxAnnuelPrc, int duree)
        {
            string resultat = "";

            try
            {
                var response = await AppelResultatAsync(capital, tauxAnnuelPrc, duree);
                if (response.IsSuccessStatusCode)
                {
                    string resultatTemp = await response.Content.ReadAsStringAsync();
                    CalculMensualiteResponse calculResponse = JsonConvert.DeserializeObject<CalculMensualiteResponse>(resultatTemp);
                    resultat = calculResponse.Resultat.ToString("C");
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

        private async Task<HttpResponseMessage> AppelResultatAsync(double capital, double tauxAnnuelPrc, int duree)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "993cf9315c764b62ade073c054869137");

            dynamic calcul = new {
                    calculMensualite = new CalculMensualite()
                    {
                        Capital = capital,
                        TauxAnnuelPrc = tauxAnnuelPrc,
                        DureeEnMois = duree
                    }
                };

            string calculSerialized = JsonConvert.SerializeObject(calcul) ;
            var stringContent = new StringContent(calculSerialized, Encoding.UTF8, "application/json");
            return await client.PostAsync("https://mza-apis-apim.azure-api.net/CalculMensualite", stringContent);
            
        }
    }
}