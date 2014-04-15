using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace arkitektum.kommit.noark5.api.Controllers
{
    public class ArkivstrukturController : ApiController
    {
       
        public IEnumerable<LinkType> GetArkivstruktur()
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            //Rettinghetsstyring...og alle andre restriksjoner
            List<LinkType> linker = new List<LinkType>();

            //linker.Add(addLink(baseUri, "Mappe"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkiv", Set._REL + "/arkiv", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/nytt-arkiv", Set._REL + "/ny-arkiv")); //Hører egentlig til administrasjon? vises hvis rolle admin?
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivskaper", Set._REL + "/arkivskaper", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel", Set._REL + "/arkivdel", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Klassifikasjonssystem", Set._REL + "/klassifikasjonssystem", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Klasse", Set._REL + "/klasse", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Mappe", Set._REL + "/mappe", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Registrering", Set._REL + "/registrering", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Basisregistrering", Set._REL + "/basisregistrering", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Dokumentbeskrivelse", Set._REL + "/dokumentbeskrivelse", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Dokumentobjekt", Set._REL + "/dokumentobjekt", "?$filter&$orderby&$top&$skip&$search"));
            
            

            return linker.AsEnumerable();
        }
    }
}
