using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace arkitektum.kommit.noark5.api.Controllers
{
    /// <summary>
    /// rel.kxml.no/noark5/v4/arkivstruktur
    /// </summary>
    public class ArkivstrukturController : ApiController
    {

        /// <summary>
        /// Henter tilgjengelige tjenester under arkivstruktur
        /// </summary>
        /// <returns>liste over tjenester</returns>
        public Links GetArkivstruktur()
        {
            var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

            Links linker = new Links();
            ////Rettinghetsstyring...og alle andre restriksjoner

            linker.Links.Add(Set.addTempLink(baseUri, "api/arkivstruktur/arkiv", Set._REL + "/arkivstruktur/arkiv", "?$filter&$orderby&$top&$skip&$search")); //Obligatorisk
            linker.Links.Add(Set.addLink(baseUri, "api/arkivstruktur/ny-arkivskaper", Set._REL + "/administrasjon/ny-arkivskaper")); //Hører egentlig til administrasjon? vises hvis rolle admin?
            linker.Links.Add(Set.addTempLink(baseUri, "api/arkivstruktur/arkivskaper", Set._REL + "/arkivstruktur/arkivskaper", "?$filter&$orderby&$top&$skip&$search"));
            linker.Links.Add(Set.addTempLink(baseUri, "api/arkivstruktur/arkivdel", Set._REL + "/arkivstruktur/arkivdel", "?$filter&$orderby&$top&$skip&$search"));
            linker.Links.Add(Set.addTempLink(baseUri, "api/arkivstruktur/klassifikasjonssystem", Set._REL + "/arkivstruktur/klassifikasjonssystem", "?$filter&$orderby&$top&$skip&$search"));
            linker.Links.Add(Set.addTempLink(baseUri, "api/arkivstruktur/klasse", Set._REL + "/arkivstruktur/klasse", "?$filter&$orderby&$top&$skip&$search"));
            linker.Links.Add(Set.addTempLink(baseUri, "api/arkivstruktur/mappe", Set._REL + "/arkivstruktur/mappe", "?$filter&$orderby&$top&$skip&$search"));
            linker.Links.Add(Set.addTempLink(baseUri, "api/arkivstruktur/registrering", Set._REL + "/arkivstruktur/registrering", "?$filter&$orderby&$top&$skip&$search"));
            linker.Links.Add(Set.addTempLink(baseUri, "api/arkivstruktur/basisregistrering", Set._REL + "/arkivstruktur/basisregistrering", "?$filter&$orderby&$top&$skip&$search"));
            linker.Links.Add(Set.addTempLink(baseUri, "api/arkivstruktur/dokumentbeskrivelse", Set._REL + "/arkivstruktur/dokumentbeskrivelse", "?$filter&$orderby&$top&$skip&$search"));
            linker.Links.Add(Set.addTempLink(baseUri, "api/arkivstruktur/dokumentobjekt", Set._REL + "/arkivstruktur/dokumentobjekt", "?$filter&$orderby&$top&$skip&$search"));


            return linker;
            
            //List<LinkType> linker = new List<LinkType>();

            ////linker.Add(addLink(baseUri, "Mappe"));
            //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/arkiv", Set._REL + "/arkivstruktur/arkiv", "?$filter&$orderby&$top&$skip&$search"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/nytt-arkiv", Set._REL + "/administrasjon/ny-arkiv")); //Hører egentlig til administrasjon? vises hvis rolle admin?
            //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/arkivskaper", Set._REL + "/arkivstruktur/arkivskaper", "?$filter&$orderby&$top&$skip&$search"));
            //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/arkivdel", Set._REL + "/arkivstruktur/arkivdel", "?$filter&$orderby&$top&$skip&$search"));
            //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/klassifikasjonssystem", Set._REL + "/arkivstruktur/klassifikasjonssystem", "?$filter&$orderby&$top&$skip&$search"));
            //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/klasse", Set._REL + "/arkivstruktur/klasse", "?$filter&$orderby&$top&$skip&$search"));
            //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/mappe", Set._REL + "/arkivstruktur/mappe", "?$filter&$orderby&$top&$skip&$search"));
            //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/registrering", Set._REL + "/arkivstruktur/registrering", "?$filter&$orderby&$top&$skip&$search"));
            //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/basisregistrering", Set._REL + "/arkivstruktur/basisregistrering", "?$filter&$orderby&$top&$skip&$search"));
            //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/dokumentbeskrivelse", Set._REL + "/arkivstruktur/dokumentbeskrivelse", "?$filter&$orderby&$top&$skip&$search"));
            //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/dokumentobjekt", Set._REL + "/arkivstruktur/dokumentobjekt", "?$filter&$orderby&$top&$skip&$search"));

            //LinkListeType liste = new LinkListeType();
            //liste._links = linker.ToArray();
            //return liste;
        }
    }
}
