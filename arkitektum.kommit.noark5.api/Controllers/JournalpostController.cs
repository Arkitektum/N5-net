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
    public class JournalpostController : ApiController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        [Route("api/sakarkiv/journalpost")]
        public IEnumerable<JournalpostType> GetJournalposter(ODataQueryOptions<JournalpostType> queryOptions)
        {
           
            //TODO støtte odata filter syntaks
            queryOptions.Validate(_validationSettings);

            //Rettighetsstyring...og alle andre restriksjoner
            List<JournalpostType> testdata = new List<JournalpostType>();


            //TODO Håndtere filter...

            if (queryOptions.Filter != null)
            {
                var q = queryOptions.Filter.FilterClause.Expression;
                if (queryOptions.Filter.RawValue.Contains("systemID"))
                {
                    var mockarkiv = GetJournalpost("fra filter eller ");
     
                    testdata.Add(GetJournalpost(((Microsoft.Data.OData.Query.SemanticAst.ConstantNode)(((Microsoft.Data.OData.Query.SemanticAst.BinaryOperatorNode)(queryOptions.Filter.FilterClause.Expression)).Right)).Value.ToString()));
                }
            }

            if(queryOptions.Top == null)
            {
                testdata.Add(GetJournalpost(Guid.NewGuid().ToString()));
                testdata.Add(GetJournalpost(Guid.NewGuid().ToString()));
                testdata.Add(GetJournalpost(Guid.NewGuid().ToString()));
                testdata.Add(GetJournalpost(Guid.NewGuid().ToString()));
                testdata.Add(GetJournalpost(Guid.NewGuid().ToString()));
            }
            else if (queryOptions.Top != null)
            {
                while (testdata.Count < queryOptions.Top.Value)
                {
                    testdata.Add(GetJournalpost(Guid.NewGuid().ToString()));
                }
            }
            


            return testdata.AsEnumerable();
        }


       

        [Route("api/sakarkiv/journalpost/{id}")]
        [HttpGet]
        public JournalpostType GetJournalpost(string id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            JournalpostType m = new JournalpostType();
            m.systemID = id;
            m.opprettetDato = DateTime.Now;
            m.opprettetDatoSpecified = true;
            m.oppdatertDato = DateTime.Now;
            m.journaldato = DateTime.Now;
            m.tittel = "journalpost - " + m.systemID;
            m.oppdatertAv = "bruker";
            m.LinkList.Clear();
            m.RepopulateHyperMedia();

            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }



    }
}
