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
    public class KlassifikasjonssystemController : ApiController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        [Route("api/arkivstruktur/Klassifikasjonssystem")]
        [HttpGet]
        public IEnumerable<KlassifikasjonssystemType> GetKlassifikasjonssystemer(ODataQueryOptions<KlassifikasjonssystemType> queryOptions)
        {
            
            return null;
        }


        [Route("api/arkivstruktur/Klassifikasjonssystem/{id}")]
        [HttpGet]
        public KlassifikasjonssystemType GetKlassifikasjonssystem(string id)
        {
            
            return null;
        }


        [Route("api/arkivstruktur/Klassifikasjonssystem/{id}")]
        [HttpPost]
        public KlassifikasjonssystemType OppdaterKlassifikasjonssystem(KlassifikasjonssystemType klassifikasjonssystem)
        {

            return null;
        }


        [Route("api/arkivstruktur/ny-klassifikasjonssystem")]
        [HttpGet]
        public KlassifikasjonssystemType InitialiserKlassifikasjonssystem()
        {
           
            return null;
        }


        [Route("api/arkivstruktur/ny-klassifikasjonssystem")]
        [HttpPost]
        public HttpResponseMessage PostKlassifikasjonssystem(KlassifikasjonssystemType klassifikasjonssystem)
        {
            if (klassifikasjonssystem != null)
            {
                
            }
            else
            {
               
            }

            return null;
        }


        [Route("api/arkivstruktur/Arkivdel/{Id}/klassifikasjonssystem")]
        [HttpGet]
        public IEnumerable<KlassifikasjonssystemType> GetKlassifikasjonssystemerIArkivdel(ODataQueryOptions<KlassifikasjonssystemType> queryOptions)
        {
            return null;
        }

        [Route("api/arkivstruktur/Arkivdel/{Id}/klassifikasjonssystem/{klassifikasjonssystemId}")]
        [HttpGet]
        public KlassifikasjonssystemType GetKlassifikasjonssystemIArkivdel(ODataQueryOptions<KlassifikasjonssystemType> queryOptions)
        {
            return null;
        }

        [Route("api/arkivstruktur/Arkivdel/{Id}/klassifikasjonssystem/{klassifikasjonssystemId}")]
        [HttpPost]
        public HttpResponseMessage OppdaterKlassifikasjonssystemIArkivdel(KlassifikasjonssystemType klassifikasjonssystem)
        {
            return null;
        }

        [Route("api/arkivstruktur/Arkivdel/{arkivdelId}/ny-klassifikasjonssystem")]
        [HttpGet]
        public KlassifikasjonssystemType InitialiserKlassifikasjonssystemIArkivdel(ODataQueryOptions<KlassifikasjonssystemType> queryOptions)
        {
            return null;
        }

        [Route("api/arkivstruktur/Arkivdel/{arkivdelId}/ny-klassifikasjonssystem")]
        [HttpPost]
        public HttpResponseMessage PostKlassifikasjonssystemIArkivdel(KlassifikasjonssystemType klassifikasjonssystem)
        {
            return null;
        }
    }
}
