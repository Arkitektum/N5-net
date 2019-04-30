using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData.Query;

namespace arkitektum.kommit.noark5.api.Controllers
{
    public class DokumentobjektController : System.Web.Http.ApiController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        [Route("api/arkivstruktur/Dokumentobjekt")]
        [HttpGet]
        public IEnumerable<DokumentobjektType> GetDokumentobjekter(ODataQueryOptions<DokumentobjektType> queryOptions)
        {
            

            List<DokumentobjektType> testdata = new List<DokumentobjektType>();
           
            testdata.Add(GetDokumentobjekt(Guid.NewGuid().ToString()));
            testdata.Add(GetDokumentobjekt(Guid.NewGuid().ToString()));
            testdata.Add(GetDokumentobjekt(Guid.NewGuid().ToString()));
            testdata.Add(GetDokumentobjekt(Guid.NewGuid().ToString()));
            testdata.Add(GetDokumentobjekt(Guid.NewGuid().ToString()));

            return testdata.AsEnumerable();
        }

        [Route("api/arkivstruktur/Dokumentobjekt/{id}")]
        [HttpGet]
        public DokumentobjektType GetDokumentobjekt(string id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            DokumentobjektType m = new DokumentobjektType();
            m.systemID = id;
            m.versjonsnummer = "1";
            m.variantformat = new VariantformatType() {kode = "A", beskrivelse = "Arkivformat" };
            m.format = new FormatType() { kode = "RA-PDF", beskrivelse = "PDF/A - ISO 19005-1:2005" };
            m.opprettetDato = DateTime.Now;

            //Url arkivkjerne til intern eller ekstern klient?
            m.referanseDokumentfil = baseUri + "api/arkivstruktur/Dokumentobjekt/" + m.systemID + "/referanseFil"; //eller som link?
            m.RepopulateHyperMedia();

            //Flytte mime type og filnavn til denne? Fjerne Filinnhold og Filreferanse? unødvendig når en kan streame opp eller ned filer
            //Trenger ikke base64 og opplastingsansvar til kjernen? Stream begrensning på 2GB? OK eller trengs også PULL funksjonene med filreferanse på store filer?


            //List<LinkType> linker = new List<LinkType>();
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Dokumentobjekt/" + m.systemID, "self"));
            //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Dokumentobjekt/" + m.systemID + "/konvertering", Set._REL + "/konvertering", "?$filter&$orderby&$top&$skip&$search"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Dokumentobjekt/" + m.systemID + "/ny-konvertering", Set._REL + "/ny-konvertering"));//Hører egentlig til administrasjon? vises hvis rolle admin?

            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Dokumentobjekt/" + m.systemID + "/referanseFil", Set._REL + "/referanseFil")); //POST laster opp, PUT oppdaterer, DELETE sletter og GET laster ned?

            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Dokumentbeskrivelse/4663", Set._REL + "/referanseDokumentbeskrivelse"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Registrering/45344", Set._REL + "/referanseRegistrering"));
            //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Dokumentbeskrivelse/4663", Set._REL + "/referanseDokumentbeskrivelse"));

            //m._links = linker.ToArray();
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }

        // NY
        [Route("api/arkivstruktur/Dokumentobjekt/{id}")]
        [HttpPost]
        public HttpResponseMessage OppdaterDokumentobjekt(string id)
        {
            return null;
        }

        // NY
        [Route("api/arkivstruktur/ny-dokumentobjekt")]
        [HttpGet]
        public DokumentobjektType InitialiserDokumentobjekt(string id)
        {
            return null;
        }

        // NY
        [Route("api/arkivstruktur/ny-dokumentobjekt")]
        [HttpPost]
        public HttpResponseMessage PostDokumentobjekt(string id)
        {
            return null;
        }


        [Route("api/arkivstruktur/Registrering/{Id}/dokumentobjekt")]
        [HttpGet]
        public IEnumerable<DokumentobjektType> GetDokumentobjekterFraRegistrering(string Id)
        {
            List<DokumentobjektType> testdata = new List<DokumentobjektType>();

            testdata.Add(GetDokumentobjekt(Guid.NewGuid().ToString()));
            testdata.Add(GetDokumentobjekt(Guid.NewGuid().ToString()));
            testdata.Add(GetDokumentobjekt(Guid.NewGuid().ToString()));
            testdata.Add(GetDokumentobjekt(Guid.NewGuid().ToString()));
            testdata.Add(GetDokumentobjekt(Guid.NewGuid().ToString()));

            return testdata.AsEnumerable();
        }

        [Route("api/arkivstruktur/Registrering/{Id}/dokumentobjekt/{dokumentobjektId}")]
        [HttpGet]
        public DokumentobjektType GetDokumentobjektFraRegistrering(string Id)
        {
            return null;
        }

        [Route("api/arkivstruktur/Registrering/{Id}/dokumentobjekt/{dokumentobjektId}")]
        [HttpPost]
        public DokumentobjektType OppdaterDokumentobjektFraRegistrering(string Id)
        {
            return null;
        }

        [Route("api/arkivstruktur/Registrering/{Id}/ny-dokumentobjekt")]
        [HttpGet]
        public DokumentobjektType InitialiserNyttDokumentobjekterIRegistrering(ODataQueryOptions<DokumentobjektType> queryOptions)
        {
            return null;
        }

        [Route("api/arkivstruktur/Registrering/{Id}/ny-dokumentobjekt")]
        [HttpPost]
        public HttpResponseMessage PostDokumentobjektIRegistrering(string Id, DokumentobjektType dokumentobjekt)
        {
            if (dokumentobjekt != null)
            {


                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, dokumentobjekt);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = dokumentobjekt.systemID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        //Nedlasting av fil til et Dokumentobjekt
        [Route("api/arkivstruktur/Dokumentobjekt/{Id}/referanseFil")]
        [HttpGet]
        public HttpResponseMessage GetFile(string Id)
        {
            //TODO hente Dokumentobjekt for filnavn og mimetype
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var path = root + @"\eksempel.pdf";
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/pdf");
            return result;
        }

        //Opplasting av fil til et Dokumentobjekt
        [Route("api/arkivstruktur/Dokumentobjekt/{Id}/referanseFil")]
        [HttpPost]
        public async Task<HttpResponseMessage> UploadFile(string Id)
        {
            //TODO finnes sikkert bedre og mer effektive måter å streame filer på
            //TODO hente Dokumentobjekt for filnavn og mimetype

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

 
        [Route("api/arkivstruktur/Dokumentobjekt/{Id}/ny-referansefil")]
        [HttpGet]
        public DokumentobjektType InitialiserReferanseFilIDokumentobjekt(string Id)
        {
            return null;
        }

        //Opplasting av fil til et Dokumentobjekt
        [Route("api/arkivstruktur/Dokumentobjekt/{Id}/ny-referansefil")]
        [HttpPost]
        public HttpResponseMessage PostReferansefilIDokumentobjekt(string Id)
        {
            return null;
        }




        
        
        
    }
}
