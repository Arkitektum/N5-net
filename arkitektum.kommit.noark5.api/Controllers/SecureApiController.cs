using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApi.Hal;

namespace arkitektum.kommit.noark5.api.Controllers
{
    public class SecureApiController : ApiController
    {
        [Route("api/secure")]
        [HttpGet]
        [Authorize]
        public Links Secure()
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            //Rettinghetsstyring...og alle andre restriksjoner
            Links linker = new Links();

            linker.LinkList.Add(addLink(baseUri, "arkivstruktur")); //Obligatorisk
            linker.LinkList.Add(addLink(baseUri, "sakarkiv"));
            linker.LinkList.Add(addLink(baseUri, "moeteogutvalgsbehandling"));
            linker.LinkList.Add(addLink(baseUri, "administrasjon"));
            //linker.Add(addLink(baseUri, "Periodisering")); //Funksjoner?
            linker.LinkList.Add(addLink(baseUri, "loggingogsporing"));
            linker.LinkList.Add(addLink(baseUri, "rapporter"));
            
            return linker;
        }

        private LinkType addLink(Uri baseUri, string rel)
        {
            return new LinkType(Set._REL + "/" + rel, baseUri + Url.Route("DefaultApi", new { controller = rel }));
        }
    }
}