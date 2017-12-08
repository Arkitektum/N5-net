using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData.Query;
using arkitektum.kommit.noark5.api.Services;

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
    }
}
