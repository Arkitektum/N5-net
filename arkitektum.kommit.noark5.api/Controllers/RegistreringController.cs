using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData.Query;
using System.Web.UI;
using arkitektum.kommit.noark5.api.Services;

namespace arkitektum.kommit.noark5.api.Controllers
{
    public class RegistreringController : ApiController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        [Route("api/arkivstruktur/registrering")]
        public IEnumerable<RegistreringType> GetRegistreringer(ODataQueryOptions<RegistreringType> queryOptions)
        {
            var results = new List<RegistreringType>();

            queryOptions.Validate(_validationSettings);

            IQueryable<RegistreringType> filtered = queryOptions.ApplyTo(MockNoarkDatalayer.Registreringer.AsQueryable()) as IQueryable<RegistreringType>;

            if (filtered != null)
                results.AddRange(filtered);

            return results.ToArray();
        }


        [Route("api/arkivstruktur/ny-registrering")]
        [HttpGet]
        public RegistreringType InitialiserRegistrering()
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;
            //Legger på standardtekster feks for pålogget bruker
            RegistreringType m = new RegistreringType();
            m.arkivertDato = DateTime.Now;
            m.arkivertAv = "Pålogget bruker 2";
            m.referanseArkivdel = null;





            //List<LinkType> linker = new List<LinkType>();
            //linker.Add(Set.addTempLink(baseUri, "api/kodelister/Dokumentmedium", Set._REL + "/administrasjon/dokumentmedium", "?$filter&$orderby&$top&$skip"));
            //linker.Add(Set.addTempLink(baseUri, "api/kodelister/Arkivstatus", Set._REL + "/administrasjon/arkivstatus", "?$filter&$orderby&$top&$skip"));


            //m._links = linker.ToArray();
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }


        [Route("api/arkivstruktur/ny-registrering")]
        [HttpPost]
        public HttpResponseMessage PostRegistrering(RegistreringType registrering)
        {
            if (registrering != null)
            {
                //TODO rettigheter og lagring til DB el.l
                var url = HttpContext.Current.Request.Url;
                var baseUri =
                    new UriBuilder(
                        url.Scheme,
                        url.Host,
                        url.Port).Uri;
                registrering.systemID = Guid.NewGuid().ToString();
                registrering.opprettetDato = DateTime.Now.AddDays(-2);
                registrering.opprettetDatoSpecified = true;
                registrering.opprettetAv = "pålogget bruker";

                registrering.RepopulateHyperMedia();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, registrering);
                response.Headers.Location = new Uri(baseUri + "api/arkivstruktur/registrering/" + registrering.systemID);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        [Route("api/arkivstruktur/Registrering/{id}")]
        [HttpGet]
        public RegistreringType GetRegistrering(string id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            RegistreringType m = new RegistreringType();
            m.systemID = id;
            m.opprettetDato = DateTime.Now;
            m.opprettetDatoSpecified = true;
            m.oppdatertDato = DateTime.Now;
            m.oppdatertAv = "bruker";

            m.RepopulateHyperMedia();

            //m._links = linker.ToArray();
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }


        [Route("api/arkivstruktur/Registrering/{id}")]
        [HttpPost]
        public HttpResponseMessage OppdaterRegistrering(RegistreringType oppdatering)
        {
            if (oppdatering != null)
            {
                //TODO rettigheter og lagring til DB el.l
                var url = HttpContext.Current.Request.Url;
                var baseUri =
                    new UriBuilder(
                        url.Scheme,
                        url.Host,
                        url.Port).Uri;
                //oppdatering.oppdatertDato = DateTime.Now;
                //oppdatering.oppdatertAv = "pålogget bruker 2";



                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, oppdatering);
                response.Headers.Location = new Uri(baseUri + "api/arkivstruktur/registrering/" + oppdatering.systemID);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        [Route("api/arkivstruktur/Arkivdel/{Id}/registrering")]
        [HttpGet]
        public IEnumerable<RegistreringType> GetRegistreringerIArkivdel(string Id)
        {
            List<RegistreringType> testdata = new List<RegistreringType>();

            testdata.Add(GetRegistrering(Guid.NewGuid().ToString()));

            return testdata.AsEnumerable();
        }


        [Route("api/arkivstruktur/Arkivdel/{Id}/ny-registrering")]
        [HttpGet]
        public RegistreringType InitialiserRegistreringerIArkivdel(string Id)
        {
            return null;
        }


        [Route("api/arkivstruktur/Arkivdel/{Id}/ny-registrering")]
        [HttpPost]
        public HttpResponseMessage PostRegistreringerIArkivdel(RegistreringType registrering)
        {
            return null;
        }


        [Route("api/arkivstruktur/Arkivdel/{Id}/registrering/{registreringsId}")]
        [HttpGet]
        public RegistreringType GetRegistreringIArkivdel(string Id, string registreringsId)
        {
            return null;
        }


        [Route("api/arkivstruktur/Arkivdel/{Id}/registrering/{registreringsId}")]
        [HttpPost]
        public HttpResponseMessage OppdaterRegistreringIArkivdel(RegistreringType registrering)
        {
            return null;
        }


        [Route("api/arkivstruktur/Mappe/{Id}/registrering")]
        [HttpGet]
        public IEnumerable<RegistreringType> GetRegistreringerIMappe(string Id)
        {
            List<RegistreringType> testdata = new List<RegistreringType>();

            testdata.Add(GetRegistrering(Guid.NewGuid().ToString()));

            return testdata.AsEnumerable();
        }


        [Route("api/arkivstruktur/Mappe/{Id}/ny-registrering")]
        [HttpGet]
        public RegistreringType InitialiserRegistreringerIMappe(string Id)
        {
            return null;
        }


        [Route("api/arkivstruktur/Mappe/{Id}/ny-registrering")]
        [HttpPost]
        public HttpResponseMessage PostRegistreringerIMappe(RegistreringType registrering)
        {
            return null;
        }


        [Route("api/arkivstruktur/Mappe/{Id}/registrering/{registreringsId}")]
        [HttpGet]
        public RegistreringType GetRegistreringIMappe(string Id, string registreringsId)
        {
            return null;
        }


        [Route("api/arkivstruktur/Mappe/{Id}/registrering/{registreringsId}")]
        [HttpPost]
        public HttpResponseMessage OppdaterRegistreringIMappe(RegistreringType registrering)
        {
            return null;
        }

    }
}
