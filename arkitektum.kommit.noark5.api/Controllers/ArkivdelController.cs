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

        [Route("api/arkivstruktur/Arkivdel")]
        [HttpGet]
        public IEnumerable<ArkivdelType> GetArkivdels(ODataQueryOptions<ArkivType> queryOptions)
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
            List<ArkivdelType> testdata = new List<ArkivdelType>();

            testdata.Add(GetArkivdel("12345"));
            testdata.Add(GetArkivdel("1235"));

            return testdata.AsEnumerable();

        }
        [Route("api/arkivstruktur/Arkiv/{Id}/arkivdel")]
        [HttpGet]
        public IEnumerable<ArkivdelType> GetArkivdelerForArkiv(string Id)
        {
            List<ArkivdelType> testdata = new List<ArkivdelType>();

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

            ArkivdelType m = new ArkivdelType();
            m.tittel = "test arkivdel " + id;
            m.systemID = id;
            m.opprettetDato = DateTime.Now;

            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + m.systemID, "self"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + m.systemID + "/mappe", Set._REL + "/mappe", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + m.systemID + "/ny-mappe", Set._REL + "/ny-mappe"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + m.systemID + "/registrering", Set._REL + "/registrering", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + m.systemID + "/ny-registrering", Set._REL + "/ny-registrering"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + m.systemID + "/klassifikasjonssystem", Set._REL + "/klassifikasjonssystem", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + m.systemID + "/ny-klassifikasjonssystem", Set._REL + "/ny-klassifikasjonssystem"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + "45345", Set._REL + "/referanseArkiv"));

            m._links = linker.ToArray();
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }
    }
}
