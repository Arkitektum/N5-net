using arkitektum.kommit.noark5.api.Services;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

namespace arkitektum.kommit.noark5.api.Controllers
{
    /// <summary>
    /// rel.kxml.no/noark5/v4/arkivstruktur/arkiv og rel.kxml.no/noark5/v4/arkivstruktur/ny-arkiv
    /// </summary>
    public class ArkivController : ApiController
    {
        private readonly static ODataValidationSettings ValidationSettings = new ODataValidationSettings();

        /// <summary>
        /// Henter tilgjengelige arkiv
        /// </summary>
        /// <param name="queryOptions">OData filter</param>
        /// <returns>en liste med arkiv</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest - ugyldig forespørsel</response>
        /// <response code="403">Forbidden - ingen tilgang</response>
        /// <response code="404">NotFound - ikke funnet</response>
        /// <response code="501">NotImplemented - ikke implementert</response>
        /// <remarks>relasjonsnøkkel <a href="http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv">http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv</a>, og dokumentasjon av <a href="http://arkivverket.metakat.no/Objekttype/Index/EAID_C24AA8BC_2F54_4277_AA3E_54644165DBD6">datamodell, restriksjoner og mulige relasjonsnøkler</a></remarks>
        [Route("api/arkivstruktur/Arkivquery")]
        [HttpGet]
        [EnableQuery(PageSize = 1, MaxExpansionDepth =1)]
        public IQueryable<ArkivType> GetArkivs()
        {
           
            //TODO Links

            return MockNoarkDatalayer.Arkiver.AsQueryable();
        }
        /// <summary>
        /// Henter tilgjengelige arkiv - filter
        /// </summary>
        /// <param name="queryOptions">OData filter</param>
        /// <returns>en liste med arkiv</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest - ugyldig forespørsel</response>
        /// <response code="403">Forbidden - ingen tilgang</response>
        /// <response code="404">NotFound - ikke funnet</response>
        /// <response code="501">NotImplemented - ikke implementert</response>
        /// <remarks>relasjonsnøkkel <a href="http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv">http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv</a>, og dokumentasjon av <a href="http://arkivverket.metakat.no/Objekttype/Index/EAID_C24AA8BC_2F54_4277_AA3E_54644165DBD6">datamodell, restriksjoner og mulige relasjonsnøkler</a></remarks>
        [Route("api/arkivstruktur/Arkiv")]
        [HttpGet]
        [EnableQuery()]
        public PageResult<ArkivType> GetArkivs2(ODataQueryOptions<ArkivType> queryOptions)
        {
            //Konformitetsnivå på søk
            ValidationSettings.MaxExpansionDepth = 1;
            ValidationSettings.MaxAnyAllExpressionDepth = 1;
            

            ////støtte odata filter syntaks
            queryOptions.Validate(ValidationSettings);
            //Må returnere next link når resultat er større enn pagesize
            ODataQuerySettings settings = new ODataQuerySettings()
            {
                PageSize = 2
            };

            IQueryable results = queryOptions.ApplyTo(MockNoarkDatalayer.Arkiver.AsQueryable(), settings);

            return new PageResult<ArkivType>(
                results as IEnumerable<ArkivType>,
                Request.GetNextPageLink(),
                Request.GetInlineCount());
        
        }

        /// <summary>
        /// Henter et arkiv med gitt id
        /// </summary>
        /// <param name="id">systemid for arkiv</param>
        /// <returns>et arkiv eller 404 hvis det ikke finnes</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest - ugyldig forespørsel</response>
        /// <response code="403">Forbidden - ingen tilgang</response>
        /// <response code="404">NotFound - ikke funnet</response>
        /// <response code="501">NotImplemented - ikke implementert</response>
        /// <remarks>relasjonsnøkkel <a href="http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv">http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv</a>, og dokumentasjon av <a href="http://arkivverket.metakat.no/Objekttype/Index/EAID_C24AA8BC_2F54_4277_AA3E_54644165DBD6">datamodell, restriksjoner og mulige relasjonsnøkler</a></remarks>
        [Route("api/arkivstruktur/Arkiv/{id}")]
        [HttpGet]
        [ResponseType(typeof(ArkivType))]
        public IHttpActionResult GetArkiv(string id)
        {
            ArkivType arkiv = MockNoarkDatalayer.GetArkivById(id);

            if (arkiv == null)
            {
                return NotFound();
            }
            return Ok(arkiv);
        }

        /// <summary>
        /// Oppdaterer arkiv
        /// </summary>
        /// <param name="id"></param>
        /// <param name="arkiv"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest - ugyldig forespørsel</response>
        /// <response code="403">Forbidden - ingen tilgang</response>
        /// <response code="404">NotFound - ikke funnet</response>
        /// <response code="409">Conflict - objektet kan være endret av andre</response>
        /// <response code="501">NotImplemented - ikke implementert</response>
        /// <remarks>relasjonsnøkkel <a href="http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv">http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv</a>, og dokumentasjon av <a href="http://arkivverket.metakat.no/Objekttype/Index/EAID_C24AA8BC_2F54_4277_AA3E_54644165DBD6">datamodell, restriksjoner og mulige relasjonsnøkler</a></remarks>
        [Route("api/arkivstruktur/Arkiv/{id}")]
        [HttpPut]
        [ResponseType(typeof(ArkivType))]
        public HttpResponseMessage OppdaterArkiv(string id, ArkivType arkiv)
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

                ArkivType m = MockNoarkDatalayer.Arkiver.FirstOrDefault(i => i.systemID == arkiv.systemID);

                if (m == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }

                m.oppdatertDato = DateTime.Now;
                m.oppdatertDatoSpecified = true;
                m.oppdatertAv = "bruker";
                m.referanseOppdatertAv = Guid.NewGuid().ToString();


                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, m);
                response.Headers.Location = new Uri(baseUri + "api/arkivstruktur/Arkiv/" + m.systemID);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        /// <summary>
        /// Sletter arkiv
        /// </summary>
        /// <param name="id">systemid for gitt arkiv</param>
        /// <returns>statuskode</returns>
        /// <response code="204">NoContent - Slettet ok</response>
        /// <response code="400">BadRequest - ugyldig forespørsel</response>
        /// <response code="403">Forbidden - ingen tilgang</response>
        /// <response code="404">NotFound - ikke funnet</response>
        /// <response code="409">Conflict - objektet kan være endret av andre</response>
        /// <response code="501">NotImplemented - ikke implementert</response>
        /// <remarks>relasjonsnøkkel <a href="http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv">http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv</a>, og dokumentasjon av <a href="http://arkivverket.metakat.no/Objekttype/Index/EAID_C24AA8BC_2F54_4277_AA3E_54644165DBD6">datamodell, restriksjoner og mulige relasjonsnøkler</a></remarks>
        [Route("api/arkivstruktur/Arkiv/{id}")]
        [HttpDelete]
        public HttpResponseMessage SlettArkiv(string id)
        {
            if (id != null)
            {
                //Kan slettes? Har rettighet? Logges mm..
                //sjekke etag om objektet er endret av andre?
                ArkivType m = MockNoarkDatalayer.Arkiver.FirstOrDefault(i => i.systemID == id);
               
                if (m == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                MockNoarkDatalayer.Arkiver.Remove(m);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NoContent);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }



        /// <summary>
        /// Henter forslag til et nytt arkiv
        /// </summary>
        /// <returns>et arkiv</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest - ugyldig forespørsel</response>
        /// <response code="403">Forbidden - ingen tilgang</response>
        /// <response code="404">NotFound - ikke funnet</response>
        /// <response code="501">NotImplemented - ikke implementert</response>
        /// <remarks>relasjonsnøkkel <a href="http://rel.kxml.no/noark5/v4/arkivstruktur/ny-arkiv">http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv</a>, og dokumentasjon av <a href="http://arkivverket.metakat.no/Objekttype/Index/EAID_C24AA8BC_2F54_4277_AA3E_54644165DBD6">datamodell, restriksjoner og mulige relasjonsnøkler</a></remarks>
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
            m.dokumentmedium = new DokumentmediumType();
            m.dokumentmedium.kode = "E";
            m.arkivstatus = new ArkivstatusType();
            m.arkivstatus.kode = "O";
            m.LinkList.Clear();
            //m.Links.Add(new WebApi.Hal.Link("","")) kodelister som er relevante 
            return m;
        }

        /// <summary>
        /// Oppretter et nytt arkiv
        /// </summary>
        /// <returns>url til nytt arkiv i location header</returns>
        /// <response code="200">ok</response>
        /// <response code="201">Created - opprettet</response>
        /// <response code="400">BadRequest - ugyldig forespørsel</response>
        /// <response code="403">Forbidden - ingen tilgang</response>
        /// <response code="404">NotFound - ikke funnet</response>
        /// <response code="409">Conflict - objektet kan være endret av andre</response>
        /// <response code="501">NotImplemented - ikke implementert</response>
        /// <remarks>relasjonsnøkkel <a href="http://rel.kxml.no/noark5/v4/arkivstruktur/ny-arkiv">http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv</a>, og dokumentasjon av <a href="http://arkivverket.metakat.no/Objekttype/Index/EAID_C24AA8BC_2F54_4277_AA3E_54644165DBD6">datamodell, restriksjoner og mulige relasjonsnøkler</a></remarks>
        [Route("api/arkivstruktur/nytt-arkiv")]
        [HttpPost]
        [ResponseType(typeof(ArkivType))]
        public HttpResponseMessage NyttArkiv(ArkivType arkiv)
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

                MockNoarkDatalayer.Arkiver.Add(arkiv);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, arkiv);
                response.Headers.Location = new Uri(baseUri + "api/arkivstruktur/Arkiv/" + arkiv.systemID);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }




        // ****    ARKIVSKAPER

        /// <summary>
        /// Henter arkivskapere innenfor arkiv
        /// </summary>
        /// <param name="arkivId"></param>
        /// <returns></returns>
        [Route("api/arkivstruktur/Arkiv/{arkivId}/arkivskaper")]
        [HttpGet]
        public IQueryable<ArkivskaperType> GetArkivskapereIArkiv(string arkivId)
        {
            //Rettinghetsstyring...og alle andre restriksjoner

            List<ArkivskaperType> list = new List<ArkivskaperType>();

            ArkivType arkiv = MockNoarkDatalayer.GetArkivById(arkivId);
            if (arkiv != null)
                foreach (var arkivskaper in arkiv.arkivskaper)
                {
                    list.Add(arkivskaper);
                }

           return list.AsQueryable();
        }

        /// <summary>
        /// Henter arkivskaper
        /// </summary>
        /// <param name="arkivskaperId"></param>
        /// <returns></returns>
        [Route("api/arkivstruktur/arkivskaper/{arkivskaperId}")]
        [HttpGet]
        public IHttpActionResult GetArkivskaper(string arkivskaperId)
        {
            ArkivskaperType arkivskaper = MockNoarkDatalayer.GetArkivskaperById(arkivskaperId);
            if (arkivskaper == null)
                return NotFound();

            return Ok(arkivskaper);
        }

        //NY
        [Route("api/arkivstruktur/ny-arkivskaper")]
        [HttpGet]
        public ArkivskaperType InitialiserArkivskaper()
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;
            //Legger på standardtekster feks for pålogget bruker
            ArkivskaperType m = new ArkivskaperType();
            m.arkivskaperNavn = "angi navn på arkivskaper";
            m.beskrivelse = "angi beskrivelse";
           
           
           
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }
        
        //NY
        [Route("api/arkivstruktur/ny-arkivskaper")]
        [HttpPost]
        public HttpResponseMessage PostArkivskaper(ArkivskaperType arkivskaper)
        {
            if (arkivskaper != null)
            {

                var url = HttpContext.Current.Request.Url;
                var baseUri =
                    new UriBuilder(
                        url.Scheme,
                        url.Host,
                        url.Port).Uri;

                arkivskaper.systemID = Guid.NewGuid().ToString();
                arkivskaper.opprettetAv = "pålogget bruker";
                arkivskaper.opprettetDato = DateTime.Now;
                arkivskaper.opprettetDatoSpecified = true;

                MockNoarkDatalayer.Arkivskaper.Add(arkivskaper);

                //List<LinkType> linker = new List<LinkType>();
                //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID, "self"));
                //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/mappe", Set._REL + "/mappe", "?$filter&$orderby&$top&$skip&$search"));
                //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/ny-mappe", Set._REL + "/ny-mappe"));
                //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/registrering", Set._REL + "/registrering", "?$filter&$orderby&$top&$skip&$search"));
                //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/ny-registrering", Set._REL + "/ny-registrering"));
                //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/klassifikasjonssystem", Set._REL + "/klassifikasjonssystem", "?$filter&$orderby&$top&$skip&$search"));
                //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + arkivdel.systemID + "/ny-klassifikasjonssystem", Set._REL + "/ny-klassifikasjonssystem"));
                //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + "45345", Set._REL + "/referanseArkiv"));

                //arkivdel._links = linker.ToArray();



                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, arkivskaper);
                response.Headers.Location = new Uri(baseUri + "api/arkivstruktur/Arkivdel/" + arkivskaper.systemID);
                return response;
            }
            else {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}
