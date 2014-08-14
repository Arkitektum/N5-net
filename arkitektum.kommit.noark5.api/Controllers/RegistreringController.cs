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
    public class RegistreringController : ApiController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        [Route("api/arkivstruktur/registrering")]
        public IEnumerable<RegistreringType> GetRegistreringer(ODataQueryOptions<RegistreringType> queryOptions)
        {
            //TODO støtte odata filter syntaks
            queryOptions.Validate(_validationSettings);


            List<RegistreringType> testdata = new List<RegistreringType>();
            testdata.Add(GetRegistrering(Guid.NewGuid().ToString()));
            testdata.Add(GetRegistrering(Guid.NewGuid().ToString()));
            testdata.Add(GetRegistrering(Guid.NewGuid().ToString()));
            testdata.Add(GetRegistrering(Guid.NewGuid().ToString()));
            testdata.Add(GetRegistrering(Guid.NewGuid().ToString()));

            return testdata.AsEnumerable();
        }

        [Route("api/arkivstruktur/Arkivdel/{Id}/registrering")]
        [HttpGet]
        public IEnumerable<RegistreringType> GetRegistreringerByArkivdel(string Id)
        {
            List<RegistreringType> testdata = new List<RegistreringType>();

            testdata.Add(GetRegistrering(Guid.NewGuid().ToString()));

            return testdata.AsEnumerable();
        }

        [Route("api/arkivstruktur/Mappe/{MappesystemID}/registrering")]
        public IEnumerable<RegistreringType> GetRegistreringerByMappe(string MappesystemID)
        {

            List<RegistreringType> testdata = new List<RegistreringType>();

            testdata.Add(GetRegistrering(Guid.NewGuid().ToString()));

            return testdata.AsEnumerable();
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

            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Registrering/" + m.systemID, "self"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Basisregistrering/" + m.systemID, Set._REL + "/utvid-til-basisregistrering"));
            linker.Add(Set.addLink(baseUri, "api/sakarkiv/Journalpost/" + m.systemID, Set._REL + "/utvid-til-journalpost"));
            linker.Add(Set.addLink(baseUri, "api/MoeteOgUtvalgsbehandling/Moeteregistrering/" + m.systemID, Set._REL + "/utvid-til-moeteregistrering"));

            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Registrering/" + m.systemID + "/dokumentbeskrivelse", Set._REL + "/dokumentbeskrivelse", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Registrering/" + m.systemID + "/ny-dokumentbeskrivelse", Set._REL + "/ny-dokumentbeskrivelse"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Registrering/" + m.systemID + "/dokumentobjekt", Set._REL + "/dokumentobjekt", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Registrering/" + m.systemID + "/ny-dokumentobjekt", Set._REL + "/ny-dokumentobjekt"));

            //Enten eller?
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Klasse/234", Set._REL + "/referanseKlasse"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Mappe/665", Set._REL + "/referanseMappe"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/6578", Set._REL + "/referanseArkivdel"));

            m._links = linker.ToArray();
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
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

                List<LinkType> linker = new List<LinkType>();
                linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Registrering/" + registrering.systemID, "self"));
                linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Basisregistrering/" + registrering.systemID, Set._REL + "/utvid-til-basisregistrering"));
                linker.Add(Set.addLink(baseUri, "api/sakarkiv/Journalpost/" + registrering.systemID, Set._REL + "/utvid-til-journalpost"));
                linker.Add(Set.addLink(baseUri, "api/MoeteOgUtvalgsbehandling/Moeteregistrering/" + registrering.systemID, Set._REL + "/utvid-til-moeteregistrering"));

                linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Registrering/" + registrering.systemID + "/dokumentbeskrivelse", Set._REL + "/dokumentbeskrivelse", "?$filter&$orderby&$top&$skip&$search"));
                linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Registrering/" + registrering.systemID + "/ny-dokumentbeskrivelse", Set._REL + "/ny-dokumentbeskrivelse"));
                linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Registrering/" + registrering.systemID + "/dokumentobjekt", Set._REL + "/dokumentobjekt", "?$filter&$orderby&$top&$skip&$search"));
                linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Registrering/" + registrering.systemID + "/ny-dokumentobjekt", Set._REL + "/ny-dokumentobjekt"));

                //Enten eller?
                linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Klasse/234", Set._REL + "/referanseKlasse"));
                linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Mappe/665", Set._REL + "/referanseMappe"));
                linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/6578", Set._REL + "/referanseArkivdel"));

                registrering._links = linker.ToArray();



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



    }
}
