﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace arkitektum.kommit.noark5.api.Controllers
{
    public class SecureApiController : ApiController
    {
        [Route("api/secure")]
        [HttpGet]
        [Authorize]
        public LinkListeType Secure()
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            //Rettinghetsstyring...og alle andre restriksjoner
            List<LinkType> linker = new List<LinkType>();

            linker.Add(addLink(baseUri, "arkivstruktur")); //Obligatorisk
            linker.Add(addLink(baseUri, "sakarkiv"));
            linker.Add(addLink(baseUri, "moeteogutvalgsbehandling"));
            linker.Add(addLink(baseUri, "administrasjon"));
            //linker.Add(addLink(baseUri, "Periodisering")); //Funksjoner?
            linker.Add(addLink(baseUri, "loggingogsporing"));
            linker.Add(addLink(baseUri, "rapporter"));
            LinkListeType liste = new LinkListeType();
            liste._links = linker.ToArray();
            return liste;
        }

        private LinkType addLink(Uri baseUri, string rel)
        {
            LinkType l2 = new LinkType();
            l2.href = baseUri + Url.Route("DefaultApi", new { controller = rel });
            l2.rel = Set._REL + "/" + rel;
            return l2;
        }
    }
}