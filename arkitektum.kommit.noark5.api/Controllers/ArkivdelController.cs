using arkitektum.kommit.noark5.api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData.Query;

namespace arkitektum.kommit.noark5.api.Controllers
{
    public class ArkivdelController : ApiController
    {
        private static readonly ODataValidationSettings ValidationSettings = new ODataValidationSettings();

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
            ValidationSettings.MaxExpansionDepth = 1;
            ValidationSettings.MaxAnyAllExpressionDepth = 1;
            ////støtte odata filter syntaks
            queryOptions.Validate(ValidationSettings);

            return queryOptions.ApplyTo(MockNoarkDatalayer.Arkivdeler.AsQueryable()) as IEnumerable<ArkivdelType>;

        }


        [Route("api/arkivstruktur/Arkivdel/{id}")]
        [HttpGet]
        [ResponseType(typeof(ArkivdelType))]
        public HttpResponseMessage GetArkivdel(string id)
        {

            ArkivdelType m = MockNoarkDatalayer.Arkivdeler.FirstOrDefault(i => i.systemID == id);

            if (m == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Finner ikke objekt");
            }
            return Request.CreateResponse(HttpStatusCode.OK, m);

            //var url = HttpContext.Current.Request.Url;
            //var baseUri =
            //    new UriBuilder(
            //        url.Scheme,
            //        url.Host,
            //        url.Port).Uri;

            //ArkivdelType a = new ArkivdelType();
            //a.tittel = "test arkivdel " + id;
            //a.systemID = id;
            //a.opprettetDato = DateTime.Now;

            ////List<LinkType> linker = new List<LinkType>();
            ////linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + a.systemID, "self"));
            ////linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + a.systemID + "/mappe", Set._REL + "/arkivstruktur/mappe", "?$filter&$orderby&$top&$skip&$search"));
            ////linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + a.systemID + "/ny-mappe", Set._REL + "/arkivstruktur/ny-mappe"));
            ////linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + a.systemID + "/registrering", Set._REL + "/arkivstruktur/registrering", "?$filter&$orderby&$top&$skip&$search"));
            ////linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + a.systemID + "/ny-registrering", Set._REL + "/arkivstruktur/ny-registrering"));
            ////linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + a.systemID + "/klassifikasjonssystem", Set._REL + "/arkivstruktur/klassifikasjonssystem", "?$filter&$orderby&$top&$skip&$search"));
            ////linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + a.systemID + "/ny-klassifikasjonssystem", Set._REL + "/arkivstruktur/ny-klassifikasjonssystem"));
            ////linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + "45345", Set._REL + "/arkivstruktur/referanseArkiv"));

            ////a._links = linker.ToArray();
            //if (a == null)
            //{
            //    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            //}

            //return a;
        }

        /// <summary>
        /// Sletter arkivdel
        /// </summary>
        /// <param name="id">systemid for gitt arkivdel</param>
        /// <returns>statuskode</returns>
        /// <response code="200">OK</response>
        /// <response code="204">NoContent - Slettet ok</response>
        /// <response code="400">BadRequest - ugyldig forespørsel</response>
        /// <response code="403">Forbidden - ingen tilgang</response>
        /// <response code="404">NotFound - ikke funnet</response>
        /// <response code="409">Conflict - objektet kan være endret av andre</response>
        /// <response code="501">NotImplemented - ikke implementert</response>
        /// <remarks>relasjonsnøkkel <a href="http://rel.kxml.no/noark5/v4/arkivstruktur/arkivdel">http://rel.kxml.no/noark5/v4/arkivstruktur/arkivdel</a>, og dokumentasjon av <a href="http://arkivverket.metakat.no/Objekttype/Index/EAID_C24AA8BC_2F54_4277_AA3E_54644165DBD6">datamodell, restriksjoner og mulige relasjonsnøkler</a></remarks>
        [Route("api/arkivstruktur/Arkivdel/{id}")]
        [HttpDelete]
        public HttpResponseMessage SlettArkivdel(string id)
        {
            if (id != null)
            {
                var url = HttpContext.Current.Request.Url;
                var baseUri =
                    new UriBuilder(
                        url.Scheme,
                        url.Host,
                        url.Port).Uri;
                //Kan slettes? Har rettighet? Logges mm..
                //Hva er forskjellen på datatype sletting?
                //sjekke etag om objektet er endret av andre?
                ArkivdelType m = MockNoarkDatalayer.Arkivdeler.FirstOrDefault(i => i.systemID == id);
                

                if (m == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Ikke funnet");
                }

                m.sletting = new SlettingType();
                m.sletting.slettingstype = new SlettingstypeType();
                m.sletting.slettingstype.kode = "SA";
                m.sletting.slettingstype.beskrivelse = "Sletting av hele innholdet i arkivdelen";
                m.sletting.slettetDato = DateTime.Now;
                m.sletting.slettetAv = "pålogget bruker";
                //_ctx.arkivdeler.Remove(m);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, m);
                response.Headers.Location = new Uri(baseUri + "api/arkivstruktur/Arkiv/" + m.systemID);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
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

                //List<LinkType> linker = new List<LinkType>();
                //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID, "self"));
                //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/mappe", Set._REL + "/arkivstruktur/mappe", "?$filter&$orderby&$top&$skip&$search"));
                //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/ny-mappe", Set._REL + "/arkivstruktur/ny-mappe"));
                //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/registrering", Set._REL + "/arkivstruktur/registrering", "?$filter&$orderby&$top&$skip&$search"));
                //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/ny-registrering", Set._REL + "/arkivstruktur/ny-registrering"));
                //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/klassifikasjonssystem", Set._REL + "/arkivstruktur/klassifikasjonssystem", "?$filter&$orderby&$top&$skip&$search"));
                //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/ny-klassifikasjonssystem", Set._REL + "/arkivstruktur/ny-klassifikasjonssystem"));
                //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + "45345", Set._REL + "/arkivstruktur/referanseArkiv"));

                //arkivdel._links = linker.ToArray();



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
            IEnumerable<ArkivdelType> m = MockNoarkDatalayer.Arkivdeler.Where(i => i.arkiv.systemID == Id);
            return m;
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
