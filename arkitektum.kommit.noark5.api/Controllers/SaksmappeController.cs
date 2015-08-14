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
    public class SaksmappeController : ApiController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        [Route("api/sakarkiv/Saksmappe")]
        [HttpGet]
        public IEnumerable<SaksmappeType> GetSaksmapper(ODataQueryOptions<SaksmappeType> queryOptions)
        {
           
            //TODO støtte odata filter syntaks
            queryOptions.Validate(_validationSettings);

            //Rettinghetsstyring...og alle andre restriksjoner
            List<SaksmappeType> testdata = new List<SaksmappeType>();

            testdata.Add(GetSaksmappe("12345"));
            testdata.Add(GetSaksmappe("234"));

            return testdata.ToArray();
        }

        // NY
        [Route("api/sakarkiv/Journalpost/{id}")]
        [HttpGet]
        public IEnumerable<SaksmappeType> GetSaksmappe(ODataQueryOptions<SaksmappeType> queryOptions)
        {

            //TODO støtte odata filter syntaks
            queryOptions.Validate(_validationSettings);

            //Rettinghetsstyring...og alle andre restriksjoner
            List<SaksmappeType> testdata = new List<SaksmappeType>();

            testdata.Add(GetSaksmappe("12345"));
            testdata.Add(GetSaksmappe("234"));

            return testdata.ToArray();
        }


        [Route("api/sakarkiv/Saksmappe/{id}/utvid-til-saksmappe")]
        [HttpGet]
        public SaksmappeType UtvidTilSaksmappe(string id)
        {
            return GetSaksmappe(id);
        }

        [Route("api/sakarkiv/Saksmappe/{id}")]
        [HttpGet]
        public SaksmappeType GetSaksmappe(string id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;


            SaksmappeType m = new SaksmappeType();
            m.tittel = "testmappe " + id;
            m.offentligTittel = "Regler for offentlig tittel ****";
            m.systemID = id;
            m.opprettetDato = DateTime.Now;
            m.opprettetDatoSpecified = true;
            m.opprettetAv = "tor";
            m.mappeID = "1234/2014";
            m.gradering = new GraderingType();
            m.gradering.graderingskode = new GraderingskodeType();
            m.gradering.graderingskode.kode = "jepp";
            m.gradering.graderingsdato = DateTime.Now;
            m.saksaar = "2014";
            m.sakssekvensnummer = "1234";

            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addLink(baseUri, "api/sakarkiv/Saksmappe/" + m.systemID, "self"));
            linker.Add(Set.addLink(baseUri, "api/sakarkiv/Mappe/" + m.systemID + "/avslutt-mappe", Set._REL + "/avslutt-mappe"));
            
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/registrering", Set._REL + "/registrering", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/ny-registrering", Set._REL + "/ny-registrering"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/ny-basisregistrering", Set._REL + "/ny-basisregistrering"));
            linker.Add(Set.addLink(baseUri, "api/sakarkiv/Saksmappe/" + m.systemID + "/ny-journalpost", Set._REL + "/ny-journalpost"));

            //Skal merknad være innline? pga komposisjon (heleide objekter)
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/merknad", Set._REL + "/merknad", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/ny-merknad", Set._REL + "/ny-merknad"));

            linker.Add(Set.addTempLink(baseUri, "api/sakarkiv/Saksmappe/" + m.systemID + "/sakspart", Set._REL + "/sakspart", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/sakarkiv/Saksmappe/" + m.systemID + "/ny-sakspart", Set._REL + "/ny-sakspart"));
            linker.Add(Set.addTempLink(baseUri, "api/sakarkiv/Saksmappe/" + m.systemID + "/presedens", Set._REL + "/presedens", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/sakarkiv/Saksmappe/" + m.systemID + "/ny-presedens", Set._REL + "/ny-presedens"));


            //Valgfritt tillegg
            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/undermappe", Set._REL + "/undermappe", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/ny-undermappe", Set._REL + "/ny-undermappe"));

            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/kryssreferanse", Set._REL + "/kryssreferanse", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/ny-kryssreferanse", Set._REL + "/ny-kryssreferanse"));

            linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/referanseSekundaerKlassifikasjon", Set._REL + "/referanseSekundaerKlassifikasjon", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Mappe/" + m.systemID + "/ny-sekundaerklassifikasjon", Set._REL + "/ny-sekundaerklassifikasjon"));

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
    }
}
