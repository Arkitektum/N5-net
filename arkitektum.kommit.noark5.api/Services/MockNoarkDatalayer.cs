using System;
using System.Collections.Generic;
using arkitektum.kommit.noark5.api.Models;

namespace arkitektum.kommit.noark5.api.Services
{
    /// <summary>
    /// Produces testdata for use in the API.
    /// </summary>
    public class MockNoarkDatalayer
    {
        private static readonly Random Random = new Random();

        public List<ArkivskaperType> Arkivskaper = new List<ArkivskaperType>();
        public List<ArkivType> Arkiver = new List<ArkivType>();
        public List<ArkivdelType> Arkivdeler = new List<ArkivdelType>();
        public List<MappeType> Mapper = new List<MappeType>();

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

            OpprettMapper();
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
            arkivskaper.RepopulateHyperMedia();
            Arkivskaper.Add(arkivskaper); // add to global list

            return arkivskaper;
        }

        private ArkivType OpprettArkiv()
        {
            var arkiv =  new ArkivType()
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
            arkiv.RepopulateHyperMedia();
            return arkiv;
        }

        /// <summary>
        /// Find a mappe by id. Uses the field systemID to search within.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MappeType GetMappeById(string id)
        {
            return Mapper.Find(m => m.systemID == id);
        }

        private void OpprettMapper()
        {
            for (int i = 0; i < 10; i++)
            {
                Mapper.Add(OpprettMappe((i+1).ToString()));
            }
        }

        private MappeType OpprettMappe(string id)
        {
            MappeType m = new MappeType();
            m.tittel = GetRandomAdjective() + " testmappe " + id;
            m.offentligTittel = "Dette er en offentlig tittel ****";
            m.systemID = id;
            m.opprettetDato = DateTime.Now;
            m.opprettetDatoSpecified = true;
            m.opprettetAv = OpprettetAv();
            m.mappeID = id + "/2014";
            m.gradering = new GraderingType
            {
                graderingskode = new GraderingskodeType {kode = "B"},
                graderingsdato = DateTime.Now
            };
            m.klasse = new KlasseType() { klasseID = "12345678901", tittel = "12345678901", klassifikasjonssystem = new KlassifikasjonssystemType { klassifikasjonstype = new KlassifikasjonstypeType { kode = "PNR", beskrivelse = "Personnr" } } }; //klassifikasjonssystem? rekkefølge?
            List<MerknadType> merknader = new List<MerknadType>
            {
                new MerknadType
                {
                    merknadstype = new MerknadstypeType {kode = "B"},
                    merknadstekst = "test"
                }
            };
            m.merknad = merknader.ToArray();
            m.LinkList.Clear();
            m.RepopulateHyperMedia();
            return m;
        }

        private string OpprettetAv()
        {
            return FirstNames[RandomNumber(0, FirstNames.Length - 1)];
        }


        private string GenerateUuuid()
        {
            return Guid.NewGuid().ToString();
        }

        private int RandomNumber(int min, int max)
        {
            return Random.Next(min, max);
        }

        private string GetRandomKommune()
        {
            return FirstLetterToUpper(GetRandomAdjective()) + " kommune";
        }

        private string GetRandomAdjective()
        {
            return Adjectives[RandomNumber(0, Adjectives.Length-1)];
        }

        private static readonly string[] FirstNames = { "Emma", "William", "Oliver", "Aksel", "Maja", "Sofie", "Nora", "Emilie", "Filip", "Jakob" };
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