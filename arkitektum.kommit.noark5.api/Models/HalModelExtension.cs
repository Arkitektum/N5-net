using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;




public partial class RegistreringType
{

    protected override void CreateHypermedia()
    {
        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        this.LinkList.Clear();
        this.LinkList.Add(new LinkType("self", baseUri + "api/arkivstruktur/Registrering/" + this.systemID));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/dokumentbeskrivelse", baseUri + "api/arkivstruktur/Registrering/" + this.systemID + "/dokumentbeskrivelse"));
        //this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/fil", baseUri + "api/arkivstruktur/Dokumentobjekt/" + this.systemID + "/referanseFil"));
        
        //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Basisregistrering/" + m.systemID, Set._REL + "/utvid-til-basisregistrering"));
        //linker.Add(Set.addLink(baseUri, "api/sakarkiv/Journalpost/" + m.systemID, Set._REL + "/utvid-til-journalpost"));
        //linker.Add(Set.addLink(baseUri, "api/MoeteOgUtvalgsbehandling/Moeteregistrering/" + m.systemID, Set._REL + "/utvid-til-moeteregistrering"));

        //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Registrering/" + m.systemID + "/dokumentbeskrivelse", Set._REL + "/dokumentbeskrivelse", "?$filter&$orderby&$top&$skip&$search"));
        //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Registrering/" + m.systemID + "/ny-dokumentbeskrivelse", Set._REL + "/ny-dokumentbeskrivelse"));
        //linker.Add(Set.addTempLink(baseUri, "api/arkivstruktur/Registrering/" + m.systemID + "/dokumentobjekt", Set._REL + "/dokumentobjekt", "?$filter&$orderby&$top&$skip&$search"));
        //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Registrering/" + m.systemID + "/ny-dokumentobjekt", Set._REL + "/ny-dokumentobjekt"));

        ////Enten eller?
        //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Klasse/234", Set._REL + "/referanseKlasse"));
        //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Mappe/665", Set._REL + "/referanseMappe"));
        //linker.Add(Set.addLink(baseUri, "api/arkivstruktur/Arkivdel/6578", Set._REL + "/referanseArkivdel"));
    }

}

public partial class DokumentbeskrivelseType
{

    protected override void CreateHypermedia()
    {
        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        this.LinkList.Clear();
        this.LinkList.Add(new LinkType("self", baseUri + "api/arkivstruktur/Dokumentbeskrivelse/" + this.systemID));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/dokumentobjekt", baseUri + "api/arkivstruktur/Dokumentbeskrivelse/" + this.systemID + "/dokumentobjekt"));

    }

}
public partial class DokumentobjektType
{

    protected override void CreateHypermedia()
    {
        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        this.LinkList.Clear();
        this.LinkList.Add(new LinkType("self", baseUri + "api/arkivstruktur/Dokumentobjekt/" + this.systemID));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/dokumentobjekt", baseUri + "api/arkivstruktur/Dokumentobjekt/" + this.systemID));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/fil", baseUri + "api/arkivstruktur/Dokumentobjekt/" + this.systemID + "/referanseFil"));
    }

}
public partial class ArkivType
{


    protected override void CreateHypermedia()
    {
        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        this.LinkList.Clear();
        this.LinkList.Add(new LinkType("self", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkiv", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkivdel", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID + "/arkivdel{?$filter&$orderby&$top&$skip&$search}"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-arkivdel", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID + "/ny-arkivdel"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkivskaper", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID + "/arkivskaper{?$filter&$orderby&$top&$skip&$search}"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-arkivskaper", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID + "/ny-arkivskaper"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/administrasjon/dokumentmedium", baseUri + "api/kodelister/Dokumentmedium{?$filter&$orderby&$top&$skip}"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/administrasjon/arkivstatus", baseUri + "api/kodelister/Arkivstatus{?$filter&$orderby&$top&$skip}"));
    }

}

public partial class ArkivskaperType
{


    protected override void CreateHypermedia()
    {
        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        this.LinkList.Clear();
        this.LinkList.Add(new LinkType("self", baseUri + "api/arkivstruktur/Arkivskaper/" + this.systemID));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkivskaper", baseUri + "api/arkivstruktur/Arkivskaper/" + this.systemID));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkiv", baseUri + "api/arkivstruktur/Arkivskaper/" + this.systemID + "/arkiv{?$filter&$orderby&$top&$skip&$search}"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-arkiv", baseUri + "api/arkivstruktur/Arkivskaper/" + this.systemID + "/nytt-arkiv"));
    }

}

public partial class MappeType
{


    protected override void CreateHypermedia()
    {

        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        this.LinkList.Clear();
        this.LinkList.Add(new LinkType("self", baseUri + "api/arkivstruktur/Mappe/" + this.systemID));

        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/mappe", baseUri + "api/arkivstruktur/Mappe/" + this.systemID));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/avslutt-mappe", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/avslutt"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/sakarkiv/utvid-til-saksmappe", baseUri + "api/sakarkiv/Saksmappe/" + this.systemID + "/utvid-til-saksmappe"));
        //this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/moeteogutvalgsbehandling/utvid-til-moetemappe", baseUri + "api/moeteogutvalgsbehandling/Arkiv/" + this.systemID + "/ny-arkivdel"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/registrering", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/registrering{?$filter&$orderby&$top&$skip&$search}"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-registrering", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/ny-registrering"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-basisregistrering", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/ny-basisregistrering"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/merknad", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/merknad{?$filter&$orderby&$top&$skip&$search}"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-merknad", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/ny-merknad"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/undermappe", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/undermappe{?$filter&$orderby&$top&$skip&$search}"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-undermappe", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/ny-undermappe"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/kryssreferanse", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/kryssreferanse{?$filter&$orderby&$top&$skip&$search}"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-kryssreferanse", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/ny-kryssreferanse"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/klasse", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/klasse"));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkivdel", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/arkivdel"));
    }

}

public partial class AbstraktSakspartType
{


    protected override void CreateHypermedia()
    {
        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        this.LinkList.Clear();
        this.LinkList.Add(new LinkType("self", baseUri + "api/sakarkiv/sakspart/" + this.systemID));
        this.LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/sakarkiv/sakspart", baseUri + "api/sakarkiv/sakspart/" + this.systemID));
    }

}