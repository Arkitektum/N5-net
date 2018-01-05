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
using Thinktecture.IdentityModel.Tokens;

namespace arkitektum.kommit.noark5.api.Controllers
{
    /// <summary>
    /// Provides api methods for saksmappe - api/sakarkiv/Saksmappe
    /// </summary>
    public class SaksmappeController : ApiController
    {
        private static readonly ODataValidationSettings ValidationSettings = new ODataValidationSettings();

        /// <summary>
        /// Returns all Saksmappe
        /// </summary>
        /// <param name="queryOptions"></param>
        /// <returns></returns>
        [Route("api/sakarkiv/Saksmappe")]
        [HttpGet]
        public IEnumerable<SaksmappeType> GetSaksmapper(ODataQueryOptions<SaksmappeType> queryOptions)
        {
            List<SaksmappeType> results = new List<SaksmappeType>();

            queryOptions.Validate(ValidationSettings);

            IQueryable<SaksmappeType> filtered = queryOptions.ApplyTo(MockNoarkDatalayer.Saksmapper.AsQueryable()) as IQueryable<SaksmappeType>;

            if (filtered != null)
                results.AddRange(filtered);

            return results.ToArray();
        }

        /// <summary>
        /// Expand Mappe to Saksmappe
        /// Required fields:
        /// * saksdato
        /// * saksansvarlig
        /// * saksstatus
        /// 
        /// Implementation of this method should take care of extra fields provided by the client that exists in saksmappe and save them as well.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="saksmappe"></param>
        /// <returns></returns>
        [Route("api/sakarkiv/Saksmappe/{id}/utvid-til-saksmappe")]
        [HttpPut]
        [ResponseType(typeof(SaksmappeType))]
        public IHttpActionResult UtvidTilSaksmappe(string id, SaksmappeType saksmappeOppdatert)
        {
            MappeType mappe = MockNoarkDatalayer.GetMappeById(id);

            if (mappe == null)
            {
                return BadRequest("Invalid saksmappe id, saksmappe could not be found");
            }
            if (saksmappeOppdatert.saksdato == DateTime.MinValue)
            {
                return BadRequest("saksdato is required to upgrade mappe to saksmappe.");
            }
            if (string.IsNullOrWhiteSpace(saksmappeOppdatert.saksansvarlig))
            {
                return BadRequest("saksansvarlig is required to upgrade mappe to saksmappe.");
            }
            if (string.IsNullOrWhiteSpace(saksmappeOppdatert.saksstatus?.kode))
            {
                return BadRequest("saksstatus is required to upgrade mappe to saksmappe.");
            }

            var saksmappe = new SaksmappeType();
            saksmappe.saksdato = saksmappeOppdatert.saksdato;
            saksmappe.saksansvarlig = saksmappeOppdatert.saksansvarlig;
            saksmappe.saksstatus = saksmappeOppdatert.saksstatus;

            saksmappe.oppdatertDato = DateTime.Now;
            saksmappe.oppdatertDatoSpecified = true;

            // copy fields from mappe
            saksmappe.tittel = mappe.tittel;
            saksmappe.offentligTittel = mappe.offentligTittel;
            saksmappe.systemID = mappe.systemID;
            saksmappe.opprettetDato = mappe.opprettetDato;
            saksmappe.opprettetDatoSpecified = mappe.opprettetDatoSpecified;
            saksmappe.oppdatertAv = mappe.oppdatertAv;
            saksmappe.mappeID = mappe.mappeID;
            saksmappe.gradering = mappe.gradering;
            saksmappe.klasse = mappe.klasse;
            saksmappe.merknad = mappe.merknad;

            saksmappe.RepopulateHyperMedia();

            MockNoarkDatalayer.Saksmapper.RemoveAll(x => x.systemID == id);
            MockNoarkDatalayer.Saksmapper.Add(saksmappe);

            return Ok(saksmappe);
        }

        /// <summary>
        /// Return a single saksmappe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/sakarkiv/Saksmappe/{id}")]
        [HttpGet]
        [ResponseType(typeof(SaksmappeType))]
        public IHttpActionResult GetSaksmappe(string id)
        {
            var saksmappe = MockNoarkDatalayer.GetSaksmappeById(id);

            if (saksmappe == null)
            {
                return NotFound();
            }

            return Ok(saksmappe);
        }


        /// <summary>
        /// Returns all klasse coupled to the given saksmappe
        /// </summary>
        /// <param name="queryOptions"></param>
        /// <param name="id">Saksmappe Id</param>
        /// <returns></returns>
        [Route("api/sakarkiv/Saksmappe/{id}/sekundaerklassifikasjoner")]
        [HttpGet]
        public IEnumerable<KlasseType> GetSekundaerklassifikasjoner(ODataQueryOptions<KlasseType> queryOptions, string id)
        {
            var saksmappe = MockNoarkDatalayer.GetSaksmappeById(id) ?? throw new ArgumentNullException("Saksmappen finnes ikke");
            var sekundaerklassifikasjoner = saksmappe.sekundaerklassifikasjon ?? throw new ArgumentNullException("Sekundærklassifikasjonen finnes ikke");

            queryOptions.Validate(ValidationSettings);

            return sekundaerklassifikasjoner.ToArray();
        }



        /// <summary>
        /// Sletter sekundærklassifikasjon
        /// </summary>
        /// <param name="id">Saksmappe Id</param>
        /// <param name="systemId">Sekundærklassifikasjoner sin klasseId</param>
        /// <returns></returns>
        [Route("api/sakarkiv/Saksmappe/{id}/sekundaerklassifikasjoner/{systemId}")]
        [HttpDelete]
        public HttpResponseMessage SlettSekundaerklassifikasjon(string id, string systemId)
        {
            if (id == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            MockNoarkDatalayer.DeleteSekundaerklassifikasjonFromSaksmappe(id, systemId);

            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            return response;
        }


        /// <summary>
        /// Legg til sekundærklassifikasjon
        /// </summary>
        /// <param name="id">Saksmappe Id</param>
        /// <param name="klasseType">KlasseType objekt</param>
        /// <returns></returns>
        [Route("api/sakarkiv/Saksmappe/{id}/sekundaerklassifikasjoner")]
        [HttpPost]
        [ResponseType(typeof(KlasseType))]
        public HttpResponseMessage NySekundaerklassifikasjon(string id, KlasseType klasseType)
        {
            // Testdata... 
            klasseType = CreateKlasseTypeExample();

            if (id == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            MockNoarkDatalayer.AddSekundaerklassifikasjonToSaksmappe(id, klasseType);

            var url = HttpContext.Current.Request.Url;
            var baseUri = new UriBuilder( url.Scheme, url.Host, url.Port).Uri;
            var response = Request.CreateResponse(HttpStatusCode.Created, klasseType);
            response.Headers.Location = new Uri(baseUri + "api/sakarkiv/Saksmappe/{id}/sekundaerklassifikasjoner");
            return response;
        }


        /// <summary>
        /// Endre sekundærklassifikasjoner
        /// </summary>
        /// <param name="id">Saksmappe Id</param>
        /// <param name="klasseType">Nye klassetyper</param>
        /// <returns></returns>
        [Route("api/sakarkiv/Saksmappe/{id}/sekundaerklassifikasjoner")]
        [HttpPut]
        [ResponseType(typeof(KlasseType))]
        public HttpResponseMessage SettSekundaerklassifikasjon(string id, KlasseType[] klasseType)
        {
            // Testdata... 
            klasseType = CreateNewKlasseTypeArray();
            
            if (id == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            MockNoarkDatalayer.SetSekundaerklassifikasjonerToSaksmappe(id, klasseType);

            var url = HttpContext.Current.Request.Url;
            var baseUri = new UriBuilder(url.Scheme, url.Host, url.Port).Uri;
            var response = Request.CreateResponse(HttpStatusCode.Created, klasseType);
            response.Headers.Location = new Uri(baseUri + "api/sakarkiv/Saksmappe/{id}/sekundaerklassifikasjoner");
            return response;
        }

        private KlasseType[] CreateNewKlasseTypeArray()
        {
            var klasseTypeArray = new KlasseType[3];
            klasseTypeArray[0] = CreateKlasseTypeExample(1);
            klasseTypeArray[1] = CreateKlasseTypeExample(2);
            klasseTypeArray[2] = CreateKlasseTypeExample(3);

            return klasseTypeArray;
        }


        private KlasseType CreateKlasseTypeExample(int i = 1)
        {
            return new KlasseType()
            {
                tittel = "Tittel " + i,
                systemID = "syst_" + i,
                beskrivelse = "Dette er en beskrivelse av " + i,
                klasseID = "KlasseId" + i,
                oppdatertDato = DateTime.Now,
                oppdatertDatoSpecified = true,
                oppdatertAv = "Test navn" + i,
                referanseOppdatertAv = "",
                opprettetDato = DateTime.Now,
                opprettetDatoSpecified = true,
                opprettetAv = "Test navn" + i,
                referanseOpprettetAv = "Test navn" + i,
            };
        }


    }
}
