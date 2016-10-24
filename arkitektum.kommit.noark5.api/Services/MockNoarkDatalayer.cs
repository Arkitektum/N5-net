using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkitektum.kommit.noark5.api.Services
{
    public class MockNoarkDatalayer
    {
        public List<ArkivskaperType> Arkivskaper = new List<ArkivskaperType>();
        public List<ArkivType> Arkiver = new List<ArkivType>();
        public List<ArkivdelType> Arkivdeler = new List<ArkivdelType>();

        /*
        public List<KlassifikasjonssystemType> klassifikasjonssystemer;
        public List<KlasseType> klasser;
        public List<MappeType> mapper;
        public List<RegistreringType> registreringer;
        public List<BasisregistreringType> basisregistreringer; // ??? bare som er spesialisering av registreringer? 
        public List<DokumentbeskrivelseType> dokumentbeskrivelser;
        public List<DokumentobjektType> dokumentobjekter;
        */
        public static DokumentmediumType ElektroniskDokumentmedium = new DokumentmediumType
        {
            kode = "E",
            beskrivelse = "Elektronisk arkiv"
        };

        public static ArkivstatusType AvsluttetArkivstatus = new ArkivstatusType
        {
            kode = "A",
            beskrivelse = "Avsluttet"
        };

        public static KlassifikasjonstypeType GbnKlassifikasjonstype = new KlassifikasjonstypeType
        {
            kode = "GBN",
            beskrivelse = "Gårds- og bruksnummer"
        };

        public static FormatType PdfFormat = new FormatType
        {
            kode = "RA-PDF",
            beskrivelse = "PDF/A - ISO 19005-1:2005"
        };

        public static VariantformatType ArkivFormat = new VariantformatType
        {
            kode = "A",
            beskrivelse = "Arkivformat"
        };

        public MockNoarkDatalayer()
        {
            Arkiver.Add(OpprettArkiv());
            Arkiver.Add(OpprettArkiv());
            Arkiver.Add(OpprettArkiv());

            //IGenerationSessionFactory factory = AutoPocoContainer.Configure(x =>
            //{
            //    x.Conventions(c =>
            //    {
            //        c.UseDefaultConventions();
            //    });
            //    x.AddFromAssemblyContainingType<ArkivType>();
            //});
            //IGenerationSession session = factory.CreateSession();



            //arkivskaper = session.List<ArkivskaperType>(3)
            //    .Impose(x => x.systemID, Guid.NewGuid().ToString())
            //    .Impose(x => x.arkivskaperID, "12345678")
            //    .Impose(x => x.arkivskaperNavn, "Test kommune")
            //    .Impose(x => x.beskrivelse, "Lorem ipsum")
            //    .Impose(x => x.oppdatertAv, null)
            //    .Impose(x => x.referanseOppdatertAv, null)
            //    .Impose(x => x.opprettetAv, "Brukernavn")
            //    .Impose(x => x.referanseOpprettetAv, Guid.NewGuid().ToString())
            //    .Get().ToList();




            //arkivdeler = session.List<ArkivdelType>(10)
            //    .Random(3)
            //          .Impose(x => x.tittel, "Arkivdel byggesak")
            //          .Impose(x => x.arkiv, arkiver[0])
            //    .All()
            //        .Impose(x => x.systemID, Guid.NewGuid().ToString())
            //        .Impose(x => x.beskrivelse, "Lorem ipsum")
            //    .Get().ToList();

            //klasser = session.List<KlasseType>(10)
            //    .Random(3)
            //          .Impose(x => x.tittel, "klasse eiendom")
            //          .Impose(x => x.klasseID, "12/23")
            //    .All()
            //        .Impose(x => x.systemID, Guid.NewGuid().ToString())
            //        .Impose(x => x.beskrivelse, "Lorem ipsum")
            //    .Get().ToList();

            //klassifikasjonssystemer = session.List<KlassifikasjonssystemType>(10)
            //    .Random(3)
            //          .Impose(x => x.tittel, "klassifikasjonssystem eiendom")
            //          .Impose(x => x.klassifikasjonstype, k)
            //          .Impose(x => x.klasse, klasser.ToArray())
            //    .All()
            //        .Impose(x => x.systemID, Guid.NewGuid().ToString())
            //        .Impose(x => x.beskrivelse, "Lorem ipsum")
            //    .Get().ToList();


            //BUG Unrecognised type requested

            //mapper = session.List<MappeType>(10)
            //    .Random(3)
            //          .Impose(x => x.tittel, "mappe byggesak")
            //          .Impose(x => x.dokumentmedium, d)
            //          .Impose(x => x.arkivdel, arkivdeler[0])
            //    .All()
            //        .Impose(x => x.systemID, Guid.NewGuid().ToString())
            //        .Impose(x => x.beskrivelse, "Lorem ipsum")
            //        .Impose(x => x.klasse, null)
            //        .Impose(x => x.referanseForelderMappe, Guid.NewGuid().ToString()) //Kan da ikke være knyttet til arkivdel
            //        .Impose(x => x.kryssreferanse, null)
            //        .Impose(x => x.nasjonalidentifikator, null)
            //        .Impose(x => x.registrering, null)
            //        .Impose(x => x.virksomhetsspesifikkeMetadata, null)
            //    .Get().ToList();

            //registreringer = session.List<RegistreringType>(10).Get().ToList();

            //basisregistreringer = session.List<BasisregistreringType>(10).Get().ToList();



            //FilreferanseType fil = new FilreferanseType();
            //fil.filnavn = "eksempel.pdf";
            //fil.uri = "ekstern/intern/rtyrty/dfdrert/sdfsdf/eksempel.pdf";
            //fil.mimeType = "application/pdf";
            //ElektroniskSignaturType sign = new ElektroniskSignaturType();

            //Kobling til dokumentbeskrivelse?
            //dokumentobjekter = session.List<DokumentobjektType>(10)
            //    .Impose(x => x.systemID, Guid.NewGuid().ToString())
            //    .Impose(x => x.dokumentfil,fil)
            //    .Impose(x => x.elektroniskSignatur, sign)
            //    .Impose(x => x.filstoerrelse, "12345")
            //    .Impose(x => x.format, f)
            //    .Impose(x => x.formatDetaljer, "komprimert med xx")
            //    .Impose(x => x.konvertering, null)
            //    .Impose(x => x.variantformat, v)
            //    .Impose(x => x.versjonsnummer, "2")
            //    .Get().ToList();

            //dokumentbeskrivelser = session.List<DokumentbeskrivelseType>(10)
            //    .Impose(x => x.systemID, Guid.NewGuid().ToString())
            //    .Impose(x => x.dokumentobjekt, dokumentobjekter.ToArray())
            //    .Impose(x => x.elektroniskSignatur, sign)
            //    .Get().ToList();
            //arkiver.ForEach(x => x.RepopulateHyperMedia());
            //arkivskaper.ForEach(x => x.RepopulateHyperMedia());
            //arkivdeler.ForEach(x => x.RepopulateHyperMedia());
        }


        private ArkivskaperType OpprettArkivskaper()
        {
            var arkivskaper = new ArkivskaperType
            {
                systemID = GenerateUuuid(),
                arkivskaperID = RandomNumber(100, 1000).ToString(),
                arkivskaperNavn = GetRandomKommune(),
                beskrivelse = "Lorem ipsum",
                opprettetAv = "brukernavn",
                referanseOppdatertAv = GenerateUuuid()
            };

            Arkivskaper.Add(arkivskaper); // add to global list

            return arkivskaper;
        }

        private ArkivType OpprettArkiv()
        {
            return new ArkivType()
            {
                tittel = FirstLetterToUpper(GetRandomAdjective()) + " arkiv",
                arkivstatus = AvsluttetArkivstatus,
                dokumentmedium = ElektroniskDokumentmedium,
                avsluttetDato = DateTime.Now,
                avsluttetDatoSpecified = true,
                systemID = GenerateUuuid(),
                beskrivelse = "lorem ipsum " + GetRandomAdjective(),
                arkivskaper = OpprettArkivskaper(),
                avsluttetAv = "brukernavn",
                referanseAvsluttetAv = GenerateUuuid(),
                opprettetAv = "brukernavn",
                referanseOpprettetAv = GenerateUuuid()
            };
        }



        private string GenerateUuuid()
        {
            return Guid.NewGuid().ToString();
        }

        private int RandomNumber(int min, int max)
        {
            var random = new Random(DateTime.Now.Millisecond);
            return random.Next(min, max);
        }

        private string GetRandomKommune()
        {
            return FirstLetterToUpper(GetRandomAdjective()) + " kommune";
        }

        private string GetRandomAdjective()
        {
            return Adjectives[RandomNumber(0, Adjectives.Length-1)];
        }

        private static readonly string[] Adjectives = {"allergisk", "begeistret", "bred", "flink", "fremmed", "høflig", "irritert", "klok" };

        private static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }
}