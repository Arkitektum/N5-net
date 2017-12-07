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
        /// TODO: Not correct implemented yet
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/sakarkiv/Saksmappe/{id}/utvid-til-saksmappe")]
        [HttpGet]
        [ResponseType(typeof(SaksmappeType))]
        public IHttpActionResult UtvidTilSaksmappe(string id)
        {
            // TODO: This is not correct implemented
            return GetSaksmappe(id);
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
