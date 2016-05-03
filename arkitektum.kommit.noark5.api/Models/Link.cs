using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

public class Links : Resource
{
}

public interface IHalResource
{
    [JsonProperty("_links")]
    IList<LinkType> Links { get; set; }
}

public abstract class Resource : IHalResource
{
    protected Resource()
    {
        Links = new List<LinkType>();
    }
    public IList<LinkType> Links { get; set; }
    public void RepopulateHyperMedia()
    {
        if (Links == null)
            Links = new List<LinkType>(); 

        CreateHypermedia();


        if ((Links != null) && !Links.Any())
            Links = null; 
    }
    protected virtual void CreateHypermedia()
    {
    }
}


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.kxml.no/rest/1.0")]
[System.Xml.Serialization.XmlRootAttribute("Link", Namespace = "http://www.kxml.no/rest/1.0", IsNullable = false)]
public partial class LinkType
{

    private string hrefField;

    private string relField;

    private bool templatedField;

    private bool templatedFieldSpecified;

    private string typeField;

    private string deprecationField;

    private string nameField;

    private string titleField;
    public LinkType(string rel, string href)
    {
        relField = rel;
        hrefField = href;
    }
    /// <remarks/>
    public string href
    {
        get
        {
            return this.hrefField;
        }
        set
        {
            this.hrefField = value;
        }
    }

    /// <remarks/>
    public string rel
    {
        get
        {
            return this.relField;
        }
        set
        {
            this.relField = value;
        }
    }

    /// <remarks/>

    public bool templated
    {
        get { return !string.IsNullOrEmpty(hrefField) && IsTemplatedRegex.IsMatch(hrefField); }
    }

    private static readonly Regex IsTemplatedRegex = new Regex(@"{.+}", RegexOptions.Compiled);

   

    /// <remarks/>
    public string type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    /// <remarks/>
    public string deprecation
    {
        get
        {
            return this.deprecationField;
        }
        set
        {
            this.deprecationField = value;
        }
    }

    /// <remarks/>
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public string title
    {
        get
        {
            return this.titleField;
        }
        set
        {
            this.titleField = value;
        }
    }
}