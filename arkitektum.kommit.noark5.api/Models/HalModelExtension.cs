using System.Linq;


public partial class RegistreringType
{

    protected override void CreateHypermedia()
    {
        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        LinkList.Clear();
        LinkList.Add(new LinkType("self", baseUri + "api/arkivstruktur/Registrering/" + this.systemID));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/dokumentbeskrivelse", baseUri + "api/arkivstruktur/Registrering/" + this.systemID + "/dokumentbeskrivelse"));
    }

}

public partial class DokumentbeskrivelseType
{

    protected override void CreateHypermedia()
    {
        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        LinkList.Clear();
        LinkList.Add(new LinkType("self", baseUri + "api/arkivstruktur/Dokumentbeskrivelse/" + this.systemID));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/dokumentobjekt", baseUri + "api/arkivstruktur/Dokumentbeskrivelse/" + this.systemID + "/dokumentobjekt"));

    }

}
public partial class DokumentobjektType
{

    protected override void CreateHypermedia()
    {
        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        LinkList.Clear();
        LinkList.Add(new LinkType("self", baseUri + "api/arkivstruktur/Dokumentobjekt/" + this.systemID));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/dokumentobjekt", baseUri + "api/arkivstruktur/Dokumentobjekt/" + this.systemID));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/fil", baseUri + "api/arkivstruktur/Dokumentobjekt/" + this.systemID + "/referanseFil"));
    }

}
public partial class ArkivType
{


    protected override void CreateHypermedia()
    {
        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        LinkList.Clear();
        LinkList.Add(new LinkType("self", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkiv", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkivdel", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID + "/arkivdel{?$filter&$orderby&$top&$skip&$search}"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-arkivdel", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID + "/ny-arkivdel"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkivskaper", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID + "/arkivskaper{?$filter&$orderby&$top&$skip&$search}"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-arkivskaper", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID + "/ny-arkivskaper"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/administrasjon/dokumentmedium", baseUri + "api/kodelister/Dokumentmedium{?$filter&$orderby&$top&$skip}"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/administrasjon/arkivstatus", baseUri + "api/kodelister/Arkivstatus{?$filter&$orderby&$top&$skip}"));
    }

}

public partial class ArkivskaperType
{


    protected override void CreateHypermedia()
    {
        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        LinkList.Clear();
        LinkList.Add(new LinkType("self", baseUri + "api/arkivstruktur/Arkivskaper/" + this.systemID));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkivskaper", baseUri + "api/arkivstruktur/Arkivskaper/" + this.systemID));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkiv", baseUri + "api/arkivstruktur/Arkivskaper/" + this.systemID + "/arkiv{?$filter&$orderby&$top&$skip&$search}"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-arkiv", baseUri + "api/arkivstruktur/Arkivskaper/" + this.systemID + "/nytt-arkiv"));
    }

}

public partial class SaksmappeType
{
    /// <summary>
    /// Override creation of hypermedia links coming from Mappe 
    /// </summary>
    protected override void CreateHypermedia()
    {
        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        base.CreateHypermedia();

        RemoveOldSelf();

        InsertNewSelf(baseUri);

        RemoveUtvidTilSaksmappe();
    }

    private void InsertNewSelf(string baseUri)
    {
        LinkList.Insert(0, new LinkType("self", baseUri + "api/arkivstruktur/saksmappe/" + systemID));
    }

    private void RemoveOldSelf()
    {
        var self = LinkList.First(x => x.rel == "self");
        LinkList.Remove(self);
    }

    private void RemoveUtvidTilSaksmappe()
    {
        var utvidTilSaksmappe =
            LinkList.First(x => x.rel == "http://rel.kxml.no/noark5/v4/api/sakarkiv/utvid-til-saksmappe");
        LinkList.Remove(utvidTilSaksmappe);
    }
}

public partial class MappeType
{


    protected override void CreateHypermedia()
    {

        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        LinkList.Clear();
        LinkList.Add(new LinkType("self", baseUri + "api/arkivstruktur/Mappe/" + this.systemID));

        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/mappe", baseUri + "api/arkivstruktur/Mappe/" + this.systemID));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/avslutt-mappe", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/avslutt"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/sakarkiv/utvid-til-saksmappe", baseUri + "api/sakarkiv/Saksmappe/" + this.systemID + "/utvid-til-saksmappe"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/registrering", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/registrering{?$filter&$orderby&$top&$skip&$search}"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-registrering", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/ny-registrering"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-basisregistrering", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/ny-basisregistrering"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/merknad", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/merknad{?$filter&$orderby&$top&$skip&$search}"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-merknad", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/ny-merknad"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/undermappe", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/undermappe{?$filter&$orderby&$top&$skip&$search}"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-undermappe", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/ny-undermappe"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/kryssreferanse", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/kryssreferanse{?$filter&$orderby&$top&$skip&$search}"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-kryssreferanse", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/ny-kryssreferanse"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/klasse", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/klasse"));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkivdel", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/arkivdel"));
    }

}

public partial class AbstraktSakspartType
{


    protected override void CreateHypermedia()
    {
        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        LinkList.Clear();
        LinkList.Add(new LinkType("self", baseUri + "api/sakarkiv/sakspart/" + this.systemID));
        LinkList.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/sakarkiv/sakspart", baseUri + "api/sakarkiv/sakspart/" + this.systemID));
    }

}