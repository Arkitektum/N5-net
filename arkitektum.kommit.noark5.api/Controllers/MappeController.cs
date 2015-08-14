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
    public class MappeController : ApiController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        [Route("api/arkivstruktur/Mappe")]
        [HttpGet]
        public IEnumerable<MappeType> GetMappes(ODataQueryOptions<MappeType> queryOptions)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            //TODO støtte odata filter syntaks
            queryOptions.Validate(_validationSettings);
            
            //$orderby=ReleaseDate asc, Rating desc
            //$filter=Price lt 10.00
            //$top=5&$skip=2


            //Rettinghetsstyring...og alle andre restriksjoner
            List<MappeType> testdata = new List<MappeType>();

            //TODO Håndtere filter... 
            if (queryOptions.Top == null)
            {
                testdata.Add(GetMappe("12345"));
                testdata.Add(GetMappe("234"));
                testdata.Add(GetMappe(Guid.NewGuid().ToString()));
                testdata.Add(GetMappe(Guid.NewGuid().ToString()));
                testdata.Add(GetMappe(Guid.NewGuid().ToString()));
                testdata.Add(GetMappe(Guid.NewGuid().ToString()));
                testdata.Add(GetMappe(Guid.NewGuid().ToString()));
                testdata.Add(GetMappe(Guid.NewGuid().ToString()));
                testdata.Add(GetMappe(Guid.NewGuid().ToString()));
                testdata.Add(GetMappe(Guid.NewGuid().ToString()));
            }
            else if (queryOptions.Top.Value == 5)
            {
                testdata.Add(GetMappe("12345"));
                testdata.Add(GetMappe("234"));
                testdata.Add(GetMappe(Guid.NewGuid().ToString()));
                testdata.Add(GetMappe(Guid.NewGuid().ToString()));
                testdata.Add(GetMappe(Guid.NewGuid().ToString()));

            }

            return testdata.ToArray();
        }

        [Route("api/arkivstruktur/Mappe/{id}")]
        [HttpGet]
        public MappeType GetMappe(string id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            MappeType m = new MappeType();
            m.tittel = "testmappe " + id;
            m.offentligTittel = "Regler for offentlig tittel ****";
            m.systemID = id;
            m.opprettetDato = DateTime.Now;
            m.opprettetDatoSpecified = true;
            m.opprettetAv = "tor";
            m.mappeID = "1234/2014";
            m.gradering = new GraderingType();
            m.gradering.graderingskode = new GraderingskodeType();
            m.gradering.graderingskode.kode = "B";
            m.gradering.graderingsdato = DateTime.Now;
            List<MerknadType> merknader = new List<MerknadType>();
            MerknadType m1= new MerknadType();
            m1.merknadstype = new MerknadstypeType();
            m1.merknadstype.kode = "B";
            m1.merknadstekst = "test";
            merknader.Add(m1);
            m.merknader = merknader.ToArray();           

            m.virksomhetsspesifikkeMetadata = "";

            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID, "self"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/avslutt-mappe", Set._REL + "/avslutt-mappe"));
            linker.Add(Set.addLink(baseUri, "api/sakarkiv/Saksmappe/" + m.systemID + "/utvid-til-saksmappe", Set._REL + "/utvid-til-saksmappe"));
            linker.Add(Set.addLink(baseUri, "api/MoeteOgUtvalgsbehandling/" + m.systemID + "/utvid-til-moetemappe", Set._REL + "/utvid-til-moetemappe")); //TODO
            
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/registrering", Set._REL + "/registrering", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/ny-registrering", Set._REL + "/ny-registrering"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/ny-basisregistrering", Set._REL + "/ny-basisregistrering"));
            //Skal merknad være innline? pga komposisjon (heleide objekter)
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/merknad", Set._REL + "/merknad", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/ny-merknad", Set._REL + "/ny-merknad"));
            //Valgfritt tillegg
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/undermappe", Set._REL + "/undermappe", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/ny-undermappe", Set._REL + "/ny-undermappe"));

            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/kryssreferanse", Set._REL + "/kryssreferanse", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/ny-kryssreferanse", Set._REL + "/ny-kryssreferanse"));
            
            //Enten eller? eller Skal begge slik som i 5.4.2 og 5.4.3
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Klasse/23434", Set._REL + "/referanseKlasse"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/" + "45345", Set._REL + "/referanseArkivdel"));

            m._links = linker.ToArray();

            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }

        [Route("api/arkivstruktur/Mappe/{id}")]
        [HttpPost]
        public MappeType OppdaterMappe(MappeType mappe)
        {
            return null;
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

            MappeType avsl = GetMappe(Id);
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

            testdata.Add(GetMappe("12345"));
            testdata.Add(c.GetSaksmappe("234"));

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


            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addTempLink(baseUri, "api/kodelister/Dokumentmedium", Set._REL + "/administrasjon/dokumentmedium", "?$filter&$orderby&$top&$skip"));
            linker.Add(Set.addTempLink(baseUri, "api/kodelister/Mappetype", Set._REL + "/administrasjon/mappetype", "?$filter&$orderby&$top&$skip"));


            m._links = linker.ToArray();
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

               


                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(baseUri + "api/arkivstruktur/Arkiv/" + mappe.systemID);
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

            testdata.Add(GetMappe("12345"));
            testdata.Add(c.GetSaksmappe("234"));

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


            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addTempLink(baseUri, "api/kodelister/Dokumentmedium", Set._REL + "/administrasjon/dokumentmedium", "?$filter&$orderby&$top&$skip"));
            linker.Add(Set.addTempLink(baseUri, "api/kodelister/Mappetype", Set._REL + "/administrasjon/mappetype", "?$filter&$orderby&$top&$skip"));


            m._links = linker.ToArray();
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

            MappeType m = GetMappe("123");
            m.tittel = tittel;
            testdata.Add(m);
            return testdata.AsEnumerable();
        }
    }
}
