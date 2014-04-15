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
    public class ArkivController : ApiController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        [Route("api/arkivstruktur/Arkiv")]
        [HttpGet]
        public IEnumerable<ArkivType> GetArkivs(ODataQueryOptions<ArkivType> queryOptions)
        {
            //støtte odata filter syntaks
            queryOptions.Validate(_validationSettings);
           

            //Rettighetsstyring...og alle andre restriksjoner
            List<ArkivType> testdata = new List<ArkivType>();

            //TODO Håndtere filter... 
            if (queryOptions.Top == null)
            {
                testdata.Add(GetArkiv("12345"));
                testdata.Add(GetArkiv("234"));
                testdata.Add(GetArkiv(Guid.NewGuid().ToString()));
                testdata.Add(GetArkiv(Guid.NewGuid().ToString()));
                testdata.Add(GetArkiv(Guid.NewGuid().ToString()));
                testdata.Add(GetArkiv(Guid.NewGuid().ToString()));
                testdata.Add(GetArkiv(Guid.NewGuid().ToString()));
                testdata.Add(GetArkiv(Guid.NewGuid().ToString()));
                testdata.Add(GetArkiv(Guid.NewGuid().ToString()));
                testdata.Add(GetArkiv(Guid.NewGuid().ToString()));
            }
            else if (queryOptions.Top.Value == 5)
            {
                testdata.Add(GetArkiv("12345"));
                testdata.Add(GetArkiv("234"));
                testdata.Add(GetArkiv(Guid.NewGuid().ToString()));
                testdata.Add(GetArkiv(Guid.NewGuid().ToString()));
                testdata.Add(GetArkiv(Guid.NewGuid().ToString()));

            }
            

            return testdata.AsEnumerable();

        }


        [Route("api/arkivstruktur/Arkiv/{id}")]
        [HttpGet]
        public ArkivType GetArkiv(string id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            ArkivType m = new ArkivType();
            m.tittel = "test arkiv " + id;
            m.systemID = id;
            m.opprettetDato = DateTime.Now;

            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + m.systemID, "self"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkiv/" + m.systemID + "/arkivdel", Set._REL + "/arkivdel", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + m.systemID + "/ny-arkivdel", Set._REL + "/ny-arkivdel"));//Hører egentlig til administrasjon? vises hvis rolle admin?
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkiv/" + m.systemID + "/arkivskaper", Set._REL + "/arkivskaper", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + m.systemID + "/ny-arkivskaper", Set._REL + "/ny-arkivskaper"));//Hører egentlig til administrasjon? vises hvis rolle admin?

            m._links = linker.ToArray();
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }

        [Route("api/arkivstruktur/nytt-arkiv")]
        [HttpGet]
        public ArkivType InitialiserArkiv()
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;
            //Legger på standardtekster feks for pålogget bruker
            ArkivType m = new ArkivType();
            m.tittel = "angi tittel på arkiv";
            m.dokumentmedium = "Elektronisk arkiv";
            m.arkivstatus = "O";
            
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

        
        [Route("api/arkivstruktur/nytt-arkiv")]
        [HttpPost]
        public HttpResponseMessage PostArkiv(ArkivType arkiv)
        {
            if (arkiv != null)
            {
                //TODO rettigheter og lagring til DB el.l
                var url = HttpContext.Current.Request.Url;
                var baseUri =
                    new UriBuilder(
                        url.Scheme,
                        url.Host,
                        url.Port).Uri;
                arkiv.systemID = Guid.NewGuid().ToString();
                arkiv.opprettetDato = DateTime.Now;
                arkiv.opprettetDatoSpecified = true;
                arkiv.opprettetAv = "pålogget bruker";
                
                List<LinkType> linker = new List<LinkType>();
                linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + arkiv.systemID, "self"));
                linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkiv/" + arkiv.systemID + "/arkivdel", Set._REL + "/arkivdel", "?$filter&$orderby&$top&$skip&$search"));
                linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + arkiv.systemID + "/ny-arkivdel", Set._REL + "/ny-arkivdel"));//Hører egentlig til administrasjon? vises hvis rolle admin?
                linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkiv/" + arkiv.systemID + "/arkivskaper", Set._REL + "/arkivskaper", "?$filter&$orderby&$top&$skip&$search"));
                linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + arkiv.systemID + "/ny-arkivskaper", Set._REL + "/ny-arkivskaper"));//Hører egentlig til administrasjon? vises hvis rolle admin?

                arkiv._links = linker.ToArray();

                

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, arkiv);
                response.Headers.Location = new Uri(baseUri + "api/arkivstruktur/Arkiv/" + arkiv.systemID);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}
