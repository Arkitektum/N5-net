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
        public MappeListe GetMappes(ODataQueryOptions<MappeType> queryOptions)
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

            MappeListe ma = new MappeListe();
            ma.mappe = testdata.ToArray();

            //trenger vi disse når top og skip kan benyttes og en for nye objekter først bør finne det objektet det skal opprettes på?
            //List<LinkType> linkerma = new List<LinkType>();
            //linkerma.Add(addLink(baseUri, "api/arkivstruktur/Mappe/ny-mappe", "/ny-mappe"));
            //linkerma.Add(addLink(url, "?page=1", "/previus")); 
            //linkerma.Add(addLink(url, "?page=3", "/next"));

            //ma._links = linkerma.ToArray();

            return ma;
        }


        [Route("api/arkivstruktur/Arkivdel/{Id}/mappe")]
        [HttpGet]
        public MappeListe GetMapperForArkivdel(string Id)
        {
            SaksmappeController c = new SaksmappeController();

            List<MappeType> testdata = new List<MappeType>();

            testdata.Add(GetMappe("12345"));
            testdata.Add(c.GetSaksmappe("234"));

            MappeListe ma = new MappeListe();
            ma.mappe = testdata.ToArray();
            return ma;
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

        // GET api/Mappe/ND 234234
        public IEnumerable<MappeType> GetMappeByTittel(string tittel)
        {
            List<MappeType> testdata = new List<MappeType>();

            MappeType m = GetMappe("123");
            m.tittel = tittel;
            testdata.Add(m);
            return testdata.AsEnumerable();
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
            m.gradering.gradering = "jepp";
            m.gradering.graderingsdato = DateTime.Now;


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
    }
}
