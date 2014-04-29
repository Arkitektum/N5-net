using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkitektum.kommit.noark5.api.Controllers
{
    public static class Set
    {
        public  const string _REL = "http://rel.kxml.no/noark5/v4/api";

        
        public static LinkType addLink(Uri baseUri, string apiUrl, string relUrl)
        {
            LinkType l = new LinkType();
            l.href = baseUri + apiUrl;
            l.rel = relUrl;
            return l;
        }

        public static LinkType addTempLink(Uri baseUri, string apiUrl, string relUrl, string template)
        {
            LinkType l = new LinkType();
            l.href = baseUri + apiUrl + "{" + template + "}";
            l.rel = relUrl;
            l.templated = true;
            l.templatedSpecified = true;
            return l;
        }
    }
}