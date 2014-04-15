using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace arkitektum.kommit.noark5.api.Controllers
{
    public class Api4Controller : ApiController
    {

        [Route("api")]
        [HttpGet]
        public LinkListe GetApi4()
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            //Rettinghetsstyring...og alle andre restriksjoner
            List<LinkType> linker = new List<LinkType>();

            linker.Add(addLink(baseUri, "Arkivstruktur")); //Obligatorisk
            linker.Add(addLink(baseUri, "Sakarkiv"));
            linker.Add(addLink(baseUri, "MoeteOgUtvalgsbehandling"));
            linker.Add(addLink(baseUri, "Periodisering")); //Funksjoner?
            linker.Add(addLink(baseUri, "OffentligJournal"));
            linker.Add(addLink(baseUri, "Restanser"));
            LinkListe liste = new LinkListe();
            liste.link = linker.ToArray();
            return liste;
        }

        private LinkType addLink(Uri baseUri, string rel)
        {
            LinkType l2 = new LinkType();
            l2.uri = baseUri + Url.Route("DefaultApi", new { controller = rel });
            l2.rel = Set._REL + "/" + rel;
            return l2;
        }
    }
}
