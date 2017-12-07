using arkitektum.kommit.noark5.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData.Query;
using arkitektum.kommit.noark5.api.Services;
using WebApi.Hal;

namespace arkitektum.kommit.noark5.api.Controllers
{
    public class MappeController : ApiController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();


        [Route("api/arkivstruktur/Mappe")]
        [HttpGet]
        public IEnumerable<MappeType> GetMappes(ODataQueryOptions<MappeType> queryOptions)
        {
            queryOptions.Validate(_validationSettings);
            
            var results = new List<MappeType>();

            var filtered = queryOptions.ApplyTo(MockNoarkDatalayer.Mapper.AsQueryable()) as IEnumerable<MappeType>;
            if (filtered != null)
                results.AddRange(filtered);

            return  results;
        }

        /// <summary>
        /// Returns a single mappe by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/arkivstruktur/Mappe/{id}")]
        [HttpGet]
        [ResponseType(typeof(MappeType))]
        public IHttpActionResult GetMappe(string id)
        {
            var mappe = MockNoarkDatalayer.GetMappeById(id);

            if (mappe == null)
                return NotFound();

            return Ok(mappe);
        }

        [Route("api/arkivstruktur/Mappe/{id}")]
        [HttpPut]
        public HttpResponseMessage OppdaterMappe(MappeType mappe)
        {
            if (mappe != null)
            {
                //TODO rettigheter og lagring til DB el.l
                var url = HttpContext.Current.Request.Url;
                var baseUri =
                    new UriBuilder(
                        url.Scheme,
                        url.Host,
                        url.Port).Uri;
               
                mappe.oppdatertAv = "pålogget bruker";
                mappe.oppdatertDato = DateTime.Now;
                mappe.oppdatertDatoSpecified = true;
                mappe.referanseOppdatertAv = Guid.NewGuid().ToString();
                mappe.LinkList.Clear();
                mappe.RepopulateHyperMedia();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, mappe);
                response.Headers.Location = new Uri(baseUri + "api/arkivstruktur/Mappe/" + mappe.systemID);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Route("api/arkivstruktur/ny-mappe")]
        [HttpGet]
        public MappeType InitialiserMappe()
        {
            return null;
        }

        [Route("api/arkivstruktur/ny-mappe")]
        [HttpPost]
        public HttpResponseMessage PostMappe(MappeType mappe)
        {
            return null;
        }      

        [Route("api/arkivstruktur/Mappe/{Id}/avslutt-mappe")]
        [HttpGet]
        public MappeType AvsluttMappe(string Id)
        {
            //TODO hvis det er en saksmappe eller møtemappe skal det sendes videre til riktig kontroller? På Saksmappe settes status i tillegg, mm

            MappeType avsl = MockNoarkDatalayer.GetMappeById(Id);
            avsl.avsluttetAv = "tor";
            avsl.avsluttetDatoSpecified = true;
            avsl.avsluttetDato = DateTime.Now;
            return avsl;
        }

        // NY
        [Route("api/arkivstruktur/Mappe/{Id}/kryssreferanse")]
        [HttpGet]
        public IEnumerable<MappeType> GetKryssreferanserFraMappe(string id)
        {
            return null;
        }

        // NY
        [Route("api/arkivstruktur/Mappe/{Id}/ny-kryssreferanse")]
        [HttpGet]
        public KryssreferanseType InitialiserFraMappeKryssreferanse(string Id)
        {
            return null;
        }

        // NY
        [Route("api/arkivstruktur/Mappe/{Id}/ny-kryssreferanse")]
        [HttpPost]
        public HttpResponseMessage PostKryssreferanseFraMappe(KryssreferanseType kryssreferanse)
        {
            return null;
        }

        // NY
        [Route("api/arkivstruktur/Mappe/{Id}/undermappe")]
        [HttpGet]
        public IEnumerable<MappeType> GetUndermapper(string Id)
        {
            return null;
        }

        // NY
        [Route("api/arkivstruktur/Mappe/{Id}/ny-undermappe")]
        [HttpGet]
        public MappeType InitialiserMappe(string arkivdelid)
        {
            return null;
        }

        // NY
        [Route("api/arkivstruktur/Mappe/{Id}/ny-undermappe")]
        [HttpPost]
        public HttpResponseMessage PostUndermapper(MappeType undermappe)
        {
            return null;
        }

        // NY
        [Route("api/arkivstruktur/Mappe/{Id}/undermappe/{undermappeId}")]
        [HttpGet]
        public MappeType GetUndermappe(string Id, string undermappeId)
        {
            return null;
        }

        // NY
        [Route("api/arkivstruktur/Mappe/{Id}/undermappe/{undermappeId}")]
        [HttpPost]
        public HttpResponseMessage OppdaterUndermappe(MappeType undermappe)
        {
            return null;
        }

        // NY
        [Route("api/arkivstruktur/Mappe/{Id}/merknad")]
        [HttpGet]
        public IEnumerable<MerknadType> GetMerknaderIMappe(string id)
        {
            return null;
        }

        // NY
        [Route("api/arkivstruktur/Mappe/{Id}/merknad/{merknadId}")]
        [HttpGet]
        public MerknadType GetMerknadIMappe(string id, string merknadId)
        {
            return null;
        }

        // NY
        [Route("api/arkivstruktur/Mappe/{Id}/merknad/{merknadId}")]
        [HttpPost]
        public HttpResponseMessage OppdaterMerknadIMappe(MerknadType merknad)
        {
            return null;
        }

        // NY
        [Route("api/arkivstruktur/Mappe/{Id}/ny-merknad")]
        [HttpGet]
        public MerknadType InitialiserMerknadIMappe(string Id)
        {
            return null;
        }

        // NY
        [Route("api/arkivstruktur/Mappe/{Id}/ny-merknad")]
        [HttpPost]
        public HttpResponseMessage PostMerknadIMappe(MerknadType merknad)
        {
            return null;
        }


        [Route("api/arkivstruktur/Arkivdel/{Id}/mappe")]
        [HttpGet]
        public IEnumerable<MappeType> GetMapperFraArkivdel(string Id)
        {
            SaksmappeController c = new SaksmappeController();

            List<MappeType> testdata = new List<MappeType>();

            testdata.Add(MockNoarkDatalayer.Mapper.First());
            testdata.Add(MockNoarkDatalayer.Saksmapper.First());

            return testdata.ToArray();
        }


        [Route("api/arkivstruktur/Arkivdel/{Id}/mappe/{mappeId}")]
        [HttpGet]
        public MappeType GetMappeFraArkivdel(string Id, string mappeId)
        {
            return null;
        }


        [Route("api/arkivstruktur/Arkivdel/{Id}/mappe/{mappeId}")]
        [HttpPost]
        public MappeType OppdaterMappeFraArkivdel(MappeType mappe)
        {
            return null;
        }


        [Route("api/arkivstruktur/Arkivdel/{Id}/ny-mappe")]
        [HttpGet]
        public MappeType InitialiserMappeIArkivdel(string Id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;
            //Legger på standardtekster feks for pålogget bruker
            MappeType m = new MappeType();
            m.tittel = "angi tittel på mappe";
            m.dokumentmedium = new DokumentmediumType() { kode = "E", beskrivelse = "Elektronisk arkiv" };
            m.mappetype = new MappetypeType() { kode = "BYGG", beskrivelse = "Byggesak" };

            m.LinkList.Clear();
            m.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/administrasjon/dokumentmedium", baseUri + "api/kodelister/Dokumentmedium{?$filter&$orderby&$top&$skip}"));
            m.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/administrasjon/mappetype", baseUri + "api/kodelister/Mappetype{?$filter&$orderby&$top&$skip}"));

            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }


        [Route("api/arkivstruktur/Arkivdel/{Id}/ny-mappe")]
        [HttpPost]
        public HttpResponseMessage PostMappeIArkivdel(MappeType mappe)
        {
            if (mappe != null)
            {
                //TODO rettigheter og lagring til DB el.l
                var url = HttpContext.Current.Request.Url;
                var baseUri =
                    new UriBuilder(
                        url.Scheme,
                        url.Host,
                        url.Port).Uri;
                mappe.systemID = Guid.NewGuid().ToString();
                mappe.opprettetAv = "pålogget bruker";
                mappe.opprettetDato = DateTime.Now;
                mappe.opprettetDatoSpecified = true;
                mappe.referanseOpprettetAv = Guid.NewGuid().ToString();
                mappe.mappeID = "123456/2016";
                mappe.LinkList.Clear();
                mappe.RepopulateHyperMedia();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, mappe);
                response.Headers.Location = new Uri(baseUri + "api/arkivstruktur/Mappe/" + mappe.systemID);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }



        [Route("api/arkivstruktur/Klasse/{Id}/mappe")]
        [HttpGet]
        public IEnumerable<MappeType> GetMapperIKlasse(string Id)
        {
            SaksmappeController c = new SaksmappeController();

            List<MappeType> testdata = new List<MappeType>();

            testdata.Add(MockNoarkDatalayer.Mapper.First());
            testdata.Add(MockNoarkDatalayer.Saksmapper.First());

            return testdata.ToArray();
        }


        [Route("api/arkivstruktur/Klasse/{Id}/mappe/{mappeId}")]
        [HttpGet]
        public MappeType GetMappeFraKlasse(string Id, string mappeId)
        {
            return null;
        }


        [Route("api/arkivstruktur/Klasse/{Id}/mappe/{mappeId}")]
        [HttpPost]
        public MappeType OppdaterMappeFraKlasse(MappeType mappe)
        {
            return null;
        }


        [Route("api/arkivstruktur/Klasse/{Id}/ny-mappe")]
        [HttpGet]
        public MappeType InitialiserMappeIKlasse(string Id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;
            //Legger på standardtekster feks for pålogget bruker
            MappeType m = new MappeType();
            m.tittel = "angi tittel på mappe";
            m.dokumentmedium = new DokumentmediumType() { kode = "E", beskrivelse = "Elektronisk arkiv" };


            //List<LinkType> linker = new List<LinkType>();
            //linker.Add(Set.addTempLink(baseUri, "api/kodelister/Dokumentmedium", Set._REL + "/administrasjon/dokumentmedium", "?$filter&$orderby&$top&$skip"));
            //linker.Add(Set.addTempLink(baseUri, "api/kodelister/Mappetype", Set._REL + "/administrasjon/mappetype", "?$filter&$orderby&$top&$skip"));


            //m._links = linker.ToArray();
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }


        [Route("api/arkivstruktur/Klasse/{Id}/ny-mappe")]
        [HttpPost]
        public HttpResponseMessage PostMappeIKlasse(MappeType mappe)
        {
            if (mappe != null)
            {
                //TODO rettigheter og lagring til DB el.l
                var url = HttpContext.Current.Request.Url;
                var baseUri =
                    new UriBuilder(
                        url.Scheme,
                        url.Host,
                        url.Port).Uri;
                mappe.systemID = Guid.NewGuid().ToString();




                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(baseUri + "api/arkivstruktur/Arkiv/" + mappe.systemID);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        
        // GET api/Mappe/ND 234234
        public IEnumerable<MappeType> GetMappeByTittel(string tittel)
        {
            List<MappeType> testdata = new List<MappeType>();

            MappeType m = MockNoarkDatalayer.Mapper.First();
            m.tittel = tittel;
            testdata.Add(m);
            return testdata.AsEnumerable();
        }
    }
}
