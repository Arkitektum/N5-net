using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace arkitektum.kommit.noark5.api.Controllers
{
    public class SecureApiController : ApiController
    {
        [Route("api/secure")]
        [HttpGet]
        [Authorize]
        public LinkListeType Secure()
        {
            return new Api4Controller().GetApi4();
        }
    }
}