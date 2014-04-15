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
    }
}
