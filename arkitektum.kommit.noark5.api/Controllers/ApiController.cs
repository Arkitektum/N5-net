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
            linker.Links.Add(Set.addLink(baseUri, "api/arkivstruktur", Set._REL + "/arkivstruktur"));
            linker.Links.Add(Set.addLink(baseUri, "api/sakarkiv", Set._REL + "/sakarkiv"));

            //linker.Add(addLink(baseUri, "moeteogutvalgsbehandling"));
            //linker.Add(addLink(baseUri, "administrasjon"));
            //linker.Add(addLink(baseUri, "Periodisering")); //Funksjoner?
            //linker.Add(addLink(baseUri, "loggingogsporing"));
            //linker.Add(addLink(baseUri, "rapporter"));
            
            return linker;
        }
       
    }
}
