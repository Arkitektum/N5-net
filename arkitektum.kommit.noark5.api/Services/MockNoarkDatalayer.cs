using System;
using System.Collections.Generic;
using System.Linq;

namespace arkitektum.kommit.noark5.api.Services
{
    internal static class MockNoarkDatalayer
    {
        private static readonly Random Random = new Random();

        internal static List<ArkivskaperType> Arkivskaper = new List<ArkivskaperType>();
        internal static List<ArkivType> Arkiver = new List<ArkivType>();
        internal static List<ArkivdelType> Arkivdeler = new List<ArkivdelType>();
        internal static List<MappeType> Mapper = new List<MappeType>();
        internal static List<SaksmappeType> Saksmapper = new List<SaksmappeType>();
        internal static List<RegistreringType> Registreringer = new List<RegistreringType>();
        internal static List<KryssreferanseType> Kryssreferanser = new List<KryssreferanseType>();

        /// <summary>
        /// Number of examples to be generated of each type. The number of items in the example arrays should be the same size
        /// </summary>
        private static int NumberOfExamples = 10;
        private static DateTime FixedDate = new DateTime(2017, 2, 28);
        private static readonly string[] FirstNames = { "Emma", "William", "Oliver", "Aksel", "Maja", "Sofie", "Nora", "Emilie", "Filip", "Jakob" };
        private static readonly string[] Adjectives = { "allergisk", "begeistret", "bred", "flink", "fremmed", "høflig", "irritert", "klok", "gammel", "lunken" };


        static MockNoarkDatalayer()
        {
            ResetMockData();
        }

        internal static void ResetMockData()
        {
            Arkiver.Clear();
            Arkivdeler.Clear();
            Arkivskaper.Clear();
            Mapper.Clear();
            Saksmapper.Clear();
            Registreringer.Clear();
            Kryssreferanser.Clear();

            OpprettArkiver();
            OpprettMapper();
            OpprettSaksmapper();
            OpprettRegistreringer();
        }

        public static KlasseType OpprettKlasse(int i)
        {
            var klasseType = new KlasseType();
            klasseType.tittel = "Tittel" + i;
            klasseType.systemID = i + "_sysId";
            klasseType.beskrivelse = "Dette er en beskrivelse av" + i;
            klasseType.klasseID = "KlasseId " + i;
            klasseType.oppdatertDato = GetDato(i);
            klasseType.oppdatertDatoSpecified = true;
            klasseType.oppdatertAv = GetName(i);
            klasseType.referanseOppdatertAv = GetName(i);
            klasseType.opprettetDato = GetDato(i);
            klasseType.opprettetDatoSpecified = true;
            klasseType.opprettetAv = GetName(i);
            klasseType.referanseOpprettetAv = GetName(i);
            return klasseType;
        }


        private static void OpprettSaksmapper()
        {
            for (int i = 1; i <= NumberOfExamples; i++)
            {
                Saksmapper.Add(OpprettSaksmappe(i));
            }
        }

        private static SaksmappeType OpprettSaksmappe(int index)
        {
            var saksmappe = new SaksmappeType
            {
                systemID = index.ToString(),
                mappeID = $"100{index}/2017",
                tittel = Tittel("saksmappe", index),
                opprettetDato = GetDato(index),
                opprettetDatoSpecified = true,
                oppdatertAv = GetName(index),
                saksaar = "2017",
                sakssekvensnummer = index.ToString(),
                sakspart = OpprettSakspart(index),
                saksdato = GetDato(index),
                nasjonalidentifikator = OpprettNasjonalidentifikator(index),
                sekundaerklassifikasjon = OpprettSekundaerklassifikasjoner()
            };
            saksmappe.sakspart[0].RepopulateHyperMedia();
            saksmappe.RepopulateHyperMedia();
            return saksmappe;
        }

        private static KlasseType[] OpprettSekundaerklassifikasjoner()
        {
            var klasseTyper = new List<KlasseType>();

            for (int i = 1; i <= 2; i++)
            {
                klasseTyper.Add(OpprettKlasse(i));
            }
            return klasseTyper.ToArray();
        }

        /// <summary>
        /// Find a registrering by id. Uses the field systemID to search within.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RegistreringType GetRegistreringById(string id)
        {
            return Registreringer.Find(m => m.systemID == id);
        }

        private static void OpprettRegistreringer()
        {
            for (int i = 1; i <= NumberOfExamples; i++)
            {
                Registreringer.Add(OpprettRegistrering(i));
            }
        }

        private static RegistreringType OpprettRegistrering(int index, string mappeId = null)
        {
            var registrering = new RegistreringType();
            registrering.LinkList = null;
            registrering.systemID = index.ToString();
            registrering.oppdatertDato = GetDato(index);
            registrering.oppdatertDatoSpecified = true;
            registrering.opprettetDato = GetDato(index);
            registrering.opprettetDatoSpecified = true;
            registrering.opprettetAv = GetName(index);
            registrering.oppdatertAv = GetName(index);
            registrering.referanseOppdatertAv = GetName(index);
            registrering.referanseOpprettetAv = GetName(index);
            registrering.logg = null;
            registrering.arkivertDato = GetDato(index);
            registrering.arkivertDatoSpecified = true;
            registrering.arkivertAv = GetName(index);
            registrering.referanseArkivertAv = GetName(index);
            registrering.kassasjon = new KassasjonType();
            registrering.skjerming = new SkjermingType();
            registrering.gradering = new GraderingType();
            registrering.referanseArkivdel = null;
            registrering.klasse = null;
            registrering.mappe = mappeId == null ? null : GetMappeById(mappeId);
            registrering.arkivdel = null;
            registrering.nasjonalidentifikator = OpprettNasjonalidentifikator(index);
            return registrering;
        }


        private static AbstraktNasjonalidentifikatorType[] OpprettNasjonalidentifikator(int index)
        {
            bool opprettPersonId = (index % 2 == 1);

            if (opprettPersonId)
            {
                return new AbstraktNasjonalidentifikatorType[]
                {
                    new PersonidentifikatorType()
                    {
                        foedselsnummer = "12334566778"
                    }
                };
            }
            return new AbstraktNasjonalidentifikatorType[]
            {
                new BygningType
                {
                    byggidentifikator = new ByggIdent() {bygningsNummer = "12345678"}
                }
            };
        }

        private static AbstraktSakspartType[] OpprettSakspart(int index)
        {
            bool opprettPersonSakspart = (index % 2 == 1);

            if (opprettPersonSakspart)
            {
                return new AbstraktSakspartType[]
                {
                    new SakspartPersonType()
                    {
                        systemID = index.ToString(),
                        foedselsnummer = "12334566778",
                        navn = "Jan Johansen",
                        sakspartRolle = new SakspartRolleType()
                        {
                            kode = "KLI",
                            beskrivelse = "Klient"
                        }
                    }
                };
            }
            return new AbstraktSakspartType[]
            {
                new SakspartEnhetType()
                {
                    systemID = index.ToString(),
                    organisasjonsnummer = "998877665",
                    navn = "Advokatselskap AS",
                    sakspartRolle = new SakspartRolleType()
                    {
                        kode = "ADV",
                        beskrivelse = "Advokat"
                    }
                }
            };
        }

        public static KryssreferanseType OpprettKryssreferanse(string mappeSystemId)
        {
            var mappe = GetMappeById(mappeSystemId);
            var kryssreferanse = new KryssreferanseType();

            if (mappe != null)
            {
                kryssreferanse.mappe = GetKryssreferanseTilMappe(mappe);
                kryssreferanse.klasse = OpprettKlasse(1);
                kryssreferanse.registrering = OpprettRegistrering(Registreringer.Count);

                kryssreferanse.referanseTilMappe = mappeSystemId;
                kryssreferanse.referanseTilKlasse = OpprettKlasse(1).systemID;
                kryssreferanse.referanseTilRegistrering = OpprettRegistrering(Registreringer.Count, mappeSystemId).systemID;
                Kryssreferanser.Add(kryssreferanse);
            }

            return kryssreferanse;
        }

        private static MappeType GetKryssreferanseTilMappe(MappeType tilMappe)
        {
            var indexMappe = Mapper.IndexOf(tilMappe);
            if (indexMappe != null)
            {
                try
                {
                    return Mapper[indexMappe + 1];
                }
                catch (Exception)
                {
                    var newMappeId = Mapper.Count + 1;
                    Mapper.Add(OpprettMappe(newMappeId.ToString()));
                    return Mapper[newMappeId];
                }
            }

            return null;
        }

        internal static void DeleteSekundaerklassifikasjonFromSaksmappe(string id, KlasseType[] klasseTyper)
        {
            foreach (var klasseType in klasseTyper)
            {
                DeleteSekundaerklassifikasjonFromSaksmappe(id, klasseType.systemID);
            }
        }

        private static DateTime GetDato(int index)
        {
            return FixedDate.AddDays(-(index + 10));
        }

        private static string Tittel(string objektType, int index)
        {
            while (index > 10)
            {
                index = index - 10;
            }
            return $"{Adjectives[index - 1]} {objektType} nr. {index}";
        }

        private static string GetName(int index)
        {
            while (index > 10)
            {
                index = index - 10;
            }
            return FirstNames[index - 1];
        }

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

        private static ArkivskaperType[] OpprettArkivskaper()
        {
            List<ArkivskaperType> arkivskaperTyper = new List<ArkivskaperType>();
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
            arkivskaperTyper.Add(arkivskaper);

            return arkivskaperTyper.ToArray();
        }

        private static void OpprettArkiver()
        {
            for (int i = 0; i < 10; i++)
            {
                Arkiver.Add(OpprettArkiv((i + 1).ToString()));
            }
        }

        private static ArkivType OpprettArkiv(string systemId)
        {
            var arkiv = new ArkivType()
            {
                tittel = FirstLetterToUpper(GetRandomAdjective()) + " arkiv",
                arkivstatus = AvsluttetArkivstatus,
                dokumentmedium = ElektroniskDokumentmedium,
                avsluttetDato = DateTime.Now,
                avsluttetDatoSpecified = true,
                systemID = systemId,
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
        public static MappeType GetMappeById(string id)
        {
            return Mapper.Find(m => m.systemID == id);
        }

        private static void OpprettMapper()
        {
            for (int i = 0; i < 10; i++)
            {
                Mapper.Add(OpprettMappe((i + 1).ToString()));
            }
        }

        private static MappeType OpprettMappe(string id)
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
                graderingskode = new GraderingskodeType { kode = "B" },
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

        private static string OpprettetAv()
        {
            return FirstNames[RandomNumber(0, FirstNames.Length - 1)];
        }

        private static string GenerateUuuid()
        {
            return Guid.NewGuid().ToString();
        }

        private static int RandomNumber(int min, int max)
        {
            return Random.Next(min, max);
        }

        private static string GetRandomKommune()
        {
            return FirstLetterToUpper(GetRandomAdjective()) + " kommune";
        }

        private static string GetRandomAdjective()
        {
            return Adjectives[RandomNumber(0, Adjectives.Length - 1)];
        }

        private static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        public static ArkivType GetArkivById(string id)
        {
            return Arkiver.Find(a => a.systemID == id);
        }

        public static ArkivskaperType GetArkivskaperById(string id)
        {
            return Arkivskaper.Find(a => a.systemID == id);
        }

        public static SaksmappeType GetSaksmappeById(string id)
        {
            return Saksmapper.Find(s => s.systemID == id);
        }


        public static void AddSekundaerklassifikasjonToSaksmappe(string saksmappeSystemId, KlasseType klasseType)
        {
            var funnet = false;
            if (klasseType != null)
            {
                foreach (var saksmappe in Saksmapper)
                {
                    if (saksmappe.systemID == saksmappeSystemId)
                    {
                        var sekundaerklassifikasjonerList = saksmappe.sekundaerklassifikasjon.ToList();
                        sekundaerklassifikasjonerList.Add(klasseType);
                        saksmappe.sekundaerklassifikasjon = sekundaerklassifikasjonerList.ToArray();
                        funnet = true;
                    }
                }
            }
            if (!funnet)
            {
                throw new ArgumentNullException("Saksmappen finnes ikke");
            }
        }

        public static void AddKryssreferanseToMappe(string mappeSystemId, KryssreferanseType kryssreferanseType)
        {
            var funnet = false;
            if (kryssreferanseType != null)
            {
                foreach (var mappe in Mapper)
                {
                    if (mappe.systemID == mappeSystemId)
                    {
                        var kryssreferaser = mappe.kryssreferanse?.ToList() ?? new List<KryssreferanseType>();
                        kryssreferaser.Add(kryssreferanseType);
                        mappe.kryssreferanse = kryssreferaser.ToArray();
                        funnet = true;
                    }
                }
            }
            if (!funnet)
            {
                throw new ArgumentNullException("Mappen finnes ikke");
            }
        }

        public static void DeleteSekundaerklassifikasjonFromSaksmappe(string saksmappeSystemId, string sekundaerklassifikasjonId)
        {
            var funnet = false;
            if (sekundaerklassifikasjonId != null)
            {
                foreach (var saksmappe in Saksmapper)
                {
                    if (saksmappe.systemID == saksmappeSystemId)
                    {
                        saksmappe.RemoveSekundaerklasseById(sekundaerklassifikasjonId);
                        funnet = true;
                    }
                }
            }
            if (!funnet)
            {
                throw new ArgumentNullException("Saksmappen finnes ikke");
            }
        }

        public static void SetSekundaerklassifikasjonerToSaksmappe(string saksmappeSystemId, KlasseType[] klasseType)
        {
            var funnet = false;
            if (klasseType != null)
            {
                foreach (var saksmappe in Saksmapper)
                {
                    if (saksmappe.systemID == saksmappeSystemId)
                    {
                        saksmappe.sekundaerklassifikasjon = klasseType;
                        funnet = true;
                    }
                }
            }
            if (!funnet)
            {
                throw new ArgumentNullException("Saksmappen finnes ikke");
            }
        }

        public static KlasseType[] GetSekundaerklassifikasjonerBySaksmappeId(string id)
        {
            var saksmappe = GetSaksmappeById(id) ?? throw new ArgumentNullException("Saksmappen finnes ikke");
            return saksmappe.sekundaerklassifikasjon;
        }

        public static KryssreferanseType[] GetKryssreferanseFraMappe(string mappeSystemId)
        {
                var mappe = GetMappeById(mappeSystemId);
                if (mappe != null)
                    return mappe.kryssreferanse ?? new KryssreferanseType[0];
                return new KryssreferanseType[0];
        }

        public static bool MappeExists(string id)
        {
            return GetMappeById(id) != null;
        }
    }
}