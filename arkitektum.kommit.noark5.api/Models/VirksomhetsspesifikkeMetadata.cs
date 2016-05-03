using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkitektum.kommit.noark5.api.Models
{
    public class Byggesak
    {
        public string systemID;
        public string bygningsnummer;
        public Prosess[] saksbehandling;
    }


    public class Prosess
    {
        public string kategori;
        public Boolean medDispensasjon;
        public Vedtak[] resultat;
    }

    public class Vedtak
    {
        public DateTime vedtaksdato;
        public string status;
        public string referanseVedtakDokument;
        public string[] referanseUnderlagsdokumenter;
    }

}