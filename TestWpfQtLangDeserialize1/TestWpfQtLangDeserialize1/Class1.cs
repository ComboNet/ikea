
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class TS
{

    private TSContext[] contextField;

    private decimal versionField;

    private string languageField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("context")]
    public TSContext[] context
    {
        get
        {
            return this.contextField;
        }
        set
        {
            this.contextField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal version
    {
        get
        {
            return this.versionField;
        }
        set
        {
            this.versionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string language
    {
        get
        {
            return this.languageField;
        }
        set
        {
            this.languageField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TSContext
{

    private string nameField;

    private TSContextMessage[] messageField;

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
    [System.Xml.Serialization.XmlElementAttribute("message")]
    public TSContextMessage[] message
    {
        get
        {
            return this.messageField;
        }
        set
        {
            this.messageField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TSContextMessage
{

    private TSContextMessageLocation[] locationField;

    private TSContextMessageSource sourceField;

    private TSContextMessageTranslation translationField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("location")]
    public TSContextMessageLocation[] location
    {
        get
        {
            return this.locationField;
        }
        set
        {
            this.locationField = value;
        }
    }

    /// <remarks/>
    public TSContextMessageSource source
    {
        get
        {
            return this.sourceField;
        }
        set
        {
            this.sourceField = value;
        }
    }

    /// <remarks/>
    public TSContextMessageTranslation translation
    {
        get
        {
            return this.translationField;
        }
        set
        {
            this.translationField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TSContextMessageLocation
{

    private string filenameField;

    private ushort lineField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string filename
    {
        get
        {
            return this.filenameField;
        }
        set
        {
            this.filenameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort line
    {
        get
        {
            return this.lineField;
        }
        set
        {
            this.lineField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TSContextMessageSource
{

    private TSContextMessageSourceByte byteField;

    private string[] textField;

    /// <remarks/>
    public TSContextMessageSourceByte @byte
    {
        get
        {
            return this.byteField;
        }
        set
        {
            this.byteField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TSContextMessageSourceByte
{

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TSContextMessageTranslation
{

    private string typeField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

