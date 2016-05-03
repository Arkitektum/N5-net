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
    public class KlasseController : ApiController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        [Route("api/arkivstruktur/Klasse")]
        [HttpGet]
        public IEnumerable<KlasseType> GetKlasser(ODataQueryOptions<KlasseType> queryOptions)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            //TODO støtte odata filter syntaks
            queryOptions.Validate(_validationSettings);

            //Rettinghetsstyring...og alle andre restriksjoner
            List<KlasseType> testdata = new List<KlasseType>();

            testdata.Add(GetKlasse("12345"));
            testdata.Add(GetKlasse("1235"));

            return testdata.AsEnumerable();
        }

        [Route("api/arkivstruktur/Klasse/{id}")]
        [HttpGet]
        public KlasseType GetKlasse(string id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            KlasseType k = new KlasseType();
            k.tittel = "test" + id;
            k.systemID = id + "_sysId";
            k.beskrivelse = "Dette er en beskrivelse av" + id;
            k.klasseID = id;

            List<string> noekkelordList = new List<string>();
            string n1 = "en";
            noekkelordList.Add(n1);
            string n2 = "to";
            noekkelordList.Add(n2);
            string n3 = "tre";
            noekkelordList.Add(n3);
            k.noekkelord = noekkelordList.ToArray();                

            k.oppdatertDato = DateTime.Now;
            k.oppdatertDatoSpecified = true;
            k.oppdatertAv = "Ola";
            k.referanseOppdatertAv = "TestReferanseOppdatert";
            k.opprettetDato = DateTime.Now;
            k.opprettetDatoSpecified = true;
            k.opprettetAv = "Per";
            k.referanseOpprettetAv = "testReferanseOpprettet";

            //List<LinkType> linker = new List<LinkType>();
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/ny-klasse/", "self"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/ny-kryssreferanse/", "self"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/klassifikasjonssystem/", "self"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/ny-registrering/", "self"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/underklasser/", "self"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/registreringer/", "self"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/mappe/", "self"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/ny-mappe/", "self"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/kryssreferanse/", "self"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/ny-klassifikasjonssystem/", "self"));           

            //k._links = linker.ToArray();

            if (k == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return k;
        }

        [Route("api/arkivstruktur/Klasse/{id}")]
        [HttpPost]
        public HttpResponseMessage OppdaterKlasse(string id)
        {            
            return null;
        }

        [Route("api/arkivstruktur/ny-klasse")]
        [HttpGet]
        public KlasseType InitialiserKlasse()
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            //Legger på standardtekster feks for pålogget bruker
            KlasseType k = new KlasseType();
            k.tittel = "angi tittel på klassen";
            k.beskrivelse = "Angi beskrivelse av klassen";

            //List<LinkType> linker = new List<LinkType>();
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/kryssreferanse/", "self"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/registreringer/", "self"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/saksmappe/", "self"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/underklasser/", "self"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/mapper/", "self")); 

            //k._links = linker.ToArray();
            if (k == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return k;
        }

        [Route("api/arkivstruktur/ny-klasse")]
        [HttpPost]
        public HttpResponseMessage PostKlasse(KlasseType klasse)
        {
            if (klasse != null)
            {
                //TODO rettigheter og lagring til DB el.l
                var url = HttpContext.Current.Request.Url;
                var baseUri =
                    new UriBuilder(
                        url.Scheme,
                        url.Host,
                        url.Port).Uri;
                
                klasse.systemID = Guid.NewGuid().ToString();
                klasse.oppdatertDato = DateTime.Now;
                
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(baseUri + "api/arkivstruktur/klasse/" + klasse.systemID);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            return null;
        }
    }
}
