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
    public class DokumentbeskrivelseController : ApiController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryOptions"></param>
        /// <returns></returns>
        [Route("api/arkivstruktur/Dokumentbeskrivelse")]
        [HttpGet]
        public IEnumerable<DokumentbeskrivelseType> GetDokumentbeskrivelser(ODataQueryOptions<DokumentbeskrivelseType> queryOptions)
        {


            List<DokumentbeskrivelseType> testdata = new List<DokumentbeskrivelseType>();

            testdata.Add(GetDokumentbeskrivelse(Guid.NewGuid().ToString()));
            testdata.Add(GetDokumentbeskrivelse(Guid.NewGuid().ToString()));
            testdata.Add(GetDokumentbeskrivelse(Guid.NewGuid().ToString()));
            testdata.Add(GetDokumentbeskrivelse(Guid.NewGuid().ToString()));
            testdata.Add(GetDokumentbeskrivelse(Guid.NewGuid().ToString()));

            return testdata.AsEnumerable();
        }

        [Route("api/arkivstruktur/Dokumentbeskrivelse/{id}")]
        [HttpGet]
        public DokumentbeskrivelseType GetDokumentbeskrivelse(string id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            DokumentbeskrivelseType m = new DokumentbeskrivelseType();
            m.systemID = id;
            m.tittel = "angitt tittel " + id;
            m.beskrivelse = "beskrivelse";
            m.opprettetDato = DateTime.Now;
            m.RepopulateHyperMedia();
           
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }

        [Route("api/arkivstruktur/Dokumentbeskrivelse/{id}")]
        [HttpPost]
        public HttpResponseMessage OppdaterDokumentbeskrivelse(string id)
        {
            return null;
        }

        [Route("api/arkivstruktur/ny-dokumentbeskrivelse")]
        [HttpGet]
        public DokumentbeskrivelseType InitialiserDokumentbeskrivelse(string id)
        {
            return null;
        }

        [Route("api/arkivstruktur/ny-dokumentbeskrivelse")]
        [HttpPost]
        public HttpResponseMessage PostDokumentbeskrivelse(string id)
        {
            return null;
        }

        

       

        [Route("api/arkivstruktur/Registrering/{Id}/dokumentbeskrivelse")]
        [HttpGet]
        public IEnumerable<DokumentbeskrivelseType> GetDokumentbeskrivelserFraRegistrering(string Id)
        {
            List<DokumentbeskrivelseType> testdata = new List<DokumentbeskrivelseType>();

            testdata.Add(GetDokumentbeskrivelse(Guid.NewGuid().ToString()));
            testdata.Add(GetDokumentbeskrivelse(Guid.NewGuid().ToString()));
            testdata.Add(GetDokumentbeskrivelse(Guid.NewGuid().ToString()));
            testdata.Add(GetDokumentbeskrivelse(Guid.NewGuid().ToString()));
            testdata.Add(GetDokumentbeskrivelse(Guid.NewGuid().ToString()));

            return testdata.AsEnumerable();
        }

        

        //NY
        [Route("api/arkivstruktur/Registrering/{id}/ny-dokumentbeskrivelse")]
        [HttpPost]
        public HttpResponseMessage PostReferansefilIRegistrering()
        {
            return null;
        }

        [Route("api/arkivstruktur/Registrering/{Id}/dokumentbeskrivelse/{dokumentbeskrivelseId}")]
        [HttpPost]
        public HttpResponseMessage PostRegistreringAvDokumentbeskrivelse(string Id, DokumentbeskrivelseType dokumentbeskrivelse, string dokumentbeskrivelseId) {
            return null;
        }
    }
}
