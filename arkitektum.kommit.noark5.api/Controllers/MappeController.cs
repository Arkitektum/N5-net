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
using System.Web.Mvc;
using arkitektum.kommit.noark5.api.Services;
using WebApi.Hal;

namespace arkitektum.kommit.noark5.api.Controllers
{
    public class MappeController : ApiController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();


        [System.Web.Http.Route("api/arkivstruktur/Mappe")]
        [System.Web.Http.HttpGet]
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
        [System.Web.Http.Route("api/arkivstruktur/Mappe/{id}")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(MappeType))]
        public IHttpActionResult GetMappe(string id)
        {
            var mappe = MockNoarkDatalayer.GetMappeById(id);

            if (mappe == null)
                return NotFound();

            return Ok(mappe);
        }

        [System.Web.Http.Route("api/arkivstruktur/Mappe/{id}")]
        [System.Web.Http.HttpPut]
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

        [System.Web.Http.Route("api/arkivstruktur/ny-mappe")]
        [System.Web.Http.HttpGet]
        public MappeType InitialiserMappe()
        {
            return null;
        }

        [System.Web.Http.Route("api/arkivstruktur/ny-mappe")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage PostMappe(MappeType mappe)
        {
            return null;
        }      

        [System.Web.Http.Route("api/arkivstruktur/Mappe/{Id}/avslutt-mappe")]
        [System.Web.Http.HttpGet]
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
        [System.Web.Http.Route("api/arkivstruktur/Mappe/{Id}/kryssreferanse")]
        [System.Web.Http.HttpGet]
        public IEnumerable<KryssreferanseType> GetKryssreferanserFraMappe(string id)
        {
            if (MockNoarkDatalayer.MappeExists(id))
            {
                var kryssreferanser = MockNoarkDatalayer.GetKryssreferanseFraMappe(id);
                return kryssreferanser;

            }
            return null;

        }

        // NY
        [System.Web.Http.Route("api/arkivstruktur/Mappe/{Id}/ny-kryssreferanse")]
        [System.Web.Http.HttpGet]
        public KryssreferanseType InitialiserFraMappeKryssreferanse(string Id)
        {
            return null;
        }

        // NY
        [System.Web.Http.Route("api/arkivstruktur/Mappe/{Id}/ny-kryssreferanse")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage PostKryssreferanseFraMappe(KryssreferanseType kryssreferanse)
        {
            return null;
        }

        // NY
        [System.Web.Http.Route("api/arkivstruktur/Mappe/{Id}/undermappe")]
        [System.Web.Http.HttpGet]
        public IEnumerable<MappeType> GetUndermapper(string Id)
        {
            return null;
        }

        // NY
        [System.Web.Http.Route("api/arkivstruktur/Mappe/{Id}/ny-undermappe")]
        [System.Web.Http.HttpGet]
        public MappeType InitialiserMappe(string arkivdelid)
        {
            return null;
        }

        // NY
        [System.Web.Http.Route("api/arkivstruktur/Mappe/{Id}/ny-undermappe")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage PostUndermapper(MappeType undermappe)
        {
            return null;
        }

        // NY
        [System.Web.Http.Route("api/arkivstruktur/Mappe/{Id}/undermappe/{undermappeId}")]
        [System.Web.Http.HttpGet]
        public MappeType GetUndermappe(string Id, string undermappeId)
        {
            return null;
        }

        // NY
        [System.Web.Http.Route("api/arkivstruktur/Mappe/{Id}/undermappe/{undermappeId}")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage OppdaterUndermappe(MappeType undermappe)
        {
            return null;
        }

        // NY
        [System.Web.Http.Route("api/arkivstruktur/Mappe/{Id}/merknad")]
        [System.Web.Http.HttpGet]
        public IEnumerable<MerknadType> GetMerknaderIMappe(string id)
        {
            return null;
        }

        // NY
        [System.Web.Http.Route("api/arkivstruktur/Mappe/{Id}/merknad/{merknadId}")]
        [System.Web.Http.HttpGet]
        public MerknadType GetMerknadIMappe(string id, string merknadId)
        {
            return null;
        }

        // NY
        [System.Web.Http.Route("api/arkivstruktur/Mappe/{Id}/merknad/{merknadId}")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage OppdaterMerknadIMappe(MerknadType merknad)
        {
            return null;
        }

        // NY
        [System.Web.Http.Route("api/arkivstruktur/Mappe/{Id}/ny-merknad")]
        [System.Web.Http.HttpGet]
        public MerknadType InitialiserMerknadIMappe(string Id)
        {
            return null;
        }

        // NY
        [System.Web.Http.Route("api/arkivstruktur/Mappe/{Id}/ny-merknad")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage PostMerknadIMappe(MerknadType merknad)
        {
            return null;
        }


        [System.Web.Http.Route("api/arkivstruktur/Arkivdel/{Id}/mappe")]
        [System.Web.Http.HttpGet]
        public IEnumerable<MappeType> GetMapperFraArkivdel(string Id)
        {
            SaksmappeController c = new SaksmappeController();

            List<MappeType> testdata = new List<MappeType>();

            testdata.Add(MockNoarkDatalayer.Mapper.First());
            testdata.Add(MockNoarkDatalayer.Saksmapper.First());

            return testdata.ToArray();
        }


        [System.Web.Http.Route("api/arkivstruktur/Arkivdel/{Id}/mappe/{mappeId}")]
        [System.Web.Http.HttpGet]
        public MappeType GetMappeFraArkivdel(string Id, string mappeId)
        {
            return null;
        }


        [System.Web.Http.Route("api/arkivstruktur/Arkivdel/{Id}/mappe/{mappeId}")]
        [System.Web.Http.HttpPost]
        public MappeType OppdaterMappeFraArkivdel(MappeType mappe)
        {
            return null;
        }


        [System.Web.Http.Route("api/arkivstruktur/Arkivdel/{Id}/ny-mappe")]
        [System.Web.Http.HttpGet]
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


        [System.Web.Http.Route("api/arkivstruktur/Arkivdel/{Id}/ny-mappe")]
        [System.Web.Http.HttpPost]
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



        [System.Web.Http.Route("api/arkivstruktur/Klasse/{Id}/mappe")]
        [System.Web.Http.HttpGet]
        public IEnumerable<MappeType> GetMapperIKlasse(string Id)
        {
            SaksmappeController c = new SaksmappeController();

            List<MappeType> testdata = new List<MappeType>();

            testdata.Add(MockNoarkDatalayer.Mapper.First());
            testdata.Add(MockNoarkDatalayer.Saksmapper.First());

            return testdata.ToArray();
        }


        [System.Web.Http.Route("api/arkivstruktur/Klasse/{Id}/mappe/{mappeId}")]
        [System.Web.Http.HttpGet]
        public MappeType GetMappeFraKlasse(string Id, string mappeId)
        {
            return null;
        }


        [System.Web.Http.Route("api/arkivstruktur/Klasse/{Id}/mappe/{mappeId}")]
        [System.Web.Http.HttpPost]
        public MappeType OppdaterMappeFraKlasse(MappeType mappe)
        {
            return null;
        }


        [System.Web.Http.Route("api/arkivstruktur/Klasse/{Id}/ny-mappe")]
        [System.Web.Http.HttpGet]
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


        [System.Web.Http.Route("api/arkivstruktur/Klasse/{Id}/ny-mappe")]
        [System.Web.Http.HttpPost]
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
