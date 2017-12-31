using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class CalculService : System.Web.Services.WebService
{
    public CalculService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //[WebMethod]
    //public string HelloWorld() {
    //    return "Hello World";
    //}

    [WebMethod]
    public double CalculMensualite(double capital, double tauxAnnuelPrc, int dureeEnMois)
    {
        return  
             Math.Round(
                 (capital * tauxAnnuelPrc/100/12) / 
                 (1 - Math.Pow(1 + (tauxAnnuelPrc/100/12),-dureeEnMois))
                 ,2);
    }

}