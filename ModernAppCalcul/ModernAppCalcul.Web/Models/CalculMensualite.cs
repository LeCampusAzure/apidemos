using Newtonsoft.Json;


namespace ModernAppCalcul.Web.Models
{
    [JsonObject(Title = "calculMensualite")]
    public class CalculMensualite
    {
        [JsonProperty(PropertyName = "capital")]
        public double Capital { get; set; }

        [JsonProperty(PropertyName = "tauxAnnuelPrc")]
        public double TauxAnnuelPrc { get; set; }

        [JsonProperty(PropertyName = "dureeEnMois")]
        public int DureeEnMois { get; set; }
    }

    [JsonObject(Title = "calculMensualiteResponse")]
    public class CalculMensualiteResponse
    {
        [JsonProperty(PropertyName = "calculMensualiteResponse")]
        public double Resultat { get; set; }
    }
}
