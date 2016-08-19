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
        public List<ArkivskaperType> arkivskaper;
        public List<ArkivType> arkiver;
        public List<ArkivdelType> arkivdeler;
        public List<KlassifikasjonssystemType> klassifikasjonssystemer;
        public List<KlasseType> klasser;
        public List<MappeType> mapper;
        public List<RegistreringType> registreringer;
        public List<BasisregistreringType> basisregistreringer; // ??? bare som er spesialisering av registreringer? 
        public List<DokumentbeskrivelseType> dokumentbeskrivelser;
        public List<DokumentobjektType> dokumentobjekter;

        public MockNoarkDatalayer()
        {
            //IGenerationSessionFactory factory = AutoPocoContainer.Configure(x =>
            //{
            //    x.Conventions(c =>
            //    {
            //        c.UseDefaultConventions();
            //    });
            //    x.AddFromAssemblyContainingType<ArkivType>();
            //});
            //IGenerationSession session = factory.CreateSession();
            //DokumentmediumType d = new DokumentmediumType();
            //d.kode = "E";
            //d.beskrivelse = "Elektronisk arkiv";
            //ArkivstatusType a = new ArkivstatusType();
            //a.kode = "A";
            //a.beskrivelse = "Avsluttet";

            //KlassifikasjonstypeType k = new KlassifikasjonstypeType();
            //k.kode = "GBN";
            //k.beskrivelse = "Gårds- og bruksnummer";
            //FormatType f = new FormatType();
            //f.kode = "RA-PDF";
            //f.beskrivelse = "PDF/A - ISO 19005-1:2005";
            //VariantformatType v = new VariantformatType();
            //v.kode = "A";
            //v.beskrivelse = "Arkivformat";

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

            //arkiver = session.List<ArkivType>(10)
            //      .Random(3)
            //          .Impose(x => x.tittel, "Arkiv ePhorte")
            //          .Impose(x => x.arkivstatus, a)
            //      .Next(5)
            //          .Impose(x => x.tittel, "Arkiv fagsystem X")
            //          .Impose(x => x.arkivstatus, a)
            //          .Impose(x => x.dokumentmedium, d)
            //          .Impose(x=> x.avsluttetDato, DateTime.Now)
            //          .Impose(x => x.avsluttetDatoSpecified, true)
            //      .Next(2)
            //          .Impose(x => x.tittel, "Arkiv fagsystem Y")
            //    .All()
            //        .Impose(x => x.systemID, Guid.NewGuid().ToString())
            //        .Impose(x => x.beskrivelse, "Lorem ipsum")
            //        .Impose(x => x.arkivskaper, arkivskaper[0])
            //        .Impose(x => x.oppdatertAv, null)
            //        .Impose(x => x.referanseOppdatertAv, null)
            //        .Impose(x => x.avsluttetAv, "Brukernavn")
            //        .Impose(x => x.referanseAvsluttetAv, Guid.NewGuid().ToString())
            //        .Impose(x => x.opprettetAv, "Brukernavn")
            //        .Impose(x => x.referanseOpprettetAv, Guid.NewGuid().ToString())
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




    }
}