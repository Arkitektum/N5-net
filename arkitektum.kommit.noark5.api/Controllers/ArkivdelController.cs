using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData.Query;

namespace arkitektum.kommit.noark5.api.Controllers
{
    public class ArkivdelController : ApiController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryOptions"></param>
        /// <returns></returns>
        [Route("api/arkivstruktur/Arkivdel")]
        [HttpGet]
        public IEnumerable<ArkivdelType> GetArkivdels(ODataQueryOptions<ArkivdelType> queryOptions)
        {
            //TODO støtte odata filter syntaks
            queryOptions.Validate(_validationSettings);

            //Rettinghetsstyring...og alle andre restriksjoner
            List<ArkivdelType> testdata = new List<ArkivdelType>();

            if (queryOptions.Filter != null)
            {
                //Se arkiv
            }

            testdata.Add(GetArkivdel("12345"));
            testdata.Add(GetArkivdel("1235"));

            return testdata.AsEnumerable();

        }


        [Route("api/arkivstruktur/Arkivdel/{id}")]
        [HttpGet]
        public ArkivdelType GetArkivdel(string id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            ArkivdelType a = new ArkivdelType();
            a.tittel = "test arkivdel " + id;
            a.systemID = id;
            a.opprettetDato = DateTime.Now;

            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + a.systemID, "self"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + a.systemID + "/mappe", Set._REL + "/arkivstruktur/mappe", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + a.systemID + "/ny-mappe", Set._REL + "/arkivstruktur/ny-mappe"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + a.systemID + "/registrering", Set._REL + "/arkivstruktur/registrering", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + a.systemID + "/ny-registrering", Set._REL + "/arkivstruktur/ny-registrering"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + a.systemID + "/klassifikasjonssystem", Set._REL + "/arkivstruktur/klassifikasjonssystem", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + a.systemID + "/ny-klassifikasjonssystem", Set._REL + "/arkivstruktur/ny-klassifikasjonssystem"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + "45345", Set._REL + "/arkivstruktur/referanseArkiv"));

            a._links = linker.ToArray();
            if (a == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return a;
        }

        // NY
        [Route("api/arkivstruktur/Arkivdel/{id}")]
        [HttpPost]
        public HttpResponseMessage OppdaterArkivdel(ArkivdelType arkivdel)
        {
            if (arkivdel != null)
            {
                //TODO rettigheter og lagring til DB el.l
                var url = HttpContext.Current.Request.Url;
                var baseUri =
                    new UriBuilder(
                        url.Scheme,
                        url.Host,
                        url.Port).Uri;
                arkivdel.opprettetDatoSpecified = true;

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, arkivdel);
                response.Headers.Location = new Uri(baseUri + "api/arkivstruktur/Arkiv/" + arkivdel.systemID);
                return response;
        }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }    
        }


        [Route("api/arkivstruktur/ny-arkivdel")]
        [HttpGet]
        public ArkivdelType InitialiserArkivdel()
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;
            //Legger på standardtekster feks for pålogget bruker
            ArkivdelType m = new ArkivdelType();
            m.tittel = "angi tittel på arkiv";
            m.dokumentmedium = new DokumentmediumType();
            m.dokumentmedium.kode = "Elektronisk arkiv";
            m.arkivdelstatus = new ArkivdelstatusType();
            m.arkivdelstatus.kode = "O";

            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addTempLink(baseUri, "api/kodelister/Dokumentmedium", Set._REL + "/administrasjon/dokumentmedium", "?$filter&$orderby&$top&$skip"));
            linker.Add(Set.addTempLink(baseUri, "api/kodelister/Arkivstatus", Set._REL + "/administrasjon/arkivstatus", "?$filter&$orderby&$top&$skip"));

            m._links = linker.ToArray();
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }


        [Route("api/arkivstruktur/ny-arkivdel")]
        [HttpPost]
        public HttpResponseMessage PostArkivdel(ArkivdelType arkivdel)
        {
            if (arkivdel != null)
            {
                //TODO rettigheter og lagring til DB el.l
                var url = HttpContext.Current.Request.Url;
                var baseUri =
                    new UriBuilder(
                        url.Scheme,
                        url.Host,
                        url.Port).Uri;
                arkivdel.systemID = Guid.NewGuid().ToString();
                arkivdel.opprettetDato = DateTime.Now;
                arkivdel.opprettetDatoSpecified = true;
                arkivdel.opprettetAv = "pålogget bruker";

                List<LinkType> linker = new List<LinkType>();
                linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID, "self"));
                linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/mappe", Set._REL + "/arkivstruktur/mappe", "?$filter&$orderby&$top&$skip&$search"));
                linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/ny-mappe", Set._REL + "/arkivstruktur/ny-mappe"));
                linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/registrering", Set._REL + "/arkivstruktur/registrering", "?$filter&$orderby&$top&$skip&$search"));
                linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/ny-registrering", Set._REL + "/arkivstruktur/ny-registrering"));
                linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/klassifikasjonssystem", Set._REL + "/arkivstruktur/klassifikasjonssystem", "?$filter&$orderby&$top&$skip&$search"));
                linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/ny-klassifikasjonssystem", Set._REL + "/arkivstruktur/ny-klassifikasjonssystem"));
                linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + "45345", Set._REL + "/arkivstruktur/referanseArkiv"));

                arkivdel._links = linker.ToArray();



                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, arkivdel);
                response.Headers.Location = new Uri(baseUri + "api/arkivstruktur/Arkivdel/" + arkivdel.systemID);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        

        [Route("api/arkivstruktur/Arkiv/{Id}/arkivdel")]
        [HttpGet]
        public IEnumerable<ArkivdelType> GetArkivdelerFraArkiv(string Id)
        {
            List<ArkivdelType> testdata = new List<ArkivdelType>();

            testdata.Add(GetArkivdel("1235"));

            return testdata.AsEnumerable();
        }


        // NY
        [Route("api/arkivstruktur/Arkiv/{arkivId}/Arkivdel/{ArkivdelId}")]
        [HttpGet]
        public ArkivdelType GetArkivdelIArkiv()
        {
            return null;
        }

        // NY
        [Route("api/arkivstruktur/Arkiv/{arkivId}/Arkivdel/{ArkivdelId}")]
        [HttpPost]
        public HttpResponseMessage OppdaterArkivdelIArkiv()
        {
            return null;
        }

        //NY
        [Route("api/arkivstruktur/Arkiv/{arkivId}/ny-arkivdel")]
        [HttpGet]
        public ArkivdelType InitialiserArkivdelIArkiv()
        {
            return null;
        }

        //NY
        [Route("api/arkivstruktur/Arkiv/{arkivId}/ny-arkivdel")]
        [HttpPost]
        public HttpResponseMessage PostArkivdel()
        {
            return null;
        }

    }
}
