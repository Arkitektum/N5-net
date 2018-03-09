using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using System.Web.Http.Cors;
using WebApi.Hal;

namespace arkitektum.kommit.noark5.api.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class Api4Controller : ApiController
    {
        /// <summary>
        /// Henter tilgjengelige moduler
        /// </summary>
        /// <returns>en liste med moduler</returns>
        /// <response code="200">OK</response>
        [Route("api")]
        [HttpGet]
        public Links GetApi()
        {
            var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

            //Rettinghetsstyring...og alle andre restriksjoner
            Links linker = new Links();
            linker.LinkList.Add(Set.addLink(baseUri, "api/arkivstruktur", Set._REL + "/arkivstruktur"));
            linker.LinkList.Add(Set.addLink(baseUri, "api/sakarkiv", Set._REL + "/sakarkiv"));

            //linker.Add(addLink(baseUri, "moeteogutvalgsbehandling"));
            //linker.Add(addLink(baseUri, "administrasjon"));
            
            
            return linker;
        }

        /// <summary>
        /// Henter tilgjengelige tjenester under sakarkiv
        /// </summary>
        /// <returns>liste over tjenester</returns>
        [Route("api/sakarkiv")]
        [HttpGet]
        public Links GetSakarkiv()
        {
            var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

            //Rettinghetsstyring...og alle andre restriksjoner
            Links linker = new Links();
            linker.LinkList.Add(Set.addTempLink(baseUri, "api/sakarkiv/saksmappe", Set._REL + "/sakarkiv/saksmappe", "?$filter&$orderby&$top&$skip&$search")); //Obligatorisk
            linker.LinkList.Add(Set.addTempLink(baseUri, "api/sakarkiv/journalpost", Set._REL + "/sakarkiv/journalpost", "?$filter&$orderby&$top&$skip&$search")); //Obligatorisk


            return linker;
        }

        /// <summary>
        /// Henter tilgjengelige tjenester under arkivstruktur
        /// </summary>
        /// <returns>liste over tjenester</returns>
        [Route("api/arkivstruktur")]
        [HttpGet]
        public Links GetArkivstruktur()
        {
            var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

            Links linker = new Links();
            ////Rettinghetsstyring...og alle andre restriksjoner

            linker.LinkList.Add(Set.addTempLink(baseUri, "api/arkivstruktur/arkiv", Set._REL + "/arkivstruktur/arkiv", "?$filter&$orderby&$top&$skip&$search")); //Obligatorisk
            linker.LinkList.Add(Set.addLink(baseUri, "api/arkivstruktur/ny-arkivskaper", Set._REL + "/administrasjon/ny-arkivskaper")); //Hører egentlig til administrasjon? vises hvis rolle admin?
            linker.LinkList.Add(Set.addTempLink(baseUri, "api/arkivstruktur/arkivskaper", Set._REL + "/arkivstruktur/arkivskaper", "?$filter&$orderby&$top&$skip&$search"));
            linker.LinkList.Add(Set.addTempLink(baseUri, "api/arkivstruktur/arkivdel", Set._REL + "/arkivstruktur/arkivdel", "?$filter&$orderby&$top&$skip&$search"));
            linker.LinkList.Add(Set.addTempLink(baseUri, "api/arkivstruktur/klassifikasjonssystem", Set._REL + "/arkivstruktur/klassifikasjonssystem", "?$filter&$orderby&$top&$skip&$search"));
            linker.LinkList.Add(Set.addTempLink(baseUri, "api/arkivstruktur/klasse", Set._REL + "/arkivstruktur/klasse", "?$filter&$orderby&$top&$skip&$search"));
            linker.LinkList.Add(Set.addTempLink(baseUri, "api/arkivstruktur/mappe", Set._REL + "/arkivstruktur/mappe", "?$filter&$orderby&$top&$skip&$search"));
            linker.LinkList.Add(Set.addTempLink(baseUri, "api/arkivstruktur/registrering", Set._REL + "/arkivstruktur/registrering", "?$filter&$orderby&$top&$skip&$search"));
            linker.LinkList.Add(Set.addTempLink(baseUri, "api/arkivstruktur/dokumentbeskrivelse", Set._REL + "/arkivstruktur/dokumentbeskrivelse", "?$filter&$orderby&$top&$skip&$search"));
            linker.LinkList.Add(Set.addTempLink(baseUri, "api/arkivstruktur/dokumentobjekt", Set._REL + "/arkivstruktur/dokumentobjekt", "?$filter&$orderby&$top&$skip&$search"));


            return linker;

           
        }

    }
}
