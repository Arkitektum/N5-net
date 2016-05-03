using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;



public partial class AbstraktArkivenhetType : Resource
{
}

public partial class DokumentobjektType : Resource
{
    public DokumentobjektType()
    {
        CreateHypermedia();
    }
    protected override void CreateHypermedia()
    {
        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        this.Links.Clear();
        this.Links.Add(new LinkType("self", baseUri + "api/arkivstruktur/Dokumentobjekt/" + this.systemID));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/dokumentobjekt", baseUri + "api/arkivstruktur/Dokumentobjekt/" + this.systemID));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/fil", baseUri + "api/arkivstruktur/Dokumentobjekt/" + this.systemID + "/referanseFil"));
    }

}
public partial class ArkivType
{
   public ArkivType()
    {
        CreateHypermedia();
    }

    protected override void CreateHypermedia()
    {
        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        this.Links.Clear();
        this.Links.Add(new LinkType("self", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkiv", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkivdel", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID + "/arkivdel{?$filter&$orderby&$top&$skip&$search}"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-arkivdel", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID + "/ny-arkivdel"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkivskaper", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID + "/arkivskaper{?$filter&$orderby&$top&$skip&$search}"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-arkivskaper", baseUri + "api/arkivstruktur/Arkiv/" + this.systemID + "/ny-arkivskaper"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/administrasjon/dokumentmedium", baseUri + "api/kodelister/Dokumentmedium{?$filter&$orderby&$top&$skip}"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/administrasjon/arkivstatus", baseUri + "api/kodelister/Arkivstatus{?$filter&$orderby&$top&$skip}"));
    }

}

public partial class ArkivskaperType
{
    public ArkivskaperType()
    {
        //CreateHypermedia();
    }

    protected override void CreateHypermedia()
    {
        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        this.Links.Clear();
        this.Links.Add(new LinkType("self", baseUri + "api/arkivstruktur/Arkivskaper/" + this.systemID));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkivskaper", baseUri + "api/arkivstruktur/Arkivskaper/" + this.systemID));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkiv", baseUri + "api/arkivstruktur/Arkivskaper/" + this.systemID +"/arkiv{?$filter&$orderby&$top&$skip&$search}"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-arkiv", baseUri + "api/arkivstruktur/Arkivskaper/" + this.systemID + "/nytt-arkiv"));
    }

}

public partial class MappeType
{
    public MappeType()
    {
        //CreateHypermedia();
    }

    protected override void CreateHypermedia()
    {

        var baseUri = arkitektum.kommit.noark5.api.Properties.Settings.Default.baseUri;

        this.Links.Clear();
        this.Links.Add(new LinkType("self", baseUri + "api/arkivstruktur/Mappe/" + this.systemID));

        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/mappe", baseUri + "api/arkivstruktur/Mappe/" + this.systemID));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/avslutt-mappe", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/avslutt"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/sakarkiv/utvid-til-saksmappe", baseUri + "api/sakarkiv/Saksmappe/" + this.systemID + "/utvid-til-saksmappe"));
        //this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/moeteogutvalgsbehandling/utvid-til-moetemappe", baseUri + "api/moeteogutvalgsbehandling/Arkiv/" + this.systemID + "/ny-arkivdel"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/registrering", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/registrering{?$filter&$orderby&$top&$skip&$search}"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-registrering", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/ny-registrering"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-basisregistrering", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/ny-basisregistrering"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/merknad", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/merknad{?$filter&$orderby&$top&$skip&$search}"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-merknad", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/ny-merknad"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/undermappe", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/undermappe{?$filter&$orderby&$top&$skip&$search}"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-undermappe", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/ny-undermappe"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/kryssreferanse", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/kryssreferanse{?$filter&$orderby&$top&$skip&$search}"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/ny-kryssreferanse", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/ny-kryssreferanse"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/klasse", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/klasse"));
        this.Links.Add(new LinkType("http://rel.kxml.no/noark5/v4/api/arkivstruktur/arkivdel", baseUri + "api/arkivstruktur/Mappe/" + this.systemID + "/arkivdel"));
    }

}