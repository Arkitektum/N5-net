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
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        /// <summary>
        /// Henter tilgjengelige arkiv
        /// </summary>
        /// <param name="queryOptions">OData filter</param>
        /// <returns>en liste med arkiv</returns>
        /// <response code="200">OK</response>
        /// <remarks>relasjonsnøkkel <a href="http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv">http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv</a>, og dokumentasjon av <a href="http://arkivverket.metakat.no/Objekttype/Index/EAID_C24AA8BC_2F54_4277_AA3E_54644165DBD6">datamodell, restriksjoner og mulige relasjonsnøkler</a></remarks>
        [Route("api/arkivstruktur/Arkiv")]
        [HttpGet]
        [EnableQuery]
        public IEnumerable<ArkivType> GetArkivs(string queryOptions)
        {
            //støtte odata filter syntaks
            //queryOptions.Validate(_validationSettings);

            //Rettighetsstyring...og alle andre restriksjoner
            List<ArkivType> testdata = new List<ArkivType>();

            //TODO Håndtere filter... 

            //if (queryOptions.Filter != null)
            //{
            //    var q = queryOptions.Filter.FilterClause.Expression;
            //    if (queryOptions.Filter.RawValue.Contains("systemID"))
            //    {
            //        var mockarkiv = GetArkiv("fra filter eller ");
            //        mockarkiv.beskrivelse = "passe filter";
            //        testdata.Add(GetArkiv(((Microsoft.Data.OData.Query.SemanticAst.ConstantNode)(((Microsoft.Data.OData.Query.SemanticAst.BinaryOperatorNode)(queryOptions.Filter.FilterClause.Expression)).Right)).Value.ToString()));
            //    }
            //}


            //if (queryOptions.Top == null)
            //{
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
            //}
            //else if (queryOptions.Top != null)
            //{
            //    while (testdata.Count < queryOptions.Top.Value)
            //    {
            //        testdata.Add(GetArkiv(Guid.NewGuid().ToString()));
            //    }


            //}


            return testdata.AsEnumerable();
        }


        /// <summary>
        /// Henter et arkiv med gitt id
        /// </summary>
        /// <param name="id">systemid for arkiv</param>
        /// <returns>et arkiv eller 404 hvis det ikke finnes</returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        /// <response code="403">Forbidden - ugyldige rettigheter</response>
        /// <remarks>relasjonsnøkkel <a href="http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv">http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv</a>, og dokumentasjon av <a href="http://arkivverket.metakat.no/Objekttype/Index/EAID_C24AA8BC_2F54_4277_AA3E_54644165DBD6">datamodell, restriksjoner og mulige relasjonsnøkler</a></remarks>
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
            m.oppdatertDato = DateTime.Now;

            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + m.systemID, "self"));
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkiv/" + m.systemID + "/arkivdel", Set._REL + "/arkivstruktur/arkivdel", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + m.systemID + "/ny-arkivdel", Set._REL + "/arkivstruktur/ny-arkivdel"));//Hører egentlig til administrasjon? vises hvis rolle admin?
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Arkiv/" + m.systemID + "/arkivskaper", Set._REL + "/arkivstruktur/arkivskaper", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + m.systemID + "/ny-arkivskaper", Set._REL + "/arkivstruktur/ny-arkivskaper"));//Hører egentlig til administrasjon? vises hvis rolle admin?

            m._links = linker.ToArray();
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return m;
        }

        /// <summary>
        /// Oppdaterer arkiv
        /// </summary>
        //[Route("api/arkivstruktur/Arkiv/{id}")]
        //[HttpPut]
        //[ResponseType(typeof(ArkivType))]
        //public HttpResponseMessage OppdaterArkiv(string id, ArkivType arkiv)
        //{
        //    if (arkiv != null)
        //    {
        //        //TODO rettigheter og lagring til DB el.l
        //        var url = HttpContext.Current.Request.Url;
        //        var baseUri =
        //            new UriBuilder(
        //                url.Scheme,
        //                url.Host,
        //                url.Port).Uri;
        //        arkiv.opprettetDatoSpecified = true;



        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, arkiv);
        //        response.Headers.Location = new Uri(baseUri + "api/arkivstruktur/Arkiv/" + arkiv.systemID);
        //        return response;
        //    }
        //    else
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //}

        /// <summary>
        /// Sletter arkiv
        /// </summary>
        /// <param name="id">systemid for gitt arkiv</param>
        /// <returns>statuskode</returns>
        /// <response code="204">Slettet</response>
        /// <response code="404">NotFound</response>
        /// <response code="403">Forbidden - ugyldige rettigheter</response>
        /// <remarks>relasjonsnøkkel <a href="http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv">http://rel.kxml.no/noark5/v4/arkivstruktur/arkiv</a>, og dokumentasjon av <a href="http://arkivverket.metakat.no/Objekttype/Index/EAID_C24AA8BC_2F54_4277_AA3E_54644165DBD6">datamodell, restriksjoner og mulige relasjonsnøkler</a></remarks>
        [Route("api/arkivstruktur/Arkiv/{id}")]
        [HttpDelete]
        public HttpResponseMessage SlettArkiv(string id)
        {
            if (id != null)
            {
                //Kan slettes? Har rettighet? Logges mm..

                HttpResponseMessage response = Request.CreateResponse(204);
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
        /// <response code="404">NotFound</response>
        /// <response code="501">NotImplemented</response>
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
            m.dokumentmedium.kode = "Elektronisk arkiv";
            m.arkivstatus = new ArkivstatusType();
            m.arkivstatus.kode = "O";
            
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

        /// <summary>
        /// Oppretter et nytt arkiv
        /// </summary>
        /// <returns>url til nytt arkiv i location header</returns>
        /// <response code="200">ok</response>
        /// <response code="201">Created</response>
        /// <response code="403">Forbidden - ugyldige rettigheter</response>
        /// <response code="501">NotImplemented</response>
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




        // ****    ARKIVSKAPER

        // NY
        [Route("api/arkivstruktur/Arkiv/{arkivId}/arkivskaper")]
        [HttpGet]
        public IEnumerable<ArkivskaperType> GetArkivskapere(ODataQueryOptions<ArkivskaperType> queryOptions, string arkivId)
        {
            //TODO støtte odata filter syntaks
            queryOptions.Validate(_validationSettings);

            //Rettinghetsstyring...og alle andre restriksjoner
            List<ArkivskaperType> testdata = new List<ArkivskaperType>();

            if (queryOptions.Filter != null)
            {
                // TODO
            }

            testdata.Add(GetArkivskaperIArkiv("12345", "123456"));
            testdata.Add(GetArkivskaperIArkiv("1235", "9876"));

            return testdata.AsEnumerable();
        }

        // NY
        [Route("api/arkivstruktur/Arkiv/{arkivId}/arkivskaper/{arkivskaperId}")]
        [HttpGet]
        public ArkivskaperType GetArkivskaperIArkiv(string arkivId, string arkivskaperId)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            ArkivskaperType a = new ArkivskaperType();

            a.systemID = arkivskaperId + "_Id";
            a.arkivskaperNavn = arkivskaperId;
            a.beskrivelse = "testbeskrivelse til " + arkivskaperId;
            a.opprettetAv = "Ola";
            a.opprettetDato = DateTime.Now;
            a.referanseOpprettetAv = "Arkiv " + arkivId;
            
            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + arkivId, Set._REL + "/referanseArkiv"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + arkivId + "/ny-arkivskaper", Set._REL + "/referanseArkiv"));

            a._links = linker.ToArray();
            if (a == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return a;
        }

        //NY
        [Route("api/arkivstruktur/Arkiv/{arkivId}/ny-arkivskaper")]
        [HttpGet]
        public ArkivskaperType InitialiserArkivskaper(string arkivId)
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
            m.opprettetAv = "Angi hvem som opprettet";
            m.referanseOpprettetAv = "Arkiv: " + arkivId;

            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkiv/" + arkivId, Set._REL + "/referanseArkiv"));

            m._links = linker.ToArray();
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }
        
        //NY
        [Route("api/arkivstruktur/Arkiv/{arkivId}/ny-arkivskaper")]
        [HttpPost]
        public HttpResponseMessage PostArkivskaper(ArkivskaperType arkivskaper, string arkivId)
        {
            if (arkivskaper != null)
            {
                //TODO rettigheter og lagring til DB el.l
                var url = HttpContext.Current.Request.Url;
                var baseUri =
                    new UriBuilder(
                        url.Scheme,
                        url.Host,
                        url.Port).Uri;

                //arkivdel.systemID = Guid.NewGuid().ToString();
                //arkivdel.opprettetDato = DateTime.Now;
                //arkivdel.opprettetDatoSpecified = true;
                //arkivdel.opprettetAv = "pålogget bruker";

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
